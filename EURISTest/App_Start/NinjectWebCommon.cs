[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(EURISTest.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(EURISTest.App_Start.NinjectWebCommon), "Stop")]

namespace EURISTest.App_Start
{
    using System;
    using System.Web;
    using EURIS.Entities;
    using EURIS.Service.Common.ServicesCommon;
    using EURIS.Service.IService;
    using EURIS.Service.Service;
    using EURIS.Service.UnitOfWork;
    using EURISTest.Controllers.ControllerServices;
    using EURISTest.Controllers.IControllerServices;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
                kernel.Bind<IProduct>().To<Product>();
                kernel.Bind<ICatalog>().To<Catalog>();
                kernel.Bind<IProductCatalog>().To<ProductCatalog>();
                kernel.Bind<IUnitOfWork>().To<UnitOfWork>();
                kernel.Bind<IProductManager>().To<ProductManager>();
                kernel.Bind<ICatalogManager>().To<CatalogManager>();
                kernel.Bind<IProductCatalogManager>().To<ProductCatalogManager>();
                kernel.Bind<ICatalogControllerServices>().To<CatalogControllerServices>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
        }        
    }
}
