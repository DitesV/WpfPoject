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

namespace WpfProject
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        GameMenu gameMenu = new GameMenu();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            gameMenu.InitializeMenuItems(canv);
            // gameMenu.DrawMenu(canv);
            gameMenu.DrawArrow(canv);

        }
        bool startGame;

        private async void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (!startGame)
            {
                if (e.Key == Key.Enter)
                {
                    switch (gameMenu.GetArrowPosition())
                    {
                        case 1:
                            canv.Children.Clear();
                            startGame = true;
                            Viewport3D viewport = new Viewport3D();

                            TextBlock menuTextBlock0 = new TextBlock();
                            menuTextBlock0.Text = "В разработке";
                            menuTextBlock0.FontSize = canv.ActualHeight / 5;
                            Canvas.SetLeft(menuTextBlock0, 20);
                            Canvas.SetTop(menuTextBlock0, 20);
                            canv.Children.Add(menuTextBlock0);
                            break;
                        case 2:
                            canv.Children.Clear();
                            startGame = true;
                            TextBlock menuTextBlock1 = new TextBlock();
                            menuTextBlock1.Text = "В разработке";
                            menuTextBlock1.FontSize = canv.ActualHeight / 5;
                            Canvas.SetLeft(menuTextBlock1, 20);
                            Canvas.SetTop(menuTextBlock1, 20);
                            canv.Children.Add(menuTextBlock1);
                            break;
                        case 3:
                            Close();
                            break;
                    }
                }
                else
                {
                    await Task.Delay(100);
                    gameMenu.SelectMenuItem(canv, e);
                }
            }
            else
            {
                if (e.Key == Key.Escape)
                {
                    startGame = false;
                    await Task.Delay(100);
                    gameMenu.SelectMenuItem(canv, e);
                }

            }
        }

        private async void Window_KeyUp(object sender, KeyEventArgs e)
        {

        }
    }
}
