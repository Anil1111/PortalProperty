/// <reference path="D:\CSharp\PortalProperty\PortalProperty\Scripts/angular.js" />

var SignIn = angular.module('SignIn', ['ngRoute','UserService']);

SignIn.config(['$routeProvider', function ($routeProvider) {
   $routeProvider.when('/AddProperty', {
        templateUrl: 'view/AddProperty.html',
        controller: 'PropertyController'
    }).otherwise({
        redirectTo: '/Home'
    });
}]);

SignIn.controller("PropertyController", function () {
    $scope.cust = {};
    $scope.isLoggedIn = false;

    $scope.register = function () {
        var logd = {
            'PropType': $scope.PropType,
            'PropDescription': $scope.PropeDesc,
            'PropStatus': $scope.PropStatus,
            'Bthroom': $scope.Bathroom,
            'Bedroom': $scope.Bedroom
        };

        PropApi.AddProperty(logd).then(function () {
            alert("Property added successfully");
            $scope.PropType = undefined;
            $scope.PropeDesc = undefined;
            $scope.PropStatus = undefined;
            $scope.Bathroom = undefined;
            $scope.Bedroom = undefined;
            $location.path('/Home');
        }),
           function (response) {
               alert("Unable to add Property");
           }
    }
});

SignIn.controller("SearchController", function ($scope, UserApi) {
    $scope.searchByLocation = function () {
        selectLocation($scope.Province, $cope.Surburb)
    }
})