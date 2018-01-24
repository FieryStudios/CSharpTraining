using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


// Need to use:

    // if /else
    // switch statement
    // mathematical operators
    // store data in fields to maintain state

    // decide on class structure


    //TODO: only allow one decimal per new entry
namespace Visual_Calculon
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>  
    /// string currentValue;
   
   
   
    public partial class MainWindow : Window
    {

        double currentValue = 0;
        bool newEntry = true;
        enum Operators { Add, Subtract, Multiply, Divide};
        Operators currentOp;

        public MainWindow()
        {
            InitializeComponent();
            calculateField.Text = currentValue.ToString();
        }

        private void NumberButtonClicked(object sender, RoutedEventArgs e)
        {
            Button numberButton = (Button)sender;

            string value = numberButton.Content.ToString();

            if (calculateField.Text == "0") {
                if (value != ".")
                {
                    if (value != "0")
                    {
                        // don't allow user to add zeros as first number
                        calculateField.Text = value;
                    }
                }
                else {

                    calculateField.Text += value;
                }
            }
            else {
                if (newEntry)
                {
                    calculateField.Text = value;

                }
                else {
                    calculateField.Text += value;
                }
               
            }
            newEntry = false;
        }


        // Operator functions

        private void OperandFunction(object sender, RoutedEventArgs e)
        { 
            Button operandButton = (Button)sender;
            currentOp = (Operators)Enum.Parse(typeof(Operators), operandButton.Name);
            Calculon();
        }


        // Evaluation Functions

        // clear text field
        private void ClearFunction(object sender, RoutedEventArgs e) {
            currentValue = 0;
            calculateField.Text = currentValue.ToString();
            newEntry = true;
        }

        // Calculate

        private void Calculon() {
            double newValue = Double.Parse(calculateField.Text);
            double result;
                switch (currentOp)
                {
                    case Operators.Add:
                        result = (currentValue + newValue);
                        break;
                    case Operators.Subtract:
                        if (currentValue == 0)
                        {
                            result = newValue;
                        }
                        else {
                            result = (currentValue - newValue);
                        }
                        
                        break;

                    case Operators.Multiply:
                        if (currentValue == 0)
                        {
                            result = newValue;
                        }                 
                        else
                        {
                            result = (currentValue * newValue);
                        }
                        break;
                    case Operators.Divide:
                        if (newValue == 0)
                        {
                            calculateField.Text = currentValue.ToString();
                            return;
                        }
                        else if (currentValue == 0)
                        {
                            result = newValue;
                            break;
                        }
                        else {
                            result = (currentValue / newValue);
                            break;
                    }
                        
                    default:
                     return;
                }
            
            currentValue = result;
            calculateField.Text = result.ToString();
            newEntry = true;
        }
        private void EqualsFunction(object sender, RoutedEventArgs e)
        {
            Calculon();
        }
    }
}
