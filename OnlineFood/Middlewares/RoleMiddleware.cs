using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineFood.Middlewares;
public class RoleMiddleware
{
    private readonly RequestDelegate _next;

    public RoleMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var path = context.Request.Path.Value;

        if (path != null && path.StartsWith("/admin"))
        {
            var role = context.Session.GetString("Role");
            if (role == null || role != "Admin")
            {
                context.Response.Redirect("/Accounts/Login");
                return;
            }
        }

        await _next(context);
    }
}