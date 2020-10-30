import 'dart:io';

import 'package:amchat/global/url.dart';
import 'package:flutter/material.dart';
import 'package:url_launcher/url_launcher.dart';

void alerta({BuildContext context, String titulo, String mensaje}) {
  showDialog(
      context: context,
      builder: (context) {
        return AlertDialog(
          shape:
              RoundedRectangleBorder(borderRadius: BorderRadius.circular(20.0)),
          title: Text(titulo,
              style: Theme.of(context).appBarTheme.textTheme.title),
          content: Text(mensaje),
          actions: <Widget>[
            FlatButton(
              child: Text('ok'),
              onPressed: () {
                Navigator.of(context).pop();
              },
            )
          ],
        );
      });
}

void launchURL(var url) async {
  if (await canLaunch(url)) {
    await launch(url);
  } else {
    throw 'Could not launch $url';
  }
}

Future<bool> internetAvailable() async {
  try {
    final result = await InternetAddress.lookup(host);

    if (result.isNotEmpty && result[0].rawAddress.isNotEmpty) {
      return true;
    }

    return false;
  } catch (_) {
    return false;
  }
}
