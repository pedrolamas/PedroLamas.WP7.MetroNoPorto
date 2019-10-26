using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;
using System.Windows.Controls.Primitives;

namespace PedroLamas.WP7.MetroNoPorto.Controls
{
    public partial class About : UserControl
    {
        private static PhoneApplicationPage _phoneApplicationPage;
        private static Popup _popup;
        private static bool _reshowAppBar;

        #region Properties

        public string ApplicationName { get; set; }

        #endregion

        public About()
        {
            InitializeComponent();

            ApplicationName = GetAppAttribute("Title").ToLower();
            DataContext = this;
        }

        private void HomepageTextBlock_ManipulationCompleted(object sender, ManipulationCompletedEventArgs e)
        {
            new WebBrowserTask
            {
                URL = "http://www.pedrolamas.com"
            }.Show();
        }

        private void EMailTextBlock_ManipulationCompleted(object sender, ManipulationCompletedEventArgs e)
        {
            new EmailComposeTask
            {
                To = "pedrolamas@gmail.com",
                Subject = GetAppAttribute("Title")
            }.Show();
        }

        private void TwitterTextBlock_ManipulationCompleted(object sender, ManipulationCompletedEventArgs e)
        {
            new WebBrowserTask
            {
                URL = "http://twitter.com/pedrolamas"
            }.Show();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        public static void Close()
        {
            if (_popup == null)
                throw new InvalidOperationException("About is not visible.");

            if (_reshowAppBar)
                _phoneApplicationPage.ApplicationBar.IsVisible = true;

            _phoneApplicationPage.BackKeyPress -= BackKeyPress;

            _popup.IsOpen = false;
            _popup = null;
        }

        public static void Show()
        {
            if (_popup != null)
                throw new InvalidOperationException("About is already shown.");

            var phoneApplicationFrame = (PhoneApplicationFrame)System.Windows.Application.Current.RootVisual;
            var dependencyObject = (DependencyObject)phoneApplicationFrame.Content;

            while (!(dependencyObject is PhoneApplicationPage))
                dependencyObject = VisualTreeHelper.GetChild(dependencyObject, 0);

            _phoneApplicationPage = (PhoneApplicationPage)dependencyObject;

            if (_phoneApplicationPage.ApplicationBar != null && _phoneApplicationPage.ApplicationBar.IsVisible)
            {
                _reshowAppBar = true;
                _phoneApplicationPage.ApplicationBar.IsVisible = false;
            }

            _phoneApplicationPage.BackKeyPress += BackKeyPress;

            _popup = new Popup
            {
                IsOpen = true,
                Child = new About()
                {
                    Width = phoneApplicationFrame.ActualWidth,
                    Height = phoneApplicationFrame.ActualHeight
                }
            };
        }

        private static void BackKeyPress(object sender, CancelEventArgs e)
        {
            e.Cancel = true;

            Close();
        }

        private string GetAppAttribute(string attributeName)
        {
            string appManifestName = "WMAppManifest.xml";
            string appNodeName = "App";

            var settings = new XmlReaderSettings();
            settings.XmlResolver = new XmlXapResolver();

            using (var rdr = XmlReader.Create(appManifestName, settings))
            {
                rdr.ReadToDescendant(appNodeName);

                if (!rdr.IsStartElement())
                    throw new FormatException(appManifestName + " is missing " + appNodeName);

                return rdr.GetAttribute(attributeName);
            }
        }
    }

    //private static class DependencyObjectExtensions
    //{
    //    public static DependencyObject[] Descendants(DependencyObject dependencyObject)
    //    {
    //        var count = VisualTreeHelper.GetChildrenCount(dependencyObject);
    //        var dependencyObjects = new DependencyObject[count];

    //        for (var index = 0; index < count; index++)
    //            dependencyObjects[index] = VisualTreeHelper.GetChild(dependencyObject, index);

    //        return dependencyObjects;
    //    }
    //}
}