using System.Collections.Generic;
using Strategize.Utility;

namespace Strategize
{
    public interface IAgent<TContext>
    {
        IAction<TContext> CurrentAction { get; }
        
        IEnumerable<IAction<TContext>> QueuedActions { get; }

        IEnumerable<IStrategy<TContext>> Strategies { get; }
        
        void Clear();

        void Consider(IStrategy<TContext> strategy, TContext context);

        void Drop(IStrategy<TContext> strategy);

        void Tick(TContext context);
    }
}