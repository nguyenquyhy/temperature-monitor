/// <reference path="../typings/tsd.d.ts" />

$(() => {
    var appViewModel = new AppViewModel();
    ko.applyBindings(appViewModel);

    $.connection.hub.logging = true;
    var temperatureHub = $.connection.temperatureHub;
    temperatureHub.client.update = (sensorId, temperature) => {
        var matchedSensor = _.find(<_.List<LiveSensorViewModel>>appViewModel.liveSensors(), sensor => {
            return sensor.id == sensorId
        });
        if (matchedSensor !== undefined) {
            matchedSensor.value(temperature);
        }
        else {
            var newSensor = new LiveSensorViewModel(sensorId, sensorId);
            newSensor.value(temperature);
            appViewModel.liveSensors.push(newSensor);
        }
    };

    //=================
    // SIGNALR IS READY
    //=================
    $.connection.hub.start().done(function () {
        appViewModel.statusString('Ready');
    });
});