angular.module('UserProfile', [])
    .controller("Controller", ['$scope', 'Model', function($scope, model) {
        $scope.AvailableBoards = model.AvailableBoards;
        $scope.AvailableCalendars = model.AvailableCalendars;
    }]);
