using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace Chemistry.GasConverter
{
    public partial class CharleLaw : PhoneApplicationPage
    {
        public CharleLaw()
        {
            InitializeComponent();
        }
        double Pi = -1;
        double Vi = -1;
        double Pf = -1;
        double Vf = -1;

        private void btnCalc_Click(object sender, RoutedEventArgs e)
        {
            if (includeNumbers())
            {
                //MessageBox.Show(Pi + "\n" + Vi + "\n" + Vf + "\n" + Pf);
                if (Pi == -1)
                {
                    Pi = (Pf * Vf) / Vi;
                    tbOutput.Text = "Pi: " + Pi;
                }
                else  if (Vi == -1)
                {
                    Vi = (Pf * Vf) / Pi;
                    tbOutput.Text = "Vi: " + Vi;
                }
                else if (Pf == -1)
                {
                    Pf = (Pi * Vi) / Vf;
                    tbOutput.Text = "Pf: " + Pf;
                }
                else if (Vf == -1)
                {
                    Vf = (Pi * Vi) / Pf;
                    tbOutput.Text = "Vf: " + Vf;
                }
            }
            else
            {
                MessageBox.Show("Error\nYou need to leave one box empty");
            }

        }
        public bool includeNumbers()
        {
            int empty = 0;
            if (tbPi.Text != "")
            {
                if (!double.TryParse(tbPi.Text, out Pi))
                    empty++;
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
            if (empty != 1)
                return false;
            return true;
        }
    }
}