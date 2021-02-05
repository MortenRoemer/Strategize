using System.Collections.Generic;
using Strategize.Utility;

namespace Strategize
{
    public class Agent<TContext> : IAgent<TContext>
    {
        #region Exposed

        public PrioritizedItem<float, IAction<TContext>> CurrentAction { get; private set; }
        public IReadOnlyList<PrioritizedItem<float, IAction<TContext>>> QueuedActions => _queuedActions;
        public IReadOnlyList<IStrategy<TContext>> Strategies => _strategies;
        
        public void Clear()
        {
            CurrentAction = default;
            _queuedActions.Clear();
            _strategies.Clear();
        }

        public void Consider(IStrategy<TContext> strategy)
        {
            if (!_strategies.Contains(strategy))
                _strategies.Add(strategy);
            
            
        }

        public void Drop(IStrategy<TContext> strategy)
        {
            throw new System.NotImplementedException();
        }

        public void Tick()
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region Internal

        private const int DEFAULT_STRATEGY_CAPACITY = 5;
        
        private readonly PriorityHeap<float, IAction<TContext>> _queuedActions = new();
        private readonly List<IStrategy<TContext>> _strategies = new(DEFAULT_STRATEGY_CAPACITY);

        #endregion


    }
}