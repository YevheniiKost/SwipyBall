using System.Collections.Generic;
using YevheniiKostenko.CoreKit.Utils;

namespace YevheniiKostenko.SwipyBall.Core.Entities
{
    public class PlayerProgress : IClonable<PlayerProgress>
    {
        public HashSet<int> CompletedLevels { get; }

        public PlayerProgress(HashSet<int> completedLevels)
        {
            CompletedLevels = completedLevels;
        }

        public PlayerProgress Clone()
        {
            return new PlayerProgress(new HashSet<int>(CompletedLevels));
        }
        
        public static PlayerProgress CreateEmpty()
        {
            return new PlayerProgress(new HashSet<int>());
        }
    }
}