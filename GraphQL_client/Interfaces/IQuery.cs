using System;
namespace GraphQLClient.Interfaces
{
    public interface IQuery
    {
        IQuery AddFields(params Func<IField>[] p);

        IQuery AddFragment(params Func<IFragment>[] fr);

		IQuery AddVariables(params QLVariable[] variables);

        string GetVariables();
    }
}
