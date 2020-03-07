using FluentAssertions;
using NUnit.Framework;
using Runner;
using System;

namespace Expressions.Tests
{
    [TestFixture]
    public class MappersTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ReflectionMapTest()
        {
            var date = DateTime.Now;
            var clonnedClass = new MappedClass
            {
                DateTimeProp = date,
                IntProp = 5,
                StringProp = "qwe"
            };

            var destClass = new DestClass
            {
                DateTimeProp = date,
                IntProp = 5,
                StringProp = "qwe"
            };

            var mapper = new ReflectionMapper();

            mapper.Register<MappedClass, DestClass>();

            var mapped = mapper.Map<MappedClass, DestClass>(clonnedClass);

            mapped.Should().BeEquivalentTo(destClass);

            Assert.Pass();
        }

        [Test]
        public void ExpressionMapTest()
        {
            var date = DateTime.Now;
            var clonnedClass = new MappedClass
            {
                DateTimeProp = date,
                IntProp = 5,
                StringProp = "qwe"
            }; 

            var destClass = new DestClass
            {
                DateTimeProp = date,
                IntProp = 5,
                StringProp = "qwe"
            };

            var mapper = new ExpressionMapper();

            mapper.Register<MappedClass, DestClass>();

            var mapped = mapper.Map<MappedClass, DestClass>(clonnedClass);

            mapped.Should().BeEquivalentTo(destClass);

            Assert.Pass();
        }
    }
}