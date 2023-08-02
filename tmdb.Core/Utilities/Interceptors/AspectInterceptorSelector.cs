using Castle.DynamicProxy;
using System.Reflection;

namespace tmdb.Core.Utilities.Interceptors
{
    public class AspectInterceptorSelector : IInterceptorSelector
    {
        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        {
            var classAttributes = type.GetCustomAttributes<MethodInterceptionBaseAttribute>
                (true).ToList();

            var methodParameters = method.GetParameters();
            var parameterTypes = new List<Type>();
            foreach (var parameter in methodParameters)
                parameterTypes.Add(parameter.ParameterType);

            var methodAttributes = type.GetMethod(method.Name, parameterTypes.ToArray())
                .GetCustomAttributes<MethodInterceptionBaseAttribute>(true);
            classAttributes.AddRange(methodAttributes);

            return classAttributes.OrderBy(x => x.Priority).ToArray();
        }
    }
}
