using MediatR;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Text;

namespace BuildingBlocks.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse>
        (ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull, IRequest<TResponse>
        where TResponse : notnull
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            logger.LogInformation("[START] Handle Request={Request} - Response={Response} - RequestData={RequestData}", 
                typeof(TRequest).Name,
                typeof(TResponse).Name,
                request);

            var timer = new Stopwatch();
            timer.Start();

            var response = await next();

            timer.Stop();

            var timeTaken = timer.Elapsed;
            if (timeTaken.Seconds > 3)
            {
                logger.LogWarning("[PERFORMANCE] The Request={Request} with {Response} took {Taken}",
                     typeof(TRequest).Name,
                    typeof(TResponse).Name,
                    timeTaken);
            }

            logger.LogInformation("[END] Handle Request={Request} - Response={Response}",
             typeof(TRequest).Name,
             typeof(TResponse).Name);

            return response;
        }
    }
}
