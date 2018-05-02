using System;
using GraphQLClient.Interfaces;

namespace GraphQLClient
{
    public class QueryBuilder
    {
        public IQuery CreateQuery(string name = null)
        {
            return new QLQuery(name);
        }
    }
}
