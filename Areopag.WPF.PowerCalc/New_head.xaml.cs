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
using System.Windows.Shapes;

namespace Areopag.WPF.PowerCalc
{
    /// <summary>
    /// Логика взаимодействия для New_head.xaml
    /// </summary>
    public partial class New_head : Window
    {

        Pump_drive Pd2 = new Pump_drive();
        Pump_head Ph2 = new Pump_head();
        Aggregate Ag2 = new Aggregate();
        
        public New_head()
        {
            InitializeComponent();
        }

        public void Add_in_tree (object head)
        {

        }

        private void Qapacity_head_textbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Ag2.Aggregate_qapacity = Convert.ToDouble(Qapacity_head_textbox.Text);
            }

            catch
            {
                Ag2.Aggregate_qapacity = 0;
            }
        }

        private void P2_head_textbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Ag2.Aggregate_P2 = Convert.ToInt32(P2_head_textbox.Text);
            }

            catch
            {
                Ag2.Aggregate_P2 = 0;
            }

        }

        private void P1_head_textbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Ag2.Aggregate_P1 = Convert.ToInt32(P1_head_textbox.Text);
            }

            catch
            {
                Ag2.Aggregate_P1 = 0;
            }

        }

        private void Vol_eff_head_textbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Ph2.Head_vol_efficiency = Convert.ToDouble(Vol_eff_head_textbox.Text);
            }

            catch
            {
                Ph2.Head_vol_efficiency = 0;
            }
        }

        private void Hydr_eff_head_textbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Ph2.Head_hydr_efficiency = Convert.ToDouble(Hydr_eff_head_textbox.Text);
            }

            catch
            {
                Ph2.Head_hydr_efficiency = 0;
            }

        }

       

        private void Stroke_lenght_head_textbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Pd2.Stroke_length = Convert.ToInt32(Stroke_lenght_head_textbox.Text);
            }

            catch
            {
                Pd2.Stroke_length = 0;
            }
        }

        private void Strokes_head_textbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Pd2.Strokes = Convert.ToInt32(Strokes_head_textbox.Text);
            }

            catch
            {
                Pd2.Strokes = 0;
            }
        }

     
        private void Qantity_heads_textbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Ag2.Heads_qantity = Convert.ToDouble(Qantity_heads_textbox.Text);
            }

            catch
            {
                Ag2.Heads_qantity = 0;
            }
        }
        
        private void Add_inside_head_button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.New_head_in_tree = "Головка "+Ag2.Aggregate_qapacity.ToString()+"/"+Ag2.Aggregate_P2.ToString();
            int n = 1;
            while (n <=Ag2.Heads_qantity)
            {
                ((MainWindow)Application.Current.MainWindow).Bl_aggr_treeview_i.Items.Add(MainWindow.New_head_in_tree);
                n++;
            } 
        }

        private void Cancel_inside_head_button_Click(object sender, RoutedEventArgs e)
        {
            New_head_window.Close();
        }
    }
}
