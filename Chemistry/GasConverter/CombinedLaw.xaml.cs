using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Diagnostics;

namespace Chemistry.GasConverter
{
    public partial class CombinedLaw : PhoneApplicationPage
    {
        public CombinedLaw()
        {
            InitializeComponent();
        }
        double Pi;
        double Vi;
        double Ti;
        double Pf;
        double Vf;
        double Tf;


        private void btnCalc_Click(object sender, RoutedEventArgs e)
        {
            clearAll();
            if (includeNumbers())
            {
                //MessageBox.Show(Pi + "\n" + Vi + "\n" + Vf + "\n" + Pf);
                if (Pi == -1)
                {
                    Pi = (Pf * Vf * Ti) / (Vi * Tf);
                    tbOutput.Text = "Pi: " + Pi;
                }
                else  if (Vi == -1)
                {
                    Vi =  (Pf * Vf * Ti) / (Pi * Tf) ;
                    tbOutput.Text = "Vi: " + Vi;
                }
                else if (Pf == -1)
                {
                    Pf = (Pi * Vi * Tf) / (Ti * Vf);
                    tbOutput.Text = "Pf: " + Pf;
                }
                else if (Vf == -1)
                {
                    Vf = (Pi * Vi * Tf) / (Pf * Ti);
                    tbOutput.Text = "Vf: " + Vf;
                }
                else if (Ti == -1)
                {
                    Ti = (Pi * Vi * Tf) / (Pf * Vf) ;
                    tbOutput.Text = "Ti: " + Ti;
                }
                else if (Tf == -1)
                {
                    Tf =  (Pf * Vf * Ti) / (Vi * Pi);
                    tbOutput.Text = "Tf: " + Tf;
                }
            }
            else
            {
                MessageBox.Show("Error\nYou need to leave one box empty");
            }

        }
        private void clearAll()
        {
            Pi = -1;
            Vi = -1;
            Ti = -1;
            Pf = -1;
            Vf = -1;
            Tf = -1;
        }
        public bool includeNumbers()
        {
            int empty = 0;
            if (tbPi.Text != "")
            {
                if (!double.TryParse(tbPi.Text, out Pi))
                {
                    empty++;
                }
            }
            else
            {
                empty++;
            }
            if (tbVi.Text != "")
            {
                if (!double.TryParse(tbVi.Text, out Vi))
                    empty++;
            }
            else
            {
                empty++;
            }
            if (tbPf.Text != "")
            {
                if (!double.TryParse(tbPf.Text, out Pf))
                    empty++;
            }
            else
            {
                empty++;
            }
            if (tbVf.Text != "")
            {
                if (!double.TryParse(tbVf.Text, out Vf))
                    empty++;
            }
            else
            {
                empty++;
            }
            if (tbTi.Text != "")
            {
                if (!double.TryParse(tbTi.Text, out Ti))
                {
                    empty++;
                }
                else
                {
                    Debug.WriteLine(tbTiSwap.Text);
                    if (tbTiSwap.Text.Contains("F"))
                        Ti = FtoK(Ti);
                    else if (tbTiSwap.Text.Contains("C"))
                        Ti = CtoK(Ti);
                }
            }
            else
            {
                empty++;
            }
            if (tbTf.Text != "")
            {
                if (!double.TryParse(tbTf.Text, out Tf))
                {
                    empty++;
                }
                else
                {
                    Debug.WriteLine(tbTfSwap.Text);
                    if (tbTfSwap.Text.Contains("F"))
                        Tf = FtoK(Tf);
                    else if (tbTfSwap.Text.Contains("C"))
                        Tf = CtoK(Tf);
                }
            }
            else
            {
                empty++;
            }
            if (empty != 1)
                return false;
            return true;
        }
        public double FtoK(double F)
        {
            double C = ((double)5 / 9) * (F - 32);
            double K = CtoK(C);
            return K;
        }
        public double CtoK(double C)
        {
            return C + 273.15;
        }
        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Ti & Tf: are in Kelvin\nVi & Vf: are in liters\nPi & Pf: are in atm","Chemisty", MessageBoxButton.OK);
        }

        private void SwapUnit(object sender, System.Windows.Input.GestureEventArgs e)
        {

            TextBlock block = (TextBlock)((Grid)sender).Children[1];
            Debug.WriteLine(block.Text);

            if (block.Text == "K")
            {
                block.Text = "C";
            }
            else if (block.Text == "C")
            {
                block.Text = "F";
            }
            else if (block.Text == "F")
            {
                block.Text = "K";
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            tbPf.Text = "";
            tbPi.Text = "";
            tbTf.Text = "";
            tbTi.Text = "";
            tbVf.Text = "";
            tbVi.Text = "";
        }
    }
}