﻿using System;
namespace GraphQLClient.Interfaces
{
    public interface IQuery
    {
        IQuery AddFields(params Func<IField>[] p);
    }
}