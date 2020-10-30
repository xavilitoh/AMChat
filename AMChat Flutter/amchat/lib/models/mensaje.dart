import 'dart:convert';

import 'package:amchat/models/user.dart';

Mensaje mensajeFromJson(String str) => Mensaje.fromJson(json.decode(str));

String mensajeToJson(Mensaje data) => json.encode(data.toJson());

class Mensajes {
  List<Mensaje> items = new List();

  Mensajes();

  Mensajes.fromJsonList(List<dynamic> jsonList) {
    if (jsonList == null) return;

    for (var item in jsonList) {
      final mensaje = new Mensaje.fromJson(item);
      items.add(mensaje);
    }
  }
}

class Mensaje {
  Mensaje({
    this.idMensaje,
    this.chatRoomIdChatRoom,
    this.message,
    this.fecha,
    this.emisorId,
    this.leido,
    this.tipoMensaje,
    this.chatRoom,
    this.emisor,
  });

  int idMensaje;
  int chatRoomIdChatRoom;
  String message;
  DateTime fecha;
  String emisorId;
  bool leido;
  int tipoMensaje;
  dynamic chatRoom;
  User emisor;

  factory Mensaje.fromJson(Map<String, dynamic> json) => Mensaje(
        idMensaje: json["idMensaje"],
        chatRoomIdChatRoom: json["chatRoomIdChatRoom"],
        message: json["message"],
        fecha: DateTime.parse(json["fecha"]),
        emisorId: json["emisorId"],
        leido: json["leido"],
        tipoMensaje: json["tipoMensaje"],
        chatRoom: json["chatRoom"],
        emisor: User.fromJson(json["emisor"]),
      );

  Map<String, dynamic> toJson() => {
        "idMensaje": idMensaje,
        "chatRoomIdChatRoom": chatRoomIdChatRoom,
        "message": message,
        "fecha": fecha.toIso8601String(),
        "emisorId": emisorId,
        "leido": leido,
        "tipoMensaje": tipoMensaje,
        "chatRoom": chatRoom,
        "emisor": emisor,
      };
}
