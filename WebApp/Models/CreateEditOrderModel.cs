using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
   public class CreateEditOrderModel
   {
      public long? Id { get; set; }
      /// <summary>
      /// Service base url
      /// </summary>
      public string ServiceApiUrl { get; set; }
   }
}