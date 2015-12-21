using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using Calc.Fx;

namespace Calc.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_OnClick(object sender, RoutedEventArgs e)
        {
            var s = textBox.Text;
            int i;

            if (!int.TryParse(s, out i))
            {
                resultBox.Text = "Please enter number!";
                return;
            }
             
            var calculation = new FactCalculation(i);

            calculation.Calculate();

            resultBox.Text = String.Format("n!={0}({1} elapsed)", calculation.Result, calculation.Elapsed);

        }
    }
}
