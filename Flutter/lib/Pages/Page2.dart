import 'package:flutter/material.dart';
import 'package:test6/widgets/DataTableWidget.dart';

class Page2 extends StatefulWidget {
  @override
  _Page2State createState() => _Page2State();
}

class _Page2State extends State<Page2> {
  @override
  Widget build(BuildContext context) {
    Map<String, DataColumnEx> dc = {
      'ID': DataColumnEx(
        show: true,
        label: "ИД",
        numeric: true,
        tooltip: "ИД tooltip",
        editable: false,
      ),
      //'ID_TYPES': DataColumnEx(show: true, editable: false),
      'NAME': DataColumnEx(
          show: true,
          label: "Название",
          numeric: false,
          tooltip: "Название tooltip",
          editable: true,
          editableTextInputType: TextInputType.text),
      'DATE': DataColumnEx(
          show: true,
          label: "Дата",
          numeric: false,
          tooltip: "Дата tooltip",
          editable: false,
          editableTextInputType: TextInputType.datetime),
    };
    return DataTableWidget(
      dbName: "130",
      viewSqlScript: "SELECT * FROM OBJECTS LIMIT 1000;",
      //rowHeight: 100,
      columns: dc,
      updateTableName: "OBJECTS",
      updateTablePK: "ID",
    );
  }
}
