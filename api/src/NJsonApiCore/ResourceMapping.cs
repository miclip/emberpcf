﻿using NJsonApi.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace NJsonApi
{
    public class ResourceMapping<TEntity, TController> : IResourceMapping
    {
        public Func<object, object> IdGetter { get; set; }
        public Action<object, string> IdSetter { get; set; }
        public Type ResourceRepresentationType { get; set; }
        public string ResourceType { get; set; }
        public Dictionary<string, Func<object, object>> PropertyGetters { get; set; }
        public Dictionary<string, Action<object, object>> PropertySetters { get; private set; }
        public Dictionary<string, Expression<Action<object, object>>> PropertySettersExpressions { get; private set; }
        public List<IRelationshipMapping> Relationships { get; set; }
        public Type Controller { get; set; }

        public ResourceMapping()
        {
            ResourceRepresentationType = typeof(TEntity);
            Controller = typeof(TController);
            PropertyGetters = new Dictionary<string, Func<object, object>>();
            PropertySetters = new Dictionary<string, Action<object, object>>();
            PropertySettersExpressions = new Dictionary<string, Expression<Action<object, object>>>();
            Relationships = new List<IRelationshipMapping>();
        }

        public ResourceMapping(Expression<Func<TEntity, object>> idPointer)
        {
            IdGetter = ExpressionUtils.CompileToObjectTypedFunction(idPointer);
            ResourceRepresentationType = typeof(TEntity);
            PropertyGetters = new Dictionary<string, Func<object, object>>();
            PropertySetters = new Dictionary<string, Action<object, object>>();
            PropertySettersExpressions = new Dictionary<string, Expression<Action<object, object>>>();
            Relationships = new List<IRelationshipMapping>();
        }

        public void AddPropertyGetter(string key, Expression<Func<TEntity, object>> expression)
        {
            PropertyGetters.Add(key, ExpressionUtils.CompileToObjectTypedFunction(expression));
        }

        public void AddPropertySetter(string key, Expression<Action<TEntity, object>> expression)
        {
            var convertedExpression = ExpressionUtils.ConvertToObjectTypeExpression(expression);

            PropertySettersExpressions.Add(key, convertedExpression);
            PropertySetters.Add(key, convertedExpression.Compile());
        }

        public bool ValidateIncludedRelationshipPaths(string[] includedPaths)
        {
            foreach (var relationshipPath in includedPaths)
            {
                IResourceMapping currentMapping = this;

                var parts = relationshipPath.Split('.');
                foreach (var part in parts)
                {
                    var relationship = currentMapping.Relationships.SingleOrDefault(x => x.RelatedBaseResourceType == part);
                    if (relationship == null)
                        return false;

                    currentMapping = relationship.ResourceMapping;
                }
            }
            return true;
        }

        public Dictionary<string, object> GetAttributes(object objectGraph)
        {
            return PropertyGetters.ToDictionary(kvp => CamelCaseUtil.ToCamelCase(kvp.Key), kvp => kvp.Value(objectGraph));
        }

        // TODO ROLA - type handling must be better in here
        public Dictionary<string, object> GetValuesFromAttributes(Dictionary<string, object> attributes)
        {
            var values = new Dictionary<string, object>();

            foreach (var propertySetter in PropertySettersExpressions)
            {
                object value;
                attributes.TryGetValue(CamelCaseUtil.ToCamelCase(propertySetter.Key), out value);
                if (value != null)
                {
                    values.Add(propertySetter.Key, value);
                }
            }

            return values;
        }
    }
}