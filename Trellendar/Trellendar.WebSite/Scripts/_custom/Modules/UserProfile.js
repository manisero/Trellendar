angular.module('UserProfile', [])
    .controller("Controller", function($scope) {
        $scope.AvailableBoards = [
            { Name: 'board1' },
            { Name: 'board2' }
        ];
    });
