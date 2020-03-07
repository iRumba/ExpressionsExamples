using BenchmarkDotNet.Attributes;
using System;

namespace Runner
{
    [MedianColumn]
    public class MappingBenchmarks
    {
        private IMyMapper _reflectionMapper;
        private ExpressionMapper _expressionMapper;

        private Func<MappedClass, DestClass> _funcMap;

        private MappedClass _objectForClone;

        [GlobalSetup]
        public void Start()
        {
            _reflectionMapper = new ReflectionMapper();
            _reflectionMapper.Register<MappedClass, DestClass>();

            _expressionMapper = new ExpressionMapper();
            _expressionMapper.Register<MappedClass, DestClass>();

            _funcMap = x => new DestClass
            {
                DateTimeProp = x.DateTimeProp,
                IntProp = x.IntProp,
                StringProp = x.StringProp
            };

            _objectForClone = new MappedClass
            {
                DateTimeProp = DateTime.Now,
                IntProp = 5,
                StringProp = "qwe"
            };
        }



        [Benchmark]
        public DestClass DirectMapping()
        {
            return new DestClass
            {
                DateTimeProp = _objectForClone.DateTimeProp,
                IntProp = _objectForClone.IntProp,
                StringProp = _objectForClone.StringProp
            };
        }

        [Benchmark]
        public DestClass ReflectionMapping()
        {
            return _reflectionMapper.Map<MappedClass, DestClass>(_objectForClone);
        }

        [Benchmark]
        public DestClass ExpressionMapping()
        {
            return _expressionMapper.Map<MappedClass, DestClass>(_objectForClone);
        }

        [Benchmark]
        public DestClass StaticLambdaMapping()
        {
            return _funcMap(_objectForClone);
        }

        [Benchmark]
        public Func<MappedClass, DestClass> GetFunc()
        {
            return _expressionMapper.GetFunc<MappedClass, DestClass>();
        }
    }
}
