import 'package:amchat/pages/chatPage.dart';
import 'package:amchat/pages/loginPage.dart';
import 'package:amchat/pages/registerPage.dart';
import 'package:amchat/pages/homePage.dart';
import 'package:amchat/services/signalR.dart';
import 'package:flutter/material.dart';

import 'blocs/provider.dart';
import 'models/preferencias.dart';

void main() async {
  WidgetsFlutterBinding.ensureInitialized();

  Preferencias p = new Preferencias();
  await p.initPrefs();

  SignalRServices signalR = new SignalRServices();
  signalR.initConnection();

  runApp(MyApp());
}

class MyApp extends StatelessWidget {
  // This widget is the root of your application.
  @override
  Widget build(BuildContext context) {
    return Provider(
      key: key,
      child: _app(context),
    );
  }

  Widget _app(BuildContext context) {
    Preferencias preferencias = new Preferencias();

    String _page;

    if (preferencias.token != "" && preferencias.token != null) {
      _page = 'home';
      print('preferencias.token');
      print(preferencias.token);
    } else {
      _page = 'login';
      print('no token');
    }

    return MaterialApp(
      debugShowCheckedModeBanner: false,
      title: 'AMChat',
      initialRoute: _page,
      routes: {
        'login': (BuildContext context) => LoginPage(),
        'register': (BuildContext context) => RegisterPage(),
        'home': (BuildContext context) => HomePage(),
        'newChat': (BuildContext context) => ChatPage(),
      },
      theme: ThemeData(
          canvasColor: Colors.transparent,
          brightness: Brightness.light,
          fontFamily: 'Montserrat',
          backgroundColor: Colors.white,
          secondaryHeaderColor: Color(0xFF0984e3),
          primaryColor: Color(0xFF007bff),
          scaffoldBackgroundColor: Colors.white,
          textTheme: TextTheme(
            title: TextStyle(
              color: Colors.black,
            ),
          ),
          appBarTheme: AppBarTheme(
              elevation: 3,
              iconTheme: IconThemeData(
                color: Colors.black,
              ),
              color: Colors.white,
              textTheme: TextTheme(
                  title: TextStyle(
                      color: Colors.black,
                      fontSize: 20,
                      fontWeight: FontWeight.bold,
                      fontFamily: 'Montserrat'),
                  body1: TextStyle(
                      color: Colors.white,
                      fontSize: 20,
                      fontWeight: FontWeight.bold))),
          buttonTheme: ButtonThemeData(textTheme: ButtonTextTheme.normal),
          chipTheme: ChipThemeData(
            backgroundColor: Color(0xFF0984e3),
            brightness: Brightness.light,
            disabledColor: Colors.black,
            padding: EdgeInsets.all(5.0),
            labelPadding: EdgeInsets.all(3.0),
            selectedColor: Colors.black,
            shape: StadiumBorder(side: BorderSide.none),
            secondarySelectedColor: Colors.black,
            labelStyle: TextStyle(color: Colors.white),
            secondaryLabelStyle: TextStyle(color: Colors.white),
          )),
      darkTheme: ThemeData(
          canvasColor: Colors.transparent,
          brightness: Brightness.dark,
          fontFamily: 'Montserrat',
          backgroundColor: Colors.black,
          primaryColor: Colors.black,
          scaffoldBackgroundColor: Color(0xFF1e272e),
          inputDecorationTheme: InputDecorationTheme(
            focusColor: Colors.white,
            fillColor: Colors.white,
            hoverColor: Colors.white,
          ),
          textTheme: TextTheme(
            title: TextStyle(
              color: Colors.white,
            ),
          ),
          appBarTheme: AppBarTheme(
              elevation: 0,
              iconTheme: IconThemeData(
                color: Colors.white,
              ),
              color: Color(0xFF1e272e),
              textTheme: TextTheme(
                  title: TextStyle(
                      color: Colors.white,
                      fontSize: 20,
                      fontWeight: FontWeight.bold,
                      fontFamily: 'Montserrat'),
                  body1: TextStyle(
                      color: Colors.black,
                      fontSize: 20,
                      fontWeight: FontWeight.bold))),
          floatingActionButtonTheme: FloatingActionButtonThemeData(
            backgroundColor: Colors.white,
            splashColor: Colors.black,
            focusColor: Colors.black,
          ),
          iconTheme: IconThemeData(
            color: Colors.white,
          ),
          chipTheme: ChipThemeData(
            backgroundColor: Colors.white,
            brightness: Brightness.dark,
            disabledColor: Colors.black,
            padding: EdgeInsets.all(5.0),
            labelPadding: EdgeInsets.all(3.0),
            selectedColor: Colors.black,
            shape: StadiumBorder(side: BorderSide.none),
            secondarySelectedColor: Colors.black,
            labelStyle: TextStyle(color: Colors.black),
            secondaryLabelStyle: TextStyle(color: Colors.white),
          )),
    );
  }
}
