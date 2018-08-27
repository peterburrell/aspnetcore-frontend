using Microsoft.AspNetCore.Mvc.Razor;

namespace Demo.Web.Features
{
    public abstract class FusionRazorPage<TModel> : RazorPage<TModel>
    {
        public string ApplicationName { get; } = "Fusion";
    }
}
