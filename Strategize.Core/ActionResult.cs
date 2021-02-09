namespace Strategize
{
    public enum ActionResult : byte
    {
        /// <summary>
        /// Indicates that the action can be cancelled after this tick
        /// </summary>
        Yield = 0,
        
        /// <summary>
        /// Indicates that the strategy and its actions should be reconsidered
        /// after this tick
        /// </summary>
        Reconsider = 1,
        
        /// <summary>
        /// Indicates that the action should be continued after this tick
        /// without considering other actions
        /// </summary>
        Continue = 2
    }
}