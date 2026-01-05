using System;

using UnityEngine;

using Cysharp.Threading.Tasks;

using YeKostenko.CoreKit.DI;
using YeKostenko.CoreKit.UI;
using YeKostenko.CoreKit.UI.Adapters;

using YevheniiKostenko.SwipyBall.Presentation.UI.Common;

namespace YevheniiKostenko.SwipyBall.Presentation.UI
{
    public class GameScreen : UIWindow, IGameScreen
    {
        [SerializeField]
        private TextComponent _scoreTextComponent;
        
        [SerializeField]
        private Transform _livesContainer;
        [SerializeField]
        private GameObject _lifeIconPrefab;
        
        private IGameScreenPresenter _presenter;
        private SimpleObjectsAdapter _livesAdapter;
        
        public event Action Create;

        [Inject]
        public void Construct(IGameScreenPresenter presenter)
        {
            _livesAdapter = new SimpleObjectsAdapter(_livesContainer, _lifeIconPrefab);
            _presenter = presenter;
            _presenter.AttachView(this); 
        }

        public override UniTask OnCreateAsync(IUIContext context)
        {
            Create?.Invoke();
            
            return base.OnCreateAsync(context);
        }

        public override UniTask OnCloseAsync()
        {
            _presenter.DetachView();
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
    }
}