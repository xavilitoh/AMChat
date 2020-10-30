// To parse this JSON data, do
//
//     final chatRoom = chatRoomFromJson(jsonString);

import 'dart:convert';

import 'package:amchat/models/mensaje.dart';

ChatRoom chatRoomFromJson(String str) => ChatRoom.fromJson(json.decode(str));

String chatRoomToJson(ChatRoom data) => json.encode(data.toJson());

class ChatRoom {
  ChatRoom({
    this.chatId,
    this.userId,
    this.user,
    this.mensajes,
  });

  int chatId;
  String userId;
  String user;
  List<Mensaje> mensajes;

  factory ChatRoom.fromJson(Map<String, dynamic> json) => ChatRoom(
        chatId: json["chatId"],
        userId: json["userId"],
        user: json["user"],
        mensajes: List<Mensaje>.from(
            json["mensajes"].map((x) => Mensaje.fromJson(x))),
      );

  Map<String, dynamic> toJson() => {
        "chatId": chatId,
        "userId": userId,
        "user": user,
        "mensajes": List<dynamic>.from(mensajes.map((x) => x.toJson())),
      };
}
