using Areopag.WPF.PowerCalc.ViewModels.Base;
using System.Data;

namespace Areopag.WPF.PowerCalc.ViewModels
{
    internal class DrivesWindowViewModel : ViewModel
    {
        private DataTable _AboutDrives;
        public DataTable AboutDrives
        {
            get => _AboutDrives;
            set => Set(ref _AboutDrives, value);
        }

    }
}
