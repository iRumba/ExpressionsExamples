using FluentAssertions;
using NUnit.Framework;
using Runner;
using System;
using System.Linq.Expressions;

namespace Expressions.Tests
{
    [TestFixture]
    public class ReplaceVisitorTests
    {
        [Test]
        public void PropsCombineTest()
        {
            Expression<Func<RootClass, NestedClass>> lambda1 = x => x.Nested;
            Expression<Func<NestedClass, int>> lambda2 = x => x.Property;

            Expression<Func<RootClass, int>> expected = x => x.Nested.Property;

            var replaceVisitor = new ReplaceVisitor(lambda2, lambda1);
            var result = replaceVisitor.Visit(lambda2);

            result.ToString().Should().BeEquivalentTo(expected.ToString());
        }

        [Test]
        public void MethodCombineTest()
        {
            Expression<Func<RootClass, NestedClass>> lambda1 = x => x.Nested;
            Expression<Func<NestedClass, int>> lambda2 = x => Helper.GetProperty(x);

            Expression<Func<RootClass, int>> expected = x => Helper.GetProperty(x.Nested);

            var replaceVisitor = new ReplaceVisitor(lambda2, lambda1);
            var result = replaceVisitor.Visit(lambda2);

            result.ToString().Should().BeEquivalentTo(expected.ToString());
        }

        [Test]
        public void PredicateCombineTest()
        {
            Expression<Func<RootClass, NestedClass>> lambda1 = x => x.Nested;
            Expression<Func<NestedClass, bool>> lambda2 = x => x.Property == 5;

            Expression<Func<RootClass, bool>> expected = x => x.Nested.Property == 5;

            var replaceVisitor = new ReplaceVisitor(lambda2, lambda1);
            var result = replaceVisitor.Visit(lambda2);

            result.ToString().Should().BeEquivalentTo(expected.ToString());
        }

        public class RootClass
        {
            public NestedClass Nested { get; set; }
        }

        public class NestedClass
        {
            public int Property { get; set; }
        }

        public static class Helper
        {
            public static int GetProperty(NestedClass nested)
            {
                return nested.Property;
            }
        }
    }
}
