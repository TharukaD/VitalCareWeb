namespace VitalCareWeb.Middleware
{
    public class ExceptionHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception error)
            {
                string errorPath = "/Error/InternalServerError";
                switch (error)
                {
                    case KeyNotFoundException e:
                        errorPath = "/Error/NotFoundError";
                        break;
                }

                _logger.LogError(error, error.Message);
                context.Response.Redirect(errorPath);
            }
        }
    }
}
