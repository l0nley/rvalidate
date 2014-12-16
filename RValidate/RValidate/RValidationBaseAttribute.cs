using System;
using System.ComponentModel.DataAnnotations;

namespace RValidate
{
    /// <summary>
    /// Base class for all validation attributes, works with RValidate action filter
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = true)]
    public abstract class RValidationBaseAttribute : ValidationAttribute
    {
        /// <summary>
        /// Create attributes. If no methods specified, then PUT and POST methods will be added by default
        /// </summary>
        /// <param name="methods">Methods, with which validation will be performed</param>
        protected RValidationBaseAttribute(params RHttpMethods[] methods)
        {
            Methods = methods.Length == 0 ? new[] { RHttpMethods.Post, RHttpMethods.Put } : methods;
        }

        /// <summary>
        /// Methods, on which validation will be performed
        /// </summary>
        public RHttpMethods[] Methods { get; protected set; }
    }
}
