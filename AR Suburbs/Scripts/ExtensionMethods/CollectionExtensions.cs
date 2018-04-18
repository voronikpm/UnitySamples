#region Using Directives

using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace Assets.Scripts.ExtensionMethods
{
    public static class CollectionExtensions
    {
        #region Methods

        #region Regular Methods

        public static void AddAll<T>(this IList<T> collection, IEnumerable<T> source, Predicate<T> predicate)
        {
            source.InvokeAction(t => collection.AddIf(t, predicate));
        }

        public static void AddIf<T>(this IList<T> collection, T item, bool condition)
        {
            if(condition)
                collection.Add(item);
        }

        public static void AddIf<T>(this IList<T> collection, T item, Func<bool> checkFunc)
        {
            collection.AddIf(item, checkFunc());
        }

        public static void AddIf<T>(this IList<T> collection, T item, Predicate<T> predicate)
        {
            collection.AddIf(item, predicate(item));
        }

        public static void AddIf<T>(this IList<T> collection, Func<T> func, bool condition)
        {
            collection.AddIf(func(), condition);
        }

        public static void AddIf<T>(this IList<T> collection, Func<T> func, Func<bool> checkFunc)
        {
            collection.AddIf(func(), checkFunc);
        }

        public static void AddIf<T>(this IList<T> collection, Func<T> func, Predicate<T> predicate)
        {
            collection.AddIf(func(), predicate);
        }

        public static void AddIf<TItem, TSource>(this IList<TItem> collection, TSource source, Func<TSource, TItem> func, Predicate<TSource> predicate)
        {
            collection.AddIf(func(source), predicate(source));
        }

        public static void AddIf<TItem, TSource>(this IList<TItem> collection, TSource source, Func<TSource, TItem> func, Predicate<TItem> predicate)
        {
            collection.AddIf(func(source), predicate);
        }

        public static void AddIf<TItem, TSource>(this IList<TItem> collection, TSource source, Func<TSource, TItem> func, Predicate<TSource> sourceCheck, Predicate<TItem> itemCheck)
        {
            if(sourceCheck(source))
                collection.AddIf(func(source), itemCheck);
        }

        public static void AddIf<TItem, TSource>(this IList<TItem> collection, TSource source, Func<TSource, TItem> func, Predicate<IList<TItem>> collectionCheck, Predicate<TSource> sourceCheck, Predicate<TItem> itemCheck)
        {
            if(collectionCheck(collection) && sourceCheck(source))
                collection.AddIf(func(source), itemCheck);
        }

        public static bool ContainsAll<T>(this IEnumerable<T> collection, IEnumerable<T> values)
        {
            var list = collection as IList<T> ?? collection.ToList();
            return values.All(value => list.Contains(value));
        }

        public static bool ContainsAny<T>(this IEnumerable<T> collection, IEnumerable<T> values)
        {
            var list = collection as IList<T> ?? collection.ToList();
            return values.Any(value => list.Contains(value));
        }

        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            var seenKeys = new HashSet<TKey>();
            foreach(var element in source)
            {
                if(seenKeys.Add(keySelector(element)))
                    yield return element;
            }
        }

        public static void InvokeAction<T>(this IEnumerable<T> collection, Action<T> action)
        {
            foreach(var item in collection)
                action(item);
        }

        public static T SelectRandomItem<T>(this IEnumerable<T> collection)
        {
            var rand = new Random();
            var list = collection as IList<T> ?? collection.ToList();
            if(list.Count == 0)
                throw new ArgumentOutOfRangeException("collection", "Tried to select an element of an empty collection");
            return list[rand.Next(0, list.Count)];
        }

        /// <summary>
        /// Returns the minimal element of the given sequence, based on
        /// the given projection.
        /// </summary>
        /// <remarks>
        /// If more than one element has the minimal projected value, the first
        /// one encountered will be returned. This overload uses the default comparer
        /// for the projected type. This operator uses immediate execution, but
        /// only buffers a single result (the current minimal element).
        /// </remarks>
        /// <typeparam name="TSource">Type of the source sequence</typeparam>
        /// <typeparam name="TKey">Type of the projected element</typeparam>
        /// <param name="source">Source sequence</param>
        /// <param name="selector">Selector to use to pick the results to compare</param>
        /// <returns>The minimal element, according to the projection.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is null</exception>
        /// <exception cref="InvalidOperationException"><paramref name="source"/> is empty</exception>
        public static TSource MinBy<TSource, TKey>(this IEnumerable<TSource> source,
            Func<TSource, TKey> selector)
        {
            return source.MinBy(selector, null);
        }

        /// <summary>
        /// Returns the minimal element of the given sequence, based on
        /// the given projection and the specified comparer for projected values.
        /// </summary>
        /// <remarks>
        /// If more than one element has the minimal projected value, the first
        /// one encountered will be returned. This operator uses immediate execution, but
        /// only buffers a single result (the current minimal element).
        /// </remarks>
        /// <typeparam name="TSource">Type of the source sequence</typeparam>
        /// <typeparam name="TKey">Type of the projected element</typeparam>
        /// <param name="source">Source sequence</param>
        /// <param name="selector">Selector to use to pick the results to compare</param>
        /// <param name="comparer">Comparer to use to compare projected values</param>
        /// <returns>The minimal element, according to the projection.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/>, <paramref name="selector"/> 
        /// or <paramref name="comparer"/> is null</exception>
        /// <exception cref="InvalidOperationException"><paramref name="source"/> is empty</exception>
        public static TSource MinBy<TSource, TKey>(this IEnumerable<TSource> source,
            Func<TSource, TKey> selector, IComparer<TKey> comparer)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (selector == null) throw new ArgumentNullException("selector");
            comparer = comparer ?? Comparer<TKey>.Default;

            using (var sourceIterator = source.GetEnumerator())
            {
                if (!sourceIterator.MoveNext())
                {
                    throw new InvalidOperationException("Sequence contains no elements");
                }
                var min = sourceIterator.Current;
                var minKey = selector(min);
                while (sourceIterator.MoveNext())
                {
                    var candidate = sourceIterator.Current;
                    var candidateProjected = selector(candidate);
                    if (comparer.Compare(candidateProjected, minKey) < 0)
                    {
                        min = candidate;
                        minKey = candidateProjected;
                    }
                }
                return min;
            }
        }

        #endregion

        #endregion
    }
}