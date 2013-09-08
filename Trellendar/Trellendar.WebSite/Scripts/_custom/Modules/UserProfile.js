angular.module('UserProfile', [])
    .controller("Controller", ['$scope', 'Model', function($scope, model) {
        $scope.Bonds = model.BoardCalendarBonds;
        $scope.SelectedBoard = model.BoardCalendarBonds[0].BoardID;
        $scope.SelectedCalendar = null;
        $scope.SelectBoard = function(boardId) {
            $scope.SelectedBoard = boardId;
            $scope.SelectedCalendar = null;
        };
        $scope.SelectCalendar = function(calendarId) {
            $scope.SelectedBoard = null;
            $scope.SelectedCalendar = calendarId;
        };
        $scope.IsBoardBound = function(boardId) {
            for (var i = 0; i != $scope.Bonds.length; i++) {
                if ($scope.Bonds[i].BoardID == boardId && $scope.Bonds[i].CalendarID == $scope.SelectedCalendar) {
                    return true;
                }
            }

            return false;
        };
        $scope.IsBoardSelectedOrBound = function(boardId) {
            return $scope.SelectedBoard == boardId || $scope.IsBoardBound(boardId);
        };
        $scope.BindBoard = function(boardId) {
            if ($scope.IsBoardBound(boardId)) {
                return;
            }

            $scope.Bonds.push({
                BoardID: boardId,
                CalendarID: $scope.SelectedCalendar
            });
        };
        $scope.UnbindBoard = function(boardId) {
            for (var i = 0; i != $scope.Bonds.length; i++) {
                if ($scope.Bonds[i].BoardID == boardId && $scope.Bonds[i].CalendarID == $scope.SelectedCalendar) {
                    $scope.Bonds.pop($scope.Bonds[i]);
                    return;
                }
            }
        };
        $scope.IsCalendarBound = function(calendarId) {
            for (var i = 0; i != $scope.Bonds.length; i++) {
                if ($scope.Bonds[i].CalendarID == calendarId && $scope.Bonds[i].BoardID == $scope.SelectedBoard) {
                    return true;
                }
            }

            return false;
        };
        $scope.IsCalendarSelectedOrBound = function(calendarId) {
            return $scope.SelectedCalendar == calendarId || $scope.IsCalendarBound(calendarId);
        };
        $scope.BindCalendar = function(calendarId) {
            if ($scope.IsCalendarBound(calendarId)) {
                return;
            }

            $scope.Bonds.push({
                BoardID: $scope.SelectedBoard,
                CalendarID: calendarId
            });
        };
        $scope.UnbindCalendar = function(calendarId) {
            for (var i = 0; i != $scope.Bonds.length; i++) {
                if ($scope.Bonds[i].CalendarID == calendarId && $scope.Bonds[i].BoardID == $scope.SelectedBoard) {
                    $scope.Bonds.pop($scope.Bonds[i]);
                    return;
                }
            }
        };
    }]);
