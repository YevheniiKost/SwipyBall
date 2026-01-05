using System;
using UnityEngine;
using Logger = YeKostenko.CoreKit.Logging.Logger;

namespace YevheniiKostenko.SwipyBall.Domain.Game
{
    public class GameModel : IGameModel
    {
        private const int MaxHitpoints = 3;
        
        private bool _isGameStarted;
        private int _hitPoints;
        private int _gameScore;
        
        public event Action LivesUpdated;
        public event Action GameStarted;
        public event Action<bool> GameEnded;
        public event Action ScoreUpdated;

        public bool IsGameStarted => _isGameStarted;
        public int MaxLives => MaxHitpoints;
        public int Lives => _hitPoints;
        public int GameScore => _gameScore;

        public bool CanStartGame()
        {
            return !_isGameStarted;
        }

        public void StartGame()
        {
            if (!CanStartGame())
            {
                Logger.LogWarning("Cannot start game, it is already started.");
                return;
            }
            
            Logger.Log("Game started.");
            UpdateHitPoints(MaxHitpoints);
            _isGameStarted = true; 
            _gameScore = 0;
            GameStarted?.Invoke();
        }

        public void HitPlayer(int damage)
        {
            if (!_isGameStarted)
            {
                Logger.LogWarning("Cannot hit player, game is not started.");
                return;
            }

            if (damage < 0)
            {
                Logger.LogError($"Invalid damage: {damage}. Damage must be non-negative.");
                return;
            }

            UpdateHitPoints(_hitPoints - damage);

            if (_hitPoints <= 0)
            {
                EndGame(false);
            }
        }

        public void PlayerReachGoal()
        {
            if (!_isGameStarted)
            {
                Logger.LogWarning("Cannot reach goal, game is not started.");
                return;
            }

            EndGame(true);
        }

        public void AddScore(int score)
        {
            if (!_isGameStarted)
            {
                Logger.LogWarning("Cannot add score, game is not started.");
                return;
            }

            if (score < 0)
            {
                Logger.LogError($"Invalid score: {score}. Score must be non-negative.");
                return;
            }

            _gameScore += score;
            ScoreUpdated?.Invoke();
            Logger.Log($"Score updated: {_gameScore}");
        }

        public void AddHealth(int health)
        {
            if (!_isGameStarted)
            {
                Logger.LogWarning("Cannot add health, game is not started.");
                return;
            }

            if (health < 0)
            {
                Logger.LogError($"Invalid health: {health}. Health must be non-negative.");
                return;
            }

            UpdateHitPoints(Mathf.Min(_hitPoints + health, MaxHitpoints));
        }

        private void EndGame(bool isPlayerWon)
        {
            Logger.Log($"Game ended. Player won: {isPlayerWon}");
            _isGameStarted = false;
            GameEnded?.Invoke(isPlayerWon);
        }

        private void UpdateHitPoints(int hitPoints)
        {
            if (hitPoints < 0 || hitPoints > MaxHitpoints)
            {
                Logger.LogError($"Invalid hit points: {hitPoints}. Must be between 0 and {MaxHitpoints}.");
                return;
            }

            _hitPoints = hitPoints;
            LivesUpdated?.Invoke();
        }
    }
}