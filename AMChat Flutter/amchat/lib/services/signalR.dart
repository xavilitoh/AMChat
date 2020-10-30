import 'package:amchat/global/url.dart';
import 'package:signalr_client/signalr_client.dart';

class SignalRServices {
  static final SignalRServices _instancia = new SignalRServices._internal();

  factory SignalRServices() {
    return _instancia;
  }

  SignalRServices._internal();

  HubConnection _hubConnection;

  HubConnection get hub {
    return _hubConnection;
  }

  Future initConnection() async {
    _hubConnection =
        HubConnectionBuilder().withUrl("http://$globalUrl/chatHub").build();

    await _hubConnection.start().then((data) {
      print("hub conectado");
    }).catchError((error) {
      print(error);
    }); // Inicia la conexion al servidor

    _hubConnection.onclose((_) {
      print("hub desconectado");
      _hubConnection.start().then((data) {
        print("hub conectado");
      }).catchError((error) {
        print(error);
      });
    });
  }
}
