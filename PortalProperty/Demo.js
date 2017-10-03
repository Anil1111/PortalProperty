/// <reference path="F:\PortalProperty\PortalProperty\Scripts/angular.js" />


var SignIn = angular.module("SignIn", ['ngRoute','UserService']);

SignIn.config(['$routeProvider', function ($routeProvider) {
    $routeProvider.
        when('/Home', {
            templateUrl: 'view/Home.html',
            controller: 'HomeController'
        }).
    when('/Add', {
        templateUrl: 'view/Add.html',
        controller: 'UserController'
    }).
    when('/Login', {
        templateUrl: 'view/Login.html',
        controller: 'LoginController'
    }).
     when('/Profile', {
         templateUrl: 'view/Profile.html',
         controller:'EditUserController'
     }).
       when('/AddProperty', {
           templateUrl: 'view/AddProperty.html',
           controller: 'AddPropController'
       }).
        when('/UploadImage', {
           templateurl: 'view/UploadImage.html',
           controller:'ImageController'
        }).
        when('/PropInfo', {
            templateurl: 'view/PropertyInfo.html',
            controller:'PropertyInfoController'
        }).
        when('/ContactUs', {
            templateUrl: 'view/ContactUs.html',
            controller:'MailController'
        }).
        when('/AdminLogin', {
            templateUrl: 'view/AdminLogin.html',
            controller:'AdminController'
        }).
        when('/DashBoard', {
            templateurl: 'view/DashBoard.html',
            controller:'DashController'
        }).
        otherwise({
        redirectTo: '/Home'
    });
}]);



SignIn.directive('ngFiles', ['$parse', function ($parse) {
    function fn_link(scope, element, attrs) {

        var Change = $parse(attrs.ngFiles);

        element.on('change', function (event) {
            Change(scope, { $files: event.target.files });
        })
    }
    return {
        link: fn_link
    }
}])



SignIn.controller("HomeController", function ($scope, UserApi, $rootScope) {
  
    getPropAndImage();
    function getPropAndImage() {
        UserApi.getPropInfo().then(function (response) {
            alert(response.data[0].image);
                  $scope.tblProperties = response.data;
                }), function () {
                    alert("Couldn't get Properties and images")
                }
    }
});



SignIn.controller("UserController", function ($scope, UserApi,$location) {
        $scope.AdUser = function () {
            var UserToAdd = {
                'firstname': $scope.firstName,
                'lastname': $scope.lastName,
                'email': $scope.Email,
                'password': $scope.Password
            };

            UserApi.AddUser(UserToAdd).then(function (reponse) {
                alert("User added successfully");
                $scope.firstname = undefined;
                $scope.lastname = undefined;
                $scope.email = undefined;
                $scope.password = undefined;
                $location.path('/AddProperty');
            }),
            function (response) {
                alert("Unable to add user");
            }
        }
});



SignIn.controller("LoginController", function ($scope, $http, UserApi, $rootScope, $location, $window) {

    $scope.LogInUser = {
        'email': $scope.email,
        'password': $scope.password,
        'firstname':$scope.firstname
    }

    $scope.loginForm = function () {
        UserApi.GetUserInfo($scope.email, $scope.password).then(function (response ){
            if (response.data === null) {
                alert("You've entered an ivalid email and password");
            } else {
                alert("Login Successful. Welcome " + response.data.firstname);
                $rootScope.currentUser = response.data;
                $location.path('/AddProperty');
            }
        }), function (response) {
            alert("Error in logging the system");
        }
    };
});



SignIn.controller("EditUserController", function ($scope, UserApi, $location, $rootScope) {

    GetUsers();
    function GetUsers() {
        UserApi.getUserss().then(function (response) {
            $scope.users = response.data;
        }), function () {
            alert("Couldn't get all the users information");
        }
    }

    $scope.Id = $rootScope.currentUser.Id;
    $scope.firstname = $rootScope.currentUser.firstname;
    $scope.lastname = $rootScope.currentUser.lastname;
    $scope.email = $rootScope.currentUser.email;
    $scope.password = $rootScope.currentUser.password;
    $scope.contact = $rootScope.currentUser.contact;
    $scope.gender = $rootScope.currentUser.gender;
    
    $scope.EditUser = function () {
        var UserToEdit = {
            'Id': $scope.Id,
            'firstname': $scope.firstname,
            'lastname': $scope.lastname,
            'email': $scope.email,
            'password': $scope.password,
            'contact': $scope.contact,
            'gender': $scope.gender
        };

        UserApi.EditUse(UserToEdit).then(function (reponse) {
            alert("User profile edited successfully");
            $scope.Id = undefined;
            $scope.firstname = undefined;
            $scope.lastname = undefined;
            $scope.email = undefined;
            $scope.password = undefined;
            $scope.contact = undefined;
            $scope.gender = undefined;
            GetUsers();
            $location.path('/AddProperty');
        }),
           function (response) {
               alert("Unable to edit user profile");
           }
    }
});
 


SignIn.controller("AddPropController", function ($scope, $location,UserApi) {
    $scope.AddProp = function () {
        var PropToAdd = {
            'Prop_Desc': $scope.Prop_Desc,
            'Prop_Type': $scope.Prop_Type,
            'Prop_Stat': $scope.Prop_Stat,
            'BedRoom': $scope.BedRoom,
            'BathRoom': $scope.BathRoom,
            'Garage': $scope.Garage,
            'Price': $scope.Price,
            'Cities': $scope.Cities,
            'Address': $scope.Address
        };

        UserApi.AddProperty(PropToAdd).then(function (reponse) {
            alert("Property added successfully");
            $scope.Prop_Desc = undefined;
            $scope.Prop_Type = undefined;
            $scope.Prop_Stat = undefined;
            $scope.BedRoom = undefined;
            $scope.BathRoom = undefined;
            $scope.Garage = undefined;
            $scope.Price = undefined;
            $scope.Cities = undefined;
            $location.path('/UploadImage');
        }),
        function (response) {
            alert("Unable to add user");
        }
    }
});



SignIn.controller("ImageController", function ($scope,$location,UserApi,$http) {
    var formdata = new FormData();

    $scope.GetImages = function ($files) {
        $scope.imagesrc = [];

        for (var g = 0; g < $files.length; g++) {
            var reader = new FileReader();
            reader.Filename = $files[g].name;

            reader.onload = function (event) {
                var image = {};
                image.Name = event.target.Filename;
                image.Size = (event.total / 1024).toFixed(2);
                image.Src = event.target.result;
                $scope.imagesrc.push(image);
                $scope.$apply();
            }
            reader.readAsDataURL($files[g]);
        };
        angular.forEach($files, function (value, key) {
            formdata.append(key, value);
        })

    };
    $scope.loadup = function () {
        var reqs = {
            method: 'POST',
            url: 'http://localhost:2831/api/tblGalleries',
            data: formdata,
            headers: {
                'Content-Type': undefined
            }
        }

        $http(reqs).then(function (gm) {
            alert("Image saved successfully" + gm);
            $scope.reset();
        }), function () {
            alert("Failed to upload image");
            $scope.reset();
        }
    }

    $scope.reset = function () {
        angular.forEach(
            angular.element("Input [Type = 'file']"),
        function (inElem) {
            angular.element(inElem).val(null);
        }
            );
        $scope.imagesrc = [];
        formdata = new FormData();
    }
})



SignIn.controller("PropertyInfoController", function ($scope, $rootScope, $location) {

    GetProper();
    function GetProper() {
        UserApi.getProperty().then(function (response) {
            $scope.property = response.data;
        }), function () {
            alert("Couldn't get all the Property information");
        }
    }

    $scope.PropID = $rootScope.currentUser.PropID;
    $scope.Prop_Desc = $rootScope.currentUser.Prop_Desc;
    $scope.Prop_Type = $rootScope.currentUser.Prop_Type;
    $scope.Prop_Stat = $rootScope.currentUser.Prop_Stat;
    $scope.BedRoom = $rootScope.currentUser.BedRoom;
    $scope.BathRoom = $rootScope.currentUser.BathRoom;
    $scope.Garage = $rootScope.currentUser.Garage;
    $scope.Price = $rootScope.currentUser.Price;
    $scope.Cities = $rootScope.currentUser.Cities;
    $scope.Address = $rootScope.currentUser.Address;
   

    $scope.EditProp = function () {
        var PropToEdit = {
            'PropId': $scope.PropID,
            'Prop_Desc': $scope.Prop_Desc,
            'Prop_Type': $scope.Prop_Type,
            'Prop_Stat': $scope.Prop_Stat,
            'BedRoom': $scope.BedRoom,
            'BathRoom': $scope.BathRoom,
            'Garage': $scope.Garage,
            'Price': $scope.Price,
            'Cities': $scope.Cities,
            'Address': $scope.Address
        }

        UserApi.EditProp(UserToEdit).then(function (response) {
            alert(" have updated your profile successfully");
            $scope.Prop_Desc = undefined;
            $scope.Prop_Type = undefined;
            $scope.Prop_Stat = undefined;
            $scope.BedRoom = undefined;
            $scope.BathRoom = undefined;
            $scope.Garage = undefined;
            $scope.Price = undefined;
            $scope.Cities = undefined;
            $scope.Address = undefined;
            GetProper();
            $location.path('/Home');
        }), function () {
            alert("Property information was not updated");
        }
    }
})


SignIn.controller("MailController", function ($scope, $location, UserApi) {

    $scope.SendMails = function () {
        var sendMail = {
            'sender':$scope.sender,
            'sEmail':$scope.sEmail,
            'senderContact':$scope.senderContact,
            'messages':$scope.messages
        };
           
        UserApi.SendMail(sendMail).then(function (response) {
            $scope.errors = response.data;
            alert("Email Sent");
            $scope.sender = undefined;
            $scope.sEmail = undefined;
            $scope.senderContact = undefined;
            $scope.messages = undefined;
            $location.path('/Home');
        }),
            function (error) {
                $scope.errors = error.data;
                alert("Message failed to send, try again");
            }
        ;
    };
})



SignIn.controller("AdminController", function ($scope, $location, UserApi, $rootScope) {
    
    var logs = {
        'UserName': $scope.username,
        'Password': $scope.passwords
    }

        $scope.AdLog = function () {
        UserApi.LoginAd($scope.username,$scope.passwords).then(function (response) {
            if (response.data === null) {
                alert("You have entered the wrong password and username");
            }
            else {
                alert("Welcome " + response.data.username);
                $rootScope.currentUser = response.data;
                $location.path('/DashBoard');
            }
        }), function () {
            alert("Error logging");
        }
    }


})

