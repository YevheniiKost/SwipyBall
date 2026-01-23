using System;
using UnityEngine;
using UnityEngine.UI;
using YeKostenko.AudioEngine;

namespace YevheniiKostenko.SwipyBall.Presentation.UI.Common
{
    [RequireComponent(typeof(Button))]
    public class ButtonView : MonoBehaviour
    {
        [SerializeField]
        private Button _button;
        [SerializeField]
        private SoundComponent2D _soundComponent;

        public Action OnButtonClick { get; set; }

        private void Awake() => _button.onClick.AddListener(HandleButtonClick);

        private void OnDestroy() => _button.onClick.RemoveListener(HandleButtonClick);

        private void OnValidate()
        {
            if (_button == null)
            {
                _button = GetComponent<Button>();
            }
        }

        private void HandleButtonClick()
        {
            OnButtonClick?.Invoke();
            _soundComponent.Play();
        }
    }
}