using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Areopag.WPF.PowerCalc
{
    partial class Pump_head
    {
        public double Plunger_diameter_calc { get; set;}//расчетный
        public double Plunger_diameter { get; set; }//из ряда
        public int Head_P2 { get; set; }
        public int Head_P1 { get; set; }
        public double Head_vol_efficiency { get; set; }
        public double Head_hydr_efficiency { get; set; }
        public double Pressure_force { get; set; }

        public double[] Plungers_row = new double[] {
                    5,
                    6,
                    7,
                    8,
                    9,
                    10,
                    12,
                    13,
                    14,
                    16,
                    18,
                    20,
                    22,
                    25,
                    28,
                    32,
                    35,
                    40,
                    45,
                    50,
                    55,
                    56,
                    60,
                    63,
                    65,
                    70,
                    75,
                    80,
                    100,
                    110,
        };
    }
}