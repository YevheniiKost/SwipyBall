using UnityEngine;
using YeKostenko.AudioEngine;
using YevheniiKostenko.CoreKit.Utils;

namespace YevheniiKostenko.SwipyBall.Presentation.Audio
{
    public class Music : SingletonMonoBehaviour<Music>
    {
        [SerializeField]
        private MusicPlayerComponent _musicPlayerComponent;

        public void Play() => _musicPlayerComponent.Play();
    }
}