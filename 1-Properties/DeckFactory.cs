namespace Properties
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// A factory class for building <see cref="ISet{T}">decks</see> of <see cref="Card"/>s.
    /// </summary>
    public class DeckFactory
    {
        public IList<string> Seeds { get; set; }
        public IList<string> Names { get; set; }

        public int DeckSize => Names.Count * Seeds.Count;

        public ISet<Card> Deck
        {   
            get 
            {
                if (Names == null || Seeds == null)
                {
                    throw new InvalidOperationException();
                }

                return new HashSet<Card>(Enumerable
                    .Range(0, Names.Count)
                    .SelectMany(i => Enumerable
                        .Repeat(i, Seeds.Count)
                        .Zip(
                            Enumerable.Range(0, Seeds.Count),
                            (n, s) => Tuple.Create(Names[n], Seeds[s], n)))
                    .Select(tuple => new Card(tuple))
                    .ToList());
            }
        }
    }
}
