using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Const
{
    public class ViewNames
    {
        public static class Order
        {
            public const string
                Index = "Index",
                CreateEdit = "CreateEdit",
                Done = "Done",
                Changed = "Changed",

                Page1 = "_Page1",
                Page2 = "_Page2",
                Page3 = "_Page3",
                Templates = "_Templates";
        }

        public static class DynamicResources
        {
            public const string
                ConfigurationJs = "_ConfigurationJs",
                PlainJs = "_PlainJs";
        }
    }
}