import 'package:amchat/global/url.dart';
import 'package:shared_preferences/shared_preferences.dart';

class Preferencias {
  static final Preferencias _instancia = new Preferencias._internal();

  factory Preferencias() {
    return _instancia;
  }

  Preferencias._internal();

  SharedPreferences _prefs;

  initPrefs() async {
    SharedPreferences.setMockInitialValues({});
    _prefs = await SharedPreferences.getInstance();
  }

  String get email {
    return _prefs.getString('email') ?? "";
  }

  set email(String email) {
    _prefs.setString('email', email);
  }

  String get foto {
    String img = _prefs.getString("foto");
    return 'http://$globalUrl/images/clientes/$img' ??
        "https://patelgroup.co/wp-content/uploads/2019/09/no-user-1.png";
  }

  set foto(String foto) {
    _prefs.setString('foto', foto);
  }

  String get id {
    return _prefs.getString('id') ?? "";
  }

  set id(String id) {
    _prefs.setString('id', id);
  }

  String get nombre {
    return _prefs.getString('nombre') ?? "";
  }

  set nombre(String nombre) {
    _prefs.setString('nombre', nombre);
  }

  String get pass {
    return _prefs.getString('pass') ?? "";
  }

  set pass(String pass) {
    _prefs.setString('pass', pass);
  }

  String get token {
    return _prefs.getString('token') ?? "";
  }

  set token(String token) {
    _prefs.setString('token', token);
  }

  String get deviceToken {
    return _prefs.getString('deviceToken') ?? "";
  }

  set deviceToken(String deviceToken) {
    _prefs.setString('deviceToken', deviceToken);
  }
}
