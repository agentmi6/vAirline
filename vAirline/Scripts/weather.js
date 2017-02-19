$(document).ready(function () {
    $.simpleWeather({
        location: '',
        woeid: '482940',
        unit: 'c',
        success: function (weather) {
            html = '<h2><i class="icon-' + weather.code + '"></i> ' + weather.temp + '&deg;' + weather.units.temp + '</h2>';
            html += '<ul><li>' + weather.city + ', ' + weather.region + '</li>';
            html += '<li class="currently">' + weather.currently + '</li>';
            html += '<li>' + weather.wind.direction + ' ' + weather.wind.speed + ' ' + weather.units.speed + '</li></ul>';

            $("#weather").html(html);
        },
        error: function (error) {
            $("#weather").html('<p>' + error + '</p>');
        }
    });

    $.simpleWeather({
        location: '',
        woeid: '615702',
        unit: 'c',
        success: function (weather) {
            html = '<h2><i class="icon-' + weather.code + '"></i> ' + weather.temp + '&deg;' + weather.units.temp + '</h2>';
            html += '<ul><li>' + weather.city + ', ' + weather.region + '</li>';
            html += '<li class="currently">' + weather.currently + '</li>';
            html += '<li>' + weather.wind.direction + ' ' + weather.wind.speed + ' ' + weather.units.speed + '</li></ul>';

            $("#weather2").html(html);
        },
        error: function (error) {
            $("#weather2").html('<p>' + error + '</p>');
        }
    });

    $.simpleWeather({
        location: '',
        woeid: '2459115',
        unit: 'c',
        success: function (weather) {
            html = '<h2><i class="icon-' + weather.code + '"></i> ' + weather.temp + '&deg;' + weather.units.temp + '</h2>';
            html += '<ul><li>' + weather.city + ', ' + weather.region + '</li>';
            html += '<li class="currently">' + weather.currently + '</li>';
            html += '<li>' + weather.wind.direction + ' ' + weather.wind.speed + ' ' + weather.units.speed + '</li></ul>';

            $("#weather3").html(html);
        },
        error: function (error) {
            $("#weather3").html('<p>' + error + '</p>');
        }
    });

    $.simpleWeather({
        location: '',
        woeid: '2442047',
        unit: 'c',
        success: function (weather) {
            html = '<h2><i class="icon-' + weather.code + '"></i> ' + weather.temp + '&deg;' + weather.units.temp + '</h2>';
            html += '<ul><li>' + weather.city + ', ' + weather.region + '</li>';
            html += '<li class="currently">' + weather.currently + '</li>';
            html += '<li>' + weather.wind.direction + ' ' + weather.wind.speed + ' ' + weather.units.speed + '</li></ul>';

            $("#weather4").html(html);
        },
        error: function (error) {
            $("#weather4").html('<p>' + error + '</p>');
        }
    });

    $.simpleWeather({
        location: '',
        woeid: '2122265',
        unit: 'c',
        success: function (weather) {
            html = '<h2><i class="icon-' + weather.code + '"></i> ' + weather.temp + '&deg;' + weather.units.temp + '</h2>';
            html += '<ul><li>' + weather.city + ', ' + weather.region + '</li>';
            html += '<li class="currently">' + weather.currently + '</li>';
            html += '<li>' + weather.wind.direction + ' ' + weather.wind.speed + ' ' + weather.units.speed + '</li></ul>';

            $("#weather5").html(html);
        },
        error: function (error) {
            $("#weather5").html('<p>' + error + '</p>');
        }
    });

    $.simpleWeather({
        location: '',
        woeid: '676757',
        unit: 'c',
        success: function (weather) {
            html = '<h2><i class="icon-' + weather.code + '"></i> ' + weather.temp + '&deg;' + weather.units.temp + '</h2>';
            html += '<ul><li>' + weather.city + ', ' + weather.region + '</li>';
            html += '<li class="currently">' + weather.currently + '</li>';
            html += '<li>' + weather.wind.direction + ' ' + weather.wind.speed + ' ' + weather.units.speed + '</li></ul>';

            $("#weather6").html(html);
        },
        error: function (error) {
            $("#weather6").html('<p>' + error + '</p>');
        }
    });

    $.simpleWeather({
        location: '',
        woeid: '1118370',
        unit: 'c',
        success: function (weather) {
            html = '<h2><i class="icon-' + weather.code + '"></i> ' + weather.temp + '&deg;' + weather.units.temp + '</h2>';
            html += '<ul><li>' + weather.city + ', ' + weather.region + '</li>';
            html += '<li class="currently">' + weather.currently + '</li>';
            html += '<li>' + weather.wind.direction + ' ' + weather.wind.speed + ' ' + weather.units.speed + '</li></ul>';

            $("#weather6").html(html);
        },
        error: function (error) {
            $("#weather6").html('<p>' + error + '</p>');
        }
    });

    $.simpleWeather({
        location: '',
        woeid: '44418',
        unit: 'c',
        success: function (weather) {
            html = '<h2><i class="icon-' + weather.code + '"></i> ' + weather.temp + '&deg;' + weather.units.temp + '</h2>';
            html += '<ul><li>' + weather.city + ', ' + weather.region + '</li>';
            html += '<li class="currently">' + weather.currently + '</li>';
            html += '<li>' + weather.wind.direction + ' ' + weather.wind.speed + ' ' + weather.units.speed + '</li></ul>';

            $("#weather7").html(html);
        },
        error: function (error) {
            $("#weather7").html('<p>' + error + '</p>');
        }
    });
});


