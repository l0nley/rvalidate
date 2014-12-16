using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace RValidate
{
    /// <summary>
    /// Validation filter attribute. Just put it on controller
    /// </summary>
    public class RValidateAttribute : ActionFilterAttribute
    {
        public RValidateAttribute(bool validateCollections = true, bool createErrorResponse = true)
        {
            ValidateCollections = validateCollections;
            CreateErrorResponse = createErrorResponse;
        }

        /// <summary>
        /// Does we need go deeper into enumerables and validate collections?
        /// </summary>
        public bool ValidateCollections { get; private set; }

        /// <summary>
        /// Create error response or not?
        /// </summary>
        public bool CreateErrorResponse { get; set; }

        /// <summary>
        /// Will be executed before action handler called
        /// </summary>
        /// <param name="actionContext">Action execution context</param>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            RHttpMethods method;
            if (Enum.TryParse(actionContext.Request.Method.Method, true, out method) == false)
            {
                // TODO Log warn unknown http method
                return;
            }

            var nulls = actionContext.ActionArguments.Where(l => l.Value == null).ToArray();
            if (nulls.Any())
            {
                foreach (var pair in nulls)
                {
                    actionContext.ModelState.AddModelError(pair.Key, "Parameter is required but not supplied in request");
                }

                if (CreateErrorResponse)
                {
                    actionContext.Response = 
                        actionContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, actionContext.ModelState);
                }

                base.OnActionExecuting(actionContext);
                return;
            }

            foreach (var argument in actionContext.ActionArguments)
            {
                RValidationHelper.ValidateProperties(argument.Value, method, actionContext.ModelState, ValidateCollections);
            }

            if (actionContext.ModelState.IsValid)
            {
                return;
            }

            if (CreateErrorResponse)
            {
                actionContext.Response =
                    actionContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, actionContext.ModelState);
            }

            base.OnActionExecuting(actionContext);
        }
    }
}