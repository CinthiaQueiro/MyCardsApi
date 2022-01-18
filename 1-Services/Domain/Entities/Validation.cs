using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Validation<TEntity> where TEntity : class
    {
        /// <summary>
        /// Check 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<ValidationResult> ValidationModel(TEntity model)
        {
            var context = new ValidationContext(model, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();

            Validator.TryValidateObject(model, context, validationResults, true);

            return validationResults;
        }
    }
}
