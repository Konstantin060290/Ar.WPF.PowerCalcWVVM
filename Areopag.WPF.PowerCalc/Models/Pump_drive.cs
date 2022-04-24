using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Areopag.WPF.PowerCalc
{
    partial class Pump_drive
    {
      public double Stroke_length { get; set; }
      public int Strokes { get; set; }
      public double Max_force { get; set; }
      public double Drive_mech_efficiency { get; set; }


        public string[] drives = { "АРХ0", "АРХ3", "АР22/АР31", "АР41/АР51", "АР24/АР34", "АР44/АР54", "АР26", "АР46/АР56", "АР45/АР55", "АР47/АР57", "АР48/АР58", "АМГ17(23)" };
        public double[] forces = { 200, 387, 1256, 1256, 2000, 1250, 3500, 2500, 1538, 800, 2300, 4000 };
        public double[] stroke_lengthes = { 16, 32, 60, 60, 60, 60, 60, 60, 60, 60, 60, 40 };

        }
}
