using System;
using UnityEngine;

namespace YevheniiKostenko.SwipyBall.Data.Config
{
    [Serializable]
    public class SerializableLevelConfig
    {
        [SerializeField]
        private int _levelId;

        public int LevelId => _levelId;
    }
}