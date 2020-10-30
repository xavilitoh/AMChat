import 'dart:convert';

import 'package:amchat/global/url.dart';
import 'package:http/http.dart' as http;
import 'package:amchat/models/preferencias.dart';
import 'dart:io';

class LoginProvider {
  final server = globalUrl;

  Future logout() async {
    final preferencias = new Preferencias();

    final data = {"token": preferencias.deviceToken};

    await http.post(
      'http://$server/api/Account/deleteToken',
      body: json.encode(data),
      headers: {"Content-Type": "application/json"},
    );

    return "ok";
  }

  Future<Map<String, dynamic>> loginFb(String fbToken) async {
    try {
      final result = await InternetAddress.lookup('google.com');
      if (result.isNotEmpty && result[0].rawAddress.isNotEmpty) {
        final preferencias = new Preferencias();

        final data = {"fbToken": fbToken, "token": preferencias.deviceToken};

        final resp = await http.post(
          'http://$server/api/Account/LoginWithFb',
          body: json.encode(data),
          headers: {"Content-Type": "application/json"},
        );

        Map<String, dynamic> result = json.decode(resp.body);

        if (result.containsKey('token')) {
          preferencias.token = result['token'];
          preferencias.nombre = result['nombre'];
          preferencias.email = result['email'];
          preferencias.id = result['id'];
          preferencias.foto = result["foto"];

          final data = {
            'IdToken': '0',
            'Id': result['id'],
            'Descripcion': result['token']
          };

          http.post('http://$server/api/Trokens',
              body: json.encode(data),
              headers: {"Content-Type": "application/json"}).then((result) {});

          return {
            "OK": true,
          };
        } else {
          return {"OK": false, "message": 'Login invalido'};
        }
      } else {
        return {"OK": false, "message": "Compruebe su conexin a internet"};
      }
    } on SocketException catch (_) {
      return {"OK": false, "message": "Compruebe su conexin a internet"};
    }
  }

  Future<Map<String, dynamic>> login(String email, String pass, context) async {
    try {
      final result = await InternetAddress.lookup('google.com');
      if (result.isNotEmpty && result[0].rawAddress.isNotEmpty) {
        final preferencias = new Preferencias();

        final data = {
          "email": email,
          "password": pass,
          "token": preferencias.deviceToken
        };

        final resp = await http.post(
          'http://$server/api/Account/login',
          body: json.encode(data),
          headers: {"Content-Type": "application/json"},
        );

        Map<String, dynamic> result = json.decode(resp.body);

        if (result.containsKey('token')) {
          preferencias.token = result['token'];
          preferencias.nombre = result['nombre'];
          preferencias.email = email;
          preferencias.pass = pass;
          preferencias.id = result['id'];
          preferencias.foto = result["foto"];
          final data = {
            'IdToken': '0',
            'Id': result['id'],
            'Descripcion': result['token']
          };

          http.post('http://$server/api/Trokens',
              body: json.encode(data),
              headers: {"Content-Type": "application/json"}).then((result) {});

          return {
            "OK": true,
          };
        } else {
          return {"OK": false, "message": 'Login invalido'};
        }
      } else {
        return {"OK": false, "message": "Compruebe su conexin a internet"};
      }
    } on SocketException catch (_) {
      return {"OK": false, "message": "Compruebe su conexin a internet"};
    }

    // Navigator.pushReplacementNamed(context, 'home');
  }

  Future<Map<String, dynamic>> newUser(
      {String email,
      String pass,
      String nombre,
      String apellido,
      context}) async {
    final preferencias = new Preferencias();

    final data = {
      "email": email,
      "password": pass,
      "nombres": nombre,
      "apellidos": apellido,
      "sexo": 2,
      "token": preferencias.deviceToken
    };

    try {
      final result = await InternetAddress.lookup('google.com');

      if (result.isNotEmpty && result[0].rawAddress.isNotEmpty) {
        final resp = await http.post(
          'http://$server/api/Account/create',
          body: json.encode(data),
          headers: {"Content-Type": "application/json"},
        );

        if (resp.body == '"Username or password invalid"') {
          return {"OK": false, "message": "Email o password incorrecto"};
        }

        Map<String, dynamic> result = json.decode(resp.body);

        if (result['isSuccess'] == false) {
          return {"OK": false, "message": result['message']};
        }

        preferencias.token = result['token'];
        preferencias.email = email;
        preferencias.pass = pass;
        preferencias.nombre = result['nombre'];
        preferencias.foto = result["foto"];

        return {
          "OK": true,
        };
      } else {
        return {"OK": false, "message": "Compruebe su conexion a internet"};
      }
    } on SocketException catch (_) {
      return {"OK": false, "message": "Compruebe su conexin a internet"};
    }
  }
}
