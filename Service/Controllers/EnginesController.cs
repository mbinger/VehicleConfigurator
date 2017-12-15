using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DAL.Common.Equipment;
using DAL.Common.Interface;
using Service.Common.DataTransfer;

namespace Service.Controllers
{
   public class EnginesController : BaseApiController<EngineDto, Engine>
   {
      public EnginesController(IRepository<Engine> repository) : base(repository)
      {
      }
   }
}
