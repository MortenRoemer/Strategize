using System.Collections.Generic;
using System.Linq;
using Strategize.Utility;

namespace Strategize
{
    public class Agent<TContext> : IAgent<TContext>
    {
        #region Exposed

        public Agent()
        {
            _queuedActions = new PriorityHeap<float, IAction<TContext>>();
            _strategies = new List<IStrategy<TContext>>(DEFAULT_STRATEGY_CAPACITY);
        }
        
        public IAction<TContext> CurrentAction { get; private set; }
        public IEnumerable<IAction<TContext>> QueuedActions
            => _queuedActions.Select(queuedAction => queuedAction.Item);
        public IEnumerable<IStrategy<TContext>> Strategies => _strategies;
        
        public void Clear()
        {
            CurrentAction = default;
            _queuedActions.Clear();
            _strategies.Clear();
        }

        public void Consider(IStrategy<TContext> strategy, TContext context)
        {
            if (!_strategies.Contains(strategy))
                _strategies.Add(strategy);

            foreach (var action in strategy.Actions)
                _queuedActions.Insert(action.EvaluatePriority(context), action);
        }

        public void Drop(IStrategy<TContext> strategy)
        {
            if (!_strategies.Remove(strategy))
                return;
            
            foreach (var action in strategy.Actions)
                _queuedActions.Remove(action);

        }

        public void Tick(TContext context)
        {
            var nextAction = _queuedActions.PeekMax().Item;

            if (!nextAction.Equals(CurrentAction))
            {
                CurrentAction.OnFinish(context);
                CurrentAction = nextAction;
                CurrentAction.OnEnter(context);
            }
            
            CurrentAction.OnTick(context);
        }

        public override string ToString()
        {
            return $"[Agent] Current Action: {CurrentAction}";
        }

        #endregion

        #region Internal

        private const int DEFAULT_STRATEGY_CAPACITY = 5;
        
        private readonly PriorityHeap<float, IAction<TContext>> _queuedActions;
        private readonly List<IStrategy<TContext>> _strategies;

        #endregion
    }
}