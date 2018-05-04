using System;
namespace GraphQLClient.Interfaces
{
    public interface IFragment: IQueryField
    {
        IFragment AddFields(params Func<IField>[] p);

        string Name { get; }
    }
}
