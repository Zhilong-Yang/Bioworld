﻿namespace Bioworld.CQRS.Queries
{
    using System.Collections.Generic;

    public interface IFilter<TResult, in TQuery> where TQuery : IQuery
    {
        IEnumerable<TResult> Filter(IEnumerable<TResult> values, TQuery query);
    }
}