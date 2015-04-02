using PetClinic.Data;
using PetClinic.Data.DomainObjects;
using PetClinic.Data.Repository;
using PetClinic.Web.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace PetClinic.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            UnityControllerFactory.Configure();

            IContextFactory migrationContext = new ContextFactory(@"data source=.\sqlexpress;initial catalog=PetClinic;integrated security=True");
            IRepository<Category> migrationRepository = new Repository<Category>(migrationContext);
            migrationRepository.UpdateSchema();
        }
    }
}
