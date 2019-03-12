using System;
using System.Configuration;
using TechnicalTest.Log;

namespace TechnicalTest.Navigation
{
    internal class Navigation
    {
        private readonly LogErrors _logErrors = new LogErrors();

        internal Uri NavigateHome(string baseDirectory, AppSettingsReader configuration)
        {
            try
            {
                var url = baseDirectory + configuration.GetValue("WebOutput", typeof(string));
                return new Uri(url);
            }
            catch (Exception e)
            {
                _logErrors.ConfigurationError(e);
                throw;
            }
        }
    }
}
