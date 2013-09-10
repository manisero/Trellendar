angular.module('UserProfile', ['http'])
    .controller("Controller", ['$scope', 'Model', '$http', 'AjaxService', function($scope, model, $http, ajaxService) {
        $scope.Bonds = model.BoardCalendarBonds;
        $scope.SelectedBoard = model.BoardCalendarBonds.length != 0 ? model.BoardCalendarBonds[0].BoardID : null;
        $scope.SelectedCalendar = null;
        $scope.Message = '';
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
                    $scope.Bonds.splice(i, 1);
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
                    $scope.Bonds.splice(i, 1);
                    return;
                }
            }
        };
        $scope.Save = function() {
            ajaxService.send($http.post("/UserProfile/Save", $scope.Bonds),
                function() {
                    $scope.Message = 'Profile saved successfully';
                },
                function(errorMessage) {
                    $scope.Message = errorMessage;
                });
        };
    }]);
