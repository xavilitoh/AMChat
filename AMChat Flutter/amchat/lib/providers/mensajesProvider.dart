import 'dart:convert';

import 'package:amchat/blocs/mensajesBloc.dart';
import 'package:amchat/blocs/provider.dart';
import 'package:amchat/global/url.dart';
import 'package:amchat/models/mensaje.dart';
import 'package:amchat/models/preferencias.dart';
import 'package:flutter/material.dart';
import 'package:http/http.dart' as http;

class MensajesProvider {
  String _url = globalUrl;
  Preferencias p = Preferencias();

  Future<List<Mensaje>> _procesarRespuesta(
      Uri url, data, BuildContext context) async {
    final resp = await http.post(url, body: data);
    final decodedData = json.decode(resp.body);

    final mensajes = new Mensajes.fromJsonList(decodedData);

    MensajesBloc stream = Provider.mensajesOf(context);
    if (stream == null) {
      stream = new MensajesBloc();
    }

    stream.setMensajes = mensajes.items;

    return mensajes.items;
  }

  Future<List<Mensaje>> getMensajes({BuildContext context}) async {
    var data = {};

    final url = Uri.http(_url, 'api/mensajes/get');

    // print('la url es: $url');

    return await _procesarRespuesta(url, data, context);
  }
}
