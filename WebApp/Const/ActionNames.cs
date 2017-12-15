using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Const
{
   public class ActionNames
   {
      public static class Order
      {
         public const string
            Index = "Index",
            Create = "Create",
            Edit = "Edit",
            Done = "Done",
            Changed = "Changed";
      }

      public static class Theme
      {
         public const string
            Index = "Index";
      }
   }
}