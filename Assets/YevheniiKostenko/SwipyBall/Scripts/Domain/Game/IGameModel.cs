using System;
using YevheniiKostenko.SwipyBall.Core.Entities;

namespace YevheniiKostenko.SwipyBall.Domain.Game
{
    public interface IGameModel
    {
        event Action HitsUpdated;
        event Action GameStarted;
        event Action<GameResult> GameEnded;
        event Action<int> ScoreUpdated;
        
        bool IsGameStarted { get; }
        int HitPoints { get; }
        int GameScore { get; }

        bool CanStartGame();
        void StartGame();
        void HitPlayer();
        void PlayerReachGoal();
        void AddScore(int score);
    }
}