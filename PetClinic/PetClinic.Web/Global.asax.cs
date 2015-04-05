using PetClinic.Data;
using PetClinic.Data.DomainObjects;
using PetClinic.Data.Repository;
using PetClinic.Web.IoC;
using System;
using System.Collections.Generic;
using System.Configuration;
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
            string connectionString = ConfigurationManager.ConnectionStrings["PetClinic"].ConnectionString;
            IContextFactory migrationContext = new ContextFactory(connectionString);
            IRepository<User> migrationRepository = new Repository<User>(migrationContext);
            migrationRepository.UpdateSchema();

            //TODO: Full 3 Test Users in the database HERE
        }
    }
}
