using System.Collections.Generic;

namespace Strategize
{
    public interface IStrategy<in TContext>
    {
        IReadOnlyCollection<IAction<TContext>> Actions { get; }
    }
}