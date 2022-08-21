using System;
using System.Windows.Controls;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
using System.Data;
using System.Windows.Forms;
using System.IO;

namespace Areopag.WPF.PowerCalc.Models
{
    internal class Calc
    {

        public void Aggr_power_calc(Aggregate Ag1, Pump_drive Pd1, Pump_head Ph1, bool ManualPlungerDiameter)
        {

            Ag1.Hydraulic_power = Ag1.Aggregate_qapacity * (Ag1.Aggregate_P2 - Ag1.Aggregate_P1) / 36.7 / 1000; //кВт
            Ag1.Power_without_sc = Ag1.Aggregate_qapacity * (Ag1.Aggregate_P2 - Ag1.Aggregate_P1) / (Ag1.Aggr_vol_efficienty * Ag1.Aggr_hydr_efficienty * 36.7 * Ag1.Aggr_mech_efficienty) / 1000;//кВт

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

            Ag1.Engine_power = Aggregate.FindNearest((Ag1.Power_without_sc * Ag1.Safety_coefficient), Ag1.Electric_drive_powers_row);
            Ph1.Plunger_diameter_calc = 1000 * Math.Sqrt(4 * Ag1.Aggregate_qapacity / (Ag1.Heads_qantity * Ag1.Aggr_vol_efficienty * Pd1.Stroke_length * Pd1.Strokes * 60 * Math.PI));//в мм
            if (ManualPlungerDiameter == false)
            {
                Ph1.Plunger_diameter = Aggregate.FindNearest(Ph1.Plunger_diameter_calc, Ph1.Plungers_row);
            }
            Ag1.Aggregate_calc_qapacity = (Math.PI*Math.Pow(Ph1.Plunger_diameter/10 , 2) / 4) * (Pd1.Stroke_length / 10) * (Pd1.Strokes * 60) * Ag1.Aggr_vol_efficienty * Ag1.Heads_qantity/1000; //л/ч
            Ph1.Pressure_force = Ag1.Aggregate_P2 * Math.PI * Math.Pow((Ph1.Plunger_diameter / 10), 2) / 4;//кГс
            Ag1.DriveName = Pd1.FindDrive(Ph1.Pressure_force, Pd1.Stroke_length, Ag1.Heads_qantity, Pd1.Strokes, Pd1._drives);
            Ag1.Torque = Ph1.Pressure_force * (Pd1.Stroke_length / 2) * 10 / 1000; //Нм
            CreateLog(Ag1);
        }
        public void Aggr_power_calc_2(Aggregate Ag1, Pump_drive Pd1, Pump_head Ph1, bool ManualPlungerDiameter)
        {
            Ag1.Hydraulic_power = Ag1.Aggregate_qapacity * (Ag1.Aggregate_P2 - Ag1.Aggregate_P1) / 36.7 / 1000; //кВт
            Ag1.Power_without_sc = Ag1.Aggregate_qapacity * (Ag1.Aggregate_P2 - Ag1.Aggregate_P1) / (Ag1.Aggr_vol_efficienty * Ag1.Aggr_hydr_efficienty * 36.7 * Ag1.Aggr_mech_efficienty) / 1000;//кВт
            Ag1.Engine_power = Aggregate.FindNearest((Ag1.Power_without_sc * Ag1.Safety_coefficient), Ag1.Electric_drive_powers_row);
            Ph1.Plunger_diameter_calc = 1000 * Math.Sqrt(4 * Ag1.Aggregate_qapacity / (Ag1.Heads_qantity * Ag1.Aggr_vol_efficienty * Pd1.Stroke_length * Pd1.Strokes * 60 * Math.PI));//в мм           
            if (ManualPlungerDiameter == false)
            {
                Ph1.Plunger_diameter = Aggregate.FindNearest(Ph1.Plunger_diameter_calc, Ph1.Plungers_row);
            }
            Ag1.Aggregate_calc_qapacity = (Math.PI * Math.Pow(Ph1.Plunger_diameter / 10, 2) / 4) * (Pd1.Stroke_length / 10) * (Pd1.Strokes * 60) * Ag1.Aggr_vol_efficienty * Ag1.Heads_qantity / 1000; //л/ч
            Ph1.Pressure_force = Ag1.Aggregate_P2 * Math.PI * Math.Pow((Ph1.Plunger_diameter / 10), 2) / 4;//кГс
            Ag1.DriveName = Pd1.FindDrive(Ph1.Pressure_force, Pd1.Stroke_length, Ag1.Heads_qantity, Pd1.Strokes, Pd1._drives);
            Ag1.Torque = Ph1.Pressure_force * (Pd1.Stroke_length / 2) * 10 / 1000; //Нм
            CreateLog(Ag1);
        }


        public static void Export_to_Excel(System.Windows.Controls.DataGrid ResultGrid)
        {

            Excel.Application excel = new Excel.Application();
            excel.Visible = true;
            Workbook workbook = excel.Workbooks.Add(System.Reflection.Missing.Value);
            Worksheet sheet1 = (Worksheet)workbook.Sheets[1];
            for (int j = 0; j < ResultGrid.Columns.Count; j++)
            {
                Range myrange = (Range)sheet1.Cells[1, j + 1];
                sheet1.Cells[1, j + 1].Font.Bold = true;
                sheet1.Columns[1].ColumnWidth = 10;
                sheet1.Columns[2].ColumnWidth = 70;
                sheet1.Columns[j + 1].ColumnWidth = 25;
                myrange.Value2 = ResultGrid.Columns[j].Header;
                myrange.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                myrange.HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter;
            }
            for (int i = 0; i < ResultGrid.Columns.Count; i++)
            {
                for (int j = 0; j < ResultGrid.Items.Count; j++)
                {
                    TextBlock b = ResultGrid.Columns[i].GetCellContent(ResultGrid.Items[j]) as TextBlock;
                    Range myRange = sheet1.Cells[j + 2, i + 1];
                    myRange.Value2 = b.Text;
                    myRange.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                    myRange.HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter;
                }

            }
        }

        #region CreateTable
        public System.Data.DataTable CreateDataTable(Aggregate Ag1, Pump_drive Pd1, Pump_head Ph1)
        {
            System.Data.DataTable dt = new System.Data.DataTable("ResultTable");
            DataColumn id = new DataColumn("Id", typeof(int));
            DataColumn name = new DataColumn("Наименование параметра", typeof(string));
            DataColumn symbol = new DataColumn("Условное обозначение", typeof(string));
            DataColumn value = new DataColumn("Значение параметра");
            DataColumn unit = new DataColumn("Единица измерения", typeof(string));

            dt.Columns.Add(id);
            dt.Columns.Add(name);
            dt.Columns.Add(symbol);
            dt.Columns.Add(value);
            dt.Columns.Add(unit);
            DataRow input = dt.NewRow();
            int i = 0;
            input[0] = i;
            input[1] = "Исходные данные";
            input[2] = "-------------------";
            input[3] = "-------------------";
            input[4] = "-------------------"; ;
            dt.Rows.Add(input);
            DataRow qapacity = dt.NewRow();
            qapacity[0] = ++i;
            qapacity[1] = "Желаемая номинальная подача агрегата";
            qapacity[2] = "Qа";
            qapacity[3] = Ag1.Aggregate_qapacity;
            qapacity[4] = "л/ч";
            dt.Rows.Add(qapacity);
            if (Ag1.Heads_qantity > 1)
            {
                DataRow head_qapacity = dt.NewRow();
                head_qapacity[0] = ++i;
                head_qapacity[1] = "Номинальная подача головки агрегата";
                head_qapacity[2] = "Qг";
                head_qapacity[3] = Math.Round(Ag1.Aggregate_qapacity / Ag1.Heads_qantity, 3);
                head_qapacity[4] = "л/ч";
                dt.Rows.Add(head_qapacity);
            }
            DataRow p2 = dt.NewRow();
            p2[0] = ++i;
            p2[1] = "Давление на выходе агрегата";
            p2[2] = "P2";
            p2[3] = Ag1.Aggregate_P2;
            p2[4] = "кгс/см2";
            dt.Rows.Add(p2);
            DataRow p1 = dt.NewRow();
            p1[0] = ++i;
            p1[1] = "Давление на входе агрегата";
            p1[2] = "P1";
            p1[3] = Ag1.Aggregate_P1;
            p1[4] = "кгс/см2";
            dt.Rows.Add(p1);
            DataRow vol_eff = dt.NewRow();
            vol_eff[0] = ++i;
            vol_eff[1] = "Объемный КПД агрегата";
            vol_eff[2] = "η1-2";
            vol_eff[3] = Ag1.Aggr_vol_efficienty;
            vol_eff[4] = "-";
            dt.Rows.Add(vol_eff);
            DataRow hydr_eff = dt.NewRow();
            hydr_eff[0] = ++i;
            hydr_eff[1] = "Гидравлический КПД агрегата";
            hydr_eff[2] = "ηг";
            hydr_eff[3] = Ag1.Aggr_hydr_efficienty;
            hydr_eff[4] = "-";
            dt.Rows.Add(hydr_eff);
            DataRow mech_eff = dt.NewRow();
            mech_eff[0] = ++i;
            mech_eff[1] = "Механический КПД агрегата";
            mech_eff[2] = "ηм";
            mech_eff[3] = Ag1.Aggr_mech_efficienty;
            mech_eff[4] = "-";
            dt.Rows.Add(mech_eff);
            DataRow heads_quantity = dt.NewRow();
            heads_quantity[0] = ++i;
            heads_quantity[1] = "Количество головок в агрегате";
            heads_quantity[2] = "z";
            heads_quantity[3] = Ag1.Heads_qantity;
            heads_quantity[4] = "шт.";
            dt.Rows.Add(heads_quantity);
            DataRow strokes = dt.NewRow();
            strokes[0] = ++i;
            strokes[1] = "Число двойных ходов плунжера в мин.";
            strokes[2] = "n";
            strokes[3] = Pd1.Strokes;
            strokes[4] = "-";
            dt.Rows.Add(strokes);
            DataRow stroke_length = dt.NewRow();
            stroke_length[0] = ++i;
            stroke_length[1] = "Величина хода плунжера";
            stroke_length[2] = "h";
            stroke_length[3] = Pd1.Stroke_length;
            stroke_length[4] = "мм";
            dt.Rows.Add(stroke_length);
            DataRow output = dt.NewRow();
            int j = 0;
            output[0] = j;
            output[1] = "Результаты расчета";
            output[2] = "-------------------";
            output[3] = "-------------------";
            output[4] = "-------------------"; ;
            dt.Rows.Add(output);
            DataRow hydraulic_power = dt.NewRow();
            hydraulic_power[0] = ++j;
            hydraulic_power[1] = "Выходная мощность насоса";
            hydraulic_power[2] = "Nв";
            hydraulic_power[3] = Math.Round(Ag1.Hydraulic_power, 3);
            hydraulic_power[4] = "кВт";
            dt.Rows.Add(hydraulic_power);
            DataRow consumption_power = dt.NewRow();
            consumption_power[0] = ++j;
            consumption_power[1] = "Потребляемая мощность насоса";
            consumption_power[2] = "Nп";
            consumption_power[3] = Math.Round(Ag1.Power_without_sc, 3);
            consumption_power[4] = "кВт";
            dt.Rows.Add(consumption_power);
            DataRow safety_factor = dt.NewRow();
            safety_factor[0] = ++j;
            safety_factor[1] = "Коэффициент запаса";
            safety_factor[2] = "k";
            safety_factor[3] = Ag1.Safety_coefficient;
            safety_factor[4] = "-";
            dt.Rows.Add(safety_factor);
            DataRow calc_motor_power = dt.NewRow();
            calc_motor_power[0] = ++j;
            calc_motor_power[1] = "Расчетная мощность электродвигателя";
            calc_motor_power[2] = "NэДвр";
            calc_motor_power[3] = Math.Round((Ag1.Power_without_sc * Ag1.Safety_coefficient), 3);
            calc_motor_power[4] = "кВт";
            dt.Rows.Add(calc_motor_power);
            DataRow motor_power = dt.NewRow();
            motor_power[0] = ++j;
            motor_power[1] = "Мощность выбранного из ряда электродвигателя";
            motor_power[2] = "NэДв";
            motor_power[3] = Ag1.Engine_power;
            motor_power[4] = "кВт";
            dt.Rows.Add(motor_power);
            DataRow calc_pl_diam = dt.NewRow();
            calc_pl_diam[0] = ++j;
            calc_pl_diam[1] = "Расчетный диаметр плунжера";
            calc_pl_diam[2] = "Dплр";
            calc_pl_diam[3] = Math.Round(Ph1.Plunger_diameter_calc, 3);
            calc_pl_diam[4] = "мм";
            dt.Rows.Add(calc_pl_diam);
            DataRow pl_diam = dt.NewRow();
            pl_diam[0] = ++j;
            pl_diam[1] = "Выбранный из ряда, диаметр плунжера";
            pl_diam[2] = "Dпл";
            pl_diam[3] = Ph1.Plunger_diameter;
            pl_diam[4] = "мм";
            dt.Rows.Add(pl_diam);
            DataRow vg = dt.NewRow();
            vg[0] = ++j;
            vg[1] = "Объем геометрического замещения";
            vg[2] = "Vг";
            vg[3] = Math.Round(((Math.PI * (Ph1.Plunger_diameter / 10) * (Ph1.Plunger_diameter / 10) / 4) * (Pd1.Stroke_length / 10)), 3);
            vg[4] = "см3";
            dt.Rows.Add(vg);
            DataRow calced_qapacity = dt.NewRow();
            calced_qapacity[0] = ++j;
            calced_qapacity[1] = "Расчетная номинальная подача агрегата";
            calced_qapacity[2] = "Qа";
            calced_qapacity[3] = Math.Round(Ag1.Aggregate_calc_qapacity, 3);
            calced_qapacity[4] = "л/ч";
            dt.Rows.Add(calced_qapacity);
            if (Ag1.Heads_qantity > 1)
            {
                DataRow head_calc_qapacity = dt.NewRow();
                head_calc_qapacity[0] = ++i;
                head_calc_qapacity[1] = "Расчетная номинальная подача головки агрегата";
                head_calc_qapacity[2] = "Qг";
                head_calc_qapacity[3] = Math.Round(Ag1.Aggregate_calc_qapacity / Ag1.Heads_qantity, 3);
                head_calc_qapacity[4] = "л/ч";
                dt.Rows.Add(head_calc_qapacity);
            }
            DataRow pl_force = dt.NewRow();
            pl_force[0] = ++j;
            pl_force[1] = "Возникающее осевое усилие на плунжере";
            pl_force[2] = "Fпл";
            pl_force[3] = Math.Round(Ph1.Pressure_force, 3);
            pl_force[4] = "кгс";
            dt.Rows.Add(pl_force);
            DataRow drive_choosen = dt.NewRow();
            drive_choosen[0] = ++j;
            drive_choosen[1] = "Предполагаемый приводной механизм(-ы)";
            drive_choosen[2] = "-";
            drive_choosen[3] = Ag1.DriveName;
            drive_choosen[4] = "-";
            dt.Rows.Add(drive_choosen);
            DataRow torque = dt.NewRow();
            torque[0] = ++j;
            torque[1] = "Требуемый момент на кривошипном валу (для одной насосной головки)";
            torque[2] = "Мкр1г";
            torque[3] = Math.Round(Ag1.Torque, 3);
            torque[4] = "Нм";
            dt.Rows.Add(torque);
            int z = 0;
            DataRow date = dt.NewRow();
            date[0] = z;
            date[1] = "Дата и время выполнения расчета";
            date[2] = "-------------------";
            date[3] = DateTime.Now.ToString();
            date[4] = "-------------------";
            dt.Rows.Add(date);

            return dt;

        }
        #endregion

        public static void CreateLog(Aggregate Ag1)
        {
            string logname = SystemInformation.UserName + " " + Ag1.Aggregate_qapacity + "-" + Ag1.Aggregate_P2 + "-" + Ag1.Heads_qantity +
                " " +(DateTime.Now.Hour * 60 * 60 + DateTime.Now.Minute * 60 + DateTime.Now.Second).ToString();
            string folder = "logs\\";
            string path = folder + logname + ".txt";
            try
            {
                Directory.CreateDirectory(folder);
                using (StreamWriter sw = new StreamWriter(path))
                {
                }
            }
            catch
            {

            }
        }

    }
}
