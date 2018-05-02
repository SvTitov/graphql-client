using System;
namespace GraphQLClient.Interfaces
{
    public interface IField
    {
        IField AddFields(params Func<IField>[] p);

        IField WithAlias(string alias);
    }
}
