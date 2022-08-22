using System.ComponentModel.DataAnnotations;
namespace weding_planer.Models;

// you must ust the import on the top to use this inherent class
public class FutureDate : ValidationAttribute{

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if(value == null){
            return ValidationResult.Success;
        }
        DateTime date = (DateTime) value;
        if(date <= DateTime.Now){
            return new ValidationResult("must be a future date");
        }
        return ValidationResult.Success;
    }
}