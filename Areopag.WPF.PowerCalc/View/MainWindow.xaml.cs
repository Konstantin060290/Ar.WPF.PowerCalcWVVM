using System.Windows;

namespace Areopag.WPF.PowerCalc.View
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
        {
        public MainWindow()
            {
            InitializeComponent();
            }

        private void Save_us_item_Click(object sender, RoutedEventArgs e)
        {
            Models.Calc.Export_to_Excel(ResultGrid);

        }
    }
    }
