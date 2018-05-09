using System;
using System.Collections.Generic;
using System.Text;
using GraphQLClient.Interfaces;
using System.Linq;

namespace GraphQLClient.Implementations
{
    public class QLQuery : IQuery
    {
        private readonly List<IField> _fields = new List<IField>();
        private readonly List<IFragment> _fragments = new List<IFragment>();
		private readonly List<(string, string)> _variables = new List<(string, string)>();
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

		public IQuery AddFragment(params Func<IFragment>[] fr)
        {
            foreach (var fragment in fr)
            {
                var gragmnt = fragment.Invoke();
                _fragments.Add(gragmnt);
            }

            return this;
        }

        public IQuery AddVariables(params (string, string)[] variables)
        {
            _variables.AddRange(variables);

            return this;
        }

        public string GetVariables()
        {
            StringBuilder builder = new StringBuilder();
            AddVariablesText(builder);

            return builder.ToString();
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

            if (_fragments.Count > 0)
                AddFragmentText(builder);

            return builder.ToString();
		}

        private void AddFragmentText(StringBuilder builder)
        {
            foreach (var item in _fragments)
                builder.Append(item.ToString());
        }

        private void AddVariablesText(StringBuilder builder)
        {
            builder.Append("{");
			_variables.ForEach((obj) => builder.AppendFormat(" \"{0}\": {1}," ,obj.Item1, obj.Item2));
            //remove last ","
			builder.Length--;
            builder.Append("}");
        }
	}
}
