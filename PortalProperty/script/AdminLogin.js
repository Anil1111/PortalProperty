/// <reference path="D:\CSharp\PortalProperty\PortalProperty\Scripts/angular.js" />
/// <reference path="D:\CSharp\PortalProperty\PortalProperty\Scripts/angular.min.js" />

var SignIn = angular.module("SignIn", ['ngRoute','AdminService']);

SignIn.config(['$routeProvider', function ($routeProvider) {
    $routeProvider.when('/AdLogin', {
        templateUrl: 'view/AdLogin.html',
        controller: 'AdminController'
    }).when('/Dash', {
        templateUrl: 'view/Dash.html',
        controller: 'BoardController'
    }).otherwise({
        redirectTo: '/Home'
    });
}]);

SignIn.controller('AdminController', function ($scope,  $location) {
    $scope.cust = {};
    $scope.isLoggedIn = false;

    $scope.login = function () {
        var logd = {
            'email': $scope.email,
            'password': $scope.password
        };

        GetInfo();
        function GetInfo() {
            UserApi.GetInfoUser($scope.email, $scope.password).then(function (data) {
                $scope.cust = data;
                if (data != null) {
                    alert("Welcome " + $scope.email);
                    $location.path("/Dash");
                }
            }), function (response) {
                alert("Wrong login details");
            }
        }
    }
});