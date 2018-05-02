using System;
using System.Collections.Generic;
using System.Text;
using GraphQLClient.Interfaces;

namespace GraphQLClient
{
    public class QLQuery : IQuery
    {
        private readonly List<IField> _fields = new List<IField>();
        private string _name;

        public QLQuery()
        {   }

        public QLQuery(string name)
        {
            _name = name;
        }

        public IQuery AddFields(params Func<IField>[] p)
        {
            foreach (var field in p)
            {
                var fld = field.Invoke();
                _fields.Add(fld);
            }

            return this;
        }

		public override string ToString()
		{
            StringBuilder builder = new StringBuilder();

            // start query (like: query Some{ )
            if (!string.IsNullOrEmpty(_name))
                builder.AppendFormat("query {0}", _name);

            if (_fields.Count > 0)
            {
                builder.Append('{');
                for (int i = 0; i < _fields.Count; i++)
                {
                    var strToAppend = $"{_fields[i].ToString()}";
                    builder.Append(" " + strToAppend);
                }
                builder.Append('}');
            }


            return builder.ToString();
		}
	}
}
