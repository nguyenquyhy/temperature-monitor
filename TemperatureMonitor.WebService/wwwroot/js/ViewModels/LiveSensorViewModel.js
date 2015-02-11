var LiveSensorViewModel = (function () {
    function LiveSensorViewModel(id, name) {
        this.id = id;
        this.name = name;
        this.value = ko.observable(0);
    }
    return LiveSensorViewModel;
})();
