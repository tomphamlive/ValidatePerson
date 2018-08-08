using Prism.Mvvm;
using ValidatePerson.Models;

namespace ValidatePerson.ViewModels
{
    public class MainViewModel : BindableBase
    {
        public MainViewModel()
        {
            PersonViewModel = new PersonViewModel(new Person());
        }

        public PersonViewModel PersonViewModel { get; }
    }
}
