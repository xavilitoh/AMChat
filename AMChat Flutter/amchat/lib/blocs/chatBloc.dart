import 'package:amchat/models/chatRoom.dart';
import 'package:rxdart/rxdart.dart';

class ChatBloc {
  final _chatController = BehaviorSubject<ChatRoom>();

  Stream<ChatRoom> get chatStream => _chatController.stream.asBroadcastStream();

  set setChat(ChatRoom lista) {
    _chatController.sink.add(lista);
  }

  ChatRoom get chat => _chatController.value;

  dispose() {
    _chatController?.close();
  }
}
