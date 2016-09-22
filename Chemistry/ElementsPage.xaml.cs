using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Advertising.Mobile.UI;
using System.Diagnostics;
using System.Device.Location;
using System.Windows.Media;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace Chemistry
{
    public partial class ElementsPage : PhoneApplicationPage
    {
        private GeoCoordinateWatcher gcw = null;
        //ProgressIndicator prog;
        public ElementsPage()
        {
            /* Progress Bar: currently too fast for this to work
            prog = new ProgressIndicator();
            prog.Text = "Getting Elements";
            prog.IsVisible = true;
            prog.IsIndeterminate = false;
            SystemTray.SetProgressIndicator(this, prog);
            //SystemTray.BackgroundColor = Color.FromArgb(255, 55, 55, 55);*/
            InitializeComponent();
            DataContext = App.ViewModel;



            NavigationInTransition navigateInTransition = new NavigationInTransition();
            navigateInTransition.Backward = new SlideTransition { Mode = SlideTransitionMode.SlideDownFadeIn };
            navigateInTransition.Forward = new SlideTransition { Mode = SlideTransitionMode.SlideUpFadeIn };

            NavigationOutTransition navigateOutTransition = new NavigationOutTransition();
            navigateOutTransition.Backward = new SlideTransition { Mode = SlideTransitionMode.SlideDownFadeOut };
            navigateOutTransition.Forward = new SlideTransition { Mode = SlideTransitionMode.SlideUpFadeOut };

            TransitionService.SetNavigationInTransition(this, navigateInTransition);
            TransitionService.SetNavigationOutTransition(this, navigateOutTransition);

            this.gcw = new GeoCoordinateWatcher();
            this.gcw.PositionChanged += new EventHandler<GeoPositionChangedEventArgs<GeoCoordinate>>(gcw_PositionChanged);
            this.gcw.Start();
        }

        /* Load data for the ViewModel Items
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
            }
        }
        // Handle selection changed on LongListSelector*/
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
        #region oldstuff
        /* public void addElements()
        {

            List<Element> table = PeriodicTable.getElements();
            elements = table;
            int i = 0;
            foreach (Element atom in table)
            {

                i++;
                prog.Value = i / table.Count;
                string output = "";
                if (atom.getSymbol().Length == 1)
                    output += atom.getSymbol() + "  ";
                else
                    output += atom.getSymbol() + " ";
                RowDefinition rowone = new RowDefinition();
                //rowone.Height = 30;
                RowDefinition rowtwo = new RowDefinition();
                Grid grid = new Grid();
                grid.RowDefinitions.Add(rowone);
                grid.RowDefinitions.Add(rowtwo);
                TextBlock outputTb;
                TextBlock secondLine;
                outputTb = new TextBlock();
                secondLine = new TextBlock();
                Grid.SetRow(outputTb, 0);
                Grid.SetRow(secondLine, 1);
                grid.Children.Add(outputTb);
                grid.Children.Add(secondLine);
                ListElements.Children.Add(grid);



                grid.Name = atom.getSymbol();
                grid.Tap += getElementSpecs;

                output += atom.getName() + " ";
                outputTb.Text = atom.getName(); //output
                //outputTb.Name = atom.getSymbol();
                //outputTb.Tap += getElementSpecs;
                outputTb.FontSize = 30;
                outputTb.Padding = new Thickness(0, 0, 0, 5);
                outputTb.Margin = new Thickness(5, 5, 5, 5);

                secondLine.Text = atom.getSymbol() + " - " + atom.getAmu();
                secondLine.FontSize = 24;
                secondLine.Padding = new Thickness(0, 0, 0, 5);
                secondLine.Margin = new Thickness(20, 3, 10, 5);

            }
            prog.IsVisible = false;

        }*/
        #endregion
        private Element getElement(string text)
        {

            ObservableCollection<Element> elements = ((PeriodicTable)DataContext).Items;
            for (int i = 0; i < text.Length; i++)
            {
                bool boolbreak = false;
                if (Char.IsUpper(text, i))
                {
                    if (i + 1 < text.Length)
                    {
                        if (Char.IsLower(text, i + 1))
                        {
                            string letter = text.Substring(i, 2);
                            for (int j = 0; j < elements.Count; j++)
                            {
                                if (elements[j].Symbol == letter)
                                {
                                    return elements[j];

                                }
                            }
                        }
                        else
                        {
                            boolbreak = true;
                        }
                    }
                    else
                    {
                        boolbreak = true;
                    }
                    if (Char.IsUpper(text, i) && boolbreak)
                    {
                        string letter = text.Substring(i, 1);
                        for (int j = 0; j < elements.Count; j++)
                        {
                            if (elements[j].Symbol == letter)
                            {
                                return elements[j];
                            }
                        }

                    }
                }
            }
            return null;
        }


        private void getElementSpecs(object sender, System.Windows.Input.GestureEventArgs e)
        {
            //Debug.WriteLine("testing 1 2 3");
            string text = ((StackPanel)sender).Name;
            Element atom = getElement(text);
            if (atom != null)
            {
                MessageBox.Show("Atomic Number: " + atom.AtomicNumber + "\n" +
                    "Name: " + atom.Name + "\n" +
                    "Symbol: " + atom.Symbol + "\n" +
                    "Atomic Mass: " + atom.Amu
                    , "Chemistry", MessageBoxButton.OK);
            }
        }

        #region ad
        private void gcw_PositionChanged(object sender, GeoPositionChangedEventArgs<GeoCoordinate> e)
        {
            this.gcw.Stop();

            ad.Latitude = e.Position.Location.Latitude;
            ad.Longitude = e.Position.Location.Longitude;

            Debug.WriteLine("Device lat/long: " + e.Position.Location.Latitude + ", " + e.Position.Location.Longitude);
        }
        private void ad_ErrorOccurred(object sender, Microsoft.Advertising.AdErrorEventArgs e)
        {
            Debug.WriteLine("AdControl error: " + e.Error.Message);

        }
        private void ad_AdRefreshed(object sender, EventArgs e)
        {
            Debug.WriteLine("AdControl new ad received");

        }
        #endregion
        private void btnSearch_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            string text = tbSearch.Text;
            Element atom = getElement(text);
            if (atom != null)
            {
                MessageBox.Show("Atomic Number: " + atom.AtomicNumber + "\n" +
                    "Name: " + atom.Name + "\n" +
                    "Symbol: " + atom.Symbol + "\n" +
                    "Atomic Mass: " + atom.Amu
                    , "Chemistry", MessageBoxButton.OK);
            }
        }
        private void tbSearch_TextInput(object sender, TextChangedEventArgs e)
        {
            DataContext = App.ViewModel;
            ObservableCollection<Element> elements = ((PeriodicTable)DataContext).Items;
            ObservableCollection<Element> updatedelements = new ObservableCollection<Element>();
            foreach (Element atom in elements)
            {
                if (atom.Name.ToLower().Contains(tbSearch.Text.ToLower()) || atom.Symbol.ToLower().Contains(tbSearch.Text.ToLower()))
                {
                    updatedelements.Add(atom);
                    //Debug.WriteLine(atom.Name);
                }
            }
            PeriodicTable newTable = new PeriodicTable();
            newTable.Items = updatedelements;
            DataContext = newTable;

        }
    }
}