using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shop.Classes
{
    public abstract class RequiredIfAttribute : ValidationAttribute
    {
        protected string valueToCompare;


        protected abstract string CustomErrorMessage { get; set; }

        public RequiredIfAttribute(string valueName)
        {
            this.valueToCompare = valueName;
        }

        protected T GetField<T>(ValidationContext validationContext)
        {
            //If I understand right objectType is e. g. BasePersonViewModel and fieldToCopmare is any field in that class
            var objectType = validationContext.ObjectInstance.GetType();
            var fieldToCompare = objectType.GetProperty(valueToCompare);

            if (fieldToCompare == null)
            {
                throw new MissingFieldException($"Nepodarilo sa získať hodnotu {fieldToCompare} na objekte {objectType.FullName}");
            }
            return (T)fieldToCompare.GetValue(validationContext.ObjectInstance); //returns the property value 
        }

        protected virtual ValidationResult BuildErrorMessage(ValidationContext validationContext)
        {
            var displayName = validationContext.DisplayName;
            return new ValidationResult(ErrorMessage
                ?? string.Format(CustomErrorMessage, displayName, valueToCompare));
        }
    }

    public class RequiredIfFalseAttribute : RequiredIfAttribute
    {

        protected override string CustomErrorMessage { get; set; } = "Údaj {0} je vyžadován, pokud není potvrzen údaj {1}";

        public RequiredIfFalseAttribute(string valueName) : base(valueName) { }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // gets the value of specified property. We get the property from constructor
            var fieldValue = GetField<bool>(validationContext);

            if (fieldValue == true || value != null && value.ToString() != string.Empty)
            {
                return ValidationResult.Success;
            }
            else
            {
                return BuildErrorMessage(validationContext);
            }
           
        }
    }

    public class RequiredIfEmpty : RequiredIfAttribute
    {
        public RequiredIfEmpty(string valueName) : base(valueName) { }

        protected override string CustomErrorMessage { get; set; } = "Musíte zadať údaj {0} pokiaľ nie je zadaný údaj {1}";

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var fieldValue = GetField<string>(validationContext);
            var stringValue = (string)value;

            if (string.IsNullOrEmpty(fieldValue) && string.IsNullOrEmpty(stringValue))
            {
                return BuildErrorMessage(validationContext);
            }
            else
            {
                return ValidationResult.Success;
            }
        }
    }

    public class RequiredIfNotEmpty : RequiredIfAttribute
    {
        public RequiredIfNotEmpty(string valueName) : base(valueName)
        {
        }

        protected override string CustomErrorMessage { get; set; } = "Pokiaľ je zadaný údaj {1} musíte zadať aj údaj {0}";

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string fieldval = GetField<string>(validationContext);
            string stringval = ((string)value);

            bool arenotnull = fieldval != null && stringval != null;
            bool arebothempty = fieldval == stringval && stringval == string.Empty;
            bool arebothfilled = fieldval?.Length > 0 && stringval?.Length > 0;

            if (arenotnull &&
                arebothempty || arebothfilled)
            {
                return ValidationResult.Success;
            }

            string defaultErrorMessage = $"Pokud je zadán údaj {valueToCompare}, je vyžadován i údaj {validationContext.DisplayName}";
            return new ValidationResult(ErrorMessage?.Length == 0 ? defaultErrorMessage : ErrorMessage);    

        }
    }


}
