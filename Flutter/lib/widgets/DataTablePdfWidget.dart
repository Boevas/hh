import 'dart:io';
import 'dart:typed_data';

//import 'package:flutter/foundation.dart';
import 'package:flutter/material.dart';

import 'package:syncfusion_flutter_pdf/pdf.dart';
import 'package:printing/printing.dart';
import 'package:path_provider/path_provider.dart';
import 'package:flutter/services.dart' show rootBundle;

class DataTablePdfWidget extends StatefulWidget {
  final DataTable dt;
  DataTablePdfWidget({required this.dt});

  @override
  _DataTablePdfWidget createState() => _DataTablePdfWidget();
}

class _DataTablePdfWidget extends State<DataTablePdfWidget> {
  Future<File> generatepdf() async {
    PdfDocument doc = PdfDocument();

    ByteData byteData = await rootBundle.load('assets/fonts/Roboto-Light.ttf');
    Uint8List fontData = byteData.buffer
        .asUint8List(byteData.offsetInBytes, byteData.lengthInBytes);

    //Create a PDF true type font object.
    final PdfFont font = PdfTrueTypeFont(fontData, 12);

//Create a PdfGrid class
    PdfGrid grid = PdfGrid();

//Add the columns to the grid
    grid.columns.add(
      count: widget.dt.columns.length,
    );

//Add header to the grid
    grid.headers.add(1);

//Add the rows-clumns to the grid
    PdfGridRow header = grid.headers[0];
    {
      for (var i = 0; i < widget.dt.columns.length; i++) {
        Text textColumn =
            (((widget.dt.columns[i].label) as Expanded).child as Text);

        header.cells[i].style.stringFormat = PdfStringFormat(
          alignment: PdfTextAlignment.center,
        );

        if (textColumn.textAlign == TextAlign.left)
          header.cells[i].style.stringFormat!.alignment = PdfTextAlignment.left;
        else if (textColumn.textAlign == TextAlign.center)
          header.cells[i].style.stringFormat!.alignment =
              PdfTextAlignment.center;
        else if (textColumn.textAlign == TextAlign.right)
          header.cells[i].style.stringFormat!.alignment =
              PdfTextAlignment.right;

        header.cells[i].value = textColumn.data.toString();
      }
    }

//Add rows to grid
    {
      for (var i = 0; i < widget.dt.rows.length; i++) {
        PdfGridRow row = grid.rows.add();
        for (var j = 0; j < widget.dt.rows[i].cells.length; j++) {
          Container c = widget.dt.rows[i].cells[j].child as Container;
          if (c.child is IconButton) continue;

          TextAlign? textAlignRowCell;
          if (c.child is TextField) {
            row.cells[j].value =
                (c.child as TextField).controller!.text.toString();
            textAlignRowCell = (c.child as TextField).textAlign;
            //debugPrint('cellValue:$cellValue');
          }

          if (c.child is Text) {
            row.cells[j].value = (c.child as Text).data.toString();
            textAlignRowCell = (c.child as Text).textAlign;
          }

          row.cells[j].style.stringFormat = PdfStringFormat(
            alignment: PdfTextAlignment.center,
          );

          if (textAlignRowCell == TextAlign.left)
            row.cells[j].style.stringFormat!.alignment = PdfTextAlignment.left;
          else if (textAlignRowCell == TextAlign.center)
            row.cells[j].style.stringFormat!.alignment =
                PdfTextAlignment.center;
          else if (textAlignRowCell == TextAlign.right)
            row.cells[j].style.stringFormat!.alignment = PdfTextAlignment.right;
        }
      }
    }
//Set the grid style
    grid.style = PdfGridStyle(
        cellPadding: PdfPaddings(left: 2, right: 3, top: 4, bottom: 5),
        //backgroundBrush: PdfBrushes.blue,
        //textBrush: PdfBrushes.white,

        font: font);

//Draw the grid
    grid.draw(page: doc.pages.add(), bounds: const Rect.fromLTWH(0, 0, 0, 0));

//Save and dispose the PDF document
    final tempDir = await getTemporaryDirectory();

    final File f = File('${tempDir.path}/SampleOutput.pdf');
    Future<File> res = f.writeAsBytes(doc.save());
    await res;

    doc.dispose();

    return res;
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
        appBar: AppBar(title: Text("title")),
        body: Center(
          child: PdfPreview(
            build: (format) =>
                generatepdf().then((value) => value.readAsBytes()),
          ),
        ));
  }
}
