import 'package:flutter/material.dart';
import 'package:multi_charts/multi_charts.dart';

class Page3 extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    final size = MediaQuery.of(context).size;
    const minwidth = 350;
    const minheight = 350;

    final bool isvertical = size.height >= size.width;
    final double coef =
        isvertical ? size.width / size.height : size.height / size.width;

    double width = 0;
    if (size.width <= minwidth)
      width = size.width;
    else
      width = isvertical ? size.width : (size.width * coef) - 30;

    double height = 0;
    if (size.height <= minheight)
      height = size.height;
    else
      height =
          isvertical ? (size.height * coef) - 30 : (size.width * coef) - 30;

    return MaterialApp(
      title: 'Radar Chart Demo',
      theme: ThemeData(
        primarySwatch: Colors.blue,
      ),
      home: Scaffold(
        appBar: AppBar(
          title: Text("!!! Scroll Charts"),
        ),
        body: SingleChildScrollView(
          child: Row(
            //direction: Axis.horizontal,
            //spacing: 10.0, // gap between adjacent chips
            //runSpacing: 10.0, // gap between lines
            children: <Widget>[
              Expanded(
                flex: 1,
                child: Column(
                  children: [
                    RadarChart(
                      maxWidth: width,
                      maxHeight: height,
                      values: [1, 2, 4, 7, 9, 0, 6],
                      labels: [
                        "Label1",
                        "Label2",
                        "Label3",
                        "Label4",
                        "Label5",
                        "Label6",
                        "Label7",
                      ],
                      maxValue: 10,
                      fillColor: Colors.blue,
                      chartRadiusFactor: 0.7,
                    ),
                    PieChart(
                      maxWidth: width,
                      maxHeight: height,
                      values: [15, 10, 30, 25, 20],
                      labels: [
                        "Label1",
                        "Label2",
                        "Label3",
                        "Label4",
                        "Label5"
                      ],
                      sliceFillColors: [
                        Colors.blueAccent,
                        Colors.greenAccent,
                        Colors.pink,
                        Colors.orange,
                        Colors.red,
                      ],
                      animationDuration: Duration(milliseconds: 1500),
                      legendPosition: LegendPosition.Right,
                    ),
                    RadarChart(
                      maxWidth: width,
                      maxHeight: height,
                      values: [6, 0, 9, 7, 4, 2, 1],
                      labels: [
                        "Label11",
                        "Label12",
                        "Label13",
                        "Label14",
                        "Label15",
                        "Label16",
                        "Label17",
                      ],
                      maxValue: 10,
                      fillColor: Colors.blue,
                      chartRadiusFactor: 0.7,
                    ),
                    PieChart(
                      maxWidth: width,
                      maxHeight: height,
                      values: [15, 10, 30, 25, 20],
                      labels: [
                        "Label11",
                        "Label12",
                        "Label13",
                        "Label14",
                        "Label15"
                      ],
                      sliceFillColors: [
                        Colors.red,
                        Colors.orange,
                        Colors.pink,
                        Colors.greenAccent,
                        Colors.blueAccent,
                      ],
                      animationDuration: Duration(milliseconds: 1500),
                      legendPosition: LegendPosition.Right,
                    ),
                  ],
                ),
              ),
            ],
          ),
        ),
      ),
    );
  }
}
