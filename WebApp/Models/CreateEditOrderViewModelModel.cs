using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
   public class CreateEditOrderViewModelModel
   {
      public CreateEditOrderModel Base { get; set; }
      public CreateEditTemplatesModel TemplatesModel { get; set; }
      public string ImagesPath { get; set; }
   }
}