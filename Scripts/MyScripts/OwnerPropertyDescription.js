/// <reference path="../jquery-2.1.1.min.js" />
/// <reference path="../knockout-3.2.0.js" />

(function () {

    var OwnerPropertyDescriptionViewModel = function () {
        var self = this;

        var data = GlobalDeclaration.UserName(); //Get the Current LogIn User

        self.OwnerPropertyId = ko.observable(0); //For OwnerPropertyId
        self.OwnerId = ko.observable(0); //For OwnerId
        self.OwnerName = ko.observable(""); //For Owner Name
        self.Address = ko.observable(""); //For Address
        self.BedRoomNo = ko.observable(0); //For No. of Bedrooms
        self.TotalRooms = ko.observable(0); //For Total Rooms
        self.PropertyBuildupArea = ko.observable(0);//For the area
        self.PropertyDescription = ko.observable(); //For Property Description
        self.PropertySaleRentStatus = ko.observable(); //For the Status as Rent or Sale
        self.SaleOrRentCost = ko.observable(); //The Cost for Sale to Rent
        self.PropertyAge = ko.observable(); //The Age of the property
        self.Status = ko.observable(); //The Status of the Property as 'Available'
        self.RegistrationDate = ko.observable(); //For the Registration Date

        self.PropertyType = ko.observable(); //The Property Type (Flat, Bunglow, RowHouse,etc)

        self.ErrorMesage = ko.observable("");

        self.PropertyTypes = ko.observableArray([
            "Flat", "Bunglow","Row House"
        ]); //Array for Property Types

        self.SelectedProperty = ko.observable();

        //Flag for Selection of Property and Status
        var propertySelected =0, saleORrentStatusSelected =0;

        //This will provide the selected property type id
        self.SelectedProperty.subscribe(function (val) {
            if(val!=='undefined'){
            propertySelected = 1;
            self.PropertyType(val);
            }
        });


        //The Array used to Store properties by owner
        self.PropertyByOwner = ko.observableArray([]); //Array for Property Owners

        //Array for Sale or Rent
        self.PropertySalesOrRentStatusConstant = ko.observableArray([
            "Sale","Rent"
        ]);

        //An observable
        self.SelectedStatus = ko.observable();

        //This will decide whether the property is for Rent or sales
        self.SelectedStatus.subscribe(function (text) {
            if (text !== 'undefined') {
                saleORrentStatusSelected=1
                self.PropertySaleRentStatus(text);
            }
        });

        
        //The Owner Description Object
        var OwnerPropertyDescription = {
            OwnerPropertyId: self.OwnerPropertyId,
            PropertyType: self.PropertyType,
            OwnerId: self.OwnerId,
            Address: self.Address,
            BedRoomNo: self.BedRoomNo,
            TotalRooms: self.TotalRooms,
            PropertyBuildupArea: self.PropertyBuildupArea,
            PropertyDescription: self.PropertyDescription,
            PropertySaleRentStatus: self.PropertySaleRentStatus,
            SaleOrRentCost: self.SaleOrRentCost,
            PropertyAge: self.PropertyAge,
            Status:self.Status
        };
       

        //Function call to load all propeties by Owner
        loadPropertiesbyowner();
       


       //Function to load all properties by owner 
        function loadPropertiesbyowner() {
                 $.ajax({
                     url: "/api/OwnerPropertyDescriptionAPI",
                    type: "GET"
                }).done(function (resp) {
                    self.PropertyByOwner(resp);
                }).error(function (err) {
                    self.ErrorMesage("Error " + err.status);
                });
        }

        //Save the Description. Saves the Property description if the 
        //Property tyoe and SaleRent Status is selected
        self.save = function () {
            if (propertySelected === 1 && saleORrentStatusSelected === 1)
                {
                    self.Status("Available");
                    $.ajax({
                        url: "/api/OwnerPropertyDescriptionAPI",
                        type: "POST",
                        data: OwnerPropertyDescription,
                        datatype: "json",
                        contenttype:"application/json;utf-8"
                    }).done(function (resp) {
                        self.OwnerPropertyId(resp.OwnerPropertyId);
                        loadPropertiesbyowner();
                    }).error(function (err) {
                        self.ErrorMesage("Error " + err.status);
                    });
               }
            propertySelected = 0;
            saleORrentStatusSelected = 0;
        }

        //Clear Inpit Elements
        self.cancel = function () {
            self.OwnerPropertyId(0);
            self.PropertyType("");
            self.Address("");
            self.BedRoomNo(0);
            self.TotalRooms(0);
            self.PropertyBuildupArea(0);
            self.PropertyDescription("");
            self.PropertySaleRentStatus("Choose Sale or Rent");
            self.SaleOrRentCost(0);
            self.PropertyAge(0);
            self.Status("")
        }

        getOwnerIdUsingEmail();
        //Function to Get the OwnerId using Email
        function getOwnerIdUsingEmail() {
            $.ajax({
                url: "/Owner/Get",
                type:"GET"
            }).done(function (resp) {
                self.OwnerId(resp.OwnerId);
                self.OwnerName(resp.OwnerName);
            }).error(function (err) {
                self.ErrorMesage(err.status);
            });
        }

    };
    ko.applyBindings(new OwnerPropertyDescriptionViewModel());

})();