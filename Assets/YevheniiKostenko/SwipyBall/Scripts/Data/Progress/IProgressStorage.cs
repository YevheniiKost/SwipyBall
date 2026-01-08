using System;
using YeKostenko.CoreKit.Scripts.Saving;
using YevheniiKostenko.SwipyBall.Core.Entities;

namespace YevheniiKostenko.SwipyBall.Data.Progress
{
    public interface IProgressStorage : ISaveDataProvider, IDisposable
    {
        PlayerProgress Progress { get; }
        void Init();
        void SaveProgress(PlayerProgress progress);
    }
}