var LiveSensorViewModel = (function () {
    function LiveSensorViewModel(id, name) {
        this.id = id;
        this.name = name;
        this.value = ko.observable(0);
        this.lastSignal = ko.observable(null);
    }
    LiveSensorViewModel.prototype.update = function (temperature) {
        this.value(temperature);
        this.lastSignal(new Date(Date.now()));
    };
    return LiveSensorViewModel;
})();
