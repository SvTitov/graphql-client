using System;
using System.Collections.Generic;
using System.Text;
using GraphQLClient.Interfaces;

namespace GraphQLClient.Implementations
{
    public class QLFragment : IFragment
    {
        private readonly List<IField> _fields = new List<IField>();
        readonly string _name;
        readonly string _type;

        public QLFragment(string name, string type)
        {
            this._type = type;
            this._name = name;
        }

        public string Name => _name;

        public IFragment AddFields(params Func<IField>[] p)
        {
            foreach (var item in p)
            {
                var field = item.Invoke();
                _fields.Add(field);
            }

            return this;
        }

		public override string ToString()
		{
            StringBuilder builder = new StringBuilder();

            builder.Append($"fragment {_name} on {_type}{{");
            foreach (var field in _fields)
                builder.Append(field.ToString());
            builder.Append("}");

            return builder.ToString();
		}
	}
}
