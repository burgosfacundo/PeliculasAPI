using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace PeliculasAPI.Helpers
{
    public class TypeBinder<T> : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var nameProp = bindingContext.ModelName;
            var value = bindingContext.ValueProvider.GetValue(nameProp);

            if(value == ValueProviderResult.None)
            {
                return Task.CompletedTask;
            }
            try
            {
                var valueDeserialized = JsonConvert.DeserializeObject<T>(value.FirstValue);
                bindingContext.Result = ModelBindingResult.Success(valueDeserialized);
            }
            catch 
            {
                bindingContext.ModelState.TryAddModelError(nameProp, "Invalid value");
            }
            return Task.CompletedTask;
        }
    }
}
