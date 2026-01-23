using UnityEngine;

using YeKostenko.AudioEngine;

namespace YevheniiKostenko.SwipyBall.Presentation.GameLevel
{
    public class PlayerSounds : MonoBehaviour
    {
        [SerializeField]
        private SoundComponent2D _jumpSound;


        public void PlayJumpSound() => _jumpSound.Play();
    }
}