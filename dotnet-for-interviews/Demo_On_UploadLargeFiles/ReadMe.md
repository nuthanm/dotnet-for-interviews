# Options to handle Large uploaded file

## Brief
Streams are better than **byte array and memory stream** - When uploading a large files

**Advantages:**

1. With Streams reduces memory usage a lot because it process the file in blocks enabling more efficient memory management.
   but With byte[] or MemoryStream it reads an entire file loaded into memory before processing
   - This causing slow ness or performance issues or memory errors.

2. With Streams, processing speed by allowing for simoultaneously reading and writing of the file.
   Means, the application can start processing the file faster and finish more efficiently.

4. Direct streaming of large files over the network
   Streams transmit data in blocks, reducing latency and improving tranmsission perf.

We can acheive this using two ways,


### Option 1: Set globally in Program.cs/Starup.cs file

```
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.Limits.MaxRequestBodySize = long.MaxValue;
});

```

### Option 2: Set at controller level

```
[RequestSizeLimit(<sizeinbytes>)]

```