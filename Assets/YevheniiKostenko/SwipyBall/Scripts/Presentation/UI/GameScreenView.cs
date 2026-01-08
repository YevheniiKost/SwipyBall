using System;

using UnityEngine;

using Cysharp.Threading.Tasks;

using YeKostenko.CoreKit.DI;
using YeKostenko.CoreKit.UI;
using YeKostenko.CoreKit.UI.Adapters;

using YevheniiKostenko.SwipyBall.Presentation.UI.Common;

namespace YevheniiKostenko.SwipyBall.Presentation.UI
{
    public class GameScreenView : UIWindow, IGameScreenView
    {
        [SerializeField]
        private TextComponent _scoreTextComponent;
        
        [SerializeField]
        private Transform _livesContainer;
        [SerializeField]
        private GameObject _lifeIconPrefab;
        [SerializeField]
        private ButtonView _pauseButton;
        
        private IGameScreenPresenter _presenter;
        private SimpleObjectsAdapter _livesAdapter;
        
        public event Action<GameScreenUIContext> Create;
        public event Action PauseClick;

        [Inject]
        public void Construct(IGameScreenPresenter presenter)
        {
            _livesAdapter = new SimpleObjectsAdapter(_livesContainer, _lifeIconPrefab);
            _presenter = presenter;
        }

        public override UniTask OnCreateAsync(IUIContext context)
        {
            GameScreenUIContext gameScreenUIContext =
                context as GameScreenUIContext ?? throw new ArgumentException("context");
            Bind();
            Create?.Invoke(gameScreenUIContext);
            return base.OnCreateAsync(context);
        }

        public override UniTask OnCloseAsync()
        {
            Unbind();
            return base.OnCloseAsync();
        }

        public void UpdateScore(int score)
        {
            _scoreTextComponent.SetText(score.ToString());
        }

        public void UpdateLives(int lives, int maxLives)
        {
            _livesContainer.gameObject.SetActive(lives > 0);
            _livesAdapter.SetItemCount(lives);
        }

        private void Bind()
        {
            _pauseButton.OnButtonClick += () => PauseClick?.Invoke();
            _presenter.AttachView(this); 
        }

        private void Unbind()
        {
            _pauseButton.OnButtonClick -= () => PauseClick?.Invoke();
            _presenter.DetachView();
        }
    }
}