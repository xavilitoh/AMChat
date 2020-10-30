import 'package:amchat/blocs/provider.dart';
import 'package:amchat/models/chatRoom.dart';
import 'package:amchat/models/mensaje.dart';
import 'package:amchat/models/preferencias.dart';
import 'package:amchat/providers/chatProvider.dart';
import 'package:amchat/widget/chattingTile.dart';
import 'package:flutter/material.dart';
import 'package:signalr_client/hub_connection.dart';

class ChatPage extends StatefulWidget {
  ChatPage({Key key}) : super(key: key);

  @override
  _ChatPageState createState() => _ChatPageState();
}

class _ChatPageState extends State<ChatPage> {
  final controller = new TextEditingController();
  final p = Preferencias();
  HubConnection signalR;
  ChatRoom chat;
  ScrollController _scrollController = new ScrollController();

  @override
  void dispose() {
    signalR.off("${1}MessagesLeidos");

    controller.dispose();

    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    final signalR = Provider.signalROf(context).hub;

    signalR.off("${1}");

    signalR.on("${1}", newMessage);

    ChatProvider up = new ChatProvider();
    return Container(
        child: FutureBuilder(
            future: up.getChat(context),
            builder: (BuildContext context, AsyncSnapshot<ChatRoom> snapshot) {
              if (snapshot.hasData) {
                return Scaffold(
                  resizeToAvoidBottomPadding: true,
                  resizeToAvoidBottomInset: true,
                  floatingActionButtonLocation:
                      FloatingActionButtonLocation.centerFloat,
                  floatingActionButton: Container(
                    padding: EdgeInsets.symmetric(horizontal: 30),
                    child: Container(
                      decoration: BoxDecoration(
                          color: Color(0xffF4F5FA),
                          borderRadius: BorderRadius.horizontal(
                              right: Radius.circular(40),
                              left: Radius.circular(40))),
                      padding: EdgeInsets.only(
                          left: MediaQuery.of(context).size.width * 0.03,
                          right: MediaQuery.of(context).size.width * 0.00),
                      child: Row(
                        mainAxisAlignment: MainAxisAlignment.spaceBetween,
                        children: <Widget>[
                          Expanded(
                            child: TextField(
                              style: TextStyle(color: Colors.black),
                              cursorColor: Theme.of(context).primaryColor,
                              controller: controller,
                              decoration: InputDecoration(
                                  hintStyle: TextStyle(color: Colors.grey),
                                  hintText: 'Escribe tu Mensaje....',
                                  border: UnderlineInputBorder(
                                      borderSide: BorderSide.none)),
                            ),
                          ),
                          CircleAvatar(
                            child: IconButton(
                              onPressed: () {
                                if (controller.text != null &&
                                    controller.text != "") {
                                  signalR.invoke("SendMessage", args: <Object>[
                                    p.id,
                                    controller.text,
                                    snapshot.data.chatId
                                  ]);
                                }

                                controller.text = "";
                              },
                              icon: Icon(Icons.send),
                            ),
                          )
                        ],
                      ),
                    ),
                  ),
                  appBar: AppBar(
                    leading: Container(
                      padding: EdgeInsets.all(6),
                      child: CircleAvatar(
                        radius: 10,
                        child: Text(snapshot.data.user[0]),
                      ),
                    ),
                    title: Text(snapshot.data.user),
                  ),
                  body: StreamBuilder(
                    stream: Provider.mensajesOf(context).mensajesStream,
                    builder: (context, AsyncSnapshot<List<Mensaje>> snapshot) {
                      if (snapshot.hasData) {
                        return Container(
                          margin: EdgeInsets.only(
                              bottom: MediaQuery.of(context).size.height * 0.1),
                          child: ListView.builder(
                              controller: _scrollController,
                              padding: EdgeInsets.only(
                                  right: 10.0,
                                  left: 10.0,
                                  bottom: 10.0,
                                  top: 10.0),
                              itemCount: snapshot.data.length,
                              itemBuilder: (BuildContext context, int index) {
                                return ChattingTile(
                                  isByMe: p.id == snapshot.data[index].emisorId,
                                  message: snapshot.data[index].message,
                                  emisor: snapshot.data[index].emisor.fullName,
                                  fecha: snapshot.data[index].fecha,
                                );
                              }),
                        );
                      } else {
                        return CircularProgressIndicator();
                      }
                    },
                  ),
                );
              } else {
                return Scaffold(
                  appBar: AppBar(
                    centerTitle: true,
                    title: Text('Cargando'),
                  ),
                  body: Container(
                    padding: EdgeInsets.symmetric(
                        vertical: MediaQuery.of(context).size.height * 0.4,
                        horizontal: MediaQuery.of(context).size.width * 0.4),
                    child: CircularProgressIndicator(),
                  ),
                );
              }
            }));
  }

  void newMessage(List<Object> arguments) {
    var mensaje = Mensaje.fromJson(arguments[0]);

    var msj = Provider.mensajesOf(context).mensajes;

    msj.add(mensaje);

    Provider.mensajesOf(context).setMensajes = msj;

    Future.delayed(const Duration(milliseconds: 200), () {
      // Here you can write your code
      _scrollController.animateTo(
          _scrollController.position.maxScrollExtent + 1,
          duration: const Duration(milliseconds: 300),
          curve: Curves.easeOut);
      setState(() {
        // Here you can write your code for open new view
      });
    });
  }
}
