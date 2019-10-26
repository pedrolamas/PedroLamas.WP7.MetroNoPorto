using PedroLamas.WP7.MetroNoPorto.Framework;
using System.Windows;
using PedroLamas.WP7.MetroNoPorto.Models;
using System;

namespace PedroLamas.WP7.MetroNoPorto.ViewModels
{
    public class MainPageViewModel : Screen
    {
        private IMetroDoPortoService _service;
        private IMetroDoPortoStatusResult _lastResult;
        private bool _busy;

        #region Properties

        public IMetroDoPortoStatusResult LastResult
        {
            get
            {
                return _lastResult;
            }
            set
            {
                if (_lastResult == value)
                    return;

                _lastResult = value;

                NotifyOfPropertyChange(() => LastResult);
                NotifyOfPropertyChange(() => Lines);
                NotifyOfPropertyChange(() => LastUpdate);
            }
        }

        public IMetroDoPortoLine[] Lines
        {
            get
            {
                if (_lastResult == null)
                    return null;

                return _lastResult.Lines;
            }
        }

        public string LastUpdate
        {
            get
            {
                if (_lastResult == null)
                    return null;

                return "Última actualização: " + _lastResult.TimeStamp.ToString("G");
            }
        }

        public bool Busy
        {
            get
            {
                return _busy;
            }
            set
            {
                if (_busy == value)
                    return;

                _busy = value;

                NotifyOfPropertyChange(() => Busy);
                NotifyOfPropertyChange(() => BusyIndicator);
            }
        }

        public Visibility BusyIndicator
        {
            get
            {
                return _busy ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        #endregion

        public MainPageViewModel()
        {
            _service = new MetroDoPortoService();
        }

        public void UpdateData()
        {
            if (_busy)
                return;

            Busy = true;

            _service.GetStatus(DataUpdated);
        }

        private void DataUpdated(IMetroDoPortoStatusResult result)
        {
            System.Windows.Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                if (result.Error != null)
                    MessageBox.Show("Ocorreu um erro ao actualizar os dados!", "Erro", MessageBoxButton.OK);
                else
                    LastResult = result;

                Busy = false;
            });
        }

        public void ShowAbout()
        {
            PedroLamas.WP7.MetroNoPorto.Controls.About.Show();
        }
    }
}