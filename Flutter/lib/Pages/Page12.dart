import 'package:flutter/material.dart';
import 'package:lazy_data_table/lazy_data_table.dart';
import 'package:test6/sqlite/Sqlite.dart';

class Page12 extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    final size = MediaQuery.of(context).size;
    final width = size.width;
    final height = size.height;
    return Container(
      width: width,
      height: height,
      child: FutureBuilder<List<Map<String, dynamic>>>(
        future:
            SqliteUtils("130").executeReader("SELECT * FROM DIAG LIMIT 1000;"),
        builder: (BuildContext c,
            AsyncSnapshot<List<Map<String, dynamic>>> snapshot) {
          debugPrint("build");

          if (!snapshot.hasData)
            return Center(child: CircularProgressIndicator());

          return Scaffold(
            appBar: AppBar(
              title: Text("Table"),
            ),
            body: LazyDataTable(
              rows: snapshot.requireData.length,
              columns: snapshot.requireData.first.keys.length,
              tableDimensions: LazyDataTableDimensions(
                cellHeight: 50,
                cellWidth: 100,
                topHeaderHeight: 50,
                leftHeaderWidth: 75,
              ),
              topHeaderBuilder: (i) => Center(
                  child: Text(
                      snapshot.requireData.first.keys.elementAt(i).toString())),
              leftHeaderBuilder: (i) => Center(child: Text("${i + 1}")),
              dataCellBuilder: (i, j) => Center(
                  child: Text(snapshot.requireData[i][snapshot
                          .requireData.first.keys
                          .elementAt(j)
                          .toString()]
                      .toString())),
              topLeftCornerWidget: Center(child: Text("№")),
            ),
          );
        },
      ),
    );
  }
}
