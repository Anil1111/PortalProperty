/// <reference path="D:\CSharp\PortalProperty\PortalProperty\Scripts/angular.min.js" />
/// <reference path="D:\CSharp\PortalProperty\PortalProperty\Scripts/angular.js" />


var SignIn = angular.module("SignIn", ['UserService','ngRoute']);

SignIn.config(['$routeProvider', function ($routeProvider) {
    $routeProvider.when('/Home', {
        templateUrl: 'view/Home.html',
        controller: 'HomeController'
    }).when('/Add', {
        templateUrl: 'view/Add.html',
        controller: 'UserController'
    }).when('/Login', {
        templateUrl: 'view/Login.html',
        controller: 'LoginController'
    }).when('/Profile', {
        templateUrl: 'view/Profile.html',
        controller: 'EditController'
    }).otherwise({
        redirectTo: '/Home'
    });
}]);

//tblUser/register
SignIn.controller("UserController", function ($scope, UserApi, $location) {
    $scope.isRegister = false;
        $scope.AdUser = function () {
            var UserToAdd = {
                'firstname': $scope.firstName,
                'lastname': $scope.lastName,
                'email': $scope.Email,
                'password': $scope.Password
            };

            UserApi.AddUser(UserToAdd).then(function () {
                $scope.isRegister = true;
                alert("User added successfully");
                $scope.firstname = undefined;
                $scope.lastname = undefined;
                $scope.email = undefined;
                $scope.password = undefined;
                $location.path("/Home");
                
            }),
            function (response) {
                alert("Unable to add user");
            }
        }
    
});

//tblUser/Home
SignIn.controller("HomeController", function ($scope, UserApi,$location) {
    var log = this;
    log.login = login;
    function init() {

    }
    init();
    
});

//tblUser/login
SignIn.controller('LoginController',['$rootScope'], function ($scope, UserApi,$http,$location,$rootScope) {
    $scope.cust = {};
    $scope.isLoggedIn = false;

    function init() {
    }
    init();

    function login(user) {
        if (!user) {
            log.errorMessage = "User is not found";
        }
        user = UserApi.GetUser(user.Email, user.Password)
        if (user == null) {

            log.errorMessage = "User is not found";
        }
        else {
            if (user.email != null && user.password != null) {
                alert("Welcome " + $scope.email);
                $location.path("/Home");
                $rootScope.currentUser = user;
                $location.path('/profile.html/' + user.id)
            }
        }
    }


    //$scope.login = function () {
    //    var logd = {
    //        'email': $scope.email,
    //        'password': $scope.password
    //    };
    
    //GetInfo();
    //function GetInfo() {
    //    UserApi.GetInfoUser($scope.email, $scope.password).then(function (data) {
    //        $scope.cust = data;
    //        if (data != null) {
    //            alert("Welcome " + $scope.email);
    //            $location.path("/Profile");
    //        }
    //    }), function (response) {
    //        alert("Wrong login details");
    //    }
    //}
    //}
});



//tblUser/update
SignIn.controller('EditController', function ($scope, UserApi) {
    $scope.GetUserByID = function (model) {
        UserApi.Users().then(function (response) {
            $scope.Userid = response.data.Userid;
            $scope.firstname = response.data.firstname;
            $scope.lastname = response.data.lastname;
            $scope.email = response.data.email;
            $scope.password = response.data.password;
        })
    }
   
    $scope.Prof = {};
    $scope.EditUser = function () {
        var UserToEdit = {
            'id':$scope.id,
            'firstname': $scope.firstName,
            'lastname': $scope.lastName,
            'email': $scope.Email,
            'password': $scope.Password
        };

        SaveUserDet();
        function SaveUserDet() {
            UserApi.UserEdit().then(function (resp) {
                $scope.isRegister = true;
                $scope.Prof = resp;

                if (resp.data != " ") {
                    alert("User information changed successfully");
                    $scope.firstname = undefined;
                    $scope.lastname = undefined;
                    $scope.email = undefined;
                    $scope.password = undefined;
                    $location.path("/Home");
                    $scope.GetUserByID();
                }
            }), function (response) {
                alert("Unable to edit user information");
            }
        }
    }
 });

    
    
 