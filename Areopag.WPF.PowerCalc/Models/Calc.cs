using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Areopag.WPF.PowerCalc.Models
{
    internal class Calc
    {

        public void Aggr_power_calc(Aggregate Ag1, Pump_drive Pd1, Pump_head Ph1)
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
            Ph1.Plunger_diameter = Aggregate.FindNearest(Ph1.Plunger_diameter_calc, Ph1.Plungers_row);
            Ph1.Pressure_force = Ag1.Aggregate_P2 * Math.PI * Math.Pow((Ph1.Plunger_diameter / 10), 2) / 4;//кГс
            Pd1.Max_force = Aggregate.FindNearest(Ph1.Pressure_force, Pd1.forces);
            Ag1.Torque = Ph1.Pressure_force * (Pd1.Stroke_length / 2) * 10 / 1000; //Нм
        }
        public void Aggr_power_calc_2(Aggregate Ag1, Pump_drive Pd1, Pump_head Ph1)
        {
            Ag1.Hydraulic_power = Ag1.Aggregate_qapacity * (Ag1.Aggregate_P2 - Ag1.Aggregate_P1) / 36.7 / 1000; //кВт
            Ag1.Power_without_sc = Ag1.Aggregate_qapacity * (Ag1.Aggregate_P2 - Ag1.Aggregate_P1) / (Ag1.Aggr_vol_efficienty * Ag1.Aggr_hydr_efficienty * 36.7 * Ag1.Aggr_mech_efficienty) / 1000;//кВт
            Ag1.Engine_power = Aggregate.FindNearest((Ag1.Power_without_sc * Ag1.Safety_coefficient), Ag1.Electric_drive_powers_row);
            Ph1.Plunger_diameter_calc = 1000 * Math.Sqrt(4 * Ag1.Aggregate_qapacity / (Ag1.Heads_qantity * Ag1.Aggr_vol_efficienty * Pd1.Stroke_length * Pd1.Strokes * 60 * Math.PI));//в мм
            Ph1.Plunger_diameter = Aggregate.FindNearest(Ph1.Plunger_diameter_calc, Ph1.Plungers_row);
            Ph1.Pressure_force = Ag1.Aggregate_P2 * Math.PI * Math.Pow((Ph1.Plunger_diameter / 10), 2) / 4;//кГс
            Pd1.Max_force = Aggregate.FindNearest(Ph1.Pressure_force, Pd1.forces);
            Ag1.Torque = Ph1.Pressure_force * (Pd1.Stroke_length / 2) * 10 / 1000; //Нм
        }

    }
}
