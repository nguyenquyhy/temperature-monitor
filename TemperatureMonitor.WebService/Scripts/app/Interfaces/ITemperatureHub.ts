interface SignalR {
    temperatureHub: ITemperatureHub;
}

interface ITemperatureHub extends HubConnection {
    client: ITemperatureHubClient;
    server: ITemperatureHubServer;
}

interface ITemperatureHubClient {
    update: (sensorId: string, value: number) => any;
}

interface ITemperatureHubServer {

}