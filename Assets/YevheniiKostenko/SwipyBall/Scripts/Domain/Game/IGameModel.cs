using System;
using YevheniiKostenko.SwipyBall.Core.Entities;

namespace YevheniiKostenko.SwipyBall.Domain.Game
{
    public interface IGameModel
    {
        event Action GameStarted;
        event Action<bool> GameEnded;
        event Action ScoreUpdated;
        event Action LivesUpdated;
        
        bool IsGameStarted { get; }
        int MaxLives { get; }
        int Lives { get; }
        int GameScore { get; }

        bool CanStartGame();
        void StartGame();
        void EndGame(bool isPlayerWon);
        void HitPlayer(int damage);
        void PlayerReachGoal();
        void AddScore(int score);
        void AddHealth(int health);
    }
}