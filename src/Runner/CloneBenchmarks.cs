using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Runner
{
    [MedianColumn]
    public class CloneBenchmarks
    {
        private IMyMapper _reflectionMapper;
        private IMyMapper _expressionMapper;

        private ClonnedClass _objectForClone;

        [GlobalSetup]
        public void Start()
        {
            _reflectionMapper = new ReflectionMapper();
            _reflectionMapper.Register<ClonnedClass, DestCloneClass>();

            _expressionMapper = new ExpressionMapper();
            _expressionMapper.Register<ClonnedClass, DestCloneClass>();

            _objectForClone = new ClonnedClass
            {
                DateTimeProp = DateTime.Now,
                IntProp = 5,
                StringProp = "qwe"
            };
        }

        [Benchmark]
        public DestCloneClass DirectClone()
        {
            return new DestCloneClass
            {
                DateTimeProp = _objectForClone.DateTimeProp,
                IntProp = _objectForClone.IntProp,
                StringProp = _objectForClone.StringProp
            };
        }

        [Benchmark]
        public DestCloneClass ReflectionClone()
        {
            return _reflectionMapper.Map<ClonnedClass, DestCloneClass>(_objectForClone);
        }

        [Benchmark]
        public DestCloneClass ExpressionClone()
        {
            return _expressionMapper.Map<ClonnedClass, DestCloneClass>(_objectForClone);
        }
    }
}
