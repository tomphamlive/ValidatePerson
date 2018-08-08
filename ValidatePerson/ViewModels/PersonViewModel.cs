using Prism.Mvvm;
using System.ComponentModel;
using ValidatePerson.Models;

namespace ValidatePerson.ViewModels
{
    // Performs data validation on its properties before its model is modified.
    // Also provides error information about the object & its properties that a View can bind to.
    public class PersonViewModel : BindableBase, IDataErrorInfo    
    {
        private readonly Person _person;

        #region Ctor

        public PersonViewModel(Person person)
        {
            _person = person;
        }

        #endregion

        #region Bindable Properties

        private int _age;
        public int Age
        {
            get => _age;
            set
            {
                if (SetProperty(ref _age, value))
                {
                    if (ValidateAge() != string.Empty)  //not valid
                        return;

                    _person.Age = _age;
                }
            }
        }

        private string _firstName;
        public string FirstName
        {
            get => _firstName;
            set
            {
                if (SetProperty(ref _firstName, value))
                {
                    if (ValidateFirstName() != string.Empty)  //not valid
                        return;

                    _person.FirstName = _firstName;
                }
            } 
        }

        #endregion

        #region Implementation of IDataErrorInfo

        // Gets the error message for the property with the given name.
        // Called by WPF on the property whose Binding's ValidatesOnDataErrors is set to true.
        public string this[string propertyName] => GetValidationError(propertyName);

        // 	Gets an error message indicating what is wrong with this object.
        public string Error { get; private set; }

        #endregion

        #region Properties Validation

        private string GetValidationError(string propertyName)
        {
            Error = string.Empty;

            if (propertyName == nameof(Age))
                Error = ValidateAge();
            else if (propertyName == nameof(FirstName))
                Error = ValidateFirstName();

            return Error;
        }

        private string ValidateAge()
        {
            var error = string.Empty;
            if (Age <= 0 || Age > 150)
            {
                error = "Age must be greater than 0 and less than 150.";
            }                 
            return error;
        }

        private string ValidateFirstName()
        {
            var error = string.Empty;
            if (string.IsNullOrWhiteSpace(FirstName))
            {
                error = "First Name cannot be empty.";
            }
            return error;
        }

        #endregion
    }
}
