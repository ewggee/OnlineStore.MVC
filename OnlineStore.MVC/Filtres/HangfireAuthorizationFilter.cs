using Hangfire.Dashboard;
using OnlineStore.Contracts.ApplicationRoles;

namespace OnlineStore.MVC.Filtres
{
    public class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            return context.GetHttpContext().User.IsInRole(AppRoles.ADMIN);
        }
    }
}
