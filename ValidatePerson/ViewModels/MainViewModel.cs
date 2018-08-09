using System.Windows;
using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;
using ValidatePerson.Models;

namespace ValidatePerson.ViewModels
{
    public class MainViewModel : BindableBase
    {
        public MainViewModel()
        {
            // MainViewModel creates a PersonViewModel with a Person
            PersonViewModel = new PersonViewModel(new Person());

            ExitCommand = new DelegateCommand(Exit);
        }

        #region Bindable Properties

        // PersonViewModel is set as PersonView's DataContext
        public PersonViewModel PersonViewModel { get; }

        public ICommand ExitCommand { get; }

        #endregion

        #region Private Methods

        private bool CanExit()
        {
            return PersonViewModel.IsValid;
        }

        private void Exit()
        {
            if (CanExit())
                Application.Current.Shutdown();
        }

        #endregion
    }
}
