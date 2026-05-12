using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Services
{
    public static class ServiceLocator
    {
        private static Dictionary<Type, IService> services = new();
        public static bool TryAddService(IService service)
        {
            if (services.ContainsKey(service.GetType())) return false;
            services.Add(service.GetType(), service);
            return true;
        }

        public static bool TryGetService<T>(out T service) where T : IService
        {
            service = default;
            if (!services.ContainsKey(typeof(T))||!services.TryGetValue(typeof(T),out IService found)) return false;
            if (found is not T) return false;
            service = (T)found;
            return true;
        }

        public static bool TryRemoveService(Type service)
        {
            if (!services.ContainsKey(service.GetType())) return false; 
            services.Remove(service.GetType());
            return true;
        }
    }
}
