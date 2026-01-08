using System;

using UnityEngine;

using Cysharp.Threading.Tasks;

using YeKostenko.CoreKit.DI;
using YeKostenko.CoreKit.UI;

using YevheniiKostenko.SwipyBall.Presentation.UI.Common;

namespace YevheniiKostenko.SwipyBall.Presentation.UI
{
    public class MainMenuScreen : UIWindow , IMainMenuView
    {
        [SerializeField]
        private ButtonView _playButton;
        
        [SerializeField]
        private ButtonView _linkedInButton;
        [SerializeField]
        private ButtonView _githubButton;
        [SerializeField]
        private ButtonView _itchIoButton;
        
        [SerializeField]
        private TextComponent _versionTextComponent;
        [SerializeField]
        private TextComponent _levelNumberTextComponent;
        
        private IMainMenuPresenter _presenter;
        
        public event Action<MainMenuUIContext> Create;
        public event Action PlayButtonClick;
        public event Action LinkedInButtonClick;
        public event Action GithubButtonClick;
        public event Action ItchIoButtonClick;

        [Inject]
        public void Construct(IMainMenuPresenter presenter)
        {
            _presenter = presenter;
            _presenter.AttachView(this); 
            
            _playButton.OnButtonClick += PlayButtonClick;
            _linkedInButton.OnButtonClick += LinkedInButtonClick;
            _githubButton.OnButtonClick += GithubButtonClick;
            _itchIoButton.OnButtonClick += ItchIoButtonClick;
        }

        public override UniTask OnCreateAsync(IUIContext context)
        {
            if(context is not MainMenuUIContext mainMenuUIContext)
                throw new ArgumentException("Invalid context type for MainMenuScreen");
            
            Create?.Invoke(mainMenuUIContext);
            
            return base.OnCreateAsync(mainMenuUIContext);
        }

        public override UniTask OnCloseAsync()
        {
            _presenter.DetachView();
            
            _playButton.OnButtonClick -= PlayButtonClick;
            _linkedInButton.OnButtonClick -= LinkedInButtonClick;
            _githubButton.OnButtonClick -= GithubButtonClick;
            _itchIoButton.OnButtonClick -= ItchIoButtonClick;
            
            return base.OnCloseAsync();
        }
        
        public void SetAppVersion(string version)
        {
            _versionTextComponent.SetText($"App Version: {version}");
        }

        public void SetNextLevelNumber(int number)
        {
            _levelNumberTextComponent.SetText($"Level {number}");
        }
    }
}