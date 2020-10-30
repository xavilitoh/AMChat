

import 'package:flutter/material.dart';

final _icons = <String, IconData> {

 'person' : Icons.person,
 'account_balance_wallet': Icons.account_balance_wallet,
 'shopping_basket' : Icons.shopping_basket,
 'store' : Icons.store
};

Icon getIcon (String nombreIcon, Color color){
  return Icon(_icons[nombreIcon], color: color,);
}