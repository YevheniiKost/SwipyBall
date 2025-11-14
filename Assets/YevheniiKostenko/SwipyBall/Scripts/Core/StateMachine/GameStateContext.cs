using YeKostenko.CoreKit.DI;

namespace YevheniiKostenko.SwipyBall.Core.GameStateMachine
{
    public class GameStateContext
    {
        public readonly Container Container;
        
        public GameStateContext(Container container)
        {
            Container = container;
        }
    }
}