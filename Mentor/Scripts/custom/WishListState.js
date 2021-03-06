﻿/**
 * Author: matti
 */


var WishListState = {};


WishListState.WishListViewModel = function() {
    var self = this;
    self.Programs = ko.observableArray();
    self.SortedPrograms = ko.computed(function() {
        return self.Programs().sort(function (left, right) {
            return left.votes() === right.votes() ? 0 : (right.votes() < left.votes() ? -1 : 1);
        });
    });

}


WishListState.Program = function (program,wishListHub) {
    var self =this;
    self.id = program.Id;
    self.name = program.Name;
    self.reason = program.Reason;
    self.creator = program.Creator;
    self.interest = program.Interest;
    self.description = program.Description;
    self.acceptCriteria = program.AcceptCriteria;
    self.votes = ko.observable(0);
    self.voted = ko.observable(false);
    self.upvote = function(userId) {
        self.voted(true);
        console.log("fired a vote");
        var previousCount = self.votes();
        self.votes(previousCount + 1);
        /*wishListHub.server.vote(this, userId, true);*/

    };
   self.downvote = function (userId) {
        self.voted(true);
        console.log("fired a vote");
        var previousCount = self.votes();
        self.votes(previousCount - 1);
        /* wishListHub.server.vote(this, userId, false);*/
    };

}


