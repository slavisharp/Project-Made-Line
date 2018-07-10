namespace MadeLine.Api.Extensions.Mvc
{
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using System.Collections.Generic;
    using System.Linq;

    public class ModelStateError
    {
        public string Property { get; set; }

        public IEnumerable<string> Errors { get; set; }
    }

    public static class ModelStateExtensions
    {
        public static IEnumerable<ModelStateError> GetErrors(this ModelStateDictionary model)
        {
            var errors = new List<ModelStateError>();
            foreach (var item in model)
            {
                if (item.Value.Errors.Any())
                {
                    errors.Add(new ModelStateError() { Errors = item.Value.Errors.Select(e => e.ErrorMessage), Property = item.Key });
                }
            }

            return errors;
        }
    }
}
