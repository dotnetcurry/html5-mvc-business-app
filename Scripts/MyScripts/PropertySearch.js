/// <reference path="../jquery-2.1.1.min.js" />
/// <reference path="../knockout-3.2.0.js" />

(function () {
    var searchViewModel = function () {

        var self = this;
        //Array for the Property type
        self.PropertyTypes = ko.observableArray(["Flat", "Bunglaow", "Row House"]);
        //Array for the FIlter condition
        self.Filters = ko.observableArray(["AND", "OR"]);
        //Array for the property Status
        self.PropertySaleRentStatus = ko.observableArray(["Rent","Sale"]);


        self.PropertyId = ko.observable(0); //For PropertyId
        self.OwnerName = ko.observable(""); //For OwnerName
        self.Contact1 = ko.observable(); //For Primary Contact
        self.Contact2 = ko.observable(); //For Secondary Contact
        self.Email = ko.observable(""); //For Email
        self.PropertyType = ko.observable(""); //For Property Type
        self.Address = ko.observable(""); //For Address
        self.BedRoomNo = ko.observable(0);//For No of Bedrooms 
        self.TotalRooms = ko.observable(0); //For Total Rooms
        self.BuildupArea = ko.observable(0); //For Area
        self.SaleRentStatus = ko.observable(""); //For Sale or Rent
        self.Status = ko.observable(""); //For Status like Available

        self.ErrorMessage = ko.observable("");

        //Flag for selection of the PropertyType and SaleRent Status

        var propertySelected = 0, saleORrentStatusSelected = 0;

        self.SearchedProperties = ko.observableArray();

        self.selectedPropertyType = ko.observable();

        self.selectedPropertyType.subscribe(function (val) {

            if (val !== 'undefines') {
                propertySelected = 1;
                self.PropertyType(val);
            }
        });

        self.selectedSaleRentStatus = ko.observable();

        self.selectedSaleRentStatus.subscribe(function (val) {
            if (val !== 'undefined') {
                saleORrentStatusSelected = 1;
                self.SaleRentStatus(val);
            }
        });

        self.Filter = ko.observable();
        self.SelectedFilter = ko.observable("");
        self.Filter.subscribe(function (val) {
            if (val !== 'undefined') {
                self.SelectedFilter(val);
            }
        });


        //Funtion to search properties based upon the Conditions
        self.SearchProperties =function(){
            if (propertySelected === 1 && saleORrentStatusSelected === 1)
            {

                //Get the Filter Values
                propertytype = self.PropertyType();
                filter = self.SelectedFilter();
                searchtype = self.SaleRentStatus();

                var url ="/Property/"+propertytype+"/" +filter+"/" +searchtype; 
                alert(url);

                $.ajax({
                    url: url,
                    type:"GET"
                }).done(function (resp) {
                    self.SearchedProperties(resp);
                }).error(function (err) {
                    self.ErrorMessage("Error!!!!" + err.status);
                });
            }
        }

    };
    ko.applyBindings(new searchViewModel());

})();