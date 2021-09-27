import 'dart:io' as io;
import 'package:flutter/material.dart';
import 'package:wc_flutter_share/wc_flutter_share.dart';
import 'package:path/path.dart' as path;

class ShareFileWidget extends StatelessWidget {
  final String sharePopupTitle;
  final String subject;
  final String text;
  final String filepath;
  ShareFileWidget(
      {required this.sharePopupTitle,
      required this.subject,
      required this.text,
      required this.filepath});

  @override
  Widget build(BuildContext context) {
    return IconButton(
      icon: Icon(Icons.share),
      onPressed: () async {
        await new io.File(filepath).readAsBytes().then((buf) async {
          await WcFlutterShare.share(
              sharePopupTitle: sharePopupTitle,
              subject: subject,
              text: text,
              fileName: path.basename(filepath),
              mimeType: 'image/pdf',
              bytesOfFile: buf.buffer.asUint8List());
        });
      },
    );
  }
}
