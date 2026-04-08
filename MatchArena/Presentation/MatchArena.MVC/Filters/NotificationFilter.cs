using MatchArena.MVC.Services.Interfaces;
using MatchArena.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class NotificationFilter : IAsyncActionFilter
{
    private readonly IPlayerClientService _playerClient;

    public NotificationFilter(IPlayerClientService playerClient)
    {
        _playerClient = playerClient;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (context.HttpContext.User.Identity?.IsAuthenticated == true)
        {
            if (context.Controller is Controller controller)
            {
                try
                {
                    var invites = await _playerClient.GetMyInvitesAsync();
                    controller.ViewBag.Invites = invites;
                }
                catch
                {
                    controller.ViewBag.Invites = null;
                }
            }
        }

        await next();
    }
}