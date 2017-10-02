# Logger
C# Library for writing output to a file

```
// Create the log object
Log log = new Log(@"D:\log.txt");
```
```
// Write a blank line
log.WriteLine();
```
// Write a Line, with the default Log type
log.WriteLine("Hello World!");
```
// Write a Line, with the default Log type & include the username
log.WriteLine("Hello World!", true);
```
// Bellow is an example using the Log Type info
log.WriteLine("Hello World!", Log.Type.info);
```
// This can also be used with the user param
log.WriteLine("Hello World!", true, Log.Type.info);
```

// Bellow are the other Types
```
log.WriteLine("Hello World!", Log.Type.info);

log.WriteLine("Hello World!", Log.Type.warn);

log.WriteLine("Hello World!", Log.Type.error);

log.WriteLine("Hello World!", Log.Type.debug);

log.WriteLine("Hello World!", Log.Type.verbose);
```

OUTPUT:
```
[16:58:17 01/10/2017][debug] - Hello World!

[16:58:17 01/10/2017][debug][Jay] - Hello World!

[16:58:17 01/10/2017][info] - Hello World!

[16:58:17 01/10/2017][info][Jay] - Hello World!

[16:58:17 01/10/2017][info] - Hello World!

[16:58:17 01/10/2017][warn] - Hello World!

[16:58:17 01/10/2017][error] - Hello World!

[16:58:17 01/10/2017][debug] - Hello World!

[16:58:17 01/10/2017][verbose] - Hello World!
```
