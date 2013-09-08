angular.module('http', [])
    .service('AjaxService', function() {
        this.send = function(request, successCallback, failureCallback) {
            request
                .success(function(data) {
                    if (data.Success) {
                        if (successCallback != null) {
                            successCallback(data.Data);
                        }
                    } else {
                        if (failureCallback != null) {
                            failureCallback(data.ErrorMessage);
                        }
                    }
                })
                .error(function() {
                    $scope.Message = 'Unknown error';
                });
        };
    });
