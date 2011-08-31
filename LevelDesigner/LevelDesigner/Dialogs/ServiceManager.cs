using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LevelDesigner
{
    class ServiceManager : IServiceProvider
    {
        Dictionary<Type, Object> services = new Dictionary<Type, object>();

        public void AddService<T>(T service)
        {
            services.Add(typeof(T), service);
        }

        public object GetService(Type serviceType)
        {
            object service;

            services.TryGetValue(serviceType, out service);

            return service;
        }
    }
}
