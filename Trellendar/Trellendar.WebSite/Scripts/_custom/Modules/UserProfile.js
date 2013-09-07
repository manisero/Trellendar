﻿angular.module('UserProfile', [])
    .controller("Controller", ['$scope', 'Model', function($scope, model) {
        $scope.Bonds = model.BoardCalendarBonds;
        $scope.SelectedBoard = null;
        $scope.SelectedCalendar = null;
    }])
    .directive("board", function() {
        return function (scope, iElement, iAttrs) {
            var boardId = iAttrs.board;

            iElement.bind('click', function () {
                scope.SelectedBoard = boardId;
                scope.SelectedCalendar = null;
                scope.$apply();
            });

            scope.$watch("SelectedBoard", function (newValue) {
                if (newValue == boardId) {
                    iElement.addClass('selected');
                } else {
                    iElement.removeClass('selected');
                }
            });
        };
    })
    .directive("calendar", function () {
        return function (scope, iElement, iAttrs) {
            var calendarId = iAttrs.calendar;

            iElement.bind('click', function () {
                scope.SelectedBoard = null;
                scope.SelectedCalendar = calendarId;
                scope.$apply();
            });

            scope.$watch("SelectedCalendar", function (newValue) {
                if (newValue == calendarId) {
                    iElement.addClass('selected');
                } else {
                    iElement.removeClass('selected');
                }
            });
        };
    });
