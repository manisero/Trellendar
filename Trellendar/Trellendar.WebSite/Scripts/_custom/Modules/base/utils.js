angular.module('utils', [])
	.filter('format', function() {
		return function (text, args) {
			return format(text, args);
		};
	})
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
