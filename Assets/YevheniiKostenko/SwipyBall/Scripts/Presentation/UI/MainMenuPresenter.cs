using YevheniiKostenko.SwipyBall.Data.Config;
using YevheniiKostenko.SwipyBall.Domain.Progress;

namespace YevheniiKostenko.SwipyBall.Presentation.UI
{
    public class MainMenuPresenter : IMainMenuPresenter
    {
        private readonly IConfigProvider _configProvider;
        private readonly IGetNextLevelUseCase _getNextLevelUseCase;

        private IMainMenuView _view;
        private MainMenuUIContext _uiContext;
        private AppConfig _appConfig;

        public MainMenuPresenter(IConfigProvider configProvider, IGetNextLevelUseCase getNextLevelUseCase)
        {
            _configProvider = configProvider;
            _getNextLevelUseCase = getNextLevelUseCase;
        }

        public void AttachView(IMainMenuView view)
        {
            if (_view != null)
            {
                throw new System.InvalidOperationException("View is already attached.");
            }
            
            _view = view ?? throw new System.ArgumentNullException(nameof(view), "View cannot be null.");

            _view.Create += OnCreate;
            _view.PlayButtonClick += OnPlayButtonClick;
            _view.LinkedInButtonClick += OnLinkedInButtonClick;
            _view.GithubButtonClick += OnGithubButtonClick;
            _view.ItchIoButtonClick += OnItchIoButtonClick;
        }

        public void DetachView()
        {
            if (_view == null)
            {
                return;
            }

            _view.Create -= OnCreate;
            _view.PlayButtonClick -= OnPlayButtonClick;
            _view.LinkedInButtonClick -= OnLinkedInButtonClick;
            _view.GithubButtonClick -= OnGithubButtonClick;
            _view.ItchIoButtonClick -= OnItchIoButtonClick;

            _view = null;
        }

        private void OnCreate(MainMenuUIContext context)
        {
            _uiContext = context;
            _appConfig = _configProvider.GetAppConfig();
            _view.SetAppVersion(_appConfig.AppVersion);
            int nextLevel = _getNextLevelUseCase.Execute();
            _view.SetNextLevelNumber(nextLevel);
        }

        private void OnPlayButtonClick()
        {
            _uiContext?.PlayButtonClick?.Invoke();
        }

        private void OnLinkedInButtonClick()
        {
            OpenLink(_appConfig.LinkedInUrl);
        }

        private void OnGithubButtonClick()
        {
            OpenLink(_appConfig.GithubUrl);
        }

        private void OnItchIoButtonClick()
        {
            OpenLink(_appConfig.ItchIoUrl);
        }

        private void OpenLink(string url)
        {
            UnityEngine.Application.OpenURL(url);
        }
    }
}