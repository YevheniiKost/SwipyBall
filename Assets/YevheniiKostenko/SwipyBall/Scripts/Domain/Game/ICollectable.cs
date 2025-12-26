using System;
using YevheniiKostenko.SwipyBall.Core.Entities;

namespace YevheniiKostenko.SwipyBall.Domain.Game
{
    public interface ICollectable
    {
        CollectableType  Type { get; }
        int Value { get; }
        Action<ICollectable> Collected { get; set; }
        void Collect();
    }
}