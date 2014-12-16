using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Http.ModelBinding;

namespace RValidate
{
    internal static class RValidationHelper
    {
        internal static void ValidateProperties(
            object instance,
            RHttpMethods methodContext,
            ModelStateDictionary modelState,
            bool validateCollections,
            string modelNameRoot = "")
        {
            if (modelState == null)
            {
                throw new ArgumentNullException("modelState");
            }

            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }

            var properties = instance.GetType().GetProperties();

            foreach (var prop in properties)
            {
                var attrs = prop.GetCustomAttributes(true).Select(l => new
                {
                    Type = l.GetType(),
                    Value = l
                }).ToArray();
                var validationAttributes = attrs
                    .Where(l => l.Type.IsSubclassOf(typeof(RValidationBaseAttribute)))
                    .Select(l => (RValidationBaseAttribute)l.Value)
                    .Where(l => l.Methods.Contains(methodContext));
                var displayAttributes = attrs
                    .Where(l =>
                        l.Type == typeof(DisplayNameAttribute) ||
                        l.Type.IsSubclassOf(typeof(DisplayNameAttribute))).ToArray();
                var displayName = modelNameRoot + prop.Name;
                if (displayAttributes.Any())
                {
                    var display = displayAttributes.First();
                    displayName = modelNameRoot + ((DisplayNameAttribute)display.Value).DisplayName;
                }

                foreach (var attr in validationAttributes)
                {
                    var value = prop.GetValue(instance);
                    
                    var validationContext = new ValidationContext(instance)
                    {
                        MemberName = prop.Name,
                        DisplayName = displayName
                    };
                    var result = attr.GetValidationResult(value, validationContext);
                    if (result != null && string.IsNullOrEmpty(result.ErrorMessage) == false)
                    {
                        modelState.AddModelError(validationContext.DisplayName, result.ErrorMessage);
                    }
                }

                if (typeof(IEnumerable).IsAssignableFrom(prop.PropertyType) == false || validateCollections == false)
                {
                    continue;
                }

                var propertyValue = (IEnumerable)(prop.GetValue(instance) ?? Enumerable.Empty<object>());
                var itemNo = 0;
                foreach (var item in propertyValue)
                {
                    ValidateProperties(item, methodContext, modelState, true, displayName + "[" + itemNo + "].");
                    itemNo++;
                }
            }
        }
    }
}