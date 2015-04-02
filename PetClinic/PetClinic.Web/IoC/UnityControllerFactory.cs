using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using System.Web.Routing;
using System.Configuration;
using PetClinic.Data;
using PetClinic.Data.Infrastructure;
using PetClinic.Data.Service;
using PetClinic.Data.Repository;

namespace PetClinic.Web.IoC
{
    public class UnityControllerFactory : DefaultControllerFactory
    {
        IUnityContainer container;

        public UnityControllerFactory(IUnityContainer container)
        {
            this.container = container;
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            try
            {
                if (controllerType == null)
                {
                    throw new ArgumentException(string.Format("Type requested is not a controller: {0}", controllerType.Name), "controllerType");
                }
                return container.Resolve(controllerType) as IController;
            }
            catch { return null; }
        }

        public static void Configure()
        {
            IUnityContainer container = new UnityContainer();

            string connectionString = ConfigurationManager.ConnectionStrings["PetClinic"].ConnectionString;
            container.RegisterType<IContextFactory, ContextFactory>(new ContainerControlledLifetimeManager(), new InjectionConstructor(connectionString))
                     .RegisterType<IUnitOfWork, UnitOfWork>(new ContainerControlledLifetimeManager())
                     .RegisterType<IProductService, ProductService>()
                     .RegisterType(typeof(IRepository<>), typeof(Repository<>));

            ControllerBuilder.Current.SetControllerFactory(new UnityControllerFactory(container));

            //RegisterModelBinders(container);
        }

        //private static void RegisterModelBinders(IUnityContainer container)
        //{
        //    ModelBinders.Binders.Add(typeof(CategoryViewModel), new CategoryViewModelBinder(container));
        //}


    }
}