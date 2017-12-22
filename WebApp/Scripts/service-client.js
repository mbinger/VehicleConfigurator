var VehicleConfigurator = {

   RestServiceClient : function() {
      var self = this;

      this.ShowError = function () {
          Tools.showError(window.vcfg.Plain.CanNotConnectRestService);
         },

      this.ShowErrors = function (errors) {
         var message = "";
         for (let i = 0; i < errors.length; i++) {
            message += errors[i];
            if (i < errors.length - 1) {
               message += "<br>";
            }
         }
         Tools.showError(message);
      },

      //get cars
      this.GetCars = function (callback) {
         $.ajax({
            type: 'get',
            url: window.vcfg.Options.ServiceApiUrl + '/cars',
            dataType: 'json',
            success: function (data) {
               for (var i = 0; i < data.length; i++) {
                  data[i].ImageUrl = window.vcfg.Options.ImagesPath + data[i].ImageUrl;
               }
               callback(data);
            },
            error: function () {
               self.ShowError();
               callback(null);
            }
         });
      },

      //get engines
      this.GetEngines = function (callback) {
         $.ajax({
            type: 'get',
            url: window.vcfg.Options.ServiceApiUrl + '/engines',
            dataType: 'json',
            success: function (data) {
               for (var i = 0; i < data.length; i++) {
                  data[i].ImageUrl = window.vcfg.Options.ImagesPath + data[i].ImageUrl;
               }
               callback(data);
            },
            error: function () {
               self.ShowError();
               callback(null);
            }
         });
      }

      //get rims
      this.GetRims = function (callback) {
         $.ajax({
            type: 'get',
            url: window.vcfg.Options.ServiceApiUrl + '/rims',
            dataType: 'json',
            success: function (data) {
               for (var i = 0; i < data.length; i++) {
                  data[i].ImageUrl = window.vcfg.Options.ImagesPath + data[i].ImageUrl;
               }
               callback(data);
            },
            error: function () {
               self.ShowError();
               callback(null);
            }
         });
      }

      //get colors
      this.GetColors = function (callback) {
         $.ajax({
            type: 'get',
            url: window.vcfg.Options.ServiceApiUrl + '/colors',
            dataType: 'json',
            success: function (data) {
               for (var i = 0; i < data.length; i++) {
                  data[i].ImageUrl = window.vcfg.Options.ImagesPath + data[i].ImageUrl;
               }
               callback(data);
            },
            error: function () {
               self.ShowError();
               callback(null);
            }
         });
      }

      //get equipment
      this.GetEquipment = function (callback) {
         $.ajax({
            type: 'get',
            url: window.vcfg.Options.ServiceApiUrl + '/additionalequipmentitems',
            dataType: 'json',
            success: function (data) {
               for (var i = 0; i < data.length; i++) {
                  data[i].ImageUrl = window.vcfg.Options.ImagesPath + data[i].ImageUrl;
               }
               callback(data);
            },
            error: function () {
               self.ShowError();
               callback(null);
            }
         });
      }

      //checkout
      this.CreateOrder = function (orderDto, orderEquipmentDto, callback) {
         $.ajax({
            type: 'post',
            url: window.vcfg.Options.ServiceApiUrl + '/orders',
            data: orderDto,
            dataType: 'json',
            success: function (data) {
               if (!data.Success) {
                  self.ShowErrors(data.Errors);
                  callback(null);
                  return;
               }

               var orderId = data.Id;
               //create equipment
               self.SetOrderEquipment(orderId, orderEquipmentDto, function(success) {
                  if (success) {
                     callback(orderId);
                  } else {
                     self.ShowError();
                     callback(null);
                     return;
                  }
               });
            },
            error: function() {
               self.ShowError();
               callback(null);
            }
         });
      }

      //checkout
      this.EditOrder = function (id, orderDto, orderEquipmentDto, callback) {
         $.ajax({
            type: 'put',
            url: window.vcfg.Options.ServiceApiUrl + '/orders/' + id,
            data: orderDto,
            dataType: 'json',
            success: function(data) {
               if (!data.Success) {
                  self.ShowErrors(data.Errors);
                  callback(null);
                  return;
               }

               //update equipment
               self.SetOrderEquipment(id,
                  orderEquipmentDto,
                  function(success) {
                     if (success) {
                        callback(true);
                     } else {
                        self.ShowError();
                        callback(false);
                        return;
                     }
                  });
            },
            error: function() {
               self.ShowError();
               callback(false);
            }
         });
      }

      //get order by id
      this.GetOrder = function (id, callback) {
         $.ajax({
            type: 'get',
            url: window.vcfg.Options.ServiceApiUrl + '/orders/'+id,
            dataType: 'json',
            success: function (data) {
               callback(data);
            },
            error: function() {
               self.ShowError();
               callback(null);
            }
         });
      }

      //get order additional equipment ids by id
      this.GetOrderEquipment = function (id, callback) {
         $.ajax({
            type: 'get',
            url: window.vcfg.Options.ServiceApiUrl + '/orderequipment/' + id,
            dataType: 'json',
            success: function (data) {
               callback(data);
            },
            error: function() {
               self.ShowError();
               callback(null);
            }
         });
      }

      //set order additional equipment ids
      this.SetOrderEquipment = function (id, orderEquipmentDto, callback) {
         $.ajax({
            type: 'post',
            url: window.vcfg.Options.ServiceApiUrl + '/orderequipment/' + id,
            data: orderEquipmentDto,
            dataType: 'json',
            async: false,
            success: function (data) {
               if (!data.Success) {
                  self.ShowErrors(data.Errors);
                  callback(false);
               }

               callback(true);
            },
            error: function() {
               self.ShowError();
               callback(false);
            }
         });
      }

      //load all data
      this.LoadData = function(orderId, callback) {
         var result =
         {
            OrderId: 0,
            CustomerName: "",
            CarId: 0,
            EngineId: 0,
            ColorId: 0,
            RimId: 0,
            EquipmentIds: [],
            Cars: [],
            Engines: [],
            Colors: [],
            Rims: [],
            EquipmentItems: []
         };

         self.GetCars(function(cars) {
            if (cars == null) {
               self.ShowError();
               callback(null);
               return;
            }
            result.Cars = cars;

            self.GetEngines(function (engines) {
               if (engines == null) {
                  self.ShowError();
                  callback(null);
                  return;
               }
               result.Engines = engines;

               self.GetColors(function (colors) {
                  if (cars == null) {
                     self.ShowError();
                     callback(null);
                     return;
                  }
                  result.Colors = colors;

                  self.GetRims(function (rims) {
                     if (rims == null) {
                        self.ShowError();
                        callback(null);
                        return;
                     }
                     result.Rims = rims;

                     self.GetEquipment(function (eq) {
                        if (eq == null) {
                           self.ShowError();
                           callback(null);
                           return;
                        }
                        result.EquipmentItems = eq;

                        if (orderId == null || orderId == "") {
                           callback(result);
                           return;
                        }

                        //load order

                        self.GetOrder(orderId, function (orderToEditDto) {
                           if (orderToEditDto == null) {
                              self.ShowError();
                              callback(null);
                              return;
                           }
                           result.OrderId = orderToEditDto.Id;
                           result.CustomerName = orderToEditDto.CustomerName;
                           result.CarId = orderToEditDto.CarId;
                           result.EngineId = orderToEditDto.EngineId;
                           result.ColorId = orderToEditDto.ColorId;
                           result.RimId = orderToEditDto.RimId;

                           //load order equipment items

                           self.GetOrderEquipment(orderId, function (equipmentIds) {
                              if (equipmentIds == null) {
                                 self.ShowError();
                                 callback(null);
                                 return;
                              }
                              result.EquipmentIds = equipmentIds;
                              callback(result);

                           }); //get order equipment items

                        }); //get order


                     }); //get equipment

                  }); //get rims

               }); //get colors

            }); //get engines

         }); //get cars
      }
   }
};