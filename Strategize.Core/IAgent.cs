using System.Collections.Generic;
using Strategize.Utility;

namespace Strategize
{
    public interface IAgent<TContext>
    {
        PrioritizedItem<float, IAction<TContext>> CurrentAction { get; }
        
        IReadOnlyList<PrioritizedItem<float, IAction<TContext>>> QueuedActions { get; }

        IReadOnlyList<IStrategy<TContext>> Strategies { get; }
        
        void Clear();

        void Consider(IStrategy<TContext> strategy);

        void Drop(IStrategy<TContext> strategy);

        void Tick();
    }
}