namespace YevheniiKostenko.SwipyBall.Data.Config
{
    public class AppConfig
    {
        public string AppVersion { get; }
        public string GithubUrl { get; }
        public string LinkedInUrl { get; }
        public string ItchIoUrl { get; }
        
        public AppConfig(string appVersion, string linkedInUrl, string githubUrl, string itchIoUrl)
        {
            AppVersion = appVersion;
            LinkedInUrl = linkedInUrl;
            GithubUrl = githubUrl;
            ItchIoUrl = itchIoUrl;
        }
    }
}