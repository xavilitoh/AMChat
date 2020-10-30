import 'dart:async';



class Validators {


  final validarEmail = StreamTransformer<String, String>.fromHandlers(
    handleData: ( email, sink ) {


      Pattern pattern = r'^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$';
      RegExp regExp   = new RegExp(pattern);

      if ( regExp.hasMatch( email ) ) {
        sink.add( email );
      } else {
        sink.addError('Email no es correcto');
      }

    }
  );


  final validarPassword = StreamTransformer<String, String>.fromHandlers(
    handleData: ( password, sink ) {

      Pattern pattern = r'^(?=.*\d)(?=.*[\u0021-\u002b\u003c-\u0040\u002E\u0027\u002D\u002F\u003A\u003B\u007B\u007C7E\u007C])(?=.*[A-Z])(?=.*[a-z])\S{8,1000}$';
      RegExp regExp   = new RegExp(pattern);

      if ( regExp.hasMatch( password ) ) {
        sink.add( password );
      } else {
        sink.addError('Contraseña no valida');
      }

    }
  );


}
