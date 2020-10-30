import 'dart:convert';

import 'package:amchat/global/url.dart';
import 'package:amchat/models/preferencias.dart';
import 'package:amchat/models/user.dart';
import 'package:http/http.dart' as http;

class UserProvider {
  String _url = globalUrl;
  Preferencias p = Preferencias();

  Future<List<User>> _procesarRespuesta(Uri url) async {
    final preferencias = new Preferencias();

    final data = {"id": preferencias.id};

    final resp = await http.post(url,
        body: json.encode(data),
        headers: {"Authorization": "Bearer ${p.token}"});
    final decodedData = json.decode(resp.body);

    final users = new Users.fromJsonList(decodedData["users"]);

    return users.items
        .where((element) => element.id != preferencias.id)
        .toList();
  }

  Future<List<User>> getusers() async {
    final url = Uri.http(
      _url,
      'api/users/GetUsers',
    );

    // print('la url es: $url');

    return await _procesarRespuesta(url);
  }
}
