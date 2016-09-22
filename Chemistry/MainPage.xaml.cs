using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Chemistry.Resources;
using System.Windows.Media;
using System.Diagnostics;
using System.Collections.ObjectModel;
using Coding4Fun.Toolkit.Storage;
using System.Xml.Serialization;
using System.Xml;
using System.Device.Location;

namespace Chemistry
{
    public partial class MainPage : PhoneApplicationPage
    {
            private GeoCoordinateWatcher gcw = null;
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            DataContext = App.ViewModel;

            #region Animation
            NavigationInTransition navigateInTransition = new NavigationInTransition();
            navigateInTransition.Backward = new SlideTransition { Mode = SlideTransitionMode.SlideUpFadeIn };
            navigateInTransition.Forward = new SlideTransition { Mode = SlideTransitionMode.SlideUpFadeIn };

            NavigationOutTransition navigateOutTransition = new NavigationOutTransition();
            navigateOutTransition.Backward = new SlideTransition { Mode = SlideTransitionMode.SlideDownFadeOut };
            navigateOutTransition.Forward = new SlideTransition { Mode = SlideTransitionMode.SlideUpFadeOut };

            TransitionService.SetNavigationInTransition(this, navigateInTransition);
            TransitionService.SetNavigationOutTransition(this, navigateOutTransition);

            // http://social.msdn.microsoft.com/Forums/silverlight/en-US/291c1434-ec00-427a-bd81-47b0252b1370/subscript-and-superscript-in-textblock?forum=silverlightnet
            #endregion
            #region Tile
            ShellTile tile = ShellTile.ActiveTiles.FirstOrDefault();

            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
            }
            if (tile != null)
            {
                ObservableCollection<Element> table = ((PeriodicTable)DataContext).Items;
                FlipTileData flipTile = new FlipTileData();
                flipTile.Title = "Chemistry Tools";
                flipTile.BackTitle = "Chemistry Tools";
                Random random = new Random();
                int randomElement = random.Next(table.Count);
                string tileStuff =
                    table[randomElement].Name + "\n"
                    + table[randomElement].Amu.ToString() + " \n"
                    + table[randomElement].AtomicNumber + "";

                //Medium size Tile 336x336 px
                flipTile.BackContent = tileStuff;
                flipTile.BackgroundImage = new Uri("/Images/mediumAppIcon.png", UriKind.Relative);
                flipTile.BackBackgroundImage = new Uri("", UriKind.Relative);

                randomElement = random.Next(PeriodicTable.PeriodicTableList.Count);


                //Wide size Tile 691x336 px
                //flipTile.WideBackgroundImage = new Uri("", UriKind.Relative);
                //flipTile.WideBackContent = tileStuff;
                //flipTile.WideBackBackgroundImage = new Uri("", UriKind.Relative);

                //Update Live Tile
                tile.Update(flipTile);
            }
            #endregion

            Visibility isVisible = (Visibility)Application.Current.Resources["PhoneLightThemeVisibility"];
            if (isVisible == System.Windows.Visibility.Visible)
            {
                // Active Windows Phone 8 Theme is Light Theme
                OutputMoles.Background = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
                OutputMoles.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));
                OutputMoles.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
                
            }
            else
            {
                // Active Windows Phone 8 Theme is Dark Theme
                OutputMoles.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
                OutputMoles.Background = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));
            }
            // Makes the System Tray Accent Color
            //    SolidColorBrush x = (SolidColorBrush)Resources["PhoneAccentBrush"];
            //    SystemTray.SetBackgroundColor(this, x.Color);



            this.gcw = new GeoCoordinateWatcher();
            this.gcw.PositionChanged += new EventHandler<GeoPositionChangedEventArgs<GeoCoordinate>>(gcw_PositionChanged);
            this.gcw.Start();
        }
        // Load data for the ViewModel Items
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
            }
        }
        private void Pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //MessageBox.Show("Did you change pages? " + (((Pivot)sender).SelectedIndex).ToString() + " " + (Resources["page0"].ToString()));
            switch (((Pivot)sender).SelectedIndex)
            {
                case 0:
                    
                    ApplicationBar = ((ApplicationBar)Resources["page0"]);
                    break;
                case 1:
                    ApplicationBar = ((ApplicationBar)Resources["page1"]);
                    break;
                case 2:
                    ApplicationBar = ((ApplicationBar)Resources["page2"]);
                    break;
                case 3:
                    ApplicationBar = ((ApplicationBar)Resources["page2"]);
                    break;
            }
        }
        #region page0
        private void FormulaCalc()
        {
            
            if (tbEquationInput.Text == "")
            {

            }
            else
            {
                ObservableCollection<Element> elements = ((PeriodicTable)DataContext).Items;
                string text = tbEquationInput.Text;
                Equation equation = new Equation(text, elements);
                string formula = equation.getFormattedFormula();
                double totalSoFar = equation.getTotalAMU();
                double calcNumber = 0;
                string output = "";
                switch (lbTextbox1.Text)
                {
                    case "grams?":
                        if (double.TryParse(tbInputNumber.Text, out calcNumber))
                        {
                            totalSoFar = calcNumber / totalSoFar;
                            output += "(" + calcNumber + "g) " + formula + " --> " + totalSoFar.ToString() + " moles";
                        }
                        else
                            output += formula + " --> " + totalSoFar.ToString() + " amu";

                        break;
                    case "moles?":
                        if (double.TryParse(tbInputNumber.Text, out calcNumber))
                        {
                            totalSoFar = calcNumber * totalSoFar;
                            output += "(" + calcNumber + " mol) " + formula + " --> " + totalSoFar.ToString() + " grams";
                        }
                        else
                            output += formula + " --> " + totalSoFar.ToString() + " amu";
                        break;
                    default:
                        output += formula + " --> " + totalSoFar.ToString() + " amu\n" + equation.getSolvingSteps(1);
                        break;
                }
                //MessageBox.Show(formula + " --> " + output);
                OutputMoles.Text = output;
            }

        }

        private void swap()
        {
            if (lbTextbox1.Text == "moles?")
            {
                lbTextbox1.Text = "grams?";
                MessageBox.Show("Swapped to grams", "Chemistry", MessageBoxButton.OK);
            }
            else
            {
                lbTextbox1.Text = "moles?";
                MessageBox.Show("Swapped to moles", "Chemistry", MessageBoxButton.OK);
            }
        }
        private void ChangeDirection(object sender, EventArgs e)
        {
            swap();
        }
        private void Help(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/HelpPage.xaml", UriKind.Relative));
            //MessageBox.Show("If the grams/moles box is clear it will return the moleculer amu", "Chemistry", MessageBoxButton.OK);
        }

        private void Calculate(object sender, EventArgs e)
        {
            FormulaCalc();
        }

        private void OutputMoles_GotFocus(object sender, RoutedEventArgs e)
        {
            OutputMoles.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));
        }

        private void OutputMoles_LostFocus(object sender, RoutedEventArgs e)
        {

            OutputMoles.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
        }


        private void lbTextbox1_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            swap();
        }
        #endregion
        
        #region page1
        private void Valence(object sender, EventArgs e)
        {
            ObservableCollection<Element> elements = ((PeriodicTable)DataContext).Items;

            Equation test = new Equation(tbEquationInputPage1.Text, elements);
            if (test.hasTransitionMetal())
                MessageBox.Show("Warning:\nYou used an Transition Metal, the calculated value is the closest guess.", "Chemistry", MessageBoxButton.OK);
            outputValence.Text = test.getFormattedFormula() + " --> " + test.getValence() + " electrons";
        }
        #endregion

        #region page2
        private void tbSearch_TextInput(object sender, TextChangedEventArgs e)
        {
                DataContext = App.ViewModel;
                ObservableCollection<Element> elements = ((PeriodicTable)DataContext).Items;
                ObservableCollection<Element> updatedelements = new ObservableCollection<Element>();
                foreach (Element atom in elements)
                {
                    if ((atom.Name.ToLower().Contains(tbSearch.Text.ToLower()) || atom.Symbol.ToLower().Contains(tbSearch.Text.ToLower())) || tbSearch.Text == "Search")
                    {
                        updatedelements.Add(atom);
                    }
                }
                PeriodicTable newTable = new PeriodicTable();
                newTable.Items = updatedelements;
                DataContext = newTable;

        }
        private void MainLongListSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // If selected item is null (no selection) do nothing
            if (ElementList.SelectedItem == null)
                return;

            // Navigate to the new page
            NavigationService.Navigate(new Uri("/DetailsElementPage.xaml?selectedItem=" + (ElementList.SelectedItem as Element).AtomicNumber, UriKind.Relative));

            // Reset selected item to null (no selection)
            ElementList.SelectedItem = null;
        }
        #endregion

        #region ad
        private void gcw_PositionChanged(object sender, GeoPositionChangedEventArgs<GeoCoordinate> e)
        {
            this.gcw.Stop();

            ad.Latitude = e.Position.Location.Latitude;
            ad.Longitude = e.Position.Location.Longitude;


            adGas.Latitude = e.Position.Location.Latitude;
            adGas.Longitude = e.Position.Location.Longitude;

            Debug.WriteLine("Device lat/long: " + e.Position.Location.Latitude + ", " + e.Position.Location.Longitude);
        }
        private void ad_ErrorOccurred(object sender, Microsoft.Advertising.AdErrorEventArgs e)
        {
            Debug.WriteLine("AdControl error: " + e.Error.Message);
            test.Height = new GridLength(0);

        }
        private void ad_AdRefreshed(object sender, EventArgs e)
        {
            Debug.WriteLine("AdControl new ad received");
            test.Height = new GridLength(80);

        }
        #endregion


        private void Grid_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (((Grid)sender).Name == "BoyleLaw")
            {
                NavigationService.Navigate(new Uri("/GasConverter/BoyleLaw.xaml", UriKind.Relative));

            }
            if (((Grid)sender).Name == "CombinedLaw")
            {
                NavigationService.Navigate(new Uri("/GasConverter/CombinedLaw.xaml", UriKind.Relative));

            }
        }

        private void tbSearch_GotFocus(object sender, RoutedEventArgs e)
        {
            if(tbSearch.Text == "Search")
                tbSearch.Text = "";
        }

        private void tbSearch_LostFocus(object sender, RoutedEventArgs e)
        {
            if(tbSearch.Text == "")
                tbSearch.Text = "Search";
        }

    }
}