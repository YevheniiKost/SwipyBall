using System.Collections.Generic;
using YevheniiKostenko.SwipyBall.Core.Entities;

namespace YevheniiKostenko.SwipyBall.Data.Progress
{
    public class PlayerProgressSerializable
    {
        public List<int> CompletedLevels;
        
        public PlayerProgress ToPlayerProgress()
        {
            PlayerProgress progress = new PlayerProgress(new HashSet<int>(CompletedLevels));
            return progress;
        }
    }
}