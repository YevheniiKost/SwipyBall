using System;
using YevheniiKostenko.SwipyBall.Core.Entities;

namespace YevheniiKostenko.SwipyBall.Domain.Game
{
    public interface IGameModel
    {
        event Action GameStarted;
        event Action<GameResult> GameEnded;
        event Action ScoreUpdated;
        event Action LivesUpdated;
        
        bool IsGameStarted { get; }
        int Lives { get; }
        int GameScore { get; }

        bool CanStartGame();
        void StartGame();
        void HitPlayer(int damage);
        void PlayerReachGoal();
        void AddScore(int score);
        void AddHealth(int health);
    }
}