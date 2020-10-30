import 'package:amchat/blocs/chatBloc.dart';
import 'package:amchat/blocs/mensajesBloc.dart';
import 'package:amchat/models/chatRoom.dart';
import 'package:amchat/services/signalR.dart';
import 'package:flutter/material.dart';

import 'login_bloc.dart';

export 'login_bloc.dart';

class Provider extends InheritedWidget {
  static Provider _instancia;
  final signalRService = SignalRServices();

  factory Provider({Key key, Widget child}) {
    if (_instancia == null) {
      _instancia = new Provider._internal(key: key, child: child);
    }

    return _instancia;
  }

  Provider._internal({Key key, Widget child}) : super(key: key, child: child);

  final loginBloc = LoginBloc();
  final mensajesBloc = MensajesBloc();
  final chatBloc = ChatBloc();

  // Provider({ Key key, Widget child })
  //   : super(key: key, child: child );

  @override
  bool updateShouldNotify(InheritedWidget oldWidget) => true;

  static LoginBloc of(BuildContext context) {
    return (context.dependOnInheritedWidgetOfExactType<Provider>().loginBloc);
  }

  static MensajesBloc mensajesOf(BuildContext context) {
    return (context
        .dependOnInheritedWidgetOfExactType<Provider>()
        .mensajesBloc);
  }

  static ChatBloc chatOf(BuildContext context) {
    return (context.dependOnInheritedWidgetOfExactType<Provider>().chatBloc);
  }

  static SignalRServices signalROf(BuildContext context) {
    return (context
        .dependOnInheritedWidgetOfExactType<Provider>()
        .signalRService);
  }
}
