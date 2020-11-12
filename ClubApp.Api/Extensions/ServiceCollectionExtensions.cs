using ClubApp.Logic.LogicBase;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ClubApp.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterLogicAssembly(this IServiceCollection services, string logicAssemblyName)
        {
            Assembly logicAssembly = Assembly.Load(logicAssemblyName);
            Type[] logicClasses = logicAssembly.GetExportedTypes().Where(t => t.IsSubclassOf(typeof(LogicBase))).ToArray();
            foreach (Type logicClass in logicClasses)
            {
                var inter = logicClass.GetInterfaces().FirstOrDefault(i => i.Name == $"I{logicClass.Name}");
                var descriptor = new ServiceDescriptor(inter, logicClass, ServiceLifetime.Scoped);
                services.Add(descriptor);
            }

            return services;
        }
    }
}
