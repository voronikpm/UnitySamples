using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Helpers
{
    public class WeightedList<T>
        where T : class
    {
        public List<WeightWrapper<T>> Weights { get; set; }

        public T GetRandomItem()
        {
            return GetRandomItem(Weights);
        }

        public T GetRandomFilteredItem(Predicate<T> filter)
        {
            var filteredList = GetFilteredList(filter);
            if(!filteredList.Any())
                return null;
            return GetRandomItem(filteredList);
        }

        public List<WeightWrapper<T>> GetFilteredList(Predicate<T> filter)
        {
            return Weights.Where(x => filter(x.Item)).ToList();
        }

        private T GetRandomItem(List<WeightWrapper<T>> list)
        {
            var total = list.Select(x => x.Weight).Aggregate((x, y) => x += y);
            var current = 0;
            var summ = 0;
            var rand = new Random().Next(total);

            while (current < list.Count)
            {
                summ += list[current].Weight;
                if (summ >= rand)
                    break;
                current++;
            }

            return list[current].Item;
        }

        public WeightedList(List<WeightWrapper<T>> weights)
        {
            Weights = weights;
        }
    }
}