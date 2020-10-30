import 'dart:convert';

import 'package:amchat/blocs/chatBloc.dart';
import 'package:amchat/blocs/mensajesBloc.dart';
import 'package:amchat/blocs/provider.dart';
import 'package:amchat/global/url.dart';
import 'package:amchat/models/chatRoom.dart';
import 'package:amchat/models/preferencias.dart';
import 'package:flutter/cupertino.dart';
import 'package:http/http.dart' as http;

class ChatProvider {
  String _url = globalUrl;
  Preferencias p = Preferencias();

  Future<ChatRoom> _procesarRespuesta(Uri url, BuildContext context) async {
    final resp =
        await http.get('http://$globalUrl/api/Chat/getChat?id=${p.id}');
    final decodedData = json.decode(resp.body);

    final chat = new ChatRoom.fromJson(decodedData);

    ChatBloc streamChat = Provider.chatOf(context);
    if (streamChat == null) {
      streamChat = new ChatBloc();
    }
    streamChat.setChat = chat;

    MensajesBloc stream = Provider.mensajesOf(context);
    if (stream == null) {
      stream = new MensajesBloc();
    }
    stream.setMensajes = chat.mensajes;

    return chat;
  }

  Future<ChatRoom> getChat(BuildContext context) async {
    final url = Uri.http(_url, 'api/Chat/getChat');

    // print('la url es: $url');

    return await _procesarRespuesta(url, context);
  }
}
