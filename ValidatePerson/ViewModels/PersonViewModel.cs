using Prism.Mvvm;
using System.ComponentModel;
using ValidatePerson.Models;

namespace ValidatePerson.ViewModels
{
    // PersonViewModel represents the display of a Person model.
    // It implements IDataErrorInfo, which means validation is performed on properties
    // of the view model. Any validation error is displayed in the View.  
    //
    // The model is updated only with valid values from the view model, which guarantees the model
    // will not contain invalid data.
    public class PersonViewModel : BindableBase, IDataErrorInfo    
    {
        // the model
        private readonly Person _person;

        #region Constructor Takes a Person

        public PersonViewModel(Person person)
        {
            _person = person;
        }

        #endregion

        #region Bindable Properties for a View

        private string _age;
        public string Age
        {
            get => _age;
            set
            {
                if (!SetProperty(ref _age, value))
                    return;

                // if no Error, then modify the model
                if (ValidateAge() == string.Empty)
                    _person.Age = int.Parse(_age);
            }
        }

        private string _firstName;
        public string FirstName
        {
            get => _firstName;
            set
            {
                if (!SetProperty(ref _firstName, value))
                    return;

                // if no Error, then modify the model
                if (ValidateFirstName() == string.Empty)
                    _person.FirstName = _firstName;
            } 
        }

        // vm is valid if it contains no error
        public bool IsValid => string.IsNullOrEmpty(Error);

        #endregion

        #region Implementation of IDataErrorInfo

        // When ValidatesOnDataErrors is set to true on a View's Binding,
        // WPF calls this method to get any error message on the binded property.
        public string this[string propertyName] => GetValidationError(propertyName);

        // Expose an error string to indicate anything wrong with this object.
        public string Error
        {
            get
            {
                var ageError = ValidateAge();
                if (!string.IsNullOrEmpty(ageError))
                    return ageError;

                var firstNameError = ValidateFirstName();
                if (!string.IsNullOrEmpty(firstNameError))
                    return firstNameError;

                return string.Empty;
            }
        }

        #endregion

        #region Properties Validation

        // Validate the specified property, and returns an error string if any.
        private string GetValidationError(string propertyName)
        {
            switch (propertyName)
            {
                case nameof(Age):
                    return ValidateAge();
                case nameof(FirstName):
                    return ValidateFirstName();
            }
            return string.Empty;
        }

        private string ValidateAge()
        {
            var error = string.Empty;
            if (string.IsNullOrWhiteSpace(Age))
                error = "Must enter value for age.";
            else if (!int.TryParse(Age, out var age))
                error = "Must enter an integer value for age.";
            else if (age <= 0 || age > 150)
                error = "Age must be greater than 0 and less than 150.";
            return error;
        }

        private string ValidateFirstName()
        {
            var error = string.Empty;
            if (string.IsNullOrWhiteSpace(FirstName))   // validation logic
            {
                error = "First Name cannot be empty.";
            }
            return error;
        }

        #endregion
    }
}
