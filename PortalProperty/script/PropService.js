/// <reference path="D:\CSharp\PortalProperty\PortalProperty\Scripts/angular.js" />
/// <reference path="D:\CSharp\PortalProperty\PortalProperty\Scripts/angular.min.js" />

var PropService = angular.module('PropService', []);

PropService.factory('PropApi', function ($http) {
    var urlBase = "http://localhost:2831/api/";
    var PropApi = {};
    //register

  //AddProperty
   PropApi.AddProperty = function (prop) {
        return $http.post(urlBase + 'tblProperties/',prop);
    }

    //Update
    
    //Add Property
    return PropApi;
});