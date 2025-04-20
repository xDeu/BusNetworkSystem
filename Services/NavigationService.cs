using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BusNetworkSystem.Services
{
    public interface INavigationService
    {
        void NavigateTo(Type pageType);
        void NavigateTo(Type pageType, object parameter);
        void GoBack();
    }
    internal class NavigationService : INavigationService
    {
        private readonly Dictionary<Type, Func<Page>> _pages = new Dictionary<Type, Func<Page>>();
        private Frame _mainFrame;

        public void Configure(Type pageType, Func<Page> pageCreator)
        {
            _pages[pageType] = pageCreator;
        }
        public void Initialize(Frame frame)
        {
            _mainFrame = frame;
        }
        public void NavigateTo(Type pageType)
        {
            if (_pages.TryGetValue(pageType, out var pageCreator)) 
            {
                _mainFrame.Navigate(pageCreator());
            }
        }
        public void NavigateTo(Type pageType, object parameter)
        {
            if (_pages.TryGetValue(pageType, out var pageCreator))
            {
                _mainFrame.Navigate(pageCreator(), parameter);
            }
        }
        public void GoBack()
        {
            if (_mainFrame.CanGoBack)
            {
                _mainFrame.GoBack();
            }
        }
    }
}
