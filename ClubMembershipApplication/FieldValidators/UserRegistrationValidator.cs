using ClubMembershipApplication.Data;
using FieldValidatorAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubMembershipApplication.FieldValidators
{
    public class UserRegistrationValidator : IFieldValidator
    {
        const int FIRSTNAME_MIN_LENGTH = 2;
        const int FIRSTNAME_MAX_LENGTH = 100;
        const int LASTNAME_MIN_LENGTH = 2;
        const int LASTNAME_MAX_LENGTH = 100;

        delegate bool EmailExistsDel(string emailAddress);

        FieldValidatorDel _fieldValidatorDel = null;

        RequiredValidDel _requiredValidDel = null;
        StringLengthValidDel _stringLengthValidDel = null;
        DateValidDel _dateValidDel = null;
        PatternMatchValidDel _patternMatchValidDel = null;
        CompareFieldsValidDel _compareFieldsValidDel = null;

        EmailExistsDel _emailExistsDel = null;

        string[] _fieldArray = null;
        IRegister _register = null;

        public UserRegistrationValidator(IRegister register)
        {
            _register = register;
        }

        public string[] FieldArray
        {
            get
            {
                if (_fieldArray == null)
                    _fieldArray = new string[Enum.GetValues(typeof(FieldConstants.UserRegistrationField)).Length];

                return _fieldArray;
            }
        }

        public FieldValidatorDel ValidatorDel => _fieldValidatorDel;

        public void InitializeValidatorDelegates()
        {
            _fieldValidatorDel = new FieldValidatorDel(ValidField);
            _emailExistsDel = new EmailExistsDel(_register.EmailExists);

            _requiredValidDel = CommonFieldValidationFunctions.RequiredValidDel;
            _stringLengthValidDel = CommonFieldValidationFunctions.StringLengthValidDel;
            _dateValidDel = CommonFieldValidationFunctions.DateValidDel;
            _patternMatchValidDel = CommonFieldValidationFunctions.PatternMatchValidDel;
            _compareFieldsValidDel = CommonFieldValidationFunctions.CompareFieldsValidDel;
        }

        private bool ValidField(int fieldIndex, string fieldValue, string[] fieldArray, out string fieldInvalidMesage)
        {
            fieldInvalidMesage = string.Empty;

            var userRegistrationField = (FieldConstants.UserRegistrationField)fieldIndex;

            switch (userRegistrationField)
            {
                case FieldConstants.UserRegistrationField.EmailAddress:
                    fieldInvalidMesage = (!_requiredValidDel(fieldValue)) ? $"You must enter a value for field: {Enum.GetName(typeof(FieldConstants.UserRegistrationField), userRegistrationField)}{Environment.NewLine}" : string.Empty;
                    fieldInvalidMesage = (fieldInvalidMesage == string.Empty && !_patternMatchValidDel(fieldValue, CommonRegularExpressionValidationPatterns.Email_Address_RegEx_Pattern)) ? $"You must enter a valid email address{Environment.NewLine}" : fieldInvalidMesage;
                    fieldInvalidMesage = (fieldInvalidMesage == string.Empty && !_emailExistsDel(fieldValue)) ? $"The provided email address already exists{Environment.NewLine}" : fieldInvalidMesage;
                    break;
                case FieldConstants.UserRegistrationField.FirstName:
                    fieldInvalidMesage = (!_requiredValidDel(fieldValue)) ? $"You must enter a value for field: {Enum.GetName(typeof(FieldConstants.UserRegistrationField), userRegistrationField)}{Environment.NewLine}" : string.Empty;
                    fieldInvalidMesage = (fieldInvalidMesage == string.Empty && !_stringLengthValidDel(fieldValue, FIRSTNAME_MIN_LENGTH, FIRSTNAME_MAX_LENGTH)) ? $"The length of field: {Enum.GetName(typeof(FieldConstants.UserRegistrationField), userRegistrationField)} must be between {FIRSTNAME_MIN_LENGTH} and {FIRSTNAME_MAX_LENGTH}{Environment.NewLine}" : fieldInvalidMesage;
                    break;
                case FieldConstants.UserRegistrationField.LastName:
                    fieldInvalidMesage = (!_requiredValidDel(fieldValue)) ? $"You must enter a value for field: {Enum.GetName(typeof(FieldConstants.UserRegistrationField), userRegistrationField)}{Environment.NewLine}" : string.Empty;
                    fieldInvalidMesage = (fieldInvalidMesage == string.Empty && !_stringLengthValidDel(fieldValue, LASTNAME_MIN_LENGTH, LASTNAME_MAX_LENGTH)) ? $"The length of field: {Enum.GetName(typeof(FieldConstants.UserRegistrationField), userRegistrationField)} must be between {LASTNAME_MIN_LENGTH} and {LASTNAME_MAX_LENGTH}{Environment.NewLine}" : fieldInvalidMesage;
                    break;
                case FieldConstants.UserRegistrationField.Password:
                    fieldInvalidMesage = (!_requiredValidDel(fieldValue)) ? $"You must enter a value for field: {Enum.GetName(typeof(FieldConstants.UserRegistrationField), userRegistrationField)}{Environment.NewLine}" : string.Empty;
                    fieldInvalidMesage = (fieldInvalidMesage == string.Empty && !_patternMatchValidDel(fieldValue, CommonRegularExpressionValidationPatterns.Strong_Password_RegEx_Pattern)) ? $"Your password must contain at least 1 small-case letter, 1 capital letter, 1 special character and the length should be between 6 - 10 characters{Environment.NewLine}" : fieldInvalidMesage;
                    break;
                case FieldConstants.UserRegistrationField.PasswordCompare:
                    fieldInvalidMesage = (!_requiredValidDel(fieldValue)) ? $"You must enter a value for field: {Enum.GetName(typeof(FieldConstants.UserRegistrationField), userRegistrationField)}{Environment.NewLine}" : string.Empty;
                    fieldInvalidMesage = (fieldInvalidMesage == string.Empty && !_compareFieldsValidDel(fieldValue, fieldArray[(int)FieldConstants.UserRegistrationField.Password])) ? $"Your entry did not match your password{Environment.NewLine}" : fieldInvalidMesage;
                    break;
                case FieldConstants.UserRegistrationField.DateOfBirth:
                    fieldInvalidMesage = (!_requiredValidDel(fieldValue)) ? $"You must enter a value for field: {Enum.GetName(typeof(FieldConstants.UserRegistrationField), userRegistrationField)}{Environment.NewLine}" : string.Empty;
                    fieldInvalidMesage = (fieldInvalidMesage == string.Empty && !_dateValidDel(fieldValue, out DateTime validDateTime)) ? $"You did not enter a valid date of birth{Environment.NewLine}" : fieldInvalidMesage;
                    break;
                case FieldConstants.UserRegistrationField.PhoneNumber:
                    fieldInvalidMesage = (!_requiredValidDel(fieldValue)) ? $"You must enter a value for field: {Enum.GetName(typeof(FieldConstants.UserRegistrationField), userRegistrationField)}{Environment.NewLine}" : string.Empty;
                    fieldInvalidMesage = (fieldInvalidMesage == string.Empty && !_patternMatchValidDel(fieldValue, CommonRegularExpressionValidationPatterns.Uk_PhoneNumber_RegEx_Pattern)) ? $"You did not enter a valid UK phone number{Environment.NewLine}" : fieldInvalidMesage;
                    break;
                case FieldConstants.UserRegistrationField.AddressFirstLine:
                    fieldInvalidMesage = (!_requiredValidDel(fieldValue)) ? $"You must enter a value for field: {Enum.GetName(typeof(FieldConstants.UserRegistrationField), userRegistrationField)}{Environment.NewLine}" : string.Empty;
                    break;
                case FieldConstants.UserRegistrationField.AddressSecondLine:
                    fieldInvalidMesage = (!_requiredValidDel(fieldValue)) ? $"You must enter a value for field: {Enum.GetName(typeof(FieldConstants.UserRegistrationField), userRegistrationField)}{Environment.NewLine}" : string.Empty;
                    break;
                case FieldConstants.UserRegistrationField.AddressCity:
                    fieldInvalidMesage = (!_requiredValidDel(fieldValue)) ? $"You must enter a value for field: {Enum.GetName(typeof(FieldConstants.UserRegistrationField), userRegistrationField)}{Environment.NewLine}" : string.Empty;
                    break;
                case FieldConstants.UserRegistrationField.PostCode:
                    fieldInvalidMesage = (!_requiredValidDel(fieldValue)) ? $"You must enter a value for field: {Enum.GetName(typeof(FieldConstants.UserRegistrationField), userRegistrationField)}{Environment.NewLine}" : string.Empty;
                    fieldInvalidMesage = (fieldInvalidMesage == string.Empty && !_patternMatchValidDel(fieldValue, CommonRegularExpressionValidationPatterns.Uk_Post_Code_RegEx_Pattern)) ? $"You did not enter a valid UK post code{Environment.NewLine}" : fieldInvalidMesage;
                    break;
                default:
                    throw new ArgumentException("This field does not exist");
            }

            return fieldInvalidMesage == string.Empty;
        }
    }
}
