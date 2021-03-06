﻿using System;
using GraphQLClient.Implementations;
using GraphQLClient.Interfaces;

namespace GraphQLClient
{
    public class QueryBuilder
    {
        public IQuery CreateQuery(string name = null)
        {
            return new QLQuery(name);
        }

		public IMutation CreateMutation(string name)
		{
			return new QLMutation();
		}
    }
}
