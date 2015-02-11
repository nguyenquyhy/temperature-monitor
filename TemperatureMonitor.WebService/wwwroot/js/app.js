$(function () {
    var appViewModel = new AppViewModel();
    ko.applyBindings(appViewModel);
    $.connection.hub.logging = true;
    var temperatureHub = $.connection.temperatureHub;
    temperatureHub.client.update = function (sensorId, temperature) {
        var matchedSensor = _.find(appViewModel.liveSensors(), function (sensor) {
            return sensor.id == sensorId;
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
    $.connection.hub.start().done(function () {
        appViewModel.statusString('Ready');
    });
});
