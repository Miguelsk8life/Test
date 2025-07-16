using System.Windows;
using MyOfficeEquipmentInventory.ViewModels;

namespace MyOfficeEquipmentInventory.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }
    }
}