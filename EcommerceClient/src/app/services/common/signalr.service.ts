import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder, HubConnectionState } from '@microsoft/signalr';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {

  private _connection: HubConnection;

  get connection() : HubConnection
  {
    return this._connection;
  }

  start(hubUrl : string)
  {
      if(!this.connection || this._connection?.state == HubConnectionState.Disconnected)
      {
        const builder : HubConnectionBuilder = new HubConnectionBuilder();

        const HubConnection : HubConnection = builder.withUrl(hubUrl)
        .withAutomaticReconnect()
        .build();

        HubConnection.start()
        .then(() => 
          {
            console.log("Connected");
          })
        .catch(error => setTimeout(() => this.start(hubUrl), 2000));

        this._connection = HubConnection;
      }

      this._connection.onreconnected(connectionId => console.log("Reconnected"));
      this._connection.onreconnecting(error => console.log("Reconnecting"));
      this._connection.onclose(error => console.log("Close reconnection"));
  }


  invoke(procedureName : string, message : any, successCallback? : (value) => void, errorCallback? : (value) => void)
  {
      this.connection.invoke(procedureName, message)
      .then(successCallback)
      .catch(errorCallback);
  }


  on(procedureName : string, callBack : (...message : any) => void)
  {
    this.connection.on(procedureName, callBack);
  }





}
