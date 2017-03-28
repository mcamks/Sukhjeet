using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlueSignal.App_Start
{
    public class UnityOfWork
    {
        public static void RegisterTypes(IUnityContainer container)
        {
            //container.RegisterType<UnityConfig, UnityConfig>(new PerThreadLifetimeManager());
        }
    }

    public static class ContainerManager
    {
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }
    }
}