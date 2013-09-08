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
        $scope.BindBoard = function(boardId) {
            for (var i = 0; i != $scope.Bonds.length; i++) {
                if ($scope.Bonds[i].BoardID == boardId && $scope.Bonds[i].CalendarID == $scope.SelectedCalendar) {
                    return;
                }
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
        $scope.BindCalendar = function(calendarId) {
            for (var i = 0; i != $scope.Bonds.length; i++) {
                if ($scope.Bonds[i].CalendarID == calendarId && $scope.Bonds[i].BoardID == $scope.SelectedBoard) {
                    return;
                }
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

            scope.$watch("SelectedCalendar", function(newValue) {
                for (var i = 0; i != scope.Bonds.length; i++) {
                    if (scope.Bonds[i].BoardID == boardId && scope.Bonds[i].CalendarID == newValue) {
                        iElement.addClass('selected');
                        return;
                    }
                }

                iElement.removeClass('selected');
            });
        };
    })
    .directive("bindBoard", function() {
        return function(scope, iElement, iAttrs) {
            var boardId = iAttrs.bindBoard;

            scope.$watch("SelectedCalendar", function(newValue) {
                if (scope.SelectedCalendar == null) {
                    iElement.addClass('hidden');
                    return;
                }

                for (var i = 0; i != scope.Bonds.length; i++) {
                    if (scope.Bonds[i].BoardID == boardId && scope.Bonds[i].CalendarID == newValue) {
                        iElement.addClass('hidden');
                        return;
                    }
                }

                iElement.removeClass('hidden');
            });
        };
    })
    .directive("unbindBoard", function() {
        return function(scope, iElement, iAttrs) {
            var boardId = iAttrs.unbindBoard;

            scope.$watch("SelectedCalendar", function(newValue) {
                for (var i = 0; i != scope.Bonds.length; i++) {
                    if (scope.Bonds[i].BoardID == boardId && scope.Bonds[i].CalendarID == newValue) {
                        iElement.removeClass('hidden');
                        return;
                    }
                }

                iElement.addClass('hidden');
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

            scope.$watch("SelectedBoard", function(newValue) {
                for (var i = 0; i != scope.Bonds.length; i++) {
                    if (scope.Bonds[i].CalendarID == calendarId && scope.Bonds[i].BoardID == newValue) {
                        iElement.addClass('selected');
                        return;
                    }
                }

                iElement.removeClass('selected');
            });
        };
    })
    .directive("bindCalendar", function() {
        return function(scope, iElement, iAttrs) {
            var calendarId = iAttrs.bindCalendar;

            scope.$watch("SelectedBoard", function(newValue) {
                if (scope.SelectedBoard == null) {
                    iElement.addClass('hidden');
                    return;
                }

                for (var i = 0; i != scope.Bonds.length; i++) {
                    if (scope.Bonds[i].CalendarID == calendarId && scope.Bonds[i].BoardID == newValue) {
                        iElement.addClass('hidden');
                        return;
                    }
                }

                iElement.removeClass('hidden');
            });
        };
    })
    .directive("unbindCalendar", function() {
        return function(scope, iElement, iAttrs) {
            var calendarId = iAttrs.unbindCalendar;

            scope.$watch("SelectedBoard", function(newValue) {
                for (var i = 0; i != scope.Bonds.length; i++) {
                    if (scope.Bonds[i].CalendarID == calendarId && scope.Bonds[i].BoardID == newValue) {
                        iElement.removeClass('hidden');
                        return;
                    }
                }

                iElement.addClass('hidden');
            });
        };
    });
