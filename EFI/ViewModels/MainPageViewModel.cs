using EFI.Services;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EFI.ViewModels
{
    public class MainPageViewModel : NotificationObject
    {
        #region Members
        private IEconomy service;
        #endregion

        #region Instance DelegateCommand
        public DelegateCommand<RoutedEventArgs> OnLoadedCommand { get; private set; }
        #endregion

        #region Constructor
        public MainPageViewModel(IEconomy service)
        {
            #region services
            this.service = service;
            #endregion

            #region DelegatCommand
            this.OnLoadedCommand = new DelegateCommand<RoutedEventArgs>(OnLoaded);
            #endregion
        }
        #endregion

        #region Method
        private void OnLoaded(RoutedEventArgs e)
        {
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                service.GetDataKeuangan((result, ex) =>
                {
                    var a = result;
                });
            });
        }
        #endregion
    }
}
