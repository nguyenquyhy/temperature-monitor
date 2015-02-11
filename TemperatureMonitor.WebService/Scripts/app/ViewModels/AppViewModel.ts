class AppViewModel {
    statusString: KnockoutObservable<String>;
    liveSensors: KnockoutObservableArray<LiveSensorViewModel>;

    constructor() {
        this.statusString = ko.observable("Initializing...");
        this.liveSensors = ko.observableArray([]);
    }
}