/// <reference path="C:\Users\User\Documents\Visual Studio 2015\Projects\WebApplication11\WebApplication11\Scripts/angular.js" />

var Prop = angular.module('Prop', ['ngRoute']);

Prop.config(['$routeProvider', function ($routeProvider) {
    $routeProvider.when('/UploadImage', {
        templateUrl: 'ViewPage/UploadImage.html',
        controller: 'fileUpController'
    }).otherwise({
        redirestTo: '/Home'
    })
}])

Prop.directive('ngFiles', ['$parse', function ($parse) {
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

Prop.controller('fileUpController', function ($scope, $http, $location) {
    var fmdata = new FormData();

    $scope.GetImages = function ($files) {
        $scope.imagesrc = [];

        for (var g = 0; g < $files.length; g++) {
            var reader = new FileReader();
            reader.Filename = $files[g].name;

            reader.onload = function (event) {
                var image = {};
                image.name = event.target.Filename;
                image.size = (event.total / 1024).toFixed(2);
                image.Src = event.target.result;
                $scope.imagesrc.push(image);
                $scope.$apply();
            }
            reader.readAsDataURL($files[g]);
        };

        angular.forEach($files, function (value, key) {
            fmdata.append(key, value);
        })
    };

    $scope.loadup = function () {
        var reqs = {
            method: 'POST',
            url: '/api/tblGalleries',
            data: fmdata,
            headers: {
                'Content-Type': undefined,
            }
        };

        $http(reqs).then(function (gm) {
            alert("Image saved successfully" + gm);
            $scope.reset();
        }), function () {
            alert("Failed to upload image");
            $scope.reset();
        }

        $scope.reset = function () {
            angular.forEach(
                angular.element("Input [Type = 'file']"),
            function (inElem) {
                angular.element(inElem).val(null);
            }
                );
            $scope.imagesrc = [];
            fmdata = new FormData();
        }
    }

    $location.path("/home.html")
})