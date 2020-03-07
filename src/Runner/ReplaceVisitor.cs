using System.Linq.Expressions;

namespace Runner
{
    public class ReplaceVisitor : ExpressionVisitor
    {
        private LambdaExpression _source;
        private ParameterExpression _sourceParam;

        private ParameterExpression _destParam;

        private Expression _destBody;

        public ReplaceVisitor(LambdaExpression source, LambdaExpression replaceTo)
        {
            _source = source;
            _sourceParam = source.Parameters[0];
            _destParam = replaceTo.Parameters[0];
            _destBody = replaceTo.Body;
        }
        public override Expression Visit(Expression node)
        {
            if (node is null)
                return null;

            if (node == _source)
                return Expression.Lambda(Visit(_source.Body), _destParam);

            if (node == _sourceParam)
                return _destBody;

            return base.Visit(node);
        }
    }
}
