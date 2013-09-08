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
        $scope.IsBoardBound = function (boardId) {
            for (var i = 0; i != $scope.Bonds.length; i++) {
                if ($scope.Bonds[i].BoardID == boardId && $scope.Bonds[i].CalendarID == $scope.SelectedCalendar) {
                    return true;
                }
            }

            return false;
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
    }])
    .directive("board", function() {
        return function(scope, iElement, iAttrs) {
            var boardId = iAttrs.board;

            scope.$watch("SelectedBoard", function(newValue) {
                if (newValue == boardId) {
                    iElement.addClass('selected');
                } else {
                    iElement.removeClass('selected');
                }
            });

            scope.$watch("SelectedCalendar", function () {
                if (scope.IsBoardBound(boardId)) {
                    iElement.addClass('selected');
                } else {
                    iElement.removeClass('selected');
                }
            });
        };
    })
    .directive("bindBoard", function() {
        return function(scope, iElement, iAttrs) {
            scope.$watch("SelectedCalendar", function(newValue) {
                if (newValue == null) {
                    iElement.addClass('hidden');
                } else {
                    if (scope.IsBoardBound(iAttrs.bindBoard)) {
                        iElement.addClass('hidden');
                    } else {
                        iElement.removeClass('hidden');
                    }
                }
            });
        };
    })
    .directive("unbindBoard", function() {
        return function(scope, iElement, iAttrs) {
            scope.$watch("SelectedCalendar", function () {
                if (scope.IsBoardBound(iAttrs.unbindBoard)) {
                    iElement.removeClass('hidden');
                } else {
                    iElement.addClass('hidden');
                }
            });
        };
    })
    .directive("calendar", function() {
        return function(scope, iElement, iAttrs) {
            var calendarId = iAttrs.calendar;

            scope.$watch("SelectedCalendar", function(newValue) {
                if (newValue == calendarId) {
                    iElement.addClass('selected');
                } else {
                    iElement.removeClass('selected');
                }
            });

            scope.$watch("SelectedBoard", function () {
                if (scope.IsCalendarBound(calendarId)) {
                    iElement.addClass('selected');
                } else {
                    iElement.removeClass('selected');
                }
            });
        };
    })
    .directive("bindCalendar", function() {
        return function(scope, iElement, iAttrs) {
            scope.$watch("SelectedBoard", function(newValue) {
                if (newValue == null) {
                    iElement.addClass('hidden');
                } else {
                    if (scope.IsCalendarBound(iAttrs.bindCalendar)) {
                        iElement.addClass('hidden');
                    } else {
                        iElement.removeClass('hidden');
                    }
                }
            });
        };
    })
    .directive("unbindCalendar", function() {
        return function(scope, iElement, iAttrs) {
            scope.$watch("SelectedBoard", function () {
                if (scope.IsCalendarBound(iAttrs.unbindCalendar)) {
                    iElement.removeClass('hidden');
                } else {
                    iElement.addClass('hidden');
                }
            });
        };
    });
