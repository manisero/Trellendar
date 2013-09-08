angular.module('utils', [])
    .directive('redirect', function() {
        return {
            restrict: 'A',
            link: function(scope, iElement, iAttrs) {
                var url = iAttrs.redirect;
                
                iElement.bind('click', function() {
                    return redirect(url);
                });
            }
        };
    });
