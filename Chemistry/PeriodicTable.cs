using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Xml;
using System.Xml.Linq;
using Coding4Fun.Toolkit.Storage;

namespace Chemistry
{
    public class PeriodicTable : INotifyPropertyChanged
    {
        public ObservableCollection<Element> Items { get; set; }
        static public List<Element> PeriodicTableList = new List<Element>();
        private string _sampleProperty = "Sample Runtime Property Value";


        public ObservableCollection<Element> items
        {
            get
            {
                return Items;
            }
            set
            {
                if (value != Items)
                {
                    Items = value;
                    NotifyPropertyChanged("Items");
                }
            }
        }

        public string SampleProperty
        {
            get
            {
                return _sampleProperty;
            }
            set
            {
                if (value != _sampleProperty)
                {
                    _sampleProperty = value;
                    NotifyPropertyChanged("SampleProperty");
                }
            }
        }

        public PeriodicTable()
        {
            this.Items = new ObservableCollection<Element>();
        }
        public ObservableCollection<Element> getElements()
        {
            return this.Items;
        }

        public bool IsDataLoaded
        {
            get;
            private set;
        }
        static public string convertSubscript(string input)
        {
            string output = "";
            foreach (char digit in input.ToCharArray())
            {
                switch (digit)
                {
                    case '0':
                        output += "\x2080";
                        break;
                    case '1':
                        output += "\x2081";
                        break;
                    case '2':
                        output += "\x2082";
                        break;
                    case '3':
                        output += "\x2083";
                        break;
                    case '4':
                        output += "\x2084";
                        break;
                    case '5':
                        output += "\x2085";
                        break;
                    case '6':
                        output += "\x2086";
                        break;
                    case '7':
                        output += "\x2087";
                        break;
                    case '8':
                        output += "\x2088";
                        break;
                    case '9':
                        output += "\x2089";
                        break;
                }
            }

            return output;
        }

        public void LoadData()
        {
            Items.Clear();
            XmlReader xmlTest = XmlReader.Create("Data/elementsList.xml");
            while (xmlTest.Read())
            {
                if (xmlTest.NodeType == XmlNodeType.Element && xmlTest.Name == "Element")
                {
                    if (xmlTest.HasAttributes)
                    {
                        Element newElement = new Element();

                        string testAtomicNumber = xmlTest.GetAttribute("AtomicNumber");
                        if (testAtomicNumber != null)
                            newElement.AtomicNumber = Int32.Parse(testAtomicNumber);

                        string testAmu = xmlTest.GetAttribute("Amu");
                        if (testAmu != null)
                            newElement.Amu = Double.Parse(testAmu);

                        string testName = xmlTest.GetAttribute("Name");
                        if (testName != null)
                            newElement.Name = testName;

                        string testSymbol = xmlTest.GetAttribute("Symbol");
                        if (testSymbol != null)
                            newElement.Symbol = testSymbol;

                        string testCategory = xmlTest.GetAttribute("Category");
                        if (testCategory != null)
                            newElement.Category = testCategory;

                        string testPeriod = xmlTest.GetAttribute("Period");
                        if (testPeriod != null)
                            newElement.Period = Int32.Parse(testPeriod);

                        string testString = xmlTest.GetAttribute("Valence");
                        if (testString != null)
                            newElement.Valence = Int32.Parse(testString);

                        string testConfig = xmlTest.GetAttribute("Config");
                        if (testConfig != null)
                            newElement.AtomicConfig = testConfig;

                        string testNegativity = xmlTest.GetAttribute("Negativity");
                        if (testNegativity != null)
                            newElement.Electronegativity = Double.Parse(testNegativity);

                        this.Items.Add(newElement);

                    }
                }
            }



            //    this.Items.Add(new Element() { AtomicNumber = 1,  Amu = 1.00794     , Name = "Hydrogen"     , Symbol = "H" , CategoryInput = Element.cat.Nonmetal           , Period = 1, Valence = 1});
            //    this.Items.Add(new Element() { AtomicNumber = 2,  Amu = 4.0026      , Name = "Helium"       , Symbol = "He", CategoryInput = Element.cat.Noble_Gas          , Period = 1, Valence = 2});
            //    this.Items.Add(new Element() { AtomicNumber = 3,  Amu = 6.941       , Name = "Lithium"      , Symbol = "Li", CategoryInput = Element.cat.Alkali             , Period = 2, Valence = 1 });
            //    this.Items.Add(new Element() { AtomicNumber = 4,  Amu = 9.01218     , Name = "Beryllium"    , Symbol = "Be", CategoryInput = Element.cat.Alkaline           , Period = 2, Valence = 2 });
            //    this.Items.Add(new Element() { AtomicNumber = 5,  Amu = 10.811      , Name = "Boron"        , Symbol = "B" , CategoryInput = Element.cat.Metalloid          , Period = 2, Valence = 3 });
            //    this.Items.Add(new Element() { AtomicNumber = 6,  Amu = 12.011      , Name = "Carbon"       , Symbol = "C" , CategoryInput = Element.cat.Nonmetal           , Period = 2, Valence = 4 });
            //    this.Items.Add(new Element() { AtomicNumber = 7,  Amu = 14.0067     , Name = "Nitrogen"     , Symbol = "N" , CategoryInput = Element.cat.Nonmetal           , Period = 2, Valence = 5 });
            //    this.Items.Add(new Element() { AtomicNumber = 8,  Amu = 15.9994     , Name = "Oxygen"       , Symbol = "O" , CategoryInput = Element.cat.Nonmetal           , Period = 2,   Valence = 6 });
            //    this.Items.Add(new Element() { AtomicNumber = 9,  Amu = 18.9984     , Name = "Fluorine"     , Symbol = "F" , CategoryInput = Element.cat.Halogen            , Period = 2, Valence = 7 });
            //    this.Items.Add(new Element() { AtomicNumber = 10, Amu = 20.1797     , Name = "Neon"         , Symbol = "Ne", CategoryInput = Element.cat.Noble_Gas          , Period = 2,   Valence = 8  });
            //    this.Items.Add(new Element() { AtomicNumber = 11, Amu = 22.98977    , Name = "Sodium"       , Symbol = "Na", CategoryInput = Element.cat.Alkali             , Period = 3, Valence = 1});
            //    this.Items.Add(new Element() { AtomicNumber = 12, Amu = 24.305      , Name = "Magnesium"    , Symbol = "Mg", CategoryInput = Element.cat.Alkaline           , Period = 3 });
            //    this.Items.Add(new Element() { AtomicNumber = 13, Amu = 26.98154    , Name = "Aluminium"    , Symbol = "Al", CategoryInput = Element.cat.Poor_Metal         , Period = 3 });
            //    this.Items.Add(new Element() { AtomicNumber = 14, Amu = 28.0855     , Name = "Silicon"      , Symbol = "Si", CategoryInput = Element.cat.Metalloid          , Period = 3 });
            //    this.Items.Add(new Element() { AtomicNumber = 15, Amu = 30.97376    , Name = "Phosphorus"   , Symbol = "P" , CategoryInput = Element.cat.Nonmetal           , Period = 3 });
            //    this.Items.Add(new Element() { AtomicNumber = 16, Amu = 32.066      , Name = "Sulfur"       , Symbol = "S" , CategoryInput = Element.cat.Nonmetal           , Period = 3 });
            //    this.Items.Add(new Element() { AtomicNumber = 17, Amu = 35.4527     , Name = "Chlorine"     , Symbol = "Cl", CategoryInput = Element.cat.Halogen            , Period = 3 });
            //    this.Items.Add(new Element() { AtomicNumber = 18, Amu = 39.948      , Name = "Argon"        , Symbol = "Ar", CategoryInput = Element.cat.Noble_Gas          , Period = 3 });
            //    this.Items.Add(new Element() { AtomicNumber = 19, Amu = 39.0983     , Name = "Potassium"    , Symbol = "K" , CategoryInput = Element.cat.Alkali             , Period = 4 });
            //    this.Items.Add(new Element() { AtomicNumber = 20, Amu = 40.078      , Name = "Calcium"      , Symbol = "Ca", CategoryInput = Element.cat.Alkaline           , Period = 4 });
            //    this.Items.Add(new Element() { AtomicNumber = 21, Amu = 44.9559     , Name = "Scandium"     , Symbol = "Sc", CategoryInput = Element.cat.Transition_Metal   , Period = 4 });
            //    this.Items.Add(new Element() { AtomicNumber = 22, Amu = 47.88       , Name = "Titanium"     , Symbol = "Ti", CategoryInput = Element.cat.Transition_Metal   , Period = 4 });
            //    this.Items.Add(new Element() { AtomicNumber = 23, Amu = 50.9415     , Name = "Vanadium"     , Symbol = "V" , CategoryInput = Element.cat.Transition_Metal   , Period = 4 });
            //    this.Items.Add(new Element() { AtomicNumber = 24, Amu = 51.996      , Name = "Chromium"     , Symbol = "Cr", CategoryInput = Element.cat.Transition_Metal   , Period = 4 });
            //    this.Items.Add(new Element() { AtomicNumber = 25, Amu = 54.938      , Name = "Manganese"    , Symbol = "Mn", CategoryInput = Element.cat.Transition_Metal   , Period = 4 });
            //    this.Items.Add(new Element() { AtomicNumber = 26, Amu = 55.847      , Name = "Iron"         , Symbol = "Fe", CategoryInput = Element.cat.Transition_Metal   , Period = 4 });
            //    this.Items.Add(new Element() { AtomicNumber = 27, Amu = 58.9332     , Name = "Cobalt"       , Symbol = "Co", CategoryInput = Element.cat.Transition_Metal   , Period = 4 });
            //    this.Items.Add(new Element() { AtomicNumber = 28, Amu = 58.6934     , Name = "Nickel"       , Symbol = "Ni", CategoryInput = Element.cat.Transition_Metal   , Period = 4 });
            //    this.Items.Add(new Element() { AtomicNumber = 29, Amu = 63.546      , Name = "Copper"       , Symbol = "Cu", CategoryInput = Element.cat.Transition_Metal   , Period = 4 });
            //    this.Items.Add(new Element() { AtomicNumber = 30, Amu = 65.39       , Name = "Zinc"         , Symbol = "Zn", CategoryInput = Element.cat.Transition_Metal   , Period = 4 });
            //    this.Items.Add(new Element() { AtomicNumber = 31, Amu = 69.723      , Name = "Gallium"      , Symbol = "Ga", CategoryInput = Element.cat.Poor_Metal         , Period = 4 });
            //    this.Items.Add(new Element() { AtomicNumber = 32, Amu = 72.61       , Name = "Germanium"    , Symbol = "Ge", CategoryInput = Element.cat.Metalloid          , Period = 4 });
            //    this.Items.Add(new Element() { AtomicNumber = 33, Amu = 74.9216     , Name = "Arsenic"      , Symbol = "As", CategoryInput = Element.cat.Metalloid          , Period = 4 });
            //    this.Items.Add(new Element() { AtomicNumber = 34, Amu = 78.96       , Name = "Selenium"     , Symbol = "Se", CategoryInput = Element.cat.Nonmetal           , Period = 4 });
            //    this.Items.Add(new Element() { AtomicNumber = 35, Amu = 79.904      , Name = "Bromine"      , Symbol = "Br", CategoryInput = Element.cat.Halogen            , Period = 4 });
            //    this.Items.Add(new Element() { AtomicNumber = 36, Amu = 83.8        , Name = "Krypton"      , Symbol = "Kr", CategoryInput = Element.cat.Noble_Gas          , Period = 4 });
            //    this.Items.Add(new Element() { AtomicNumber = 37, Amu = 85.4678     , Name = "Rubidium"     , Symbol = "Rb", CategoryInput = Element.cat.Alkali             , Period = 5 });
            //    this.Items.Add(new Element() { AtomicNumber = 38, Amu = 87.62       , Name = "Strontium"    , Symbol = "Sr", CategoryInput = Element.cat.Alkaline           , Period = 5 });
            //    this.Items.Add(new Element() { AtomicNumber = 39, Amu = 88.9059     , Name = "Yttrium"      , Symbol = "Y" , CategoryInput = Element.cat.Transition_Metal   , Period = 5 });
            //    this.Items.Add(new Element() { AtomicNumber = 40, Amu = 91.224      , Name = "Zirconium"    , Symbol = "Zr", CategoryInput = Element.cat.Transition_Metal   , Period = 5 });
            //    this.Items.Add(new Element() { AtomicNumber = 41, Amu = 92.9064     , Name = "Niobium"      , Symbol = "Nb", CategoryInput = Element.cat.Transition_Metal   , Period = 5 });
            //    this.Items.Add(new Element() { AtomicNumber = 42, Amu = 95.94       , Name = "Molybdenum"   , Symbol = "Mo", CategoryInput = Element.cat.Transition_Metal   , Period = 5 });
            //    this.Items.Add(new Element() { AtomicNumber = 43, Amu = 98          , Name = "Technetium"   , Symbol = "Tc", CategoryInput = Element.cat.Transition_Metal   , Period = 5 });
            //    this.Items.Add(new Element() { AtomicNumber = 44, Amu = 101.07      , Name = "Ruthenium"    , Symbol = "Ru", CategoryInput = Element.cat.Transition_Metal   , Period = 5 });
            //    this.Items.Add(new Element() { AtomicNumber = 45, Amu = 102.9055    , Name = "Rhodium"      , Symbol = "Rh", CategoryInput = Element.cat.Transition_Metal   , Period = 5 });
            //    this.Items.Add(new Element() { AtomicNumber = 46, Amu = 106.42      , Name = "Palladium"    , Symbol = "Pd", CategoryInput = Element.cat.Transition_Metal   , Period = 5 });
            //    this.Items.Add(new Element() { AtomicNumber = 47, Amu = 107.868     , Name = "Silver"       , Symbol = "Ag", CategoryInput = Element.cat.Transition_Metal   , Period = 5 });
            //    this.Items.Add(new Element() { AtomicNumber = 48, Amu = 112.41      , Name = "Cadmium"      , Symbol = "Cd", CategoryInput = Element.cat.Transition_Metal   , Period = 5 });
            //    this.Items.Add(new Element() { AtomicNumber = 49, Amu = 114.82      , Name = "Indium"       , Symbol = "In", CategoryInput = Element.cat.Poor_Metal         , Period = 5 });
            //    this.Items.Add(new Element() { AtomicNumber = 50, Amu = 118.71      , Name = "Tin"          , Symbol = "Sn", CategoryInput = Element.cat.Poor_Metal         , Period = 5 });
            //    this.Items.Add(new Element() { AtomicNumber = 51, Amu = 121.757     , Name = "Antimony"     , Symbol = "Sb", CategoryInput = Element.cat.Metalloid          , Period = 5 });
            //    this.Items.Add(new Element() { AtomicNumber = 52, Amu = 127.6       , Name = "Tellurium"    , Symbol = "Te", CategoryInput = Element.cat.Metalloid          , Period = 5 });
            //    this.Items.Add(new Element() { AtomicNumber = 53, Amu = 126.9045    , Name = "Iodine"       , Symbol = "I" , CategoryInput = Element.cat.Halogen            , Period = 5 });
            //    this.Items.Add(new Element() { AtomicNumber = 54, Amu = 131.29      , Name = "Xenon"        , Symbol = "Xe", CategoryInput = Element.cat.Noble_Gas          , Period = 5 });
            //    this.Items.Add(new Element() { AtomicNumber = 55, Amu = 132.9054    , Name = "Cesium"       , Symbol = "Cs", CategoryInput = Element.cat.Alkali             , Period = 6 });
            //    this.Items.Add(new Element() { AtomicNumber = 56, Amu = 137.33      , Name = "Barium"       , Symbol = "Ba", CategoryInput = Element.cat.Alkaline           , Period = 6 });
            //    this.Items.Add(new Element() { AtomicNumber = 57, Amu = 138.9055    , Name = "Lanthanum"    , Symbol = "La", CategoryInput = Element.cat.Lanthancid         , Period = 6 });
            //    this.Items.Add(new Element() { AtomicNumber = 58, Amu = 140.12      , Name = "Cerium"       , Symbol = "Ce", CategoryInput = Element.cat.Lanthancid         , Period = 6 });
            //    this.Items.Add(new Element() { AtomicNumber = 59, Amu = 140.9077    , Name = "Praseodymium" , Symbol = "Pr", CategoryInput = Element.cat.Lanthancid         , Period = 6 });
            //    this.Items.Add(new Element() { AtomicNumber = 60, Amu = 144.24      , Name = "Neodymium"    , Symbol = "Nd", CategoryInput = Element.cat.Lanthancid         , Period = 6 });
            //    this.Items.Add(new Element() { AtomicNumber = 61, Amu = 145         , Name = "Promethium"   , Symbol = "Pm", CategoryInput = Element.cat.Lanthancid         , Period = 6 });
            //    this.Items.Add(new Element() { AtomicNumber = 62, Amu = 150.36      , Name = "Samarium"     , Symbol = "Sm", CategoryInput = Element.cat.Lanthancid         , Period = 6 });
            //    this.Items.Add(new Element() { AtomicNumber = 63, Amu = 151.965     , Name = "Europium"     , Symbol = "Eu", CategoryInput = Element.cat.Lanthancid         , Period = 6 });
            //    this.Items.Add(new Element() { AtomicNumber = 64, Amu = 157.25      , Name = "Gadolinium"   , Symbol = "Gd", CategoryInput = Element.cat.Lanthancid         , Period = 6 });
            //    this.Items.Add(new Element() { AtomicNumber = 65, Amu = 158.9253    , Name = "Terbium"      , Symbol = "Tb", CategoryInput = Element.cat.Lanthancid         , Period = 6 });
            //    this.Items.Add(new Element() { AtomicNumber = 66, Amu = 162.5       , Name = "Dysprosium"   , Symbol = "Dy", CategoryInput = Element.cat.Lanthancid         , Period = 6 });
            //    this.Items.Add(new Element() { AtomicNumber = 67, Amu = 164.9303    , Name = "Holmium"      , Symbol = "Ho", CategoryInput = Element.cat.Lanthancid         , Period = 6 });
            //    this.Items.Add(new Element() { AtomicNumber = 68, Amu = 167.26      , Name = "Erbium"       , Symbol = "Er", CategoryInput = Element.cat.Lanthancid         , Period = 6 });
            //    this.Items.Add(new Element() { AtomicNumber = 69, Amu = 168.9342    , Name = "Thulium"      , Symbol = "Tm", CategoryInput = Element.cat.Lanthancid         , Period = 6 });
            //    this.Items.Add(new Element() { AtomicNumber = 70, Amu = 173.04      , Name = "Ytterbium"    , Symbol = "Yb", CategoryInput = Element.cat.Lanthancid         , Period = 6 });
            //    this.Items.Add(new Element() { AtomicNumber = 71, Amu = 174.967     , Name = "Lutetium"     , Symbol = "Lu", CategoryInput = Element.cat.Lanthancid         , Period = 6 });
            //    this.Items.Add(new Element() { AtomicNumber = 72, Amu = 178.49      , Name = "Hafnium"      , Symbol = "Hf", CategoryInput = Element.cat.Transition_Metal   , Period = 6 });
            //    this.Items.Add(new Element() { AtomicNumber = 73, Amu = 180.9479    , Name = "Tantalum"     , Symbol = "Ta", CategoryInput = Element.cat.Transition_Metal   , Period = 6 });
            //    this.Items.Add(new Element() { AtomicNumber = 74, Amu = 183.85      , Name = "Tungsten"     , Symbol = "W" , CategoryInput = Element.cat.Transition_Metal   , Period = 6 });
            //    this.Items.Add(new Element() { AtomicNumber = 75, Amu = 186.207     , Name = "Rhenium"      , Symbol = "Re", CategoryInput = Element.cat.Transition_Metal   , Period = 6 });
            //    this.Items.Add(new Element() { AtomicNumber = 76, Amu = 190.2       , Name = "Osmium"       , Symbol = "Os", CategoryInput = Element.cat.Transition_Metal   , Period = 6 });
            //    this.Items.Add(new Element() { AtomicNumber = 77, Amu = 192.22      , Name = "Iridium"      , Symbol = "Ir", CategoryInput = Element.cat.Transition_Metal   , Period = 6 });
            //    this.Items.Add(new Element() { AtomicNumber = 78, Amu = 195.08      , Name = "Platinum"     , Symbol = "Pt", CategoryInput = Element.cat.Transition_Metal   , Period = 6 });
            //    this.Items.Add(new Element() { AtomicNumber = 79, Amu = 196.9665    , Name = "Gold"         , Symbol = "Au", CategoryInput = Element.cat.Transition_Metal   , Period = 6 });
            //    this.Items.Add(new Element() { AtomicNumber = 80, Amu = 200.59      , Name = "Mercury"      , Symbol = "Hg", CategoryInput = Element.cat.Transition_Metal   , Period = 6 });
            //    this.Items.Add(new Element() { AtomicNumber = 81, Amu = 204.383     , Name = "Thallium"     , Symbol = "Tl", CategoryInput = Element.cat.Poor_Metal         , Period = 6 });
            //    this.Items.Add(new Element() { AtomicNumber = 82, Amu = 207.2       , Name = "Lead"         , Symbol = "Pb", CategoryInput = Element.cat.Poor_Metal         , Period = 6 });
            //    this.Items.Add(new Element() { AtomicNumber = 83, Amu = 208.9804    , Name = "Bismuth"      , Symbol = "Bi", CategoryInput = Element.cat.Poor_Metal         , Period = 6 });
            //    this.Items.Add(new Element() { AtomicNumber = 84, Amu = 209         , Name = "Polonium"     , Symbol = "Po", CategoryInput = Element.cat.Metalloid          , Period = 6 });
            //    this.Items.Add(new Element() { AtomicNumber = 85, Amu = 210         , Name = "Astatine"     , Symbol = "At", CategoryInput = Element.cat.Halogen            , Period = 6 });
            //    this.Items.Add(new Element() { AtomicNumber = 86, Amu = 222         , Name = "Radon"        , Symbol = "Rn", CategoryInput = Element.cat.Noble_Gas          , Period = 6 });
            //    this.Items.Add(new Element() { AtomicNumber = 87, Amu = 223         , Name = "Francium"     , Symbol = "Fr", CategoryInput = Element.cat.Alkali             , Period = 7 });
            //    this.Items.Add(new Element() { AtomicNumber = 88, Amu = 226.0254    , Name = "Radium"       , Symbol = "Ra", CategoryInput = Element.cat.Alkaline           , Period = 7 });
            //    this.Items.Add(new Element() { AtomicNumber = 89, Amu = 227         , Name = "Actinium"     , Symbol = "Ac", CategoryInput = Element.cat.Actinoids          , Period = 7 });
            //    this.Items.Add(new Element() { AtomicNumber = 90, Amu = 232.0381    , Name = "Thorium"      , Symbol = "Th", CategoryInput = Element.cat.Actinoids          , Period = 7 });
            //    this.Items.Add(new Element() { AtomicNumber = 91, Amu = 231.0359    , Name = "Protactinium" , Symbol = "Pa", CategoryInput = Element.cat.Actinoids          , Period = 7 });
            //    this.Items.Add(new Element() { AtomicNumber = 92, Amu = 238.029     , Name = "Uranium"      , Symbol = "U" , CategoryInput = Element.cat.Actinoids          , Period = 7 });
            //    this.Items.Add(new Element() { AtomicNumber = 93, Amu = 237.0482    , Name = "Neptunium"    , Symbol = "Np", CategoryInput = Element.cat.Actinoids          , Period = 7 });
            //    this.Items.Add(new Element() { AtomicNumber = 94, Amu = 244         , Name = "Plutonium"    , Symbol = "Pu", CategoryInput = Element.cat.Actinoids          , Period = 7 });
            //    this.Items.Add(new Element() { AtomicNumber = 95, Amu = 243         , Name = "Americium"    , Symbol = "Am", CategoryInput = Element.cat.Actinoids          , Period = 7 });
            //    this.Items.Add(new Element() { AtomicNumber = 96, Amu = 247         , Name = "Curium"       , Symbol = "Cm", CategoryInput = Element.cat.Actinoids          , Period = 7 });
            //    this.Items.Add(new Element() { AtomicNumber = 97, Amu = 247         , Name = "Berkelium"    , Symbol = "Bk", CategoryInput = Element.cat.Actinoids          , Period = 7 });
            //    this.Items.Add(new Element() { AtomicNumber = 98, Amu = 251         , Name = "Californium"  , Symbol = "Cf", CategoryInput = Element.cat.Actinoids          , Period = 7 });
            //    this.Items.Add(new Element() { AtomicNumber = 99, Amu = 252         , Name = "Einsteinium"  , Symbol = "Es", CategoryInput = Element.cat.Actinoids          , Period = 7 });
            //    this.Items.Add(new Element() { AtomicNumber = 100,Amu = 257         , Name = "Fermium"      , Symbol = "Fm", CategoryInput = Element.cat.Actinoids          , Period = 7 });
            //    this.Items.Add(new Element() { AtomicNumber = 101,Amu = 258         , Name = "Mendelevium"  , Symbol = "Md", CategoryInput = Element.cat.Actinoids          , Period = 7 });
            //    this.Items.Add(new Element() { AtomicNumber = 102,Amu = 259         , Name = "Nobelium"     , Symbol = "No", CategoryInput = Element.cat.Actinoids          , Period = 7 });
            //    this.Items.Add(new Element() { AtomicNumber = 103,Amu = 262         , Name = "Lawrencium"   , Symbol = "Lr", CategoryInput = Element.cat.Actinoids          , Period = 7 });
            //    this.Items.Add(new Element() { AtomicNumber = 104,Amu = 261         , Name = "Rutherfordium", Symbol = "Rf", CategoryInput = Element.cat.Transition_Metal   , Period = 7 });
            //    this.Items.Add(new Element() { AtomicNumber = 105,Amu = 262         , Name = "Dubnium"      , Symbol = "Db", CategoryInput = Element.cat.Transition_Metal   , Period = 7 });
            //    this.Items.Add(new Element() { AtomicNumber = 106,Amu = 266         , Name = "Seaborgium"   , Symbol = "Sg", CategoryInput = Element.cat.Transition_Metal   , Period = 7 });
            //    this.Items.Add(new Element() { AtomicNumber = 107,Amu = 264         , Name = "Bohrium"      , Symbol = "Bh", CategoryInput = Element.cat.Transition_Metal   , Period = 7 });
            //    this.Items.Add(new Element() { AtomicNumber = 108,Amu = 269         , Name = "Hassium"      , Symbol = "Hs", CategoryInput = Element.cat.Transition_Metal   , Period = 7 });
            //    this.Items.Add(new Element() { AtomicNumber = 109,Amu = 268         , Name = "Meitnerium"   , Symbol = "Mt", CategoryInput = Element.cat.Transition_Metal   , Period = 7 });
            //    this.Items.Add(new Element() { AtomicNumber = 110,Amu = 269         , Name = "Darmstadtium" , Symbol = "Ds", CategoryInput = Element.cat.Transition_Metal   , Period = 7 });
            //    this.Items.Add(new Element() { AtomicNumber = 111,Amu = 272         , Name = "Roentgenium"  , Symbol = "Rg", CategoryInput = Element.cat.Transition_Metal   , Period = 7 });
            //    this.Items.Add(new Element() { AtomicNumber = 112,Amu = 277         , Name = "Copernicium"  , Symbol = "Cn", CategoryInput = Element.cat.Transition_Metal   , Period = 7 });
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
