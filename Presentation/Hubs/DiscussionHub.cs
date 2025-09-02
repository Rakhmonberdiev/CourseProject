using Microsoft.AspNetCore.SignalR;
namespace Presentation.Hubs
{
    public sealed class DiscussionHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            var httpCtx = Context.GetHttpContext();
            var invId = httpCtx?.Request.Query["invId"].ToString();
            if (!string.IsNullOrEmpty(invId))
                await Groups.AddToGroupAsync(Context.ConnectionId, invId);
            await base.OnConnectedAsync();
        }
    }
}
