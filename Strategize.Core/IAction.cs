namespace Strategize
{
    public interface IAction<in TContext>
    {
        IStrategy<TContext> Strategy { get; }

        float EvaluatePriority(TContext context);
        
        void OnEnter(TContext context);

        void OnTick(TContext context);

        void OnFinish(TContext context);
    }
}