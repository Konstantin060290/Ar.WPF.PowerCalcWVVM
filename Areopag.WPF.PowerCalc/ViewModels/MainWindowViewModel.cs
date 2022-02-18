using Areopag.WPF.PowerCalc.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Areopag.WPF.PowerCalc.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        #region AggregateProperties
        
        public double _Aggregate_qapacity;
        public double _Aggregate_P2;
        public double _Aggregate_P1;
        public double _Safety_coefficient;
        public double _Heads_qantity;        
        public double _Aggr_vol_efficienty;
        public double _Aggr_hydr_efficienty;
        public double _Aggr_mech_efficienty;
        #endregion

        #region Pump_driveProperties
        public double _Stroke_length;
        public int _Strokes;
        #endregion

        #region Pump_headProperties


        #endregion
        public MainWindowViewModel()
        {
            //Pump_drive Pd1 = new Pump_drive();
            //Pump_head Ph1 = new Pump_head();
            //Aggregate Ag1 = new Aggregate();
            ////AggregateProperties
            //_Aggregate_qapacity = Ag1.Aggregate_qapacity;
            //_Aggregate_P2 = Ag1.Aggregate_P2;
            //_Aggregate_P1 = Ag1.Aggregate_P1;
            //_Safety_coefficient = Ag1.Safety_coefficient;
            //_Heads_qantity = Ag1.Heads_qantity;
            //_Aggr_vol_efficienty = Ag1.Aggr_vol_efficienty;
            //_Aggr_hydr_efficienty = Ag1.Aggr_hydr_efficienty;
            //_Aggr_mech_efficienty = Ag1.Aggr_mech_efficienty;
            ////PumpDriveProperties
            //_Stroke_length = Pd1.Stroke_length;
            //_Strokes = Pd1.Strokes;
         }
        public Aggregate Ag1 = new Aggregate();

        private string _Title = "Расчет параметров дозировочного агрегата";
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }

        public double Aggregate_qapacity
        {
            get => _Aggregate_qapacity;
            set => Set(ref _Aggregate_qapacity, value);
        }
        public double Aggregate_P2
        {
            get => _Aggregate_P2;
            set
            {
                Set(ref _Aggregate_P2, value);
                Set2(Ag1.Aggregate_P2, value);
            }
        }

        public double Aggregate_P1
        {
            get => _Aggregate_P1;
            set => Set(ref _Aggregate_P1, value);
        }

    }
}
