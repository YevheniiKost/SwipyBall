using TMPro;
using UnityEngine;

namespace YevheniiKostenko.SwipyBall.Presentation.UI.Common
{
    public class TextComponent : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _textMeshProUGUI;
        
        public void SetText(string text)
        {
            _textMeshProUGUI.text = text;
        }

        private void OnValidate()
        {
            if (_textMeshProUGUI == null)
                _textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        }
    }
}