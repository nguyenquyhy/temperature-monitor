class LiveSensorViewModel {
    id: String;
    name: String;
    value: KnockoutObservable<number>;

    constructor(id, name) {
        this.id = id;
        this.name = name;
        this.value = ko.observable(0);
    }
}