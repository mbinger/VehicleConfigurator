var ViewModel = function(orderId) {
    var restServiceClient = new VehicleConfigurator.RestServiceClient();

    var page1Visible = true;
    var page2Visible = false;
    var page3Visible = false;
    var editMode = false;

    if (orderId.length > 0) {
        page1Visible = false;
        page3Visible = true;
        editMode = true;
    }

    this.apply = function() {
        restServiceClient.LoadData(orderId, function(orderToEdit) {
            if (orderToEdit == null) {
                $("#div-loading").hide();
                return;
            }

            //Init knockout.js viewmodel
            function AppViewModel() {
                var self = this;

                //order id
                this.OrderId = ko.observable(orderToEdit.OrderId);

                //the checkout buttons (return, checkout) on the page-3 are enabled
                this.CheckoutButtonsEnabled = ko.observable(true);

                this.CustomerName = ko.observable(orderToEdit.CustomerName);
                this.CarId = ko.observable(orderToEdit.CarId);
                this.EngineId = ko.observable(orderToEdit.EngineId);
                this.ColorId = ko.observable(orderToEdit.ColorId);
                this.RimId = ko.observable(orderToEdit.RimId);
                this.EquipmentIds = ko.observableArray(orderToEdit.EquipmentIds);
                this.Cars = orderToEdit.Cars;
                this.Engines = orderToEdit.Engines;
                this.Rims = orderToEdit.Rims;
                this.Colors = orderToEdit.Colors;
                this.EquipmentItems = orderToEdit.EquipmentItems;

                //is a "image-radio element" checked for certain radio group
                this.isRadioChecked = function(id, radioGroupName) {
                    return ko.computed(function() {
                            switch (radioGroupName) {
                            case window.vcfg.Placeholder.RadioGroupCars:
                                return self.CarId() == id;

                            case window.vcfg.Placeholder.RadioGroupEngines:
                                return self.EngineId() == id;

                            case window.vcfg.Placeholder.RadioGroupColors:
                                return self.ColorId() == id;

                            case window.vcfg.Placeholder.RadioGroupRims:
                                return self.RimId() == id;

                            case window.vcfg.Placeholder.RadioGroupEq:
                                var arr = self.EquipmentIds();
                                for (let i = 0; i < arr.length; i++) {
                                    if (arr[i] == id) {
                                        return true;
                                    }
                                }
                            }
                            return false;
                        },
                        this);
                };

                //setup pages visiblity
                this.page1Visible = ko.observable(page1Visible);
                this.page2Visible = ko.observable(page2Visible);
                this.page3Visible = ko.observable(page3Visible);

                //false - create new order mode, true - edit existing order mode
                this.editMode = editMode;

                //check if the create buttons (return, checkout) are visible on the page-3: the control is in "create" mode and page 3 is active
                this.createButtonsVisible = ko.computed(function() {
                        return !self.editMode && self.page3Visible();
                    },
                    this);

                //check if the edit buttons (return, save) are visible on the page-3: the control is in "edit" mode and page 3 is active
                this.editButtonsVisible = ko.computed(function() {
                        return self.editMode && self.page3Visible();
                    },
                    this);

                //button "next" on page-1 is clicked
                this.btnNext1Click = function() {
                    Tools.hideWarning();
                    //validate input
                    if (this.CarId() <= 0) {
                        Tools.showWarning(window.Plain.RequiredCar);
                        return;
                    }
                    if (this.EngineId() <= 0) {
                        Tools.showWarning(window.Plain.RequiredEngine);
                        return;
                    }
                    if (this.RimId() <= 0) {
                        Tools.showWarning(window.Plain.RequiredRim);
                        return;
                    }
                    if (this.ColorId() <= 0) {
                        Tools.showWarning(window.Plain.RequiredColor);
                        return;
                    }

                    //switch to page-2
                    this.page1Visible(false);
                    this.page2Visible(true);
                };

                //button "next" on page-2 is clicked
                this.btnNext2Click = function() {
                    Tools.hideWarning();

                    //validate input
                    if (this.CustomerName().length <= 0) {
                        Tools.showWarning(window.Plain.RequiredCustomerName);
                        return;
                    }

                    //switch to page-3
                    this.page2Visible(false);
                    this.page3Visible(true);
                };

                //button "return" on the page-2 clicked, return to page-1
                this.btnReturn1Click = function() {
                    this.page2Visible(false);
                    this.page1Visible(true);
                };

                //button "return" on the page-3 clicked, return to page-2
                this.btnReturn2Click = function() {
                    this.page3Visible(false);
                    this.page2Visible(true);
                };

                //button "edit" on the page-3 in "edit" mode is clicked, return to page-1
                this.btnEditClick = function() {
                    this.page3Visible(false);
                    this.page1Visible(true);
                };

                //Make OrderDto from binded information
                this.MakeOrderDto = function() {
                    var orderDto = {
                        Id: self.OrderId(),
                        CustomerName: self.CustomerName(),
                        AdditionalEquipmentItems: [],
                        CarId: self.CarId(),
                        EngineId: self.EngineId(),
                        RimId: self.RimId(),
                        ColorId: self.ColorId()
                    };
                    return orderDto;
                }

                //Make OrderEquipmentDto from binded information
                this.MakeOrderEquipmentDto = function() {
                    var equipmentIds = [];
                    var equipment = self.currentEquiment();
                    for (let i = 0; i < equipment.length; i++) {
                        equipmentIds.push(equipment[i].Id);
                    }
                    return { EquipmentIds: equipmentIds };
                }

                //button "save" in the "edit" mode on page-3 is clicked
                this.btnSaveClick = function() {
                    Tools.hideError();
                    Tools.hideWarning();

                    //disable checkout buttons
                    self.CheckoutButtonsEnabled(false);

                    try {
                        //make OrderDto and OrderEquipmentDto
                        var orderDto = self.MakeOrderDto();
                        var orderEquipmentDto = self.MakeOrderEquipmentDto();

                        //call the service to edit order
                        restServiceClient.EditOrder(orderDto.Id, orderDto, orderEquipmentDto, function(success) {
                            self.CheckoutButtonsEnabled(true);

                            if (!success) {
                                Tools.showError(window.Plain.UnknownError);
                                return;
                            }
                            //navigate to success page
                            var url = window.vcfg.Url.OrderChanged + orderDto.Id;
                            location.href = url;
                        });

                    } catch (e) {
                        console.log(e);
                        Tools.showError(window.Plain.UnknownError);
                    }
                };

                //button "checkout" in the "create" mode on page-3 is clicked
                this.btnCheckoutClick = function() {
                    Tools.hideError();
                    Tools.hideWarning();
                    self.CheckoutButtonsEnabled(false);

                    try {

                        //make OrderDto and OrderEquipmentDto
                        var orderDto = self.MakeOrderDto();
                        var orderEquipmentDto = self.MakeOrderEquipmentDto();

                        //call the service to create order
                        restServiceClient.CreateOrder(orderDto, orderEquipmentDto, function(orderId) {
                            self.CheckoutButtonsEnabled(true);
                            if (orderId == null || orderId <= 0) {
                                Tools.showError(window.Plain.UnknownError);
                                return;
                            }
                            var url = window.vcfg.Url.OrderDone + orderId;
                            location.href = url;
                        });

                    } catch (e) {
                        console.log(e);
                        Tools.showError(window.Plain.UnknownError);
                    }
                };
            }

            //init the view model and load all reference books

            var appViewModel = new AppViewModel();
            var self = appViewModel;

            //get "current car" object from cars references
            appViewModel.currentCar = ko.computed(function() {
                    for (let i = 0; i < self.Cars.length; i++) {
                        if (self.CarId() == self.Cars[i].Id) {
                            return self.Cars[i];
                        }
                    }
                    return {
                        Name: "",
                        Price: 0
                    }
                },
                self);

            //get "current engine" object from engines references
            appViewModel.currentEngine = ko.computed(function() {
                    for (let i = 0; i < self.Engines.length; i++) {
                        if (self.EngineId() == self.Engines[i].Id) {
                            return self.Engines[i];
                        }
                    }
                    return {
                        Name: "",
                        Price: 0
                    }
                },
                self);

            //get "current rim" object from rims references
            appViewModel.currentRim = ko.computed(function() {
                    for (let i = 0; i < self.Rims.length; i++) {
                        if (self.RimId() == self.Rims[i].Id) {
                            return self.Rims[i];
                        }
                    }
                    return {
                        Name: "",
                        Price: 0
                    }
                },
                self);

            //get "current color" object from colors references
            appViewModel.currentColor = ko.computed(function() {
                    for (let i = 0; i < self.Colors.length; i++) {
                        if (self.ColorId() == self.Colors[i].Id) {
                            return self.Colors[i];
                        }
                    }
                    return {
                        Name: "",
                        Price: 0
                    }
                },
                self);

            //get array of "current equipment" objects from equipment  references
            appViewModel.currentEquiment = ko.computed(function() {
                    var equipment = [];

                    var equipmentIds = self.EquipmentIds();
                    for (let i = 0; i < self.EquipmentItems.length; i++) {
                        for (let j = 0; j < equipmentIds.length; j++) {
                            if (equipmentIds[j] == self.EquipmentItems[i].Id) {
                                equipment.push(self.EquipmentItems[i]);
                                break;
                            }
                        }
                    }

                    return equipment;
                },
                self);

            //get total price
            appViewModel.totalPrice = ko.computed(function() {
                    var equipmentPrice = 0;
                    var currentEquipment = self.currentEquiment();
                    for (let i = 0; i < currentEquipment.length; i++) {
                        equipmentPrice += currentEquipment[i].Price;
                    }

                    return self.currentCar().Price +
                        self.currentEngine().Price +
                        self.currentRim().Price +
                        self.currentColor().Price +
                        equipmentPrice;
                },
                self);

            //apply bindings
            ko.applyBindings(appViewModel);

            //show container and hide loadd indicator
            $("#div-loading").hide();
            $("#div-main").show();
        }); //LoadData
    }//apply
};
