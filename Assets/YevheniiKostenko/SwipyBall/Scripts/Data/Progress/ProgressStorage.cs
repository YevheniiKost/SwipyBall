using System;
using System.Collections.Generic;

using Cysharp.Threading.Tasks;

using YeKostenko.CoreKit.Logging;
using YeKostenko.CoreKit.Scripts.Saving;

using YevheniiKostenko.SwipyBall.Core.Entities;

namespace YevheniiKostenko.SwipyBall.Data.Progress
{
    internal class ProgressStorage : IProgressStorage
    {
        private readonly SaveService _saveService;

        private PlayerProgress _progress;
        private bool _isInitialized;

        public ProgressStorage(SaveService saveService)
        {
            _saveService = saveService;
        }
        
        public string Key => "progress_data";
        public Type DataType => typeof(PlayerProgressSerializable);

        public PlayerProgress Progress => _progress.Clone();

        public void Init()
        {
            if (_isInitialized)
            {
                return;
            }
            
            _progress = PlayerProgress.CreateEmpty();
            _saveService.RegisterProvider(this);
            _saveService.LoadAsync().Forget();
            
            _isInitialized = true;
        }

        public void SaveProgress(PlayerProgress progress)
        {
            if (progress == null)
            {
                Logger.LogWarning("Trying to save null progress");
                return;
            }
            
            _progress = progress.Clone();
            _saveService.SaveAsync().Forget();
        }

        public object CaptureState()
        {
            PlayerProgressSerializable serializable = new PlayerProgressSerializable
            {
                CompletedLevels = new List<int>(_progress.CompletedLevels)
            };
            return serializable;
        }

        public void RestoreState(object state)
        {
            if (state is PlayerProgressSerializable serializable)
            {
                _progress = serializable.ToPlayerProgress();
            }
            else
            {
                _progress = new PlayerProgress(new HashSet<int>());
            }
        }

    }
}