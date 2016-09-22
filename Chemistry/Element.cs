using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chemistry
{
    public class Element : INotifyPropertyChanged
    {
        public Element(Element copy)
        {
            this.atomicNumber = copy.atomicNumber;
            this.amu = copy.amu;
            this.name = copy.name;
            this.symbol = copy.symbol;
            this.category = copy.category;
            this.period = copy.period;
            this.valence = copy.valence;
            this.count = copy.count;
            this.config = copy.config;
            this.negativity = copy.negativity;

        }
        public Element()
        {

        }

        private int atomicNumber = 0;
        private double amu = 0;
        private string name = "";
        private string symbol = "";
        private cat category = cat.not_added;
        private int period = -1;
        private int valence = 0;
        private int count = 1;
        private string config = "";
        private double negativity = 0.0;
        public enum cat 
        {
            Nonmetal,
            Transition_Metal,
            Noble_Gas,
            Alkali,
            Alkaline,
            Lanthancid,
            Metalloid,
            Halogen,
            Actinoids,
            Poor_Metal,
            not_added
        }

        /*public Element(int number, double amu, string name, string symbol)
        {
            this.atomicNumber = number;
            this.amu = amu;
            this.name = name;
            this.symbol = symbol;
        }*/
        public int Count
        {
            get
            {
                return count;
            }
            set
            {
                if (value != amu)
                {
                    count = value;
                    NotifyPropertyChanged("Count");
                }
            }
        }

        public double Amu
        {
            get
            {
                return amu;
            }
            set
            {
                if (value != amu)
                {
                    amu = value;
                    NotifyPropertyChanged("Amu");
                }
            }
        }
        public double Electronegativity
        {
            get
            {
                return negativity;
            }
            set
            {
                if (value != negativity)
                {
                    negativity = value;
                    NotifyPropertyChanged("Electronegativity");
                }
            }
        }
        public string Name
        {
            get
            {
                return  name;
            }
            set
            {
                if (value != name)
                {
                    name = value;
                    NotifyPropertyChanged("Name");
                }
            }
        }
        public string AtomicConfig
        {
            get
            {
                return config;
            }
            set
            {
                if (value != config)
                {
                    config = value;
                    NotifyPropertyChanged("AtomicConfig");
                }
            }
        }
        public string Symbol
        {
            get
            {
                return symbol;
            }
            set
            {
                if (value !=symbol)
                {
                    symbol = value;
                    NotifyPropertyChanged("Symbol");
                }
            }
        }
        public string Category
        {
            get
            {
                string output = category.ToString();
                output = output.Replace('_', ' ');
                return output;
            }
            set
            {
                if (value != "cat." + category)
                {
                    category = (cat) Enum.Parse(typeof (cat), value, false);
                    NotifyPropertyChanged("Category");
                }
            }
        }
        public cat CategoryInput
        {
            get
            {
                return category;
            }
            set
            {
                if (value != category)
                {
                    category = value;
                    NotifyPropertyChanged("Category");
                }
            }
        }
        public int Valence
        {
            get
            {
                return valence;
            }
            set
            {
                if (value != valence)
                {
                    valence = value;
                    NotifyPropertyChanged("Valence");
                }
            }
        }
        public int AtomicNumber
        {
            get
            {
                return atomicNumber;
            }
            set
            {
                if (value != atomicNumber)
                {
                    atomicNumber = value;
                    NotifyPropertyChanged("AtomicNumber");
                }
            }
        }
        public int Period
        {
            get
            {
                return period;
            }
            set
            {
                if (value != period)
                {
                    period = value;
                    NotifyPropertyChanged("Period");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
