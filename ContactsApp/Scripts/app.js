var ViewModel = function () {
    var self = this;
    self.contacts = ko.observableArray();
    self.error = ko.observable();
    self.detail = ko.observable();
    self.selectedOption = ko.observable();
    self.newContact = {
        Id: ko.observable(),
        FirstName: ko.observable(),
        LastName: ko.observable(),
        Phone: ko.observable(),
        Email: ko.observable(),
        Status: ko.observable()
    }

    var contactsUri = '/api/Contacts/';

    function ajaxHelper(uri, method, data) {
        self.error(''); // Clear error message
        return $.ajax({
            type: method,
            url: uri,
            dataType: 'json',
            contentType: 'application/json',
            data: data ? JSON.stringify(data) : null
        }).fail(function (jqXHR, textStatus, errorThrown) {
            self.error(errorThrown);
        });
    }

    //Get All Contacts.
    function getAllContacts() {
        ajaxHelper(contactsUri, 'GET').done(function (data) {
            self.contacts(data);
        });
    }

    //Get Contact Details by Id
    self.getContactDetail = function (item) {
        ajaxHelper(contactsUri + item.Id, 'GET').done(function (data) {
            self.detail(data);
        });
    }

    //Get Contact Details by Id
    self.getContactDetailEdit = function (item) {
        ajaxHelper(contactsUri + item.Id, 'GET').done(function (data) {
            self.newContact.Id(data.Id);
            self.newContact.FirstName(data.FirstName);
            self.newContact.LastName(data.LastName);
            self.newContact.Email(data.Email);
            self.newContact.Phone(data.Phone);
            self.newContact.Status(data.Status);
            //self.selectedOption(data.Status);
            //$('#inputStatus').val(data.Status);
            document.getElementById("inputStatus").value = "" + data.Status + "";
            console.log("Status Value:" + data.Status);
            document.getElementById("h2title").textContent = "Update Contact";
        });

        
        $('#Save').hide();
        $('#Clear').hide();
        $('#Update').show();
        $('#Cancel').show();
    }

    //Add Contacts
    self.addContact = function (formElement) {
        var contact = {
            FirstName: self.newContact.FirstName(),
            LastName: self.newContact.LastName(),
            Phone: self.newContact.Phone(),
            Email: self.newContact.Email(),
            Status: self.newContact.Status()
        };

        ajaxHelper(contactsUri, 'POST', contact).done(function (item) {
            self.contacts.push(item);
            self.clearFields();
        });
    }

    //Update Contacts  
    self.updateContact = function () {  
        var contact = {
            Id: self.newContact.Id(),
            FirstName: self.newContact.FirstName(),
            LastName: self.newContact.LastName(),
            Phone: self.newContact.Phone(),
            Email: self.newContact.Email(),
            Status: self.newContact.Status()
        };
  
        ajaxHelper(contactsUri + self.newContact.Id(), 'PUT', contact).done(function () {
            getAllContacts();
            self.cancel();  
        });
    }

    //Chnage Status false
    self.changeStatus = function (item) {
        var newStat;
        if (item.Status == true)
            newStat = false;
        else
            newStat = true;
        var contact = {
            Id: item.Id,
            FirstName: item.FirstName,
            LastName: item.LastName,
            Phone: item.Phone,
            Email: item.Email,
            Status: newStat
        };

        ajaxHelper(contactsUri + item.Id, 'PUT', contact).done(function () {
            getAllContacts();
            self.cancel();
        });

    }

    //Delete Contacts
    self.deleteContact = function (item) {
        ajaxHelper(contactsUri + item.Id, 'DELETE').done(function (data) {
            getAllContacts();
        });
    }

    //Clear Fields
    self.clearFields = function clearFields() {
        self.newContact.Id();
        self.newContact.FirstName('');
        self.newContact.LastName('');
        self.newContact.Email('');
        self.newContact.Phone('');
    }

    //Cancel Button
    self.cancel = function () {
        self.clearFields();
        $('#Save').show();
        $('#Clear').show();
        $('#Update').hide();
        $('#Cancel').hide();
        document.getElementById("h2title").textContent = "Add Contact";
    }

    getAllContacts();
};

ko.applyBindings(new ViewModel());