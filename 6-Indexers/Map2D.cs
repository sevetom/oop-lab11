namespace Indexers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    /// <inheritdoc cref="IMap2D{TKey1,TKey2,TValue}" />
    public class Map2D<TKey1, TKey2, TValue> : IMap2D<TKey1, TKey2, TValue>
    {
        private TValue[,] _map;
        private IList<TKey1> _rows;
        private IList<TKey2> _columns;
        /// <inheritdoc cref="IMap2D{TKey1, TKey2, TValue}.NumberOfElements" />

        public Map2D()
        {
        }

        public int NumberOfElements
        {
            get => _map.GetLength(0) * _map.GetLength(1);
        }

        /// <inheritdoc cref="IMap2D{TKey1, TKey2, TValue}.this" />
        public TValue this[TKey1 key1, TKey2 key2]
        {
            get => _map[_rows.IndexOf(key1), _columns.IndexOf(key2)];
            set => _map[_rows.IndexOf(key1), _columns.IndexOf(key2)] = value;
        }

        /// <inheritdoc cref="IMap2D{TKey1, TKey2, TValue}.GetRow(TKey1)" />
        public IList<Tuple<TKey2, TValue>> GetRow(TKey1 key1)
        {   
            IList<Tuple<TKey2, TValue>> retlist = new List<Tuple<TKey2, TValue>>(_rows.Count()); 
            foreach (TKey2 c in _columns)
            {
                retlist.Add(new Tuple<TKey2, TValue>(c, _map[_rows.IndexOf(key1), _columns.IndexOf(c)]));
            }
            return retlist;
        }

        /// <inheritdoc cref="IMap2D{TKey1, TKey2, TValue}.GetColumn(TKey2)" />
        public IList<Tuple<TKey1, TValue>> GetColumn(TKey2 key2)
        {
            IList<Tuple<TKey1, TValue>> retlist = new List<Tuple<TKey1, TValue>>(_columns.Count()); 
            foreach (TKey1 r in _rows)
            {
                retlist.Add(new Tuple<TKey1, TValue>(r, _map[_rows.IndexOf(r), _columns.IndexOf(key2)]));
            }
            return retlist;
        }

        /// <inheritdoc cref="IMap2D{TKey1, TKey2, TValue}.GetElements" />
        public IList<Tuple<TKey1, TKey2, TValue>> GetElements()
        {
            IList<Tuple<TKey1, TKey2, TValue>> retlist = new List<Tuple<TKey1, TKey2, TValue>>(_map.GetLength(0) * _map.GetLength(1)); 
            foreach (TKey1 r in _rows)
            {
                foreach (TKey2 c in _columns)
                {
                    retlist.Add(new Tuple<TKey1, TKey2, TValue>(r, c, _map[_rows.IndexOf(r), _columns.IndexOf(c)]));
                }
            }
            return retlist;
        }

        /// <inheritdoc cref="IMap2D{TKey1, TKey2, TValue}.Fill(IEnumerable{TKey1}, IEnumerable{TKey2}, Func{TKey1, TKey2, TValue})" />
        public void Fill(IEnumerable<TKey1> keys1, IEnumerable<TKey2> keys2, Func<TKey1, TKey2, TValue> generator)
        {   
            _map = new TValue[keys1.Count(), keys2.Count()];
            _rows = new List<TKey1>(keys1.Count());
            _columns = new List<TKey2>(keys2.Count());
            foreach (TKey2 k2 in keys2)
            {
                _columns.Add(k2);
            }
            foreach (TKey1 r in keys1)
            {   
                _rows.Add(r);
                foreach (TKey2 c in keys2)
                {
                    _map[_rows.IndexOf(r), _columns.IndexOf(c)] = generator(r, c);
                }
            }
        }

        /// <inheritdoc cref="IEquatable{T}.Equals(T)" />
        public bool Equals(IMap2D<TKey1, TKey2, TValue> other)
        {
            foreach (TKey1 k1 in _rows)
            {
                int r = _rows.IndexOf(k1);
                foreach (TKey2 k2 in _columns)
                {
                    int c = _columns.IndexOf(k2);
                    if (!(_map[r, c].Equals(other[k1, k2])))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        /// <inheritdoc cref="object.Equals(object?)" />
        public override bool Equals(object obj) => obj is IMap2D<TKey1, TKey2, TValue> map && Equals(map);

        /// <inheritdoc cref="object.GetHashCode"/>
        public override int GetHashCode() => HashCode.Combine(_map, _rows, _columns);

        /// <inheritdoc cref="IMap2D{TKey1, TKey2, TValue}.ToString"/>
        public override string ToString()
        {
            string retstr = "[";
            foreach (TKey2 k2 in _columns)
            {
                retstr += $"{k2} ";
            }
            retstr += "]\n";
            foreach (TKey1 k1 in _rows)
            {
                int r = _rows.IndexOf(k1);
                retstr += $"[{k1}]  ";
                foreach (TKey2 k2 in _columns)
                {
                    int c = _columns.IndexOf(k2);
                    retstr += $"{_map[r, c]} ";
                }
                retstr += "\n";
            }
            return retstr;
        }
    }
}
