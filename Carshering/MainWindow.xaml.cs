using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.Entity;

namespace Carshering
{
    
    public partial class MainWindow : Window
    {
        private async void btnJoin_Click(object sender, RoutedEventArgs e)
        {
            string loginName = Login.Text.Trim();
            string passworddd = passwordName.Password;
            if (string.IsNullOrEmpty(loginName) || string.IsNullOrEmpty(passworddd))
            {
                MessageBox.Show("Введите логин и пароль!");
                return;
            }
            try
            {
                using (var context = new CarsheringEntities1())
                {

                    var user = await context.users
                        .Include(u => u.roles)
                             .FirstOrDefaultAsync(u => u.login == loginName && u.password == passworddd);
                    if (user == null)
                    {
                        MessageBox.Show("Вы ввели неверный логин или пароль. Повторите попытку");

                    }
                    if (user != null)
                    {


                        if (user.role_id != 1)
                        {
                            UserWindow userWindow = new UserWindow();
                            userWindow.Show();
                            this.Close();
                        }
                        else
                        {
                            AdminWindow adminWindow = new AdminWindow();
                            adminWindow.Show();
                            this.Close();

                        }
                        if (user.is_loced == 1)
                        {
                            MessageBox.Show("Вы в бане!");

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private Image firstButton;
        public MainWindow()
        {
            InitializeComponent();
            LoadPazzle();
        }

        private void LoadPazzle()
        {
            var rnd = new Random();
            var pieces = Enumerable.Range(1, 4).OrderBy(x => rnd.Next()).ToList();

            pieces.ForEach(x =>
            {
                var img = new Image
                {
                    Source = new BitmapImage(new Uri($"Images/{x}.png", UriKind.Relative)),
                    Tag = x,
                    Stretch = Stretch.Fill
                };
                img.MouseLeftButtonUp += Pices_Click;
                PazzleGrid.Children.Add(img);
            });
        }
        private void CheckPazzle()
        {
            if (PazzleGrid.Children.OfType<Image>()
                .Select((img, i) => i+1 == (int)img.Tag)
                .All(x => x))
            {
                MessageBox.Show("Amazing!");
            }
        }
        private void Pices_Click(object sender, MouseButtonEventArgs e)
        {
            if (sender is Image clicked)
            {
                if (firstButton == null)
                {
                    firstButton = clicked;
                    firstButton.Opacity = 0.5;
                    return;
                }
                if (clicked != firstButton)
                {
                    (firstButton.Source, clicked.Source) = (clicked.Source, firstButton.Source);
                    (firstButton.Tag, clicked.Tag) = (clicked.Tag, firstButton.Tag);
                }
                firstButton.Opacity = 1;
                firstButton = null;
                CheckPazzle();
            }
        }


        private void btnAPI_Click(object sender, RoutedEventArgs e)
        {
            APIWindow apiWindow = new APIWindow();
            apiWindow.Show();
            this.Close();
        }
    }
}
