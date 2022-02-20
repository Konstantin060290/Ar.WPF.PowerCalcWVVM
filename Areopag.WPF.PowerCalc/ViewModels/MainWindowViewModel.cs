using Areopag.WPF.PowerCalc.ViewModels.Base;
using System.Windows;
using Areopag.WPF.PowerCalc.Commands;
using System.Diagnostics;

namespace Areopag.WPF.PowerCalc.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        #region AggregateProperties
        
        private double _Aggregate_qapacity;
        private double _Aggregate_P2;
        private double _Aggregate_P1;
        private double _Safety_coefficient;
        private int _Heads_qantity;
        private double _Aggr_vol_efficienty;
        private double _Aggr_hydr_efficienty;
        private double _Aggr_mech_efficienty;
        #endregion

        #region Pump_driveProperties
        private int _Stroke_length;
        private int _Strokes;
        #endregion

        #region Pump_headProperties


        #endregion

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

        //Команды

        private RelayCommand _helloCommand;

        public RelayCommand HelloCommand
        {
            get
            {
                return _helloCommand ??
                       (_helloCommand = new RelayCommand(obj => MessageBox.Show("Hello from command", "Тут вроде как заголовок")));
            }
        }


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
                           string about = "Данная программа позволяет выполнить расчет параметров дозировочных агрегатов по " + "\n" +
                            "ТУ 3632-003-46919837-2007.";
                           string author = "Филюрин К.В.";
                           string year_author = "2021";
                           string version = "1.0.0.0";
                           MessageBox.Show((about + "\n" + "\n" + "Автор - " + author + "\n" + "\n" + "Год выхода - " + year_author + "\n" + "\n" + "Версия - " + version),
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

        private RelayCommand _ShowDrivesCommand;

        public RelayCommand ShowDrivesCommand
        {
            get
            {
                return _ShowDrivesCommand ??
                       (_ShowDrivesCommand = new RelayCommand(obj =>
                       {
                           Pump_drive Pd1 = new Pump_drive();
                           string all = "";
                           for (int i = 0; i < Pd1.drives.Length; i++)
                           {
                               all += string.Concat(Pd1.drives[i] + " (F=" + Pd1.forces[i].ToString() + " кгс, величина хода: " + Pd1.stroke_lengthes[i].ToString() + " мм);" + "\r\n" + "\r\n");
                           }
                           MessageBox.Show(all, "Ряд применяемых приводов и их предельные усилия на ползуне", MessageBoxButton.OK, MessageBoxImage.Information);
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





    }
}
