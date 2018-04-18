using System;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.ExtensionMethods
{
    public static class MonoBehaviourExtensions
    {
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
    }
}