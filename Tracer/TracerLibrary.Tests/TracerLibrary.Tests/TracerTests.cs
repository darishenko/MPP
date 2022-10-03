using System.Collections.Generic;
using System.Threading;
using TracerLibrary.Logic;
using Xunit;

namespace TracerLibrary.Tests
{
    public class TracerTests
    {
        [Fact]
        public void GetTraceResult_ThreadsTraceIsEmpty_ReturnedTraceResultWithEmptyThreadInfos()
        {
            var tracer = new MainTracer();

            var result = tracer.GetTraceResult();
            Assert.Empty(result.ThreadsInfo);
        }

        [Fact]
        public void GetTraceResult_OneMethodInOneThread_ReturnedTraceResultWithOneMethodInOneThread()
        {
            var tracer = new MainTracer();
            var testClass = new TestClass(tracer);
            testClass.TestMethod1();

            var result = tracer.GetTraceResult();
            Assert.Single(result.ThreadsInfo);
            Assert.Single(result.ThreadsInfo[0].Methods);
        }

        [Fact]
        public void GetTraceResult_OneMethodInOneThread_ReturnedTraceResultWithCorrectElapsedMilliseconds()
        {
            var tracer = new MainTracer();
            var testClass = new TestClass(tracer);
            testClass.TestMethod1();

            var result = tracer.GetTraceResult();
            Assert.True(result.ThreadsInfo[0].Methods[0].ElapsedMills >= 10);
            Assert.True(result.ThreadsInfo[0].TotalElapsedMilliseconds >=
                        result.ThreadsInfo[0].Methods[0].ElapsedMills);
        }

        [Fact]
        public void GetTraceResult_OneMethodInOneThread_ReturnedTraceResultWithCorrectNames()
        {
            var tracer = new MainTracer();
            var testClass = new TestClass(tracer);
            testClass.TestMethod1();

            var result = tracer.GetTraceResult();
            Assert.Equal(nameof(testClass.TestMethod1), result.ThreadsInfo[0].Methods[0].Name);
            Assert.Equal(nameof(TestClass), result.ThreadsInfo[0].Methods[0].Class);
        }

        [Fact]
        public void GetTraceResult_NestedMethodsInOneThread_ReturnedTraceResultWithCorrectNesting()
        {
            var tracer = new MainTracer();
            var testClass = new TestClass(tracer);
            testClass.TestMethod2();

            var result = tracer.GetTraceResult();
            Assert.Single(result.ThreadsInfo);
            Assert.Single(result.ThreadsInfo[0].Methods);
            Assert.Single(result.ThreadsInfo[0].Methods[0].Methods);
        }

        [Fact]
        public void GetTraceResult_NestedMethodsInOneThread_ReturnedTraceResultWithCorrectNames()
        {
            var tracer = new MainTracer();
            var testClass = new TestClass(tracer);
            testClass.TestMethod2();

            var result = tracer.GetTraceResult();
            Assert.Equal(nameof(testClass.TestMethod2), result.ThreadsInfo[0].Methods[0].Name);
            Assert.Equal(nameof(TestClass), result.ThreadsInfo[0].Methods[0].Class);
            Assert.Equal(nameof(testClass.TestMethod1), result.ThreadsInfo[0].Methods[0].Methods[0].Name);
            Assert.Equal(nameof(TestClass), result.ThreadsInfo[0].Methods[0].Methods[0].Class);
        }

        [Fact]
        public void GetTraceResult_NestedMethodsInOneThread_ReturnedTraceResultWithCorrectElapsedMilliseconds()
        {
            var tracer = new MainTracer();
            var testClass = new TestClass(tracer);
            testClass.TestMethod2();

            var result = tracer.GetTraceResult();
            Assert.True(result.ThreadsInfo[0].Methods[0].ElapsedMills >= 20);
            Assert.True(result.ThreadsInfo[0].TotalElapsedMilliseconds >=
                        result.ThreadsInfo[0].Methods[0].ElapsedMills);
            Assert.True(result.ThreadsInfo[0].Methods[0].Methods[0].ElapsedMills >= 10);
            Assert.True(result.ThreadsInfo[0].Methods[0].ElapsedMills >=
                        result.ThreadsInfo[0].Methods[0].Methods[0].ElapsedMills);
        }

        [Theory]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public void GetTraceResult_ManyThreads_ReturnedTraceResultWithCorrectThreadsCount(int countOfThreads)
        {
            var tracer = new MainTracer();
            var testClass = new TestClass(tracer);

            var events = new List<WaitHandle>();

            for (var i = 0; i < countOfThreads; i++)
            {
                var resetEvent = new ManualResetEvent(false);
                ThreadPool.QueueUserWorkItem(
                    _ =>
                    {
                        testClass.TestMethod1();
                        resetEvent.Set();
                    });
                events.Add(resetEvent);
            }

            WaitHandle.WaitAll(events.ToArray());

            var result = tracer.GetTraceResult();
            Assert.Equal(countOfThreads, result.ThreadsInfo.Count);
        }

        private class TestClass
        {
            private readonly MainTracer _tracer;

            public TestClass(MainTracer tracer)
            {
                _tracer = tracer;
            }

            public void TestMethod1()
            {
                _tracer.StartTrace();
                Thread.Sleep(10);
                _tracer.StopTrace();
            }

            public void TestMethod2()
            {
                _tracer.StartTrace();
                Thread.Sleep(20);
                TestMethod1();
                _tracer.StopTrace();
            }
        }
    }
}