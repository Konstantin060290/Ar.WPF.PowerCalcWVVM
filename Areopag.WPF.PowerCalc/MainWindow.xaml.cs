using System;
using System.Windows;
using System.Windows.Controls;
using System.Linq;
using Microsoft.Win32;
using System.IO;
using System.Windows.Media;
using System.Text;

namespace Areopag.WPF.PowerCalc
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
        {
        Pump_drive Pd1 = new Pump_drive();
        Pump_head Ph1 = new Pump_head();
        Aggregate Ag1 = new Aggregate();


        public MainWindow()
            {
            InitializeComponent();
            }

        public void Aggr_power_calc()
            {

            Ag1.Hydraulic_power = Ag1.Aggregate_qapacity * Ag1.Aggregate_P2 / 36.7 / 1000; //кВт
            Ag1.Power_without_sc = Ag1.Aggregate_qapacity * (Ag1.Aggregate_P2 - Ag1.Aggregate_P1) / ((Ag1.Aggr_vol_efficienty * Ag1.Aggr_hydr_efficienty) * 36.7 * Pd1.Drive_mech_efficiency) / 1000;//кВт
           
            if (Ag1.Power_without_sc >= Ag1.Drive_powers_without_sc[10])
                {
                Ag1.Safety_coefficient = Ag1.Safety_coeffitients[7];//1,5
                }
            if (Ag1.Power_without_sc < Ag1.Drive_powers_without_sc[10] & Ag1.Power_without_sc >= Ag1.Drive_powers_without_sc[9])
                {
                Ag1.Safety_coefficient = Ag1.Safety_coeffitients[6];//1,8
                }
            if (Ag1.Power_without_sc < Ag1.Drive_powers_without_sc[9] & Ag1.Power_without_sc >= Ag1.Drive_powers_without_sc[8])
                {
                Ag1.Safety_coefficient = Ag1.Safety_coeffitients[5];//2,2
                }
            if (Ag1.Power_without_sc < Ag1.Drive_powers_without_sc[8] & Ag1.Power_without_sc >= Ag1.Drive_powers_without_sc[7])
                {
                Ag1.Safety_coefficient = Ag1.Safety_coeffitients[4];//2,4
                }
            if (Ag1.Power_without_sc < Ag1.Drive_powers_without_sc[7] & Ag1.Power_without_sc >= Ag1.Drive_powers_without_sc[6])
                {
                Ag1.Safety_coefficient = Ag1.Safety_coeffitients[3];//2,5
                }
            if (Ag1.Power_without_sc < Ag1.Drive_powers_without_sc[6] & Ag1.Power_without_sc >= Ag1.Drive_powers_without_sc[5])
                {
                Ag1.Safety_coefficient = Ag1.Safety_coeffitients[2];//2,8
                }
            if (Ag1.Power_without_sc < Ag1.Drive_powers_without_sc[5] & Ag1.Power_without_sc >= Ag1.Drive_powers_without_sc[3])
                {
                Ag1.Safety_coefficient = Ag1.Safety_coeffitients[1];//3
                }
            if (Ag1.Power_without_sc < Ag1.Drive_powers_without_sc[3])
                {
                Ag1.Safety_coefficient = Ag1.Safety_coeffitients[0];//3,4
                }

            Ag1.Engine_power = Aggregate.FindNearest((Ag1.Power_without_sc* Ag1.Safety_coefficient), Ag1.Electric_drive_powers_row);
            Ph1.Plunger_diameter_calc = 1000 * Math.Sqrt(4 * Ag1.Aggregate_qapacity / (Ag1.Heads_qantity*Ph1.Head_vol_efficiency* Pd1.Stroke_length* Pd1.Strokes * 60 * Math.PI));//в мм
            Ph1.Plunger_diameter = Aggregate.FindNearest(Ph1.Plunger_diameter_calc, Ph1.Plungers_row);
            Ph1.Pressure_force = Ag1.Aggregate_P2 * Math.PI* Math.Pow((Ph1.Plunger_diameter / 10), 2) / 4;//кГс
            Pd1.Max_force = Aggregate.FindNearest(Ph1.Pressure_force, Pd1.forces);
            Ag1.Torque = Ph1.Pressure_force * (Pd1.Stroke_length/2) * 10 / 1000; //Нм
           }
        public void Aggr_power_calc_2()
        {
            Ag1.Hydraulic_power = Ag1.Aggregate_qapacity * Ag1.Aggregate_P2 / 36.7 / 1000; //кВт
            Ag1.Power_without_sc = Ag1.Aggregate_qapacity * (Ag1.Aggregate_P2 - Ag1.Aggregate_P1) / ((Ag1.Aggr_vol_efficienty * Ag1.Aggr_hydr_efficienty) * 36.7 * Pd1.Drive_mech_efficiency) / 1000;//кВт
            Ag1.Engine_power = Aggregate.FindNearest((Ag1.Power_without_sc * Ag1.Safety_coefficient), Ag1.Electric_drive_powers_row);
            Ph1.Plunger_diameter_calc = 1000 * Math.Sqrt(4 * Ag1.Aggregate_qapacity / (Ag1.Heads_qantity*Ph1.Head_vol_efficiency * Pd1.Stroke_length * Pd1.Strokes * 60 * Math.PI));//в мм
            Ph1.Plunger_diameter = Aggregate.FindNearest(Ph1.Plunger_diameter_calc, Ph1.Plungers_row);
            Ph1.Pressure_force = Ag1.Aggregate_P2* Math.PI * Math.Pow((Ph1.Plunger_diameter / 10), 2) / 4;//кГс
            Pd1.Max_force = Aggregate.FindNearest(Ph1.Pressure_force, Pd1.forces);
            Ag1.Torque = Ph1.Pressure_force * (Pd1.Stroke_length/2) * 10 / 1000; //Нм
            }


        //Работа с интерфейсом
        private void Save_us_Click(object sender, RoutedEventArgs e)
            {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Text file (*.txt)|*.txt";
            DateTime now = DateTime.Now;

            dlg.FileName = "Расчет параметров дозировочного агрегата " + Convert.ToString(now.Day) + "." + Convert.ToString(now.Month) + "." + Convert.ToString(now.Year) +
                " " + Convert.ToString(now.Hour) + "_" + Convert.ToString(now.Minute) + "_" + Convert.ToString(now.Second);


            if (dlg.ShowDialog() == true)
                {
                File.WriteAllText(dlg.FileName, Result_textbox.Text);
                }
            }
        private void MenuItem_Exit_Click(object sender, RoutedEventArgs e)
            {
            My_main_window.Close();
            }

        private void Qapacity_textbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Ag1.Aggregate_qapacity = Convert.ToDouble(Qapacity_textbox.Text);
            }

            catch
            {
                Ag1.Aggregate_qapacity = 0;
            }
        }

        private void Result_button_click(object sender, RoutedEventArgs e)
        {
            if (Enter_safety_coeff_checkbox.IsChecked == true)
            {
                Aggr_power_calc_2();
            }

            else
            {
                Aggr_power_calc();
            }


            Result_textbox.Text = Ag1.Aggr_result(Ag1.Engine_power, Ag1.Hydraulic_power, Ag1.Power_without_sc, Ag1.Safety_coefficient,
            Ph1.Plunger_diameter_calc, Ph1.Plunger_diameter, Ph1.Pressure_force, Pd1.Max_force, Ag1.Torque);

        }

        private void P2_textbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Ag1.Aggregate_P2 = Convert.ToInt32(P2_textbox.Text);
            }
            catch
            {
                Ag1.Aggregate_P2 = 0;
            }
        }

       
        private void P1_textbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Ag1.Aggregate_P1 = Convert.ToInt32(P1_textbox.Text);
            }
            catch
            {
                Ag1.Aggregate_P1 = 0;
            }
        }
        private void P1_textbox_Loaded(object sender, RoutedEventArgs e)
            {
            Ag1.Aggregate_P1 = Convert.ToInt32(P1_textbox.Text);
            }
        private void Vol_eff_textbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            
            try
            {
               Ph1.Head_vol_efficiency = Convert.ToDouble(Vol_eff_textbox.Text);
               Ag1.Aggr_vol_efficienty = Math.Pow(Ph1.Head_vol_efficiency, Ag1.Heads_qantity);
               Vol_eff_aggr_label.Content = "η объемный агрегата=" + Ag1.Aggr_vol_efficienty.ToString("0.000");
                }
            catch
            {
                Ph1.Head_vol_efficiency = 0;
            }
        }

        private void Vol_eff_textbox_Loaded(object sender, RoutedEventArgs e)
            {
            Ph1.Head_vol_efficiency = Convert.ToDouble(Vol_eff_textbox.Text);
            Ag1.Aggr_vol_efficienty = Math.Pow(Ph1.Head_vol_efficiency, Ag1.Heads_qantity);
            Vol_eff_aggr_label.Content = "η объемный агрегата=" + Ag1.Aggr_vol_efficienty.ToString("0.000");
            }

        private void Mech_eff_textbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Pd1.Drive_mech_efficiency = Convert.ToDouble(Mech_eff_textbox.Text);
            }
            catch
            {
                Pd1.Drive_mech_efficiency = 0;
            }
        }
        private void Mech_eff_textbox_Loaded(object sender, RoutedEventArgs e)
            {
            Pd1.Drive_mech_efficiency = Convert.ToDouble(Mech_eff_textbox.Text);
            }

        private void Hydr_eff_textbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Ph1.Head_hydr_efficiency = Convert.ToDouble(Hydr_eff_textbox.Text);
                Ag1.Aggr_hydr_efficienty = Math.Pow(Ph1.Head_hydr_efficiency, Ag1.Heads_qantity);
                Hydr_eff_aggr_label.Content = "η гидравл. агрегата=" + Ag1.Aggr_hydr_efficienty.ToString("0.000");
                }
            catch
            {
                Ph1.Head_hydr_efficiency = 0;
            }
        }
        private void Hydr_eff_textbox_Loaded(object sender, RoutedEventArgs e)
            {
            Ph1.Head_hydr_efficiency = Convert.ToDouble(Hydr_eff_textbox.Text);
            Ag1.Aggr_hydr_efficienty = Math.Pow(Ph1.Head_hydr_efficiency, Ag1.Heads_qantity);
            Hydr_eff_aggr_label.Content = "η гидравл. агрегата=" + Ag1.Aggr_hydr_efficienty.ToString("0.000");
            }
        private void Safety_coeff_textbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Ag1.Safety_coefficient = Convert.ToDouble(Safety_coeff_textbox.Text);
            }

            catch
            {
                Ag1.Safety_coefficient = 0;
            }
        }
        private void Safety_coeff_textbox_Loaded(object sender, RoutedEventArgs e)
            {
            Ag1.Safety_coefficient = Convert.ToDouble(Safety_coeff_textbox.Text);
            }


        //Работа с чек-боксами
        #region CheckBoxes
        private void My_main_window_Loaded(object sender, RoutedEventArgs e)
        {
            Safety_coeff_label.Visibility = Visibility.Hidden;
            Safety_coeff_textbox.Visibility = Visibility.Hidden;
            Strokes_label.Visibility = Visibility.Hidden;
            Stroke_label.Visibility = Visibility.Hidden;
            Strokes_textbox.Visibility = Visibility.Hidden;
            Stroke_lenght_textbox.Visibility = Visibility.Hidden;
        }

        private void Calc_add_parameters_checkbox_Checked(object sender, RoutedEventArgs e)
        {
            Strokes_label.Visibility = Visibility.Visible;
            Stroke_label.Visibility = Visibility.Visible;
            Strokes_textbox.Visibility = Visibility.Visible;
            Stroke_lenght_textbox.Visibility = Visibility.Visible;
        }

        private void Calc_add_parameters_checkbox_Unchecked(object sender, RoutedEventArgs e)
        {
            Strokes_label.Visibility = Visibility.Hidden;
            Stroke_label.Visibility = Visibility.Hidden;
            Strokes_textbox.Visibility = Visibility.Hidden;
            Stroke_lenght_textbox.Visibility = Visibility.Hidden;
        }

        private void Enter_safety_coeff_checkbox_Checked(object sender, RoutedEventArgs e)
            {
            Safety_coeff_label.Visibility = Visibility.Visible;
            Safety_coeff_textbox.Visibility = Visibility.Visible;

            }
        private void Enter_safety_coeff_checkbox_Unchecked(object sender, RoutedEventArgs e)
            {
            Safety_coeff_label.Visibility = Visibility.Hidden;
            Safety_coeff_textbox.Visibility = Visibility.Hidden;
            }
        #endregion CheckBoxes

        private void Strokes_textbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Pd1.Strokes = Convert.ToInt32(Strokes_textbox.Text);
            }
            catch
            {
                Pd1.Strokes = 0;
            }
        }
        private void Strokes_textbox_Loaded(object sender, RoutedEventArgs e)
            {
            Pd1.Strokes = Convert.ToInt32(Strokes_textbox.Text);
            }
        private void Stroke_lenght_textbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Pd1.Stroke_length = Convert.ToInt32(Stroke_lenght_textbox.Text);
            }
            catch
            {
                Pd1.Stroke_length = 0;
            }
        }

        private void Show_pl_row_btn_Click(object sender, RoutedEventArgs e)
            {
            MessageBox.Show(string.Join("\t", Ph1.Plungers_row), "Ряд применяемых в программе плунжеров, мм", MessageBoxButton.OK, MessageBoxImage.Information);
            }

        private void ED_row_btn_Click(object sender, RoutedEventArgs e)
            {
            MessageBox.Show(string.Join("\t", Ag1.Electric_drive_powers_row), "Ряд применяемых в программе электродвигателей, кВт", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        
        private void Show_drives_row_Click(object sender, RoutedEventArgs e)
            {
            string all="";
            for (int i=0; i<Pd1.drives.Length;i++)
                {
                all+=string.Concat(Pd1.drives[i] +" (F="+Pd1.forces[i].ToString()+ " кгс, величина хода: "+ Pd1.stroke_lengthes[i].ToString()+" мм);" + "\r\n"+ "\r\n");
                }
            MessageBox.Show(all, "Ряд применяемых приводов и их предельные усилия на ползуне", MessageBoxButton.OK, MessageBoxImage.Information);
            }

        private void Qantity_of_heads_textbox_TextChanged(object sender, TextChangedEventArgs e)
            {
            try
                {
                Ag1.Heads_qantity = Convert.ToInt32(Qantity_of_heads_textbox.Text);
                Ag1.Aggr_hydr_efficienty = Math.Pow(Ph1.Head_hydr_efficiency, Ag1.Heads_qantity);
                Hydr_eff_aggr_label.Content = "η гидравл. агрегата=" + Ag1.Aggr_hydr_efficienty.ToString("0.000");
                Ag1.Aggr_vol_efficienty = Math.Pow(Ph1.Head_vol_efficiency, Ag1.Heads_qantity);
                Vol_eff_aggr_label.Content = "η объемный агрегата=" + Ag1.Aggr_vol_efficienty.ToString("0.000");
                }
            catch
                {
                Ag1.Heads_qantity = 0;
                }
            }

        private void Qantity_of_heads_textbox_Loaded(object sender, RoutedEventArgs e)
            {
            Ag1.Heads_qantity = Convert.ToInt32(Qantity_of_heads_textbox.Text);
            }

        //О программе
        #region About

        static string about = "Данная программа позволяет выполнить расчет параметров дозировочных агрегатов по " + "\n" +
         "ТУ 3632-003-46919837-2007.";
        static string author = "Филюрин К.В.";
        static string year_author = "2021";
        static string version = "0.0.3";


        private void MenuItem_About_Click(object sender, RoutedEventArgs e)
            {
            MessageBox.Show((about + "\n" + "\n" + "Автор - " + author + "\n" + "\n" + "Год выхода - " + year_author + "\n" + "\n" + "Версия - " + version),
                "Справка", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        #endregion About

        //Недоделки
        #region  Nedodelki
        private void Add_head_Click(object sender, RoutedEventArgs e)
            {

            New_head NH1 = new New_head();
            NH1.Show();


            }

        public object SelectedTreeElem { get; set; }
        static public object New_head_in_tree { get; set; }

        //private void TreeViewItem_Selected(Object sender, RoutedEventArgs e)
        //{
        //    TreeViewItem item = e.OriginalSource as TreeViewItem;
        //    if (item != null)
        //    {
        //        ItemsControl parent = GetSelectedTreeViewItemParent(item);
        //    }
        //}
        private void Change_head_button_Click(object sender, RoutedEventArgs e)
            {

            }

        public ItemsControl GetSelectedTreeViewItemParent(TreeViewItem item)
            {
            DependencyObject parent = VisualTreeHelper.GetParent(item);
            while (!(parent is TreeViewItem || parent is TreeView))
                {
                parent = VisualTreeHelper.GetParent(parent);
                }

            return parent as ItemsControl;
            }

        private void Bl_aggr_treeview_i_Selected(object sender, RoutedEventArgs e)
            {
            TreeViewItem item = e.OriginalSource as TreeViewItem;
            if (item != null)
                {
                ItemsControl parent = GetSelectedTreeViewItemParent(item);
                parent.Items.Remove(item);
                }


            }

        private void Remove_head_button_Click(object sender, RoutedEventArgs e)
            {
            //TreeNode node = this.trvMenu.Nodes.Cast<TreeNode>().FirstOrDefault(x => x.Name == "TestNode")
            //if (node != null)
            //{
            //    this.trvMenu.Nodes.Remove(node);
            //}

            }
        #endregion Nedodelki

        }
    }
