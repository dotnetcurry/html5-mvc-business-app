/// <reference path="../jquery-2.1.1.min.js" />
/// <reference path="../knockout-3.2.0.js" />

(function () {
    var CustomerViewModel = function () {
        var self = this;

        var data = GlobalDeclarationCust.UserName();


        getCustomerByEmail();

        self.CustomerId = ko.observable(0); //For Customer Id
        self.CustomerName = ko.observable(""); //For Customer Name
        self.Address = ko.observable(""); //For Address
        self.City = ko.observable(""); //For City
        self.Email = ko.observable(""); //For Email
        self.Contact1 = ko.observable(""); //The Field for Primary Contact
        self.Contact2 = ko.observable("");//The Field for Secondary Contact

        self.Email(data);

        self.ErrorMessage = ko.observable("");

        var Customer = {
            CustomerId: self.CustomerId,
            CustomerName: self.CustomerName,
            Address: self.Address,
            City: self.City,
            Email: self.Email,
            Contact1: self.Contact1,
            Contact2: self.Contact2
        };

        //Function to get Customer info based upon the Login Email 
        function getCustomerByEmail() {
            $.ajax({
                url: "/Customer/Get",
                type:"GET"
            }).done(function (resp) {
                if (resp.CustomerId !== 0) {
                    self.CustomerId(resp.CustomerId);
                    self.CustomerName(resp.CustomerName);
                    self.Address(resp.Address);
                    self.City(resp.City);
                    self.Contact1(resp.Contact1);
                    self.Contact2(resp.Contact2);
                    self.Email(resp.Email);
                }
            }).error(function (err) {
                self.ErrorMessage("Error!!!" + err.status);
            });
        }
        //Save the Customer
        self.save = function () {

           // alert("Data" + ko.toJSON(Customer));
            $.ajax({
                url: "/api/CustomerInfoAPI",
                type: "POST",
                data: Customer,
                datatype: "json",
                contenttype:"application/json;utf-8"
            }).done(function (resp) {
                self.CustomerId(resp.CustomerId);
            }).error(function (err) {
                self.ErrorMessage("Error " + err.status);
            });
        };

        self.clear = function () {
            self.CustomerId(0);
            self.CustomerName("");
            self.Address("");
            self.City("");
            self.Email("");
            self.Contact1("");
            self.Contact2("");
        }
    };
    ko.applyBindings(new CustomerViewModel());
})();