using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using WindowsPhoneDriverBrowser;
using WindowsPhoneDriverBrowser.CommandHandlers;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Xna.Framework;
using PivotApp1.Resources;

namespace PivotApp1
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Set the data context of the listbox control to the sample data
            DataContext = App.ViewModel;

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        // Load data for the ViewModel Items
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Task.Run(() => { 
                var env = new CommandEnvironment(Browser);
                var c1 = new FindElementCommandHandler();
                var rv1 = c1.Execute(env,
                    new Dictionary<string, object>
                    {
                        {"using", "xpath"},
                        {"value", "//*[@id=\"manpage\"]/div/ul[1]/li[2]/a"}
                    });
                Debug.WriteLine(rv1);
                var c = (rv1.Value as Dictionary<string, object>)["ELEMENT"];
                Debug.WriteLine(c);

                var c2 = new GetElementTextCommandHandler();
                var rv2 = c2.Execute(env, new Dictionary<string, object>() {{"ID", c}});
                Debug.WriteLine(rv2);
                if (Debugger.IsAttached)
                {
                    Debugger.Break();
                }
            });
        }

        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}