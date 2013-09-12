var redirect = function(url) {
    window.open(url, '_blank');
    return false;
};

var format = function (text, args) {
	var result = text;

	for (var i = 0; i != args.length; i++) {
		result = result.replace('{' + i + '}', args[i]);
	}

	return result;
};
