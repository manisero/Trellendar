angular.module('UserProfile', [])
    .controller("Controller", ['$scope', 'Model', function($scope, model) {
        $scope.Bonds = model.BoardCalendarBonds;
        $scope.SelectedBoard = model.BoardCalendarBonds[0].BoardID;
        $scope.SelectedCalendar = null;
    }])
    .directive("board", function() {
        return function(scope, iElement, iAttrs) {
            var boardId = iAttrs.board;

            iElement.bind('click', function() {
                scope.SelectedBoard = boardId;
                scope.SelectedCalendar = null;
                scope.$apply();
            });

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

            iElement.bind('click', function() {
                for (var i = 0; i != scope.Bonds.length; i++) {
                    if (scope.Bonds[i].BoardID == boardId && scope.Bonds[i].CalendarID == scope.SelectedCalendar) {
                        return;
                    }
                }

                scope.Bonds.push({
                    BoardID: boardId,
                    CalendarID: scope.SelectedCalendar
                });
            });

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

            iElement.bind('click', function() {
                for (var i = 0; i != scope.Bonds.length; i++) {
                    if (scope.Bonds[i].BoardID == boardId && scope.Bonds[i].CalendarID == scope.SelectedCalendar) {
                        scope.Bonds.pop(scope.Bonds[i]);
                        return;
                    }
                }
            });

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

            iElement.bind('click', function() {
                scope.SelectedBoard = null;
                scope.SelectedCalendar = calendarId;
                scope.$apply();
            });

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

            iElement.bind('click', function() {
                for (var i = 0; i != scope.Bonds.length; i++) {
                    if (scope.Bonds[i].CalendarID == calendarId && scope.Bonds[i].BoardID == scope.SelectedBoard) {
                        return;
                    }
                }

                scope.Bonds.push({
                    BoardID: scope.SelectedBoard,
                    CalendarID: calendarId
                });
            });

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

            iElement.bind('click', function() {
                for (var i = 0; i != scope.Bonds.length; i++) {
                    if (scope.Bonds[i].CalendarID == calendarId && scope.Bonds[i].BoardID == scope.SelectedBoard) {
                        scope.Bonds.pop(scope.Bonds[i]);
                        return;
                    }
                }
            });

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
