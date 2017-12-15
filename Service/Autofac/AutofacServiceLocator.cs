using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac;
using DAL.Common.Interface;

namespace Service.Autofac
{
   /// <summary>
   /// Autofac service locator
   /// </summary>
   public class AutofacServiceLocator: IServiceLocator, IDisposable
   {
      public AutofacServiceLocator(ILifetimeScope scope)
      {
         _scope = scope;
      }

      private ILifetimeScope _scope;

      public T GetService<T>() where T : class
      {
         return _scope.Resolve<T>();
      }

      public T GetNamedServiceSafe<T>(Type kind) where T : class
      {
         if (_scope.IsRegisteredWithName<T>(kind.Name))
         {
            return _scope.ResolveNamed<T>(kind.Name);
         }
         if (_scope.IsRegistered<T>())
         {
            return _scope.Resolve<T>();
         }
         return null;
      }

      public void Dispose()
      {
         Dispose(true);
         GC.SuppressFinalize(this);
      }

      protected virtual void Dispose(bool dispose)
      {
         if (dispose)
         {
            var disposableScope = _scope as IDisposable;
            if (disposableScope != null)
            {
               disposableScope.Dispose();
            }
            _scope = null;
         }
      }
   }
}