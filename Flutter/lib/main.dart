import 'package:flutter/material.dart';

//Pages
import 'Pages/Page1.dart';
import 'Pages/Page2.dart';
import 'Pages/Page3.dart';
import 'Pages/Page4.dart';
import 'Pages/Page5.dart';
import 'Pages/Page12.dart';

void main() {
  runApp(MyApp());
}

class MyApp extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'Flutter Start',
      theme: ThemeData(
        primarySwatch: Colors.blue,
      ),
      home: MyHomePage(),
    );
  }
}

class MyHomePage extends StatefulWidget {
  const MyHomePage({Key? key}) : super(key: key);

  @override
  _MyHomePageState createState() => _MyHomePageState();
}

class _MyHomePageState extends State<MyHomePage> {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        //leading: Image.asset("lib/images/SNAP.png"),
        title: Text("title"),
      ),
      drawer: Drawer(
        child: ListView(
          children: <Widget>[
            ListTile(
              leading: Image.asset("lib/icons/Page1.png"),
              title: Text('Page1'),
              onTap: () {
                Navigator.of(context).pop();
                Navigator.push(
                    context,
                    MaterialPageRoute(
                        builder: (BuildContext context) => Page1(
                              ChartDemoData1().getdata(),
                              animate: false,
                            )));
              },
            ),
            Divider(
              color: Colors.black,
              height: 5.0,
            ),
            ListTile(
              leading: Image.asset("lib/icons/Page2.png"),
              title: Text('Page2'),
              onTap: () {
                Navigator.of(context).pop();
                Navigator.push(
                    context,
                    MaterialPageRoute(
                        builder: (BuildContext context) => Page2()));
              },
            ),
            Divider(
              color: Colors.black,
              height: 5.0,
            ),
            ListTile(
              leading: Image.asset("lib/icons/Page3.png"),
              title: Text('Page3'),
              onTap: () {
                Navigator.of(context).pop();
                Navigator.push(
                    context,
                    MaterialPageRoute(
                        builder: (BuildContext context) => Page3()));
              },
            ),
            Divider(
              color: Colors.black,
              height: 5.0,
            ),
            ListTile(
              leading: Image.asset("lib/icons/Page4.png"),
              title: Text('Page4'),
              onTap: () {
                Navigator.of(context).pop();
                Navigator.push(
                    context,
                    MaterialPageRoute(
                        builder: (BuildContext context) => Page4()));
              },
            ),
            Divider(
              color: Colors.black,
              height: 5.0,
            ),
            ListTile(
              leading: Image.asset("lib/icons/Page5.png"),
              title: Text('Page5'),
              onTap: () {
                Navigator.of(context).pop();
                Navigator.push(
                    context,
                    MaterialPageRoute(
                        builder: (BuildContext context) => Page5("Page5")));
              },
            ),
            Divider(
              color: Colors.black,
              height: 5.0,
            ),
            ListTile(
              leading: Image.asset("lib/icons/Page12.png"),
              title: Text('Page12'),
              onTap: () {
                Navigator.of(context).pop();
                Navigator.push(
                    context,
                    MaterialPageRoute(
                        builder: (BuildContext context) => Page12()));
              },
            ),
          ],
        ),
      ),
      body: MaterialApp(
        debugShowCheckedModeBanner: false,
        home: Scaffold(
          backgroundColor: Colors.green[100],

          //body
          body: GridView.count(
            crossAxisCount: 3,
            children: <Widget>[
              InkResponse(
                containedInkWell: true,
                //highlightColor: Colors.red,
                splashColor: Colors.blue.withOpacity(0.9),
                //radius: 100,
                borderRadius: BorderRadius.circular(180.0),
                //customBorder: ,
                onTap: () {
                  //Navigator.of(context).pop();
                  Navigator.push(
                      context,
                      MaterialPageRoute(
                          builder: (BuildContext context) => Page1(
                                ChartDemoData1().getdata(),
                                animate: false,
                              )));
                },
                child: Container(
                    decoration: BoxDecoration(
                      shape: BoxShape.circle,
                      //border: Border.all(color: Colors.black, width: 1),
                    ),
                    child: Stack(children: <Widget>[
                      Center(child: Image.asset("lib/icons/Page1.png")),
                      Align(
                          alignment: Alignment.bottomCenter,
                          child: Text("Page1",
                              style: new TextStyle(fontSize: 50.0))),
                    ])),
              ),
              Card(
                elevation: 0,
                color: Colors.transparent,
                shape: RoundedRectangleBorder(
                  borderRadius: BorderRadius.circular(60.0),
                ),
                margin: EdgeInsets.all(8.0),
                child: InkWell(
                  onTap: () {
                    //Navigator.of(context).pop();
                    Navigator.push(
                        context,
                        MaterialPageRoute(
                            builder: (BuildContext context) => Page2()));
                  },
                  splashColor: Colors.green[600],
                  child: Center(
                    child: Column(
                      mainAxisSize: MainAxisSize.min,
                      children: <Widget>[
                        Image.asset("lib/icons/Page2.png"),
                        Text("Page2", style: new TextStyle(fontSize: 30.0))
                      ],
                    ),
                  ),
                ),
              ),
              Card(
                shape: RoundedRectangleBorder(
                  borderRadius: BorderRadius.circular(60.0),
                ),
                margin: EdgeInsets.all(8.0),
                child: InkWell(
                  onTap: () {
                    //Navigator.of(context).pop();
                    Navigator.push(
                        context,
                        MaterialPageRoute(
                            builder: (BuildContext context) => Page3()));
                  },
                  splashColor: Colors.green[600],
                  child: Center(
                    child: Column(
                      mainAxisSize: MainAxisSize.min,
                      children: <Widget>[
                        Image.asset("lib/icons/Page3.png"),
                        Text("Page3", style: new TextStyle(fontSize: 30.0))
                      ],
                    ),
                  ),
                ),
              ),
              Card(
                shape: RoundedRectangleBorder(
                  borderRadius: BorderRadius.circular(60.0),
                ),
                margin: EdgeInsets.all(8.0),
                child: InkWell(
                  onTap: () {
                    //Navigator.of(context).pop();
                    Navigator.push(
                        context,
                        MaterialPageRoute(
                            builder: (BuildContext context) => Page4()));
                  },
                  splashColor: Colors.green[600],
                  child: Center(
                    child: Column(
                      mainAxisSize: MainAxisSize.min,
                      children: <Widget>[
                        Image.asset("lib/icons/Page4.png"),
                        Text("Page4", style: new TextStyle(fontSize: 30.0))
                      ],
                    ),
                  ),
                ),
              ),
              Card(
                shape: RoundedRectangleBorder(
                  borderRadius: BorderRadius.circular(60.0),
                ),
                margin: EdgeInsets.all(8.0),
                child: InkWell(
                  onTap: () {
                    //Navigator.of(context).pop();
                    Navigator.push(
                        context,
                        MaterialPageRoute(
                            builder: (BuildContext context) => Page5("Page5")));
                  },
                  splashColor: Colors.green[600],
                  child: Center(
                    child: Column(
                      mainAxisSize: MainAxisSize.min,
                      children: <Widget>[
                        Image.asset("lib/icons/Page5.png"),
                        Text("Page5", style: new TextStyle(fontSize: 30.0))
                      ],
                    ),
                  ),
                ),
              ),
              Card(
                shape: RoundedRectangleBorder(
                  borderRadius: BorderRadius.circular(60.0),
                ),
                margin: EdgeInsets.all(8.0),
                child: InkWell(
                  onTap: () {
                    //Navigator.of(context).pop();
                    Navigator.push(
                        context,
                        MaterialPageRoute(
                            builder: (BuildContext context) => Page12()));
                  },
                  splashColor: Colors.green[600],
                  child: Center(
                    child: Column(
                      mainAxisSize: MainAxisSize.min,
                      children: <Widget>[
                        Image.asset("lib/icons/Page12.png"),
                        Text("Page12", style: new TextStyle(fontSize: 30.0))
                      ],
                    ),
                  ),
                ),
              ),
            ],
          ),
        ),
      ),
    );
  }
}
