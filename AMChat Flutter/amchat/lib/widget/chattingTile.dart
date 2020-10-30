import 'package:flutter/material.dart';

class ChattingTile extends StatelessWidget {
  final bool isByMe;
  final String message;
  final DateTime fecha;
  final String emisor;

  ChattingTile(
      {@required this.isByMe, @required this.message, this.fecha, this.emisor});

  @override
  Widget build(BuildContext context) {
    return Container(
      margin: EdgeInsets.only(bottom: 20.0),
      child: Row(
          mainAxisAlignment:
              isByMe ? MainAxisAlignment.end : MainAxisAlignment.start,
          crossAxisAlignment: CrossAxisAlignment.start,
          children: <Widget>[
            !isByMe
                ? Container(
                    margin: EdgeInsets.only(right: 15.0),
                    child: CircleAvatar(
                      child: Text(
                        emisor[0],
                        style: TextStyle(color: Colors.white),
                      ),
                      backgroundColor: Colors.black38,
                    ),
                  )
                : Container(),
            Container(
              decoration: BoxDecoration(
                  color:
                      isByMe ? Theme.of(context).primaryColor : Colors.black38,
                  borderRadius: BorderRadius.all(Radius.circular(20))),
              padding: EdgeInsets.symmetric(vertical: 6, horizontal: 20),
              child: Container(
                constraints: BoxConstraints(
                    maxWidth: MediaQuery.of(context).size.width * 2 / 3),
                child: Row(
                  crossAxisAlignment: CrossAxisAlignment.end,
                  mainAxisAlignment: MainAxisAlignment.spaceBetween,
                  children: <Widget>[
                    isByMe
                        ? Text(
                            '${fecha.toLocal().hour}:${fecha.toLocal().minute}',
                            style: TextStyle(
                                color: Colors.white,
                                fontSize: 10,
                                fontWeight: FontWeight.w500),
                          )
                        : Container(),
                    Column(
                      crossAxisAlignment: !isByMe
                          ? CrossAxisAlignment.start
                          : CrossAxisAlignment.end,
                      children: <Widget>[
                        Text(
                          emisor,
                          style: TextStyle(
                              color: Colors.white,
                              fontSize: 17,
                              fontWeight: FontWeight.bold),
                          overflow: TextOverflow.clip,
                        ),
                        Container(
                          alignment: isByMe
                              ? Alignment.centerRight
                              : Alignment.centerLeft,
                          width: MediaQuery.of(context).size.width * 0.6,
                          child: Text(
                            message,
                            style: TextStyle(
                                color: Colors.white,
                                fontSize: 17,
                                fontWeight: FontWeight.w500),
                            overflow: TextOverflow.clip,
                          ),
                        ),
                      ],
                    ),
                    !isByMe
                        ? Text(
                            '${fecha.toLocal().hour}:${fecha.toLocal().minute}',
                            style: TextStyle(
                                color: Colors.white,
                                fontSize: 10,
                                fontWeight: FontWeight.w500),
                          )
                        : Container(),
                  ],
                ),
              ),
            ),
            isByMe
                ? Container(
                    margin: EdgeInsets.only(left: 15.0),
                    child: CircleAvatar(child: Text(emisor[0])),
                  )
                : Container()
          ]),
    );
  }
}
