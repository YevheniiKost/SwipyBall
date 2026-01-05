using System;
using UnityEngine;
using UnityEngine.UI;

namespace YevheniiKostenko.SwipyBall.Presentation.UI.Common
{
    [RequireComponent(typeof(Button))]
    public class ButtonView : MonoBehaviour
    {
        [SerializeField]
        private Button _button;
        
        public Action OnButtonClick { get; set; }
        
        private void Awake()
        {
            _button.onClick.AddListener(HandleButtonClick);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(HandleButtonClick);
        }

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
        }
    }
}