angular.module('localization', [])
    .filter('localize', function() {
        return function (text) {
            if (resources.hasOwnProperty(text)) {
                return resources[text];
            }

            return text;
        };
    });