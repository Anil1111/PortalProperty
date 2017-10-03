/// <reference path="F:\PortalProperty\PortalProperty\Scripts/angular.js" />


var UserService = angular.module('UserService', []);

UserService.factory('UserApi', function ($http) {
    var urlBase = "http://localhost:2831/api/";
    var UserApi = {};


    //############### USERS #####################
    //Get all users
    UserApi.getUserss = function () {
        return $http.get(urlBase + 'tblUsers');
    }

    //RegisterUser
    UserApi.AddUser = function (user) {
        return $http.post(urlBase + '/tblUsers/', user);
    }


    //LoginUser
     UserApi.GetUserInfo = function (Email, Password) {
        return $http.get(urlBase + 'tblUsers?email=' + Email + '&password='+ Password);
    }
    

    //update user details
     UserApi.EditUse = function (UserToEdit) {
         var datta = $http({
             method: 'PUT',
             url: urlBase + 'tblUsers/' + UserToEdit.Id,
             data: UserToEdit,
             
         });
         return datta;
     }




    //#################### Properties ####################
    //Get Properties
     UserApi.getProperty = function () {
         return $http.get(urlBase + 'tblProperties')
     }

    //AddProperty
     UserApi.AddProperty = function (PropToAdd) {
         return $http.post(urlBase + '/tblProperties/', PropToAdd);
     }



    //Get Property Information
       UserApi.EditProp = function (PropToEdit) {
           var datt = $http({
               method: 'PUT',
               url: urlBase + 'tblProperties?Prop_id=' + PropToEdit.Prop_id,
               data: PropToEdit
           });
           return datt;
       }
   


    //Get Prop
     UserApi.getPropInfo = function () {
         return $http.get(urlBase + '/GetProp');
     };




    //################### Send Email ######################
    //Send email8
     UserApi.SendMail = function (sendMail) {
         return $http.post(urlBase + 'Emails', sendMail);
    }
     


    //################### Admin ####################
     UserApi.LoginAd = function (username,password) {
         return $http.get(urlBase + 'tblAdmins?UserName=' + username + '&Password=' + password);
     }

     return UserApi;
});


