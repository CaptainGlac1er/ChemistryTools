using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chemistry
{
    class Equation
    {
        ObservableCollection<Element> elements;
        private List<Element> equation = new List<Element>();
        private string formula = "";
        private string solvingTest = "";
        
        public Equation(String Equation, ObservableCollection<Element> elements)
        {
            this.elements = elements;
            calcStuff(Equation);

        }

        private void calcStuff(String text)
        {
            bool parIn = false;
            List<Element> inParElements = new List<Element>();
            for (int i = 0; i < text.Length; i++)
            {
                bool boolbreak = false;
                if (text.ToCharArray()[i] == '(')
                {
                    parIn = true;
                    formula += '(';
                }
                else if (text.ToCharArray()[i] == ')')
                {
                    parIn = false;
                    formula += ')';
                    int t = 1;
                    bool chance = true;
                    string number = "";
                    do
                    {
                        if (i + t < text.Length && Char.IsDigit(text, i + t))
                        {

                            number += text.Substring(i + t, 1);
                            t++;
                            chance = true;
                        }
                        else
                        {
                            chance = false;
                        }
                    } while (chance);
                    if (number != "")
                    {
                        int times = Int32.Parse(number);
                        for (int q = 0; q < inParElements.Count; q++)
                        {
                            inParElements[q].Count = inParElements[q].Count * times;
                        }
                        formula += PeriodicTable.convertSubscript(number.ToString());
                    }
                    foreach (Element elem in inParElements)
                        equation.Add(elem);
                    inParElements = new List<Element>();
                     
                }

                else if (Char.IsUpper(text, i))
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
                                    Element currentElement = new Element(elements[j]);
                                    int t = 2;
                                    bool chance = true;
                                    string number = "";
                                    formula += letter;

                                    do
                                    {
                                        if (i + t < text.Length && Char.IsDigit(text, i + t))
                                        {

                                            number += text.Substring(i + t, 1);
                                            t++;
                                            chance = true;
                                        }
                                        else
                                        {
                                            chance = false;
                                        }
                                    } while (chance);
                                    if (number != "")
                                    {
                                        currentElement.Count = Int32.Parse(number);
                                        formula += PeriodicTable.convertSubscript(number.ToString());
                                    }
                                    if (parIn)
                                        inParElements.Add(currentElement);
                                    else
                                        equation.Add(currentElement);
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
                                Element currentElement = new Element(elements[j]);
                                int t = 1;
                                bool chance = true;
                                string number = "";
                                formula += letter;
                                //OutputMoles.Text = text.Length + " " + i + t;
                                do
                                {
                                    if (i + t < text.Length && Char.IsDigit(text, i + t))
                                    {

                                        number += text.Substring(i + t, 1);
                                        t++;
                                        chance = true;
                                    }
                                    else
                                    {
                                        chance = false;
                                    }
                                } while (chance);
                                if (number != "")
                                {
                                    currentElement.Count = Int32.Parse(number);
                                    formula += PeriodicTable.convertSubscript(number.ToString());
                                }
                                if (parIn)
                                    inParElements.Add(currentElement);
                                else
                                    equation.Add(currentElement);
                            }
                        }

                    }
                }
            }
        }
        public bool hasTransitionMetal()
        {
            foreach (Element elem in equation)
                if (elem.CategoryInput == Element.cat.Transition_Metal)
                    return true;
            return false;

        }
        public string getFormattedFormula()
        {
            return formula;
        }
        public int getValence()
        {
            int valence = 0;
            foreach (Element elem in equation)
                valence += elem.Valence * elem.Count;
            return valence;
        }

        public String toString()
        {
            string output = "{";
            foreach (Element elem in equation)
            {
                output = output + "(" + elem.Symbol + "," + elem.Count + "),";
            }
            output.TrimEnd(new char[]{','});
            output = output + "}\n" + formula;
            return output;
        }
        public double getTotalAMU()
        {
            double total = 0.0;
            foreach (Element elem in equation)
                total += elem.Amu * elem.Count;
            return total;
        }
        public string getSolvingSteps(int i){
            string output = "";
            if(i == 1){
            bool test = false;
            foreach(Element elem in equation){
                if (test)
                    output += "\n" + elem.Symbol + ": + " + elem.Amu;
                else { 
                    output += elem.Symbol + ":   " + elem.Amu;
                    test = true;
            }
            }
                output += "\nTotal: " + getTotalAMU();

            }
            return output;
        }

    }
}
