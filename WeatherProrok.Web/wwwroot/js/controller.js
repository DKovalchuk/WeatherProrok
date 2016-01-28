
'use strict';

/*angular
    .module('app')
    .controller('WeatherController', WeatherController);

WeatherController.$inject = ['$scope'];*/

var WeatherApp = angular.module('WeatherApp', ['SignalR']);

WeatherApp.controller('WeatherController', ['$scope', '$http', 'Hub', function ($scope, $http, Hub) {
    $scope.title = 'WeatherController';
        
    /*$http.get('/api/weather/current').success(function (data) {
        $scope.weathers = data;
    });*/

    activate();

    function activate() {
        var hub = new Hub('weather', {
            listeners: {
                'updateCurrentWeather': function (weathers) {
                    $scope.weathers = weathers;
                    weatherIcons();
                    $scope.$apply();
                    completeUpdateWeater();
                    console.log('Weather updated');
                },
                'startUpdatingWeather': function () {
                    startUpdateWeater();
                    console.log('Start updating weather...');
                }
            },
            stateChanged: function (state) {
                switch (state.newState) {
                    case $.signalR.connectionState.connecting:
                        console.log('connecting...');
                        break;
                    case $.signalR.connectionState.connected:
                        console.log('connected');
                        break;
                    case $.signalR.connectionState.reconnecting:
                        console.log('reconnecting...');
                        break;
                    case $.signalR.connectionState.disconnected:
                        console.log('disconnected');
                        break;
                }
            }
        });
    }

    function weatherIcons() {
        var weathers = $scope.weathers;
        for (var i = 0; i < weathers.length; i++) {
            var weather = weathers[i];
            if (weather.Precipitation == 'No precipitations') {
                if (weather.Cloudity == 'No clouds') {
                    $scope.weathers[i].WeatherIcon = 'wi-day-sunny';
                } else {
                    $scope.weathers[i].WeatherIcon = 'wi-cloudy';
                }
            } else if (weather.Precipitation.toLowerCase().indexOf('rain') > -1) {
                $scope.weathers[i].WeatherIcon = 'wi-rain';
            } else if (weather.Precipitation.toLowerCase().indexOf('snow') > -1) {
                $scope.weathers[i].WeatherIcon = 'wi-snow';
            } else
                $scope.weathers[i].WeatherIcon = 'wi-na';
        }
    }
}]
);


