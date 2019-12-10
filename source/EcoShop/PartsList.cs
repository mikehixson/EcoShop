using System.Collections.Generic;
using System.Collections;
using System;

namespace EcoShop
{
    public class PartsList : IEnumerable<PartsListItem>
    {
        private readonly Dictionary<string, PartsListItem> _index;

        private readonly Dictionary<string, PartsListItem> _unused;

        public IEnumerable<PartsListItem> Unused {  get { return _unused.Values; } }

        public PartsList()
        {
            _index = new Dictionary<string, PartsListItem>();
            _unused = new Dictionary<string, PartsListItem>();
        }

        public void Increment(string name, decimal amount)
        {
            Increment(_index, name, amount);
        }

        public void Decrement(string name, decimal amount)
        {
            Decrement(_index, name, amount);
        }

        public void IncrementUnused(string name, decimal amount)
        {
            Increment(_unused, name, amount);
        }

        public void DecrementUnused(string name, decimal amount)
        {
            Decrement(_unused, name, amount);
        }

        public decimal Consume(string name, decimal amount)
        {
            if (_unused.TryGetValue(name, out var item))
            {
                var consume = Math.Min(item.Amount, amount);
                item.Decrement(consume);

                if (item.Amount == 0)
                    _unused.Remove(name);

                return amount - consume;
            }

            return amount;
        }

        private void Increment(Dictionary<string, PartsListItem>collection, string name, decimal amount)
        {
            if (!collection.TryGetValue(name, out var item))
            {
                item = new PartsListItem(name);
                collection.Add(name, item);
            }

            item.Increment(amount);
        }

        private void Decrement(Dictionary<string, PartsListItem> collection, string name, decimal amount)
        {
            if (!collection.TryGetValue(name, out var item))
            {
                item = new PartsListItem(name);
                collection.Add(name, item);
            }

            item.Decrement(amount);
        }

        public IEnumerator<PartsListItem> GetEnumerator()
        {
            return _index.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }


    public class PartsListItem
    {
        public string Name { get; }
        public decimal Amount { get; private set; }

        public PartsListItem(string name, decimal amount = 0)
        {
            Name = name;
            Amount = amount;
        }

        public void Increment(decimal amount)
        {
            Amount += amount;
        }

        public void Decrement(decimal amount)
        {
            Amount -= amount;
        }
    }
}
