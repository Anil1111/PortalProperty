/// <reference path="C:\Users\User\documents\visual studio 2015\Projects\PortalProperty\PortalProperty\Scripts/angular.js" />


var LoginService = angular.module('LoginService', []);

LoginService.factory('LoginApi', function ($http) {
    
    //var urlBase = "http://localhost:2831/api/";
    //var LoginApi = {};

    //LoginApi.GetUser= function (use) {
    //    return $http.post(urlBase + '/tblUsers/', use);
    //}
    //return LoginApi;
    var LoginApi = {};

    LoginApi.GetUserInfo = function (use) {
        debugger;
        return $http({
            url: '/index/login.html',
            method: 'POST',
            data: JSON.stringify(use),
            headers: save
        });
    }
    return LoginApi;
});
