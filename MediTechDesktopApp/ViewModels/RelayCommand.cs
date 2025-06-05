// File: ViewModels/RelayCommand.cs
using System;
using System.Windows.Input;

namespace MediTechDesktopApp.ViewModels
{
    /// <summary>
    /// Simple ICommand to wire button clicks to ViewModel methods.
    /// </summary>
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Predicate<object> _canExecute;

        public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
            => _canExecute == null || _canExecute(parameter);

        public void Execute(object parameter)
            => _execute(parameter);

        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Call this when the “CanExecute” condition changes.
        /// </summary>
        public void RaiseCanExecuteChanged()
            => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
