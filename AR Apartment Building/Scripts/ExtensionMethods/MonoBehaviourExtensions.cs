#region Using Directives

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#endregion

namespace Assets.Scripts.ExtensionMethods
{
    public static class MonoBehaviourExtensions
    {
        #region Methods

        #region Regular Methods

        public static IEnumerable<Coroutine> ChainCoroutines(this MonoBehaviour gameObject, params IEnumerator[] actions)
        {
            return actions.Select(gameObject.StartCoroutine);
        }

        public static IEnumerator DelaySeconds(Action action, float delay)
        {
            yield return new WaitForSeconds(delay);
            action();
        }

        public static IEnumerator Do(Action action)
        {
            action();
            yield return 0;
        }

        public static T GetChild<T>(this GameObject gameObject, Func<T, bool> comparer)
            where T : MonoBehaviour
        {
            return gameObject.GetComponentsInChildren<T>().FirstOrDefault(comparer);
        }

        public static GameObject GetChildObject<T>(this GameObject gameObject, Func<T, bool> comparer)
            where T : MonoBehaviour
        {
            var retVal = gameObject.GetComponentsInChildren<T>().FirstOrDefault(comparer);
            return retVal ? retVal.gameObject : null;
        }

        public static IEnumerator WaitForSeconds(float time)
        {
            yield return new WaitForSeconds(time);
        }

        public static IEnumerator WaitUntil(Func<bool> predicate)
        {
            yield return new WaitUntil(predicate);
        }

        public static IEnumerator WaitWhile(Func<bool> predicate)
        {
            yield return new WaitWhile(predicate);
        }

        #endregion

        #endregion
    }
}