namespace MyAspCoreApp.Web.Helpers
{
    public class Helper : IHelper
    {
        private bool isConfiguration;
        public Helper(bool isConfiguration)
        {
            this.isConfiguration = isConfiguration;
        }
        public string Upper(string text)
        {
            return text.ToUpper();
        }
    }
}
