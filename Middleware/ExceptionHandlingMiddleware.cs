using Microsoft.AspNetCore.Mvc;
using AwesomeCSharpNvim.Models.Exceptions;

namespace AwesomeCSharpNvim.Middleware;
public class ExceptionHandlingMiddleware
{
	private readonly RequestDelegate _next;
	public ExceptionHandlingMiddleware(RequestDelegate next)
	{
		_next = next;
	}

	public async Task InvokeAsync(HttpContext context)
	{
		try
		{
			await _next(context);
		}
		catch (NotFoundException notFoundException)
		{
			var problemDetails = new ProblemDetails
			{
				Title = notFoundException.Message,
				Status = StatusCodes.Status404NotFound
			};
			context.Response.StatusCode = StatusCodes.Status404NotFound;
			await context.Response.WriteAsJsonAsync(problemDetails);
		}
		catch (Exception)
		{
			var problemDetails = new ProblemDetails
			{
				Status = StatusCodes.Status500InternalServerError
			};
			context.Response.StatusCode = StatusCodes.Status500InternalServerError;
			await context.Response.WriteAsJsonAsync(problemDetails);
		}
	}
}