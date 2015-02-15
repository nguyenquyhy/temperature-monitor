class LiveSensorViewModel {
    id: String;
    name: String;
    value: KnockoutObservable<number>;
    lastSignal: KnockoutObservable<Date>;

    constructor(id, name) {
        this.id = id;
        this.name = name;
        this.value = ko.observable(0);
        this.lastSignal = ko.observable(null);
    }

    update(temperature) {
        this.value(temperature);
        this.lastSignal(new Date(Date.now()));
    }
}