using System;
using YevheniiKostenko.SwipyBall.Scripts.Core.Entities;

namespace YevheniiKostenko.SwipyBall.Scripts.Domain
{
    public interface IGameModel
    {
        event Action<float> Swipe;
        event Action HitsUpdated;
        event Action GameStarted;
        event Action<GameResult> GameEnded;
        event Action<int> ScoreUpdated;
        
        bool IsGameStarted { get; }
        int HitPoints { get; }
        int GameScore { get; }

        bool CanStartGame();
        void StartGame();
        void SwipeDetected(float angle);
        void HitPlayer();
        void PlayerReachGoal();
        void AddScore(int score);
    }
}