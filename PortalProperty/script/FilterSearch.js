var SignIn = angular.module('SignIn', [])

SignIn.controller('FilterController', ['$scope', function ($scope) {
    var Place = [$]
    $scope.resetAll = function () {
        $scope.filteredList = $scope.allItems;
        $scope.Surburb = '';
        $scope.Province ='';
        $scope.searchText = '';
    }
    $scope.locs = [];

    $scope.add = function () {
        $scope.allPlaces.push({
            Surburb: $scope.Surburb,
            Province: $scope.Province,
        });
    $scope.resetAll();
    }


}]);

