namespace Presentation.Endpoints.Auth.Base
{
    public static class BaseAuth
    {
        public static IEndpointRouteBuilder MapAuth(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api/auth").WithTags("Auth");
            group.MapLoginEndpoint()
                .MapRegister()
                .MapLogout()
                .MapUserInfo();
            return app;
        }
    }
}
