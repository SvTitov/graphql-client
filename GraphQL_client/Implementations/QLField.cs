using System;
using System.Collections.Generic;
using System.Text;
using GraphQLClient.Interfaces;

namespace GraphQLClient.Implementations
{
    public class QLField : IField
    {
        private string _name;
        private string _alias;
        private readonly Dictionary<string, string> _args = new Dictionary<string, string>();
        private readonly List<IField> _fields = new List<IField>();


        public QLField(string name)
        {
            _name = name;
        }

        public QLField(string name, params (string,string) [] args)
            : this(name)
        {
            foreach (var item in args)
            {
                _args.Add(item.Item1, item.Item2);
            }
        }

        public IField AddFields(params Func<IField>[] p)
        {
            foreach (var item in p)
            {
                var field = item.Invoke();
                _fields.Add(field);
            }

            return this;
        }

        public IField WithAlias(string alias)
        {
            this._alias = alias;
            return this;
        }

		public override string ToString()
		{
            StringBuilder builder = new StringBuilder();
            bool isOnce = _fields.Count < 1;
            string args = GetArgsString();
            bool isHaveArgs = !string.IsNullOrEmpty(args);

            if (!string.IsNullOrEmpty(_alias))
                builder.Append($"{_alias}:");

            builder.Append($" {_name} {(isHaveArgs ? ("(" + args + ")") : string.Empty)}");

            if (_fields.Count > 0)
            {
                builder.Append('{');

                foreach (var field in _fields)
                    builder.Append(field.ToString());
                builder.Append('}');
            }

            return builder.ToString();
		}

        private string GetArgsString()
        {
            StringBuilder builder = new StringBuilder();
            foreach (var item in _args)
            {
                builder.Append($" {item.Key}: {item.Value}");
            }

            return builder.ToString();
        }
	}
}
