#region Using Directives

using System;
using System.Windows.Input;

#endregion

namespace Assets.Scripts.Entities
{
    public class RelayCommand : EntityBase, ICommand
    {
        #region Fields

        #region  Private Fields

        private readonly Predicate<object> _canExecute;
        private readonly Action<object> _execute;

        #endregion

        #endregion

        #region Constructors

        public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
        {
            if(execute == null)
                throw new ArgumentNullException("execute");
            _execute = execute;
            _canExecute = canExecute;
        }

        public RelayCommand(Action execute, Predicate<object> canExecute = null) : this(x => execute(), canExecute)
        {
        }

        #endregion

        #region Interface Implementations

        #region ICommand Members

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        #endregion

        #endregion
    }
}