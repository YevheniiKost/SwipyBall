using System;
using UnityEngine;
using YevheniiKostenko.SwipyBall.Scripts.Core.Entities;
using Logger = YeKostenko.CoreKit.Logging.Logger;

namespace YevheniiKostenko.SwipyBall.Scripts.Domain
{
    public class GameModel : IGameModel
    {
        private const int MaxHitpoints = 3;
        
        private bool _isGameStarted;
        private int _hitPoints;
        private int _gameScore;
        
        public event Action<float> Swipe;
        public event Action HitsUpdated;
        public event Action GameStarted;
        public event Action<GameResult> GameEnded;
        public event Action<int> ScoreUpdated;

        public bool IsGameStarted => _isGameStarted;
        public int HitPoints => _hitPoints;
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

        public void SwipeDetected(float angle)
        {
            Logger.Log($"Swipe detected with angle: {angle}");
            Swipe?.Invoke(angle);
        }

        public void HitPlayer()
        {
            if (!_isGameStarted)
            {
                Logger.LogWarning("Cannot hit player, game is not started.");
                return;
            }

            UpdateHitPoints(_hitPoints - 1);

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
            ScoreUpdated?.Invoke(_gameScore);
            Logger.Log($"Score updated: {_gameScore}");
        }

        private void EndGame(bool isPlayerWon)
        {
            Logger.Log($"Game ended. Player won: {isPlayerWon}");
            _isGameStarted = false;
            GameEnded?.Invoke(new GameResult(isPlayerWon));
        }

        private void UpdateHitPoints(int hitPoints)
        {
            if (hitPoints < 0 || hitPoints > MaxHitpoints)
            {
                Logger.LogError($"Invalid hit points: {hitPoints}. Must be between 0 and {MaxHitpoints}.");
                return;
            }

            _hitPoints = hitPoints;
            HitsUpdated?.Invoke();
        }
    }
}