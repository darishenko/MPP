using System;
using System.IO;
using System.Threading;
using Application.Writer;
using TracerLibrary.Logic;
using TracerLibrary.Model;
using TracerLibrary.Serialization.Interface;
using TracerLibrary.Serialization.Serializer;

namespace ConsoleApp
{
    public class Program
    {
        private static void Main()
        {
            
            //TestEntryClass testEntryClass = new(tracer);
            //testEntryClass.TestEntryMethod();
            
            var program = new Program();
            var thread = new Thread(program.Method);
            var tracer = new MainTracer();
            
            var foo = new Foo(tracer);
            foo.MyMethod();
            thread.Start(tracer);
            thread.Join();

            TraceResult traceResult = tracer.GetTraceResult();
            ITraceSerializer xmlSerializer = new XmlTraceSerializer();
            ITraceSerializer jsonSerializer = new JsonTraceSerializer();
            
            IWriter consoleTraceResultPrinter = new ConsoleWriter(xmlSerializer);
            consoleTraceResultPrinter.Print(traceResult);
            consoleTraceResultPrinter.SetTraceSerializer(jsonSerializer);
            consoleTraceResultPrinter.Print(traceResult);
            
           // IWriter fileTraceResultPrinter = new FileWriter(xmlSerializer, "../../../../../ResultFiles/result");
           var projectDirectory = Directory.GetParent(Environment.CurrentDirectory)?.Parent!.FullName;
           var dataPath = Path.Combine(projectDirectory!, "result");
           IWriter fileTraceResultPrinter = new FileWriter(xmlSerializer, dataPath);
           fileTraceResultPrinter.Print(traceResult);
            fileTraceResultPrinter.SetTraceSerializer(jsonSerializer);
            fileTraceResultPrinter.Print(traceResult);
            Console.WriteLine(dataPath);
        }

        private void Method(object? o)
        {
            var tracer = (MainTracer) o!;
            tracer.StartTrace();
            Thread.Sleep(100);
            tracer.StopTrace();
        }
    }
    
    public class Foo
    {
        private Bar _bar;
        private ITracer _tracer;

        internal Foo(ITracer tracer)
        {
            _tracer = tracer;
            _bar = new Bar(_tracer);
        }

        public void MyMethod()
        {
            _tracer.StartTrace();
            _bar.InnerMethod();
            _tracer.StopTrace();
        }
    }

    public class Bar
    {
        private ITracer _tracer;

        internal Bar(ITracer tracer)
        {
            _tracer = tracer;
        }

        public void InnerMethod()
        {
            _tracer.StartTrace();
            Thread.Sleep(100);
            _tracer.StopTrace();
        }
    }
}