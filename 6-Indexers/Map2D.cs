namespace Indexers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    /// <inheritdoc cref="IMap2D{TKey1,TKey2,TValue}" />
    public class Map2D<TKey1, TKey2, TValue> : IMap2D<TKey1, TKey2, TValue>
    {
        private readonly Dictionary<Tuple<TKey1, TKey2>, TValue> _map = new Dictionary<Tuple<TKey1, TKey2>, TValue>();

        /// <inheritdoc cref="IMap2D{TKey1, TKey2, TValue}.NumberOfElements" />
        public int NumberOfElements
        {
            get => _map.Count();
        }

        /// <inheritdoc cref="IMap2D{TKey1, TKey2, TValue}.this" />
        public TValue this[TKey1 key1, TKey2 key2]
        {
            get => this._map[Tuple.Create(key1, key2)];
            set => this._map[Tuple.Create(key1, key2)] = value;
        }

        /// <inheritdoc cref="IMap2D{TKey1, TKey2, TValue}.GetRow(TKey1)" />
        public IList<Tuple<TKey2, TValue>> GetRow(TKey1 key1)
        {   
            return this._map.Keys
                .Where(tuple => tuple.Item1.Equals(key1))
                .Select(tuple => Tuple.Create(tuple.Item2, this._map[tuple]))
                .ToList();
        }

        /// <inheritdoc cref="IMap2D{TKey1, TKey2, TValue}.GetColumn(TKey2)" />
        public IList<Tuple<TKey1, TValue>> GetColumn(TKey2 key2)
        {
            return this._map.Keys
                .Where(tuple => tuple.Item2.Equals(key2))
                .Select(tuple => Tuple.Create(tuple.Item1, this._map[tuple]))
                .ToList();
        }

        /// <inheritdoc cref="IMap2D{TKey1, TKey2, TValue}.GetElements" />
        public IList<Tuple<TKey1, TKey2, TValue>> GetElements()
        {
            return this._map.Keys
                .Select(tuple => Tuple.Create(tuple.Item1, tuple.Item2, this._map[tuple]))
                .ToList();
        }

        /// <inheritdoc cref="IMap2D{TKey1, TKey2, TValue}.Fill(IEnumerable{TKey1}, IEnumerable{TKey2}, Func{TKey1, TKey2, TValue})" />
        public void Fill(IEnumerable<TKey1> keys1, IEnumerable<TKey2> keys2, Func<TKey1, TKey2, TValue> generator)
        {
            foreach (TKey1 k1 in keys1)
            {
                foreach (TKey2 k2 in keys2)
                {
                    this[k1, k2] = generator(k1, k2);
                }
            }
        }

        /// <inheritdoc cref="IEquatable{T}.Equals(T)" />
        public bool Equals(Map2D<TKey1, TKey2, TValue> other) => Equals(this._map, other._map);

        /// <inheritdoc cref="IEquatable{T}.Equals(T)"/>
        public bool Equals(IMap2D<TKey1, TKey2, TValue> other)
        {
            if (other is Map2D<TKey1, TKey2, TValue> otherMap2d)
            {
                return this.Equals(otherMap2d);
            }
            return false;
        }

        /// <inheritdoc cref="object.Equals(object?)" />
        public override bool Equals(object obj)
        {
            if (obj is null || obj.GetType() != this.GetType()) return false;

            if (obj == this) return true;

            return this.Equals(obj as Map2D<TKey1, TKey2, TValue>);
        }

        /// <inheritdoc cref="object.GetHashCode"/>
        public override int GetHashCode() => this._map != null ? this._map.GetHashCode() : 0;

        /// <inheritdoc cref="IMap2D{TKey1, TKey2, TValue}.ToString"/>
        public override string ToString()
        {
            return "{ " + string.Join(", ", this.GetElements()
                       .Select(e => $"({e.Item1}, {e.Item2}) -> {e.Item3}")) + "}";
        }
    }
}
