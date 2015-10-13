/// <reference path="../jquery-2.1.1.min.js" />
/// <reference path="../knockout-3.2.0.js" />


(function () {
    var OwnerViewModel = function () {
        var self = this;
        
         //This function is declared in the Index.cshtml of the Owner View
        var data =  GlobalDeclaration.UserName(); //Get the Current LogIn User

        getOwnerIdUsingEmail(); //Load the Ownerdetails if found

        self.OwnerId = ko.observable(0);  //The OwnerId
        self.OwnerName = ko.observable(""); //The Owner Name
        self.Address = ko.observable(""); //The Address
        self.City = ko.observable(""); //The City
        self.Email = ko.observable(""); //The Email

         
        self.Email(data); //Set the login Email to the Email Observable

        self.Contact1 = ko.observable(""); //The Field for Primary Contact
        self.Contact2 = ko.observable("");//The Field for Secondary Contact

        self.ErrorMessage = ko.observable("");


        //The Owner Object, this will be used for Adding Owner
        var Owner = {
            OwnerId: self.OwnerId,
            OwnerName: self.OwnerName,
            Address: self.Address,
            City: self.City,
            Email: self.Email,
            Contact1: self.Contact1,
            Contact2:self.Contact2
        };

        //The Function to Save Owner
        self.save = function () {
            $.ajax({
                url: "/api/OwnerInfoAPI",
                type: "POST",
                data: Owner,
                datatype: "json",
                contenttype:"application/json;utf-8"
            }).done(function (resp) {
                self.OwnerId(resp.OwnerId);
            }).error(function (err) {
                self.ErrorMessage("Error!  " + err.status);
            });
        }


        self.cancel = function () {
            self.OwnerId(0);
            self.OwnerName("");
            self.Address("");
            self.City("");
            self.Email("");
            self.Contact1("");
            self.Contact2("");
        }

        //The Function will decide whether to Register Properties
        self.canRegisterProperty = function () {
            var canRegister = false;
            if (self.OwnerId() > 0)
            {
                canRegister = true;
            }
            return canRegister;
        }

        //Function to Get the Owner Details based upon Email
        function getOwnerIdUsingEmail() {
            alert("Wel-Come " + data);
            $.ajax({
                url: "/Owner/Get",
                type: "GET"
            }).done(function (resp) {
                if (resp.OwnerId !== 0)
                {
                    self.OwnerId(resp.OwnerId);
                    self.OwnerName(resp.OwnerName);
                    self.Address(resp.Address);
                    self.City(resp.City);
                    self.Email(resp.Email);
                    self.Contact1(resp.Contact1);
                    self.Contact2(resp.Contact1);
                }
            }).error(function (err) {
                alert("Error Occured");
                self.ErrorMessage(err.status);
                alert(JSON.stringify(Owner));
            });
        }
    };
    ko.applyBindings(new OwnerViewModel());
})();