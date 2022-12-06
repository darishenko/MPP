using Xunit;
using FakerLib;
using FakerTests.Mocks;
using FakerTests.Models;

namespace FakerTests
{
    public class FakerTest
    {
        private Faker _faker;

        public FakerTest()
        {
            _faker = new Faker();
        }


        [Fact]
        public void TestCycle()
        {
            var a = _faker.Create<A>();
            Assert.NotNull(a.bClass);
            Assert.NotNull(a.bClass.cClass);
            Assert.Null(a.bClass.aClass); 
            Assert.Null(a.bClass.cClass.aClass);
        }

        [Fact]
        public void TestListCreator()
        {
            var c = _faker.Create<C>();
            Assert.NotNull(c.list);
        }

        [Fact]
        public void TestClassWithPrivateConstructorWithoutFullParameters()
        {
            var c = _faker.Create<ClassWithConstructor>();
            Assert.NotNull(c.c);
        }


        [Fact]
        public void TestBoolFromDll()
        {
            var instance = _faker.Create<ClassWithBool>();
            Assert.True(instance.boolean == false || instance.boolean);
        }

        [Fact]
        public void TestConfigOfFaker()
        {
            var config = new FakerConfig();
            config.Add<ClassWithString, string, DefaultStringCreator>(instance => instance.str);
            _faker = new Faker(config);
            var instance = _faker.Create<ClassWithString>();
            Assert.Equal("default", instance.str);
        }
    }
}