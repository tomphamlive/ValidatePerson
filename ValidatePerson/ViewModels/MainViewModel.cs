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
            PersonViewModel = new PersonViewModel(new Person {Age = 19, FirstName = "John"});
            PersonViewModel.PropertyChanged += OnChangedPersonViewModel;

            IsValid = PersonViewModel.IsValid;

            ExitCommand = new DelegateCommand(Exit, CanExit);
        }

        #region Bindable Properties

        // PersonViewModel is set as PersonView's DataContext
        public PersonViewModel PersonViewModel { get; }

        public ICommand ExitCommand { get; }

        private bool _isValid;
        public bool IsValid
        {
            get => _isValid;
            set => SetProperty(ref _isValid, value);
        }

        #endregion

        #region Private Methods

        // Whenever PersonViewModel changes, see if it is valid
        private void OnChangedPersonViewModel(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            IsValid = PersonViewModel.IsValid;
        }

        private bool CanExit()
        {
            return IsValid;
        }

        private void Exit()
        {
            Application.Current.Shutdown();
        }

        #endregion
    }
}
