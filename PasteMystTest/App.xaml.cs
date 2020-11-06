using System.Windows;
using PasteMystTest.Graphics;

namespace PasteMystTest
{

    public partial class App
    {

        private void Initialize(object sender, StartupEventArgs args)
        {
            Current.MainWindow = new WnMain();
            Current.MainWindow.Show();
        }

    }

}