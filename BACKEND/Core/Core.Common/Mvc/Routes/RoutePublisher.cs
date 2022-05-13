using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Routing;

namespace Core.Common.Mvc.Routes
{
    /// <summary>
    /// Route publisher
    /// </summary>
    public partial class RoutePublisher : IRoutePublisher
    {
      
        /// <summary>
        /// Register routes
        /// </summary>
        /// <param name="routes">Routes</param>
        public virtual void RegisterRoutes(RouteCollection routes)
        {
            AppDomainTypeFinder typeFinder = new AppDomainTypeFinder();
            var routeProviderTypes = typeFinder.FindClassesOfType<IRouteProvider>();
            var routeProviders = new List<IRouteProvider>();
            foreach (var providerType in routeProviderTypes)
            {
                var provider = Activator.CreateInstance(providerType) as IRouteProvider;
                routeProviders.Add(provider);
            }
            routeProviders = routeProviders.OrderByDescending(rp => rp.Priority).ToList();
            routeProviders.ForEach(rp => rp.RegisterRoutes(routes));
        }
    }
}
