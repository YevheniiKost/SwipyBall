using System;

namespace YevheniiKostenko.SwipyBall.Presentation.UI
{
    public interface IGameScreen
    {
        event Action Create;
        
        void UpdateScore(int score);
        void UpdateLives(int lives, int maxLives);
    }
}