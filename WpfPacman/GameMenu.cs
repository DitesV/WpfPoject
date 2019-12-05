using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;
using System.Windows.Media.Animation;
using System.Threading.Tasks;
using System.Threading;

namespace WpfProject
{
    public class GameMenu
    {
        String[] menuText;

        TextBlock menuTextBlock0 = new TextBlock();
        TextBlock menuTextBlock1 = new TextBlock();
        TextBlock menuTextBlock2 = new TextBlock();
        List<TextBlock> menuItems = new List<TextBlock>();

        Polygon[] arrow = new Polygon[2];
        Point[] Point1 = new Point[6];
        PointCollection[] myPointCollection = new PointCollection[2];
        int arrowPosition = 1;
        bool runSelectItem = true;

        public GameMenu()
        {
            menuText = new String[3];
            menuText[0] = "Start";
            menuText[1] = "Map editor";
            menuText[2] = "Exit";

            for (var i = 0; i < menuText.Length; i++)
            {
                var textBlock = new TextBlock();
                textBlock.Text = menuText[i];
                menuItems.Add(textBlock);
            }

            for (int i = 0; i < 2; i++)
            {
                arrow[i] = new Polygon();
                myPointCollection[i] = new PointCollection();
            }
            SetPointArrow(100, 100);
        }
        public void SetPointArrow(double x, double y)
        {
            Point1[0] = new Point(x, y);
            Point1[1] = new Point(x + 40, y + 20);
            Point1[2] = new Point(x - 20, y + 20);

            myPointCollection[0].Clear();
            myPointCollection[0].Add(Point1[0]);
            myPointCollection[0].Add(Point1[1]);
            myPointCollection[0].Add(Point1[2]);

            Point1[3] = new Point(x, y + 40);

            myPointCollection[1].Clear();
            myPointCollection[1].Add(Point1[1]);
            myPointCollection[1].Add(Point1[2]);
            myPointCollection[1].Add(Point1[3]);
        }
        public void DrawMenu(Canvas canv)
        {
            for (var i = 0; i < menuItems.Count; i++)
            {
                menuItems[i].FontSize = canv.ActualHeight / 8;
                Canvas.SetLeft(menuItems[i], canv.ActualWidth / 2 + 40);
                Canvas.SetTop(menuItems[i], canv.ActualHeight - canv.ActualHeight / 4 * (menuItems.Count - i));
                canv.Children.Add(menuItems[i]);
            }
        }
        public void DrawArrow(Canvas canv)
        {

            SolidColorBrush mySolidColorBrush = new SolidColorBrush();
            mySolidColorBrush.Color = Color.FromRgb(255, 80, 80);
            arrow[0].Fill = mySolidColorBrush;
            arrow[0].Points = myPointCollection[0];

            SolidColorBrush mySolidColorBrush2 = new SolidColorBrush();
            mySolidColorBrush2.Color = Color.FromRgb(200, 0, 0);
            arrow[1].Fill = mySolidColorBrush2;
            arrow[1].Points = myPointCollection[1];

            SetPointArrow((canv.ActualWidth / 2), (canv.ActualHeight / (4 * arrowPosition)));
            canv.Children.Add(arrow[0]);
            canv.Children.Add(arrow[1]);
        }
        public void InitializeMenuItems(Canvas canv)
        {
            for (var i = 0; i < menuItems.Count; i++)
            {
                menuItems[i].FontSize = canv.ActualHeight / 8;
                Canvas.SetLeft(menuItems[i], canv.ActualWidth / 2 + 40);
                Canvas.SetTop(menuItems[i], canv.ActualHeight - canv.ActualHeight / 4 * (menuItems.Count - i) - (i == 0 ? 40 : 0));
                canv.Children.Add(menuItems[i]);
            }
        }
        public async void SelectMenuItem(Canvas canv, KeyEventArgs e)
        {
            if (runSelectItem)
            {
                canv.Children.Clear();
                runSelectItem = false;
                switch (e.Key)
                {
                    case Key.S:
                    case Key.Down:
                        if (arrowPosition < 3)
                            arrowPosition++;
                        break;
                    case Key.W:
                    case Key.Up:
                        if (arrowPosition > 1)
                            arrowPosition--;
                        break;
                    default: break;
                }
                switch (arrowPosition)
                {
                    case 1:
                        canv.Children.Clear();
                        DrawMenu(canv);
                        for (int i = 0; i <= 40; i += 5)
                        {
                            Canvas.SetTop(menuItems[0], canv.ActualHeight - canv.ActualHeight / 4 * 3 - i);
                            await Task.Delay(1);
                        }
                        break;
                    case 2:
                        canv.Children.Clear();
                        DrawMenu(canv);
                        for (int i = 0; i <= 40; i += 5)
                        {
                            Canvas.SetTop(menuItems[1], canv.ActualHeight - canv.ActualHeight / 4 * 2 - i);
                            await Task.Delay(1);
                        }
                        break;
                    case 3:
                        canv.Children.Clear();
                        DrawMenu(canv);
                        for (int i = 0; i <= 40; i += 5)
                        {
                            Canvas.SetTop(menuItems[2], canv.ActualHeight - canv.ActualHeight / 4 - i);
                            await Task.Delay(1);
                        }
                        break;
                    default: break;
                }
                canv.Children.Remove(arrow[0]);
                SetPointArrow((canv.ActualWidth / 2), (canv.ActualHeight / 4 * arrowPosition));
                canv.Children.Add(arrow[0]);

                canv.Children.Remove(arrow[1]);
                SetPointArrow((canv.ActualWidth / 2), (canv.ActualHeight / 4 * arrowPosition));
                canv.Children.Add(arrow[1]);
            }
            runSelectItem = true;
        }
        public int GetArrowPosition()
        {
            return arrowPosition;
        }
    }
}