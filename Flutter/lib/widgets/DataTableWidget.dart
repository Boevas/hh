import 'package:flutter/material.dart';
import 'package:flutter/rendering.dart';
import 'package:test6/sqlite/Sqlite.dart';
import 'package:test6/widgets/DataTablePdfWidget.dart';

class DataColumnEx {
  bool show;
  String label;
  String tooltip;
  bool numeric;
  bool editable;
  TextInputType editableTextInputType;
  //double width;
  TextAlign align;
  //double fontSize;
  DataColumnEx({
    required this.show,
    this.label = "",
    this.tooltip = "",
    this.numeric = false,
    this.editable = false,
    this.editableTextInputType = TextInputType.text,
    //this.width = 100,
    this.align = TextAlign.left,
    //this.fontSize = 20,
  });
}

class DataTableWidget extends StatefulWidget {
  final String dbName;
  final String viewSqlScript;
  final Map<String, DataColumnEx> columns;
  //final double rowHeight;
  final String? updateTableName;
  final String? updateTablePK;

  DataTableWidget({
    required this.dbName,
    required this.viewSqlScript,
    required this.columns,
    //required this.rowHeight,
    this.updateTableName,
    this.updateTablePK,
  });

  @override
  _DataTableWidgetState createState() => _DataTableWidgetState();
}

class _DataTableWidgetState extends State<DataTableWidget> {
  bool _sortAscending = true;
  int _sortColumnIndex = 0;
  late List<Map<String, dynamic>> _snapshot;
  TextEditingController sqlController = new TextEditingController(text: "");
  late DataTable dt;

  @override
  void initState() {
    super.initState();

    sqlController.text = widget.viewSqlScript;
  }

  List<Map<String, dynamic>> _getsnapshot() {
    return _snapshot;
  }

  _setsnapshot(AsyncSnapshot<List<Map<String, dynamic>>> snapshot) {
    _snapshot = List<Map<String, dynamic>>.from(snapshot.requireData).toList();
  }

  _sort() {
    String columnName = _getsnapshot().first.keys.elementAt(_sortColumnIndex);
    if (_sortAscending)
      _getsnapshot().sort((a, b) => (b[columnName]).compareTo(a[columnName]));
    else
      _getsnapshot().sort((a, b) => (a[columnName]).compareTo(b[columnName]));
  }

  DataColumnEx getDataColumnEx(String columnName) {
    DataColumnEx? col = widget.columns[columnName];

    return col != null
        ? col
        : DataColumnEx(
            show: true,
            label: columnName,
            tooltip: columnName,
            numeric: false,
            editable: false,
            editableTextInputType: TextInputType.name);
  }

  List<DataColumn> _getDataColumn() {
    List<DataColumn> listDataColumn = <DataColumn>[];

    for (var i = 0; i < _getsnapshot()[0].keys.length; i++) {
      String columnName = _getsnapshot()[0].keys.elementAt(i).toString();

      DataColumnEx column = getDataColumnEx(columnName);

      if (false == column.show) continue;

      if ("DELETE" == columnName) {
        listDataColumn.add(DataColumn(
            tooltip: column.tooltip,
            label: Expanded(
                child: Text(
              column.label,
              textAlign: TextAlign.center,
              //style: TextStyle(fontSize: column.fontSize),
            ))));
      } else {
        listDataColumn.add(DataColumn(
            onSort: (columnIndex, ascending) {
              setState(() {
                _sortColumnIndex = columnIndex;
                _sortAscending = ascending;
              });
            },
            tooltip: column.tooltip,
            label: Expanded(
                child: Text(
              column.label,
              textAlign: TextAlign.center,
              //style: TextStyle(fontSize: column.fontSize),
            ))));
      }
    }
    return listDataColumn;
  }

  List<DataRow> _getDataRow() {
    List<DataRow> listDataRow = <DataRow>[];

    for (var rowIdx = 0; rowIdx < _getsnapshot().length; rowIdx++) {
      List<DataCell> ldc = <DataCell>[];
      for (var ceilIdx = 0;
          ceilIdx < _getsnapshot()[0].keys.length;
          ceilIdx++) {
        String columnName =
            _getsnapshot().first.keys.elementAt(ceilIdx).toString();

        DataColumnEx column = getDataColumnEx(columnName);
        if (false == column.show) continue;

        String ceilText = _getsnapshot()[rowIdx][columnName].toString();

        TextEditingController tec = TextEditingController(text: ceilText);
        if ("DELETE" == columnName) {
          if ("1" != ceilText) continue;

          ldc.add(DataCell(Container(
              decoration: BoxDecoration(border: Border.all(width: 1)),
              alignment: Alignment.center,
              //width: column.width,
              child: IconButton(
                icon: Icon(Icons.delete),
                //iconSize: widget.rowHeight,
                tooltip: column.tooltip,
                onPressed: () {
                  String updateSql =
                      'DELETE FROM ${widget.updateTableName} WHERE ${widget.updateTablePK}=${_getsnapshot()[rowIdx][widget.updateTablePK].toString()}';
                  SqliteUtils(widget.dbName).executeNonQuery(updateSql);
                  setState(() {});
                },
              ))));
        } else {
          ldc.add(column.editable
              ? DataCell(
                  Container(
                    decoration: BoxDecoration(
                      border: Border.all(width: 1),
                    ),
                    alignment: Alignment.center,
                    //width: column.width,
                    child: TextField(
                      decoration: InputDecoration(
                        hintText: "Enter a message",
                        suffixIcon: IconButton(
                          onPressed: () => tec.clear(),
                          icon: Icon(Icons.clear),
                        ),
                      ),
                      textAlign: column.align,
                      //style: TextStyle(fontSize: column.fontSize),
                      controller: tec,
                      //initialValue: ceilText,
                      keyboardType: column.editableTextInputType,
                      onSubmitted: (val) {
                        if (false == column.numeric) val = "'" + val + "'";
                        String updateSql =
                            'UPDATE ${widget.updateTableName} SET ${_getsnapshot().first.keys.elementAt(ceilIdx).toString()}=${val.toString()} WHERE ${widget.updateTablePK}=${_getsnapshot()[rowIdx][widget.updateTablePK].toString()}';

                        SqliteUtils(widget.dbName).executeNonQuery(updateSql);
                        //_getsnapshot()[rowIdx][columnName] = val;
                      },
                    ),
                  ),
                  //showEditIcon: true,
                )
              : DataCell(Container(
                  decoration: BoxDecoration(border: Border.all(width: 1)),
                  alignment: Alignment.center,
                  //width: column.width,
                  child: Text(
                    ceilText,
                    textAlign: column.align,
                    //style: TextStyle(fontSize: column.fontSize),
                  ),
                )));
        }
      }

      listDataRow.add(DataRow(
        // color: MaterialStateColor.resolveWith((states) {
        //   return rowIdx % 2 == 0 ? Colors.red : Colors.black; //make tha magic!
        // }),
        // color: MaterialStateProperty.resolveWith<Color>(
        //     (Set<MaterialState> states) {
        //   if (states.contains(MaterialState.selected))
        //     return Theme.of(context).colorScheme.primary.withOpacity(0.58);
        //   return null; // Use the default value.
        // }),
        // selected: true,
        // onSelectChanged: (value) {
        //   setState(() {});
        // },
        cells: ldc,
      ));
    }
    return listDataRow;
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
        appBar: AppBar(
          title: Text("Page2"),
        ),
        body: new Container(
          //alignment: Alignment.center,
          child: Column(
            children: <Widget>[
              TextField(
                textAlign: TextAlign.left,
                controller: sqlController,
                decoration: new InputDecoration(
                  helperText: "SQL Script",
                ),
              ),
              SizedBox(
                height: 10,
                width: 10,
              ),
              getdatatable(context),
            ],
          ),
        ),
        floatingActionButtonLocation: FloatingActionButtonLocation.centerDocked,
        floatingActionButton: Padding(
          padding: const EdgeInsets.all(12.0),
          child: Row(
            mainAxisAlignment: MainAxisAlignment.end,
            children: <Widget>[
              FloatingActionButton(
                heroTag: "btn1",
                onPressed: () {
                  showDialog(
                      context: context,
                      builder: (_) => DataTablePdfWidget(
                            dt: dt,
                          ));
                },
                child: Icon(Icons.picture_as_pdf_outlined),
                tooltip: 'Экспорт в Pdf"',
              ),
              FloatingActionButton(
                heroTag: "btn2",
                onPressed: () {
                  setState(() {});
                },
                tooltip: 'Test',
                child: Icon(Icons.square_foot_sharp),
              )
            ],
          ),
        ));
  }

  Widget getdatatable(BuildContext context) {
    final sizemonitor = MediaQuery.of(context).size;
    return Expanded(
      child: SingleChildScrollView(
        scrollDirection: Axis.vertical,
        child: SingleChildScrollView(
          scrollDirection: Axis.horizontal,
          child: Container(
            width: sizemonitor.width,
            //height: sizemonitor.height,
            child: FittedBox(
              fit: BoxFit.fitWidth,
              child: FutureBuilder<List<Map<String, dynamic>>>(
                future: SqliteUtils(widget.dbName)
                    .executeReader(sqlController.text),
                builder: (BuildContext c,
                    AsyncSnapshot<List<Map<String, dynamic>>> aspsh) {
                  debugPrint("build");

                  if (!aspsh.hasData)
                    return Column(
                      crossAxisAlignment: CrossAxisAlignment.center,
                      mainAxisAlignment: MainAxisAlignment.center,
                      children: <Widget>[
                        Center(
                          child: Container(
                            height: 20,
                            width: 20,
                            margin: EdgeInsets.all(5),
                            child: CircularProgressIndicator(
                              strokeWidth: 2.0,
                              valueColor: AlwaysStoppedAnimation(Colors.green),
                            ),
                          ),
                        ),
                      ],
                    );

                  _setsnapshot(aspsh);
                  //sleep(Duration(seconds: 5));
                  _sort();

                  dt = DataTable(
                    horizontalMargin: 5,
                    columnSpacing: 0,
                    //dataRowHeight: widget.rowHeight,
                    //headingRowHeight: widget.rowHeight,
                    //headingRowColor:
                    //    MaterialStateColor.resolveWith((states) => Colors.blue),
                    sortAscending: _sortAscending,
                    sortColumnIndex: _sortColumnIndex,
                    showCheckboxColumn: false,
                    columns: _getDataColumn(),
                    rows: _getDataRow(),
                  );
                  return dt;
                },
              ),
            ),
          ),
        ),
      ),
    );
  }
}
