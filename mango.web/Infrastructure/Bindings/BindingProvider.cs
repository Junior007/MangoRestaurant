
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Globalization;

namespace mango.web.Infrastructure.Bindings
{
    public class BindingProvider : IModelBinderProvider
    {
        public IModelBinder? GetBinder(ModelBinderProviderContext context)
        {
            GetBinders();

            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (context.Metadata.ModelType == null)
                return null;
            return _modelsBinder.Where(o => o.GetType().Name.ToLower() == context.Metadata.ModelType.Name.ToLower()).FirstOrDefault();

        }

        private static IEnumerable<IModelBinder?> _modelsBinder = null;
        private static IEnumerable<IModelBinder?> GetBinders()
        {
            if (_modelsBinder != null)
                return _modelsBinder;

            var typeForSearch = typeof(IModelBinder);

            //Una referencia al ensamblado actual
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            //En el ensamblado actual busco las clases instanciables como IModelBinder sus instancias en una lista
            _modelsBinder = assembly
                .GetTypes()
                .Where(t => t.IsAssignableTo(typeForSearch))
                .Select(t => (IModelBinder?)Activator.CreateInstance(t));

            return _modelsBinder;

        }

    }



}
