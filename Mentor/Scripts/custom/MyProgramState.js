/**
    A class that contains  application state. part of the KnockoutJS implementation in 
    MyPrograms.cshtml.
*/


var MyProgramState = {}

MyProgramState.Program = function (id, name) {
    var self = this;
    self.imageUrl = "/pictures/badtminton.jpg";
    self.programLink = "/programs/index/" + id;
    self.Name = name;
    self.ID = id;
};


MyProgramState.Programs = function () {
    var self = this;
    self.Programs = ko.observableArray();

};


MyProgramState.OurSurgestions = function () {
    var self = this;
    self.SurgestionPrograms = ko.observableArray();
};