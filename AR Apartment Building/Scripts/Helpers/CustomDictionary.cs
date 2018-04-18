#region Using Directives

using System.Collections.Generic;

#endregion

namespace Assets.Scripts.Helpers
{
    public class CustomDictionary<TKey, TValue>
    {
        #region Fields

        #region  Private Fields

        private readonly Dictionary<TKey, TValue> _inputs = new Dictionary<TKey, TValue>();

        #endregion

        #endregion

        #region Properties

        #region Regular Properties

        public TValue this[TKey axis]
        {
            get
            {
                if(!_inputs.ContainsKey(axis))
                    _inputs.Add(axis, default(TValue));
                return _inputs[axis];
            }
            set
            {
                if(!_inputs.ContainsKey(axis))
                    _inputs.Add(axis, default(TValue));
                _inputs[axis] = value;
            }
        }

        #endregion

        #endregion
    }
}