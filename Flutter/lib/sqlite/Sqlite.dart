import 'dart:async';
import 'dart:io' as io;
import 'dart:typed_data';
import 'package:path/path.dart';
import 'package:flutter/services.dart';
import 'package:sqflite/sqflite.dart';

class Sqlite {
  static late Future<Database> fdb;
  static bool isInitDB = false;

  Sqlite(String fileName) {
    if (false == isInitDB) {
      {
        fdb = _init(fileName);
        isInitDB = true;
      }
    }
  }

  Future<Database> _init(String fileName) async {
    var dbDir = await getDatabasesPath();
    var dbPath = join(dbDir, fileName);

    if (false == await io.Directory(dbDir).exists())
      await io.Directory(dbDir).create();

    if (true == await io.File(dbPath).exists())
      return await openDatabase(dbPath); //return await deleteDatabase(dbPath);

    ByteData data = await rootBundle.load(join("assets/db", fileName));
    List<int> bytes =
        data.buffer.asUint8List(data.offsetInBytes, data.lengthInBytes);
    await io.File(dbPath).writeAsBytes(bytes);

    return await openDatabase(dbPath);
  }

  // Execute an SQL query with no return value
  Future execute(String sql) async {
    final Database db = await fdb;
    return await db.execute(sql);
  }

  /// Returns a list of rows that were found
  Future<List<Map<String, dynamic>>> rawQuery(String sql) async {
    final Database db = await fdb;
    return await db.rawQuery(sql);
  }
}

class SqliteUtils extends Sqlite {
  SqliteUtils(String fileName) : super(fileName);

  Future<List<Map<String, dynamic>>> executeReader(String sql) async {
    return await rawQuery(sql);
  }

  Future<Object> executeScalar(String sql) async {
    var result = await rawQuery(sql);
    return result.isNotEmpty ? result.first.values.first : Null;
  }

  Future executeNonQuery(String sql) async {
    return await execute(sql);
  }
}
