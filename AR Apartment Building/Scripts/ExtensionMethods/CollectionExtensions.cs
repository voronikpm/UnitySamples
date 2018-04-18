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

        public static bool InvokeIf<T>(this T item, Action<T> action, bool check)
        {
            if(check)
                action(item);
            return check;
        }

        public static bool InvokeIf<T>(this T item, Action<T> action, Func<bool> checkFunc)
        {
            bool check = checkFunc();
            return item.InvokeIf(action, check);
        }

        public static bool InvokeIf<T>(this T item, Action<T> action, Predicate<T> predicate)
        {
            bool check = predicate(item);
            return item.InvokeIf(action, check);
        }

        public static TResult InvokeIf<TItem, TResult>(this TItem item, Func<TItem, TResult> func, bool check)
            where TResult : class
        {
            return check ? func(item) : null;
        }

        public static TResult InvokeIf<TItem, TResult>(this TItem item, Func<TItem, TResult> func, Func<bool> check)
            where TResult : class
        {
            return item.InvokeIf(func, check());
        }

        public static TResult InvokeIf<TItem, TResult>(this TItem item, Func<TItem, TResult> func, Predicate<TItem> check)
            where TResult : class
        {
            return item.InvokeIf(func, check(item));
        }

        public static T SelectRandomItem<T>(this IEnumerable<T> collection)
        {
            var rand = new Random();
            var list = collection as IList<T> ?? collection.ToList();
            if(list.Count == 0)
                throw new ArgumentOutOfRangeException("collection", "Tried to select an element of an empty collection");
            return list[rand.Next(0, list.Count)];
        }

        #endregion

        #endregion
    }
}