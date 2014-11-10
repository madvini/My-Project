﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ETask1.Models
{
    public class EtaskValidation
    {
        /// <summary>
        ///  Validation: To Check Entered Date is Less  Than Current Date
        /// </summary>
        /// 
        public sealed class InputLessThanFutureDateAttribute : ValidationAttribute
        {
            public override bool IsValid(object value)
            {
                DateTime d = (DateTime)value;
                return (d <=DateTime.Today);
            }
        }
        public sealed class InputGreaterThanFutureDateAttribute : ValidationAttribute
        {
            public override bool IsValid(object value)
            {
                DateTime d = (DateTime)value;
                return (d >= DateTime.Today);
            }
        }

        public sealed class DateEndAttribute : ValidationAttribute
        {
            public string DateStartProperty { get; set; }
            public override bool IsValid(object value)
            {
                // Get Value of the DateStart property
                string dateStartString = HttpContext.Current.Request[DateStartProperty];
                DateTime dateEnd = (DateTime)value;
                DateTime dateStart = DateTime.Parse(dateStartString);

                return dateStart < dateEnd;
            }
        }
        public class DateGreaterThanAttribute : ValidationAttribute
        {
            string otherPropertyName;

            public DateGreaterThanAttribute(string otherPropertyName, string errorMessage)
                : base(errorMessage)
            {
                this.otherPropertyName = otherPropertyName;
            }

            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                ValidationResult validationResult = ValidationResult.Success;
                try
                {
                    // Using reflection we can get a reference to the other date property, in this example the project start date
                    var otherPropertyInfo = validationContext.ObjectType.GetProperty(this.otherPropertyName);
                    // Let's check that otherProperty is of type DateTime as we expect it to be
                    if (otherPropertyInfo.PropertyType.Equals(new DateTime().GetType()))
                    {
                        DateTime toValidate = (DateTime)value;
                        DateTime referenceProperty = (DateTime)otherPropertyInfo.GetValue(validationContext.ObjectInstance, null);
                        // if the end date is lower than the start date, than the validationResult will be set to false and return
                        // a properly formatted error message
                        if (toValidate.CompareTo(referenceProperty) < 1)
                        {
                            validationResult = new ValidationResult(ErrorMessageString);
                        }
                    }
                    else
                    {
                        validationResult = new ValidationResult("An error occurred while validating the property. OtherProperty is not of type DateTime");
                    }
                }
                catch (Exception ex)
                {
                    // Do stuff, i.e. log the exception
                    // Let it go through the upper levels, something bad happened
                    throw ex;
                }

                return validationResult;
            }
        }
    }
}