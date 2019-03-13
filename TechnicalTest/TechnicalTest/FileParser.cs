using System.Configuration;
using System.Windows.Forms;
using TechnicalTest.Enums;
using TechnicalTest.Helpers;
using TechnicalTest.Log;


namespace TechnicalTest
{
    public partial class FileParser : Form
    {
        private readonly DbHelper _dbHelper = new DbHelper();
        private HtmlDocument _currentDocument;
        private readonly LogErrors _logErrors = new LogErrors();
        private readonly Navigation.Navigation _navigation = new Navigation.Navigation();
        private readonly Pages.Pages _pages = new Pages.Pages();
        public FileParser(string baseDirectory, AppSettingsReader configuration)
        {
            InitializeComponent();
            
           browser.Navigate(_navigation.NavigateHome(baseDirectory, configuration));
            new HtmlHelper(_dbHelper);
            
        }

        

        

        private void Browser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            _currentDocument = browser.Document;
            if (_currentDocument != null)
            {
                if (_currentDocument?.Title == null)
                {
                    _logErrors.NoTitleTag();
                }
                //else
                //{
                //    _pages.SetupPage(_currentDocument, _htmlHelper);
                //}
                
                _currentDocument.Click += OnClick;
            }
            else
            {
                _logErrors.ErrorWithHtmlPageLoading();
            }
        }

        



        void OnClick(object sender, HtmlElementEventArgs e)
        {
            
            var activeElement = _currentDocument.ActiveElement;

            if (activeElement != null && (activeElement.TagName.Equals(ElementType.Button.ToString().ToUpper()) || activeElement.TagName.Equals(ElementType.A.ToString().ToUpper())))
            {
                _pages.SetupClickEventsForPage(_currentDocument.Title, activeElement, _currentDocument);
                
            }



        }

        


        



    }
}
