/// <reference path="D:\CSharp\PortalProperty\PortalProperty\Scripts/angular.js" />
/// <reference path="D:\CSharp\PortalProperty\PortalProperty\Scripts/angular.min.js" />

var UserService = angular.module('UserService', []);


UserService.factory('UserApi', function ($http) {
    var urlBase = "http://localhost:2831/api/";
    var UserApi = {};
   //register
    UserApi.AddUser = function (user) {
        return $http.post(urlBase + 'tblUsers/', user);
    }

    //Login
    UserApi.GetUser = function (Email, Password) {
        return $http.get(urlBase + 'tblUsers/?email=' + Email + '&password=' + Password);
    }

    //Update
    UserApi.UserEdit = function () {
        return $http.put(urlBase + 'tblUsers/');
    }
    return UserApi;
});


UserService.factory('SearchApi', function ($http) {
    var urlBase = "http://localhost:2831/api/";

    SearchApi.searchLoc = function () {
        return $http.get(urlBase + '/tblLocation/?Province=' + province + '&Surburb=' + surburb);
    }
})

UserService.factory('GalApi', function ($http) {
   

    
})








