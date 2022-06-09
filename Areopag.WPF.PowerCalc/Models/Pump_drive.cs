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

        //предельное усилие, кгс,
        //величина хода, мм,
        //максимальное количество головок в агрегате на базе данного привода,
        //предельное число ходов, мин-1

        static List<int> APX0_30 = new List<int>()
        { 220, 16, 6, 30};

        static List<int> APX0_50 = new List<int>()
        { 220, 16, 6, 50};

        static List<int> APX0_100 = new List<int>()
        { 220, 16, 6, 100};

        static List<int> APX3_100 = new List<int>()
        { 387, 32, 2, 100};

        static List<int> APX1_100 = new List<int>()
        { 1257, 60, 1, 100};

        static List<int> APX1_120 = new List<int>()
        { 1257, 60, 1, 120};

        static List<int> APX1_160 = new List<int>()
        { 800, 60, 1, 160};

        static List<int> AP24AP34_100 = new List<int>()
        { 2000, 60, 2, 100};

        static List<int> AP24AP34_120 = new List<int>()
        { 2000, 60, 2, 120};

        static List<int> AP44AP54_100 = new List<int>()
        { 1257, 60, 2, 100};

        static List<int> AP44AP54_120 = new List<int>()
        { 1257, 60, 2, 120};

        static List<int> AP45AP55_120 = new List<int>()
        { 1538, 60, 5, 120};

        static List<int> AP26AP36_140 = new List<int>()
        { 3500, 60, 8, 140};

        static List<int> AP46AP56_140 = new List<int>()
        { 2500, 60, 8, 140};

        static List<int> AP47AP57_70 = new List<int>()
        { 800, 60, 12, 70};

        static List<int> AP47AP57_100 = new List<int>()
        { 800, 60, 12, 100};

        static List<int> AP47AP57_144 = new List<int>()
        { 800, 60, 12, 144};

        static List<int> AP48AP58_100 = new List<int>()
        { 2300, 60, 6, 100};

        static List<int> AP48AP58_120 = new List<int>()
        { 1900, 60, 6, 120};

        static List<int> AP48AP58_150 = new List<int>()
        { 1500, 60, 6, 150};

        static List<int> AMG23_300 = new List<int>()
        { 4000, 40, 3, 300};

        static List<int> AMG23_300x2 = new List<int>()
        { 4000, 40, 6, 300};

        static List<int> AMGM17_220 = new List<int>()
        { 4000, 40, 3, 220};

        static List<int> AMGM17_220x2 = new List<int>()
        { 4000, 40, 6, 220};
        static List<int> AMG48_300 = new List<int>()
        { 4000, 90, 3, 300};

        public Dictionary<string, List<int>> _drives = new Dictionary<string, List<int>>()
        {
        {"АРХ0 (30 ход./мин.)", APX0_30},
        {"АРХ0 (50 ход./мин.)", APX0_50},
        {"АРХ0 (100 ход./мин.)", APX0_100},
        {"АРХ3 (100 ход./мин.)", APX3_100},
        {"АРХ1 (100 ход./мин.)", APX1_100},
        {"АРХ1 (120 ход./мин.)", APX1_120},
        {"АРХ1 (160 ход./мин.)", APX1_160},
        {"АР24/АР34 (100 ход./мин.)", AP24AP34_100},
        {"АР24/АР34 (120 ход./мин.)", AP24AP34_120},
        {"АР44/АР54 (100 ход./мин.)", AP44AP54_100},
        {"АР44/АР54 (120 ход./мин.)", AP44AP54_120},
        {"АР26/АР36 (140 ход./мин. max)", AP26AP36_140},
        {"АР46/АР56 (140 ход./мин. max)", AP46AP56_140},
        {"АР45/АР55 (120 ход./мин. max)", AP45AP55_120},
        {"АР47/АР57 (70 ход./мин.)", AP47AP57_70},
        {"АР47/АР57 (100 ход./мин.)", AP47AP57_100},
        {"АР47/АР57 (144 ход./мин.)", AP47AP57_144},
        {"АР48/АР58 (100 ход./мин.)", AP48AP58_100},
        {"АР48/АР58 (120 ход./мин.)", AP48AP58_120},
        {"АР48/АР58 (150 ход./мин.)", AP48AP58_150},
        {"АМГ23 (300 ход./мин. max)", AMG23_300},
        {"2хАМГ23 (300 ход./мин. max)", AMG23_300x2},
        {"АМГМ17 (220 ход./мин. max)", AMGM17_220},
        {"2хАМГМ17 (220 ход./мин. max)", AMGM17_220x2},
        {"АМГ48 (300 ход./мин. max)", AMG48_300},
        };
        public string FindDrive(double force, double stroke, double headsquantity, int strokes, Dictionary<string, List<int>> dict)
        {
            string drivename = "Не найден подходящий приводной механизм";
            int iterator = 0;
            if(headsquantity ==1 && force <= 2100)
            {
                for (int i = 0; i < dict.Count; i++)
                {
                    var item = dict.ElementAt(i);
                    if    (Convert.ToUInt32(force)*0.95 <= item.Value[0]
                        && Convert.ToUInt32(stroke) == item.Value[1]
                        && Convert.ToUInt32(headsquantity) <= item.Value[2]
                        && strokes == item.Value[3])
                            {
                                iterator++;
                                if (iterator == 1)
                                {
                                    drivename = item.Key + "\n";
                                }
                                else
                                {
                                    drivename += item.Key + "\n";
                                }
                            }
                    if (force == 0)
                    {
                        drivename = "Не найден подходящий приводной механизм";
                    }
                }
            }
            else
            {
                for (int i = 0; i < dict.Count; i++)
                {
                    var item = dict.ElementAt(i);
                    if (Convert.ToUInt32(force)*0.95 <= item.Value[0]
                        && Convert.ToUInt32(stroke) == item.Value[1]
                        && Convert.ToUInt32(headsquantity) <= item.Value[2]
                        && strokes <= item.Value[3])
                            {
                                iterator++;
                                if (iterator == 1)
                                {
                                    drivename = item.Key + "\n";
                                }
                                else
                                {
                                    drivename += item.Key + "\n";
                                }
                            }
                    if (force == 0)
                    {
                        drivename = "Не найден подходящий приводной механизм";
                    }
                }
            }
            if (drivename!= "Не найден подходящий приводной механизм")
            {
                drivename = drivename.Substring(0, drivename.Length - 1);
            }
            return drivename;
        }

    }
}
