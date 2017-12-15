﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL.Common.Equipment;
using DAL.Common.Interface;
using Service.Common.DataTransfer;

namespace Service.Controllers
{
   public class CarsController : BaseApiController<CarDto, Car>
   {
      public CarsController(IRepository<Car> repository): base(repository)
      {
      }
   }
}