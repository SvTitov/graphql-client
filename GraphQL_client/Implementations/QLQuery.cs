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
		private readonly List<QLVariable> _variables = new List<QLVariable>();
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

        public IQuery AddVariables(params QLVariable[] variables)
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

			// add text variable like ($episode: Episode)
			if (_variables.Count > 0)
				builder.Append(GetVariablesText());

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

		private string GetVariablesText()
		{
			StringBuilder builder = new StringBuilder();

			builder.Append("(");
			_variables.ForEach(variable =>
			{
				builder.AppendFormat("${0}: {1} ", variable.Name, variable.Type);
				if (!string.IsNullOrEmpty(variable.DefaultValue))
					builder.Append($"= {variable.DefaultValue} ");
			});
			builder.Append("}");

			return builder.ToString();
		}

		private void AddVariablesText(StringBuilder builder)
        {
            builder.Append("{");
			_variables.ForEach((obj) => builder.AppendFormat(" \"{0}\": {1}," ,obj.Name, string.IsNullOrEmpty(obj.Value) 
			                                                 ? obj.DefaultValue : obj.Value ));
            //remove last ","
			builder.Length--;
            builder.Append("}");
        }
	}
}
