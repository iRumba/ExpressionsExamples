using FluentAssertions;
using NUnit.Framework;
using Runner;
using System;

namespace Expressions.Tests
{
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
            var clonnedClass = new ClonnedClass
            {
                DateTimeProp = date,
                IntProp = 5,
                StringProp = "qwe"
            };

            var destClass = new DestCloneClass
            {
                DateTimeProp = date,
                IntProp = 5,
                StringProp = "qwe"
            };

            var mapper = new ReflectionMapper();

            mapper.Register<ClonnedClass, DestCloneClass>();

            var mapped = mapper.Map<ClonnedClass, DestCloneClass>(clonnedClass);

            mapped.Should().BeEquivalentTo(destClass);

            Assert.Pass();
        }

        [Test]
        public void ExpressionMapTest()
        {
            var date = DateTime.Now;
            var clonnedClass = new ClonnedClass
            {
                DateTimeProp = date,
                IntProp = 5,
                StringProp = "qwe"
            };

            var destClass = new DestCloneClass
            {
                DateTimeProp = date,
                IntProp = 5,
                StringProp = "qwe"
            };

            var mapper = new ExpressionMapper();

            mapper.Register<ClonnedClass, DestCloneClass>();

            var mapped = mapper.Map<ClonnedClass, DestCloneClass>(clonnedClass);

            mapped.Should().BeEquivalentTo(destClass);

            Assert.Pass();
        }
    }
}