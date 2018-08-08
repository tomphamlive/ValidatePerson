using System.Windows;
using ValidatePerson.ViewModels;

namespace ValidatePerson.Views
{
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();

            DataContext = new MainViewModel();
        }
    }
}
