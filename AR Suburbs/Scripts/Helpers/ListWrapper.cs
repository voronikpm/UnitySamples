using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Helpers
{
    [Serializable]
    public class ListWrapper
    {
        public List<UnityEngine.Object> List;

        public UnityEngine.Object this[int index]
        {
            get { return List[index]; }
        }

        public static implicit operator UnityEngine.Object[](ListWrapper listWrapper)
        {
            return listWrapper.List.ToArray();
        }

        public static implicit operator List<UnityEngine.Object>(ListWrapper listWrapper)
        {
            return listWrapper.List;
        }

        public static implicit operator ListWrapper(List<UnityEngine.Object> list)
        {
            return new ListWrapper(list);
        }

        public static implicit operator ListWrapper(UnityEngine.Object[] array)
        {
            return new ListWrapper(array.ToList());
        }

        public ListWrapper()
        {
            List = new List<UnityEngine.Object>();
        }

        public ListWrapper(List<UnityEngine.Object> list)
        {
            List = list;
        }
    }
}