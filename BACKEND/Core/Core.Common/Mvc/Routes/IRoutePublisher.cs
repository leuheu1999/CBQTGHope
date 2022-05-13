using System.Web.Routing;

namespace Core.Common.Mvc.Routes
{
    /// <summary>
    /// Route publisher
    /// </summary>
    public partial interface IRoutePublisher
    {
        /// <summary>
        /// Register routes
        /// </summary>
        /// <param name="routes">Routes</param>
        void RegisterRoutes(RouteCollection routes);
    }
}
