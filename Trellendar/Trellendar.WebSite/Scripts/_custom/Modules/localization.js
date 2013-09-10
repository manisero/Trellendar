angular.module('localization', [])
    .filter('localize', function() {
        return function (text) {
            if (text == 'Profile saved successfully') {
                return 'success';
            } else {
                return 'test';
            }
        };
    });