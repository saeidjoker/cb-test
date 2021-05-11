using Cleverbit.CodingTask.Application.Models.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Cleverbit.CodingTask.Host.Filters {

    public class ApiErrorFilter : IActionFilter, IOrderedFilter {
        public int Order { get; } = int.MaxValue - 10;

        public void OnActionExecuting(ActionExecutingContext context) {
        }

        public void OnActionExecuted(ActionExecutedContext context) {
            if (!(context.Exception is ApiError exception)) return;

            context.Result = new ObjectResult(exception.Message) {
                StatusCode = exception.Status,
            };

            context.ExceptionHandled = true;
        }
    }

}