using System;

namespace YevheniiKostenko.SwipyBall.Presentation.UI
{
    public interface IGameScreenView
    {
        event Action<GameScreenUIContext> Create;
        event Action PauseClick;
        
        void UpdateScore(int score);
        void UpdateLives(int lives, int maxLives);
    }
}