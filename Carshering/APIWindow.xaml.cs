using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;
using System.Diagnostics;
using System.Net.Http;
using Newtonsoft.Json;
using Word = Microsoft.Office.Interop.Word;
using Microsoft.Office.Interop.Word;
using Table = Microsoft.Office.Interop.Word.Table;

namespace Carshering
{
    /// <summary>
    /// Interaction logic for APIWindow.xaml
    /// </summary>
    public partial class APIWindow : System.Windows.Window
    {
        public APIWindow()
        
        {
                InitializeComponent();
               
        }

        
        private void btnCenel_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
       
       
    }

}
