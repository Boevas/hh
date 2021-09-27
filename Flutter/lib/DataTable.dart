/*
class DataTable {
  List<Map<String, dynamic>> _data;
  DataTable(List<Map<String, dynamic>> data) {
    _data = data;
  }
  List<Map<String, dynamic>> _getData() {
    return _data;
  }
  void _setData(List<Map<String, dynamic>> data) {
    _data=data;
  }

  List<Map<String, dynamic>> _whereColumnValue(
       String column, String value) {
    return _getData().where((element) => element[column] == value.toString());
  }

  List<Map<String, dynamic>> _whereMapColumnValue(
       Map<String, String> whereConditions) {
    for (String column in whereConditions.keys) {
      _setData(
          _whereColumnValue(column, whereConditions[column].toString()));
    }

    return _getData();
  }

  void _applyWhereCondition(String where) 
  {
    List<String> res = where.split("AND"); 
    for(String value in res)
    { 
      List<String> resCondition;
      String columnName;
      String columnValue;

      resCondition = value.split("=");
      if(2 == resCondition.length)
      {
        columnName=resCondition.elementAt(0).trim();
        columnValue=resCondition.elementAt(1).trim();
        _setData(_getData().where((element) => element[columnName].toString()==columnValue));
        continue;
      }

      resCondition = value.split("!=");
      if(2 == resCondition.length)
      {
        columnName=resCondition.elementAt(0).trim();
        columnValue=resCondition.elementAt(1).trim();
        _setData(_getData().where((element) => element[columnName].toString()!=columnValue));
        continue;
      }

      resCondition = value.split(">");
      if(2 == resCondition.length)
      {
        columnName=resCondition.elementAt(0).trim();
        columnValue=resCondition.elementAt(1).trim();
        _setData(_getData().where((element) => double.parse(element[columnName])>double.parse(columnValue)));
        continue;
      }

      resCondition = value.split("<");
      if(2 == resCondition.length)
      {
        columnName=resCondition.elementAt(0).trim();
        columnValue=resCondition.elementAt(1).trim();
        _setData(_getData().where((element) => double.parse(element[columnName])<double.parse(columnValue)));
        continue;
      }
      else
      {
        //TODO Error show
      }
    }
  }

  void _applyOrderBy(String orderBy) 
  {
    List<String> res = orderBy.split(","); 
    for(String value in res)
    { 
    }
  }
  List<Map<String, dynamic>> select(String where,String orderBy) 
  {
    _applyWhereCondition(where);
    _applyOrderBy(orderBy);
    return _getData();
  }
}
*/
