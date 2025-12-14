using UnityEngine;
using YeKostenko.CoreKit.DI;
using YeKostenko.CoreKit.UI;
using YevheniiKostenko.SwipyBall.Presentation.UI.Common;

namespace YevheniiKostenko.SwipyBall.Presentation.UI
{
    public class GameScreen : UIWindow, IGameScreen
    {
        [SerializeField]
        private TextComponent _scoreTextComponent;
        
        [SerializeField]
        private TextComponent _livesTextComponent;
        
        private IGameScreenPresenter _presenter;

        [Inject]
        public void Construct(IGameScreenPresenter presenter)
        {
            _presenter = presenter;
            _presenter.AttachView(this);
        }
        
        public void UpdateScore(int score)
        {
            _scoreTextComponent.SetText(score.ToString());
        }

        public void UpdateLives(int lives)
        {
            _livesTextComponent.SetText(lives.ToString());
        }
    }
}