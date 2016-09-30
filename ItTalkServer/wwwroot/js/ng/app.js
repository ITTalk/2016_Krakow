"use strict";

var app = angular.module("serverApp", ["SignalR"]);

app.run(["$http", function ($http) {

}]);

app.controller("MainController", ["$scope", "signalrHubFactory", "$http", "$interval", function($scope, signalrHubFactory, $http, $interval) {
    var self = this;
    var statuses = {
        notRegistered: "Not registered",
        registered: "Registered"   
    };
    var numberOfNodes = 8;

    self.$isFetchingData = false;

    $scope.statuses = statuses;

    self.getNumberOfNodes = function () {
        return numberOfNodes;
    }

    self.nodes = [];

    var getNode = function(id){
        return self.nodes.filter(function(item){return item.id == id})[0];
    }

    var init = function() {
        for(var i = 1; i <= numberOfNodes; ++i)
            self.nodes.push({id: i, status: statuses.notRegistered, image: ''});

        // fetch the existing data
        var url = "/api";
        self.$isFetchingData = true;
        $http.get(url).then(function(response) {
            var data = response.data;
            if(data.length > 0){
                angular.forEach(data, function(item){
                        var node = getNode(item.id);
                        node.status = statuses.registered;
                        node.image = item.image;
                        node.registeredAt = item.registeredAt;
                });
            }
            self.$isFetchingData = false;
        });    
    }


    $scope.$on("clientRegistration", function (event, data) {
        var node = getNode(data.Id);
        node.status = statuses.registered;
        node.image = data.Image;
        node.registeredAt = data.RegisteredAt;
    })

    init();
}]);

app.factory("signalrHubFactory", [
    "$rootScope", "Hub", function ($rootScope, hub) {
        var dealImportProgressHub = new hub("itTalkHub", {
            rootPath: '/ittalk16/hubs',
            listeners: {
                'ClientRegistration': function (response) {
                    $rootScope.$broadcast("clientRegistration", response);
                    console.log(response);
                    $rootScope.$apply();
                },
                'SayHello': function (response) {
                    console.log(response);
                }
            },
            methods: [],
            errorHandler: function(error) {
                console.error(error);
            },
            stateChanged: function (state) {
                switch (state.newState) {
                    case $.signalR.connectionState.connecting:
                        console.debug('SignalR connecting...');
                        break;
                    case $.signalR.connectionState.connected:
                        console.debug('SignalR connection established.');
                        break;
                    case $.signalR.connectionState.reconnecting:
                        console.debug('SignalR trying to reconnect...');
                        break;
                    case $.signalR.connectionState.disconnected:
                        console.debug('SignalR disconnected.');
                        break;
                }
            }
        });

        return dealImportProgressHub;
    }
]);