﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using Lambdy.Constants.Sql;
using Lambdy.Parameters;
using Lambdy.TreeNodes.ClauseSectionNodes;
using Lambdy.TreeNodes.ExpressionNodes;

namespace Lambdy.SubBuilders.Raw
{
    public class RawBuilder<TModel> where TModel: class
    {
        private readonly LambdyBuilder<TModel> _parentBuilder;
        private readonly ParameterTracker _parentParameterTracker;
        private readonly RawBuilderClauseReferences _clauseReferences;
        
        internal RawBuilder(
            LambdyBuilder<TModel> parentBuilder,
            ParameterTracker parameterTracker,
            RawBuilderClauseReferences references)
        {
            _parentBuilder = parentBuilder;
            _parentParameterTracker = parameterTracker;
            _clauseReferences = references;
        }

        public LambdyBuilder<TModel> From(string sqlFragment)
        {
            sqlFragment = SubstringSqlClause(sqlFragment, SqlClauses.From);
            
            _clauseReferences
                .FromClause
                .Node = new RawNode(sqlFragment);
            
            return _parentBuilder;
        }
        
        public LambdyBuilder<TModel> Join(string sqlFragment)
        {
            _clauseReferences
                .JoinClause
                .Nodes
                .Add(new RawNode(sqlFragment));
            
            return _parentBuilder;
        }
        
        public LambdyBuilder<TModel> Where(
            string sqlFragment)
        {
            sqlFragment = SubstringSqlClause(sqlFragment, SqlClauses.Where);
            
            _clauseReferences
                .WhereClause
                .Nodes
                .Add(new RawNode(sqlFragment));
            
            return _parentBuilder;
        }
        
        public LambdyBuilder<TModel> Where(
            string sqlFragment,
            object parameters)
        {
            Where(sqlFragment);
            AppendParametersFromObject(parameters);

            return _parentBuilder;
        }
        
        public LambdyBuilder<TModel> OrderBy(
            string sqlFragment)
        {
            sqlFragment = SubstringSqlClause(sqlFragment, SqlClauses.OrderBy);

            _clauseReferences
                .OrderClause
                .Nodes = new List<OrderClauseEntryNode>()
            {
                new OrderClauseEntryNode()
                {
                    Node = new RawNode(sqlFragment)
                }
            };
            
            return _parentBuilder;
        }
        
        public LambdyBuilder<TModel> OrderBy(
            string sqlFragment,
            object parameters)
        {
            OrderBy(sqlFragment);
            AppendParametersFromObject(parameters);
            
            return _parentBuilder;
        }
        
        private string SubstringSqlClause(string sqlFragment, string clause)
        {
            var index = sqlFragment.IndexOf(
                clause,
                StringComparison.InvariantCultureIgnoreCase);
            
            if (index >= 0)
            {
                var substringFrom = index + clause.Length + 1;
                sqlFragment = sqlFragment.Substring(substringFrom);
            }

            return sqlFragment;
        }

        private void AppendParametersFromObject(object parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException($"{nameof(parameters)} cannot be null!");
            }
            
            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(parameters))
            {
                var value = property.GetValue(parameters);
                _parentParameterTracker
                    .AddParameter(property.Name, value);
            }
        }
    }
}