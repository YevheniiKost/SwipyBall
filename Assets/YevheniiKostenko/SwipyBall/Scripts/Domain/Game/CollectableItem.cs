using System;
using YevheniiKostenko.SwipyBall.Core.Entities;

namespace YevheniiKostenko.SwipyBall.Domain.Game
{
    public abstract class CollectableItem : ICollectable
    {
        public abstract CollectableType Type { get; }
        public abstract int Value { get; }
        
        public Action<ICollectable> Collected { get; set; }
        public virtual void Collect()
        {
            Collected?.Invoke(this);
        }
    }
}