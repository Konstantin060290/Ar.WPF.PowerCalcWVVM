using Areopag.WPF.PowerCalc.ViewModels.Base;
using System.Windows;
using Areopag.WPF.PowerCalc.Commands;
using System.Diagnostics;
using Areopag.WPF.PowerCalc.Models;
using System.Data;
using System.Windows.Controls;
using System;
using System.ComponentModel;
using Areopag.WPF.PowerCalc.View;

namespace Areopag.WPF.PowerCalc.ViewModels
{
    internal class MainWindowViewModel : ViewModel, IDataErrorInfo
    {
        #region AggregateFields
        
        private string _Aggregate_qapacity = "0";
        private double _Aggregate_P2;
        private double _Aggregate_P1 = 1;
        private double _Safety_coefficient = 1;
        private int _Heads_qantity = 1;
        private double _Aggr_vol_efficienty = 0.9;
        private double _Aggr_hydr_efficienty = 0.9;
        private double _Aggr_mech_efficienty = 0.8;
        private double _Aggregate_Power;
        #endregion

        #region Pump_driveFields
        private int _Stroke_length = 60;
        private int _Strokes = 100;
        #endregion

        #region Validation
        public string this[string columnName]
        {
            get
            {
                string error = String.Empty;
                switch (columnName)
                {
                    case "Aggregate_qapacity":
                        if (Aggregate_qapacity.Contains("."))
                        {
                            error = "Используйте запятую в качестве десятичного разделителя.";
                        }
                        for (int i = 0; i < Aggregate_qapacity.Length; i++)
                        {
                            if (!Char.IsDigit(Aggregate_qapacity[i]) && !Aggregate_qapacity.Contains(","))
                            {
                                error = "Неверный символ ввода, используйте только числа.";
                            }
                        }
                        break;
                }
                return error;
            }
        }
        public string Error
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        //Свойства
        public string Aggregate_qapacity
        {
            get
            {
                return _Aggregate_qapacity;
            }

            set
            {
                Set(ref _Aggregate_qapacity, value);
            }
            
        }

        public double Aggregate_P2
        {
            get => _Aggregate_P2;
            set
            {
                Set(ref _Aggregate_P2, value);
            }
        }
        public double Aggregate_P1
        {
            get => _Aggregate_P1;
            set => Set(ref _Aggregate_P1, value);
        }

        public double Safety_coefficient
        {
            get => _Safety_coefficient;
            set => Set(ref _Safety_coefficient, value);
        }

        public int Heads_qantity
        {
            get => _Heads_qantity;
            set => Set(ref _Heads_qantity, value);
        }

        public double Aggr_vol_efficienty
        {
            get => _Aggr_vol_efficienty;
            set => Set(ref _Aggr_vol_efficienty, value);
        }
        public double Aggr_hydr_efficienty
        {
            get => _Aggr_hydr_efficienty;
            set => Set(ref _Aggr_hydr_efficienty, value);
        }
        public double Aggr_mech_efficienty
        {
            get => _Aggr_mech_efficienty;
            set => Set(ref _Aggr_mech_efficienty, value);
        }

        public int Stroke_length
        {
            get => _Stroke_length;
            set => Set(ref _Stroke_length, value);
        }

        public int Strokes
        {
            get => _Strokes;
            set => Set(ref _Strokes, value);
        }

        private bool _EnterSC;
        public bool EnterSC
        {
            get => _EnterSC;
            set => Set(ref _EnterSC, value);
        }

        private bool _EnterPlungerDiam;
        public bool EnterPlungerDiam
        {
            get => _EnterPlungerDiam;
            set => Set(ref _EnterPlungerDiam, value);
        }

        private int _ManualPlungerDiam;
        public int ManualPlungerDiam
        {
            get => _ManualPlungerDiam;
            set => Set(ref _ManualPlungerDiam, value);
        }

        public double Aggregate_Power
        {
            get => _Aggregate_Power;
            set => Set(ref _Aggregate_Power, value);
        }
        private DataTable _Results;
        public DataTable Results
        {
            get => _Results;
            set => Set(ref _Results, value);
        }

        //Команды

        private RelayCommand _ExitCommand;

        public RelayCommand ExitCommand
        {
            get
            {
                return _ExitCommand ??
                       (_ExitCommand = new RelayCommand(obj =>
                       {
                           System.Environment.Exit(0);
                       }
                       ));
            }
        }

        private RelayCommand _AboutCommand;
        public RelayCommand AboutCommand
        {
            get
            {
                return _AboutCommand ??
                       (_AboutCommand = new RelayCommand(obj =>
                       {
                           string about = "Данное программное обеспечение позволяет выполнить расчет параметров дозировочных агрегатов по " + "\n" +
                            "ТУ 3632-003-46919837-2007.";
                           string author = "kfilyurin@gmail.com";
                           string version =
                           "Текущая версия - 1.3.1 - 09.06.2022" + "\n" +
                           "В данной версии введены следующие изменения:" + "\n" +
                           "1. Исправлено отображение подбираемых приводных механизмов;" + "\n" +
                           "2. Увеличены поля под входные данные;" + "\n" +
                           "3. Введено логирование." + "\n" +
                           "\n" +                           
                           "История предыдущих версий:"+ "\n" +

                           "Версия 1.3.0 - 05.06.2022" + "\n" +
                           "В данной версии введены следующие изменения:" + "\n" +
                           "1. Детализирован ряд приводных механизмов;" + "\n" +
                           "2. Добавлен функционал автоподбора приводного механизма." + "\n" +
                           "\n" +

                           "Версия 1.2.0 - 24.04.2022" + "\n" + 
                           "В данной версии введены следующие изменения:" + "\n" +
                           "1. Приложение переписано под паттерн MVVM;" + "\n" +
                           "2. Добавлена возможность ввода диаметра плунжера;" + "\n" +
                           "3. В таблицу результатов введена рассчетная номинальная подача агрегата, которая " +
                           "рассчитывается из фактических значений диаметра плунжера,"  + "\n" +
                           "коэффициента подачи и т.д." + "\n" +
                           "4. В ряд приводных механизмов введен новый - АРХ8;" + "\n" +
                           "5. В ряд плунжеров введен диаметр 90 мм.";

                           MessageBox.Show((about + "\n" + "\n" + "Автор - " + author + "\n" + "\n" + version),
                           "Справка", MessageBoxButton.OK, MessageBoxImage.Information);
                       }
                       ));
            }
        }

        private RelayCommand _ShowPlungersCommand;

        public RelayCommand ShowPlungersCommand
        {
            get
            {
                return _ShowPlungersCommand ??
                       (_ShowPlungersCommand = new RelayCommand(obj =>
                       {
                           Pump_head Ph1 = new Pump_head();
                           MessageBox.Show(string.Join("\t", Ph1.Plungers_row), "Ряд применяемых в программе плунжеров, мм", MessageBoxButton.OK, MessageBoxImage.Information);
                       }
                       ));
            }
        }

        private RelayCommand _ShowDrivesCommand;
        public RelayCommand ShowDrivesCommand
        {
            get
            {
                return _ShowDrivesCommand ??
                       (_ShowDrivesCommand = new RelayCommand(obj =>
                       {
                           System.Data.DataTable dt = new System.Data.DataTable("DrivesTable");

                           DataColumn id = new DataColumn("Id", typeof(int));
                           DataColumn name = new DataColumn("Наименование серии привода", typeof(string));
                           DataColumn stroke = new DataColumn("Величина хода ползуна (мм)", typeof(int));
                           DataColumn h_qty = new DataColumn("Максиальное число головок в агрегате на базе привода (шт)", typeof(int));
                           DataColumn force = new DataColumn("Предельное усилие на ползуне (кгс)", typeof(int));
                           DataColumn strokes = new DataColumn("Предельное число ходов в мин", typeof(int));

                           dt.Columns.Add(id);
                           dt.Columns.Add(name);
                           dt.Columns.Add(stroke);
                           dt.Columns.Add(h_qty);
                           dt.Columns.Add(force);
                           dt.Columns.Add(strokes);

                           Pump_drive pd1 = new Pump_drive();

                           int i = 0;

                           foreach (var drive in pd1._drives)
                           {
                               DataRow row = dt.NewRow();
                               row[0] = i;
                               row[1] = drive.Key;//name
                               row[2] = drive.Value[1];//stroke
                               row[3] = drive.Value[2];//heads quantity
                               row[4] = drive.Value[0];//force
                               row[5] = drive.Value[3];//strokes
                               dt.Rows.Add(row);
                               i++;
                           }
                           DrivesWindowViewModel viewModel = new DrivesWindowViewModel();
                           viewModel.AboutDrives = dt;
                           DrivesWindow drivesWindow = new DrivesWindow();
                           drivesWindow.DataContext = viewModel;
                           drivesWindow.Show();
                       }
                       ));
            }
        }

        private RelayCommand _ShowElectricDrivesCommand;

        public RelayCommand ShowElectricDrivesCommand
        {
            get
            {
                return _ShowElectricDrivesCommand ??
                       (_ShowElectricDrivesCommand = new RelayCommand(obj =>
                       {
                           Aggregate Ag1 = new Aggregate();
                           MessageBox.Show(string.Join("\t", Ag1.Electric_drive_powers_row), "Ряд применяемых в программе электродвигателей, кВт", MessageBoxButton.OK, MessageBoxImage.Information);
                       }
                       ));
            }
        }


        private RelayCommand _LaunchPGAApplicationCommand;
        public RelayCommand LaunchPGAApplicationCommand
        {
            get
            {
                return _LaunchPGAApplicationCommand ??
                       (_LaunchPGAApplicationCommand = new RelayCommand(obj =>
                       {
                           try
                           {
                               Process p = new Process();
                               p.StartInfo.FileName = @"Расчет объема ПГА.exe";
                               p.Start();
                           }
                           catch
                           {
                               MessageBox.Show("Приложение \"Расчет объема ПГА.exe\" не найдено");
                           }
                       }
                       ));
            }
        }

        private RelayCommand _ResultCommand;
        public RelayCommand ResultCommand
        {
            get
            {
                return _ResultCommand ??
                       (_ResultCommand = new RelayCommand(obj =>
                       {
                           if (EnterSC == true)
                           {
                               Aggregate Ag1 = new Aggregate();
                               try
                               {
                                   Ag1.Aggregate_qapacity = Convert.ToDouble(Aggregate_qapacity);
                               }
                               catch
                               {
                                   Ag1.Aggregate_qapacity = 0;
                               }                               
                               Ag1.Aggregate_P2 = Aggregate_P2;
                               Ag1.Aggregate_P1 = Aggregate_P1;
                               Ag1.Aggr_vol_efficienty = Aggr_vol_efficienty;
                               Ag1.Aggr_mech_efficienty = Aggr_mech_efficienty;
                               Ag1.Aggr_hydr_efficienty = Aggr_hydr_efficienty;
                               Ag1.Heads_qantity = Heads_qantity;
                               Ag1.Safety_coefficient = Safety_coefficient;
                               Pump_drive Pd1 = new Pump_drive();
                               Pd1.Strokes = Strokes;
                               Pd1.Stroke_length = Stroke_length;
                               Pump_head Ph1 = new Pump_head();
                               if (EnterPlungerDiam == true)
                               {
                                   Ph1.Plunger_diameter = ManualPlungerDiam;
                               }
                               Calc C1 = new Calc();
                               C1.Aggr_power_calc_2(Ag1, Pd1, Ph1, EnterPlungerDiam);
                               Results = C1.CreateDataTable(Ag1, Pd1, Ph1);                        
                           }
                           else
                           {
                               Aggregate Ag1 = new Aggregate();
                               try
                               {
                                   Ag1.Aggregate_qapacity = Convert.ToDouble(Aggregate_qapacity);
                               }
                               catch
                               {
                                   Ag1.Aggregate_qapacity = 0;
                               }
                               Ag1.Aggregate_P2 = Aggregate_P2;
                               Ag1.Aggregate_P1 = Aggregate_P1;
                               Ag1.Aggr_vol_efficienty = Aggr_vol_efficienty;
                               Ag1.Aggr_mech_efficienty = Aggr_mech_efficienty;
                               Ag1.Aggr_hydr_efficienty = Aggr_hydr_efficienty;
                               Ag1.Heads_qantity = Heads_qantity;
                               Ag1.Safety_coefficient = Safety_coefficient;
                               Pump_drive Pd1 = new Pump_drive();
                               Pd1.Strokes = Strokes;
                               Pd1.Stroke_length = Stroke_length;
                               Pump_head Ph1 = new Pump_head();
                               if (EnterPlungerDiam == true)
                               {
                                   Ph1.Plunger_diameter = ManualPlungerDiam;
                               }
                               Calc C1 = new Calc();
                               C1.Aggr_power_calc(Ag1, Pd1, Ph1, EnterPlungerDiam);
                               Results = C1.CreateDataTable(Ag1, Pd1, Ph1);
                           }
                       }
                       ));
            }
        }
    }
}
