using System;
namespace GraphQLClient.Interfaces
{
    public interface IField : IQueryField
    {
        IField AddFields(params Func<IQueryField>[] p);

        IField WithAlias(string alias);
    }
}
