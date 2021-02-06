using System.Collections.Generic;

namespace Strategize
{
    public interface IStrategy<in TContext>
    {
        IEnumerable<IAction<TContext>> Actions { get; }
    }
}