using System.Collections.Concurrent;
using System.Diagnostics;
using TracerLibrary.Model;

namespace TracerLibrary.Logic
{

    public class MethodTracer
    {
        public Stopwatch Stopwatch { get; } = new();
        public ConcurrentStack<MethodInfo> Methods { get; } = new();

        public void AddMethod(MethodInfo methodInfo)
        {
            Methods.Push(methodInfo);
        }
    }
}