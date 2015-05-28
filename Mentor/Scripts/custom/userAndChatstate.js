

var userAndChatstate = {}

userAndChatstate.ProgramMessage = function (message, sender, date) {
    var self = this;
    self.Message = message;
    self.FullName = sender;
    if (date != null) {
        self.Date = date;
    }
};


userAndChatstate.User = function (id, firstName, lastName) {
    var self = this;
    self.id = id;
    self.firstName = firstName;
    self.lastName = lastName;
};


userAndChatstate.ChatViewModel = function () {
    var self = this;
    self.messages = ko.observableArray();
}


userAndChatstate.UsersViewModel = function () {
    var self = this;
    self.users = ko.observableArray();
    self.customRemove = function (userToRemove) {
        var userIdToRemove = userToRemove.id;
        self.users.remove(function (item) {
            return item.id === userIdToRemove;
        });
    }
}
