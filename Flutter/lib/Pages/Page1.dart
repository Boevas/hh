/// No Axis Example
// EXCLUDE_FROM_GALLERY_DOCS_START
import 'dart:math';
// EXCLUDE_FROM_GALLERY_DOCS_END
import 'package:flutter/material.dart';
import 'package:charts_flutter/flutter.dart' as charts;

class OrdinalSales {
  final String year;
  final int sales;

  OrdinalSales(this.year, this.sales);
}

class ChartDemoData1 {
  List<charts.Series<OrdinalSales, String>> getdata() {
    List<OrdinalSales> globalSalesData = [
      new OrdinalSales('2014', new Random().nextInt(100) * 100),
      new OrdinalSales('2015', new Random().nextInt(100) * 100),
      new OrdinalSales('2016', new Random().nextInt(100) * 100),
      new OrdinalSales('2017', new Random().nextInt(100) * 100),
    ];

    return [
      new charts.Series<OrdinalSales, String>(
        id: 'Global Revenue',
        domainFn: (OrdinalSales sales, _) => sales.year,
        measureFn: (OrdinalSales sales, _) => sales.sales,
        data: globalSalesData,
      ),
    ];
  }
}

/// Example of hiding both axis.
class Page1 extends StatelessWidget {
  final List<charts.Series<dynamic, String>> seriesList;
  final bool animate;

  Page1(this.seriesList, {required this.animate});

  @override
  Widget build(BuildContext context) {
    return new charts.BarChart(
      seriesList,
      animate: animate,
      primaryMeasureAxis:
          new charts.NumericAxisSpec(renderSpec: new charts.NoneRenderSpec()),
      domainAxis: new charts.OrdinalAxisSpec(
          showAxisLine: true, renderSpec: new charts.NoneRenderSpec()),
    );
  }
}
