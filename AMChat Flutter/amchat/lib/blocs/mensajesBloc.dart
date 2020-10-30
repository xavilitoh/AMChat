import 'package:amchat/models/mensaje.dart';
import 'package:rxdart/rxdart.dart';

class MensajesBloc {
  final _mensajeController = BehaviorSubject<List<Mensaje>>();

  Stream<List<Mensaje>> get mensajesStream =>
      _mensajeController.stream.asBroadcastStream();

  set setMensajes(List<Mensaje> lista) {
    _mensajeController.sink.add(lista);
  }

  List<Mensaje> get mensajes => _mensajeController.value;

  dispose() {
    _mensajeController?.close();
  }
}
