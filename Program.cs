#define RZ

using System.Diagnostics;

#if RZ
const string mod = "resize";
#else 
const string mod = "new";
#endif

Console.WriteLine($"waiting..");
Thread.Sleep(5000);

byte[] buffer = new byte[1024];
int count = 1000000;

Console.WriteLine($"mod: \t\t\t{mod}");
Console.WriteLine($"start buffer size: \t{buffer.Length}");
Console.WriteLine($"repetitions: \t\t{count}");

Stopwatch stopwatch = new();

stopwatch.Start();
#if RZ
ComputeResize(ref buffer, count);
#else 
ComputeNew(ref buffer, count);
#endif
stopwatch.Stop();

Console.WriteLine($"run time: \t\t{stopwatch.ElapsedTicks * 100}ns, {stopwatch.ElapsedMilliseconds}ms");
var mem = GC.GetTotalMemory(true);
Console.WriteLine($"memory usage: \t\t{mem}bytes, {mem / 1024}kb");


void ComputeResize(ref byte[] buffer, int count) {
    var cts = buffer.Length;
    for (int i = 0; i < count; i++)
        Array.Resize(ref buffer, cts++);
}

void ComputeNew(ref byte[] buffer, int count) {
    var cts = buffer.Length;
    for (int i = 0; i < count; i++)
        buffer = new byte[cts++];
}