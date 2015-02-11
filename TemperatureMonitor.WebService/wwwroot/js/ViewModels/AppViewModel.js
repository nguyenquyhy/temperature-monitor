var AppViewModel = (function () {
    function AppViewModel() {
        this.statusString = ko.observable("Initializing...");
        this.liveSensors = ko.observableArray([]);
    }
    return AppViewModel;
})();
