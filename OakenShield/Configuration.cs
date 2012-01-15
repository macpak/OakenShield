using System.Collections.Generic;
using Castle.Core;
using Castle.Windsor;

namespace OakenShield
{
    public static class Configuration
    {
        public static void Register(this WindsorContainer container, IEnumerable<MethodDescriptor> methodDescriptors)
        {
            EnsureInterceptorRegistered(container);

            foreach (var methodDescriptor in methodDescriptors)
            {
                container.Register(
                    Castle.MicroKernel.Registration.Component.For(methodDescriptor.DeclaringType)
                        .ImplementedBy(methodDescriptor.DeclaringType).Interceptors(typeof(FilteringInterceptor)).SelectInterceptorsWith(new FilteringInterceptorSelector(methodDescriptor.Predicates)));
            }
        }

        private static void EnsureInterceptorRegistered(IWindsorContainer container)
        {
            if (!container.Kernel.HasComponent(typeof (FilteringInterceptor)))
            {
                container.Register(
                    Castle.MicroKernel.Registration.Component.For<FilteringInterceptor>().ImplementedBy
                        <FilteringInterceptor>());
            }
        }
    }

    //For<IType>().MethodSelector(x => x.MethodSelector).Arg(x => x.arg).greater(5);
}