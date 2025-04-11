using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Security.Cryptography;
using BusNetworkSystem.Custom;
using System.Windows.Shapes;
using System;

namespace BusNetworkSystem.Pages.PersonalCabinet
{
    /// <summary>
    /// Логика взаимодействия для PasswordChange.xaml
    /// </summary>
    public partial class PasswordChange : Window
    {
        CustomMessageBox box;
        private Brush _originalBorderBrush;
        private Brush _originalBackground;

        public PasswordChange()
        {
            InitializeComponent();
        }
        private void PassB_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (!(sender is PasswordBox passwordBox)) return;

            // Получаем Border из визуального дерева
            Border border = FindVisualChild<Border>(passwordBox, "border");
            if (border == null) return;

            // Сохраняем оригинальные значения
            _originalBorderBrush = border.BorderBrush.Clone();
            _originalBackground = border.Background.Clone();

            // Создаем эффект подсветки
            var clickPosition = e.GetPosition(border);

            // Создаем новый Border для анимации поверх основного
            var highlightBorder = new Border
            {
                Width = border.ActualWidth,
                Height = border.ActualHeight,
                CornerRadius = new CornerRadius(10),
                BorderThickness = new Thickness(1),
                BorderBrush = new SolidColorBrush(Color.FromArgb(150, 173, 216, 230)),
                Background = Brushes.Transparent,
                IsHitTestVisible = false,
                RenderTransformOrigin = new Point(
                    clickPosition.X / border.ActualWidth,
                    clickPosition.Y / border.ActualHeight),
                RenderTransform = new ScaleTransform(1, 1)
            };

            // Добавляем highlightBorder в тот же родительский контейнер
            var parent = VisualTreeHelper.GetParent(border) as Panel;
            parent?.Children.Add(highlightBorder);

            // Анимация увеличения и прозрачности
            var scaleAnimation = new DoubleAnimation
            {
                From = 1,
                To = 20,
                Duration = TimeSpan.FromSeconds(0.5),
                FillBehavior = FillBehavior.Stop
            };

            var opacityAnimation = new DoubleAnimation
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromSeconds(0.5),
                FillBehavior = FillBehavior.Stop
            };

            // Применяем анимации
            highlightBorder.RenderTransform.BeginAnimation(ScaleTransform.ScaleXProperty, scaleAnimation);
            highlightBorder.RenderTransform.BeginAnimation(ScaleTransform.ScaleYProperty, scaleAnimation);
            highlightBorder.BeginAnimation(UIElement.OpacityProperty, opacityAnimation);

            // Удаляем highlightBorder после анимации
            scaleAnimation.Completed += (s, args) =>
            {
                if (parent != null && parent.Children.Contains(highlightBorder))
                {
                    parent.Children.Remove(highlightBorder);
                }
            };
        }

        // Вспомогательный метод для поиска дочерних элементов в визуальном дереве
        private static T FindVisualChild<T>(DependencyObject parent, string childName)
            where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                if (child is T result && (child as FrameworkElement)?.Name == childName)
                {
                    return result;
                }

                var childResult = FindVisualChild<T>(child, childName);
                if (childResult != null)
                    return childResult;
            }
            return null;
        }

        private void ConfirmChange_Click(object sender, RoutedEventArgs e)
        {
            if ((!string.IsNullOrWhiteSpace(OldPassBox.Password) && !string.IsNullOrWhiteSpace(NewPassBox.Password) && !string.IsNullOrWhiteSpace(RepeatPassBox.Password)) && RepeatPassBox.Password == NewPassBox.Password)
            {
                box = new CustomMessageBox("Пароль изменён", "Выполнено", MessageBoxImage.Information);
                box.CancelButton.Visibility = Visibility.Collapsed;
                box.ShowDialog();
                this.Close();
            }
            else if (!(RepeatPassBox.Password == NewPassBox.Password))
            {
                box = new CustomMessageBox("Повторите правильный пароль", "Ошибка", MessageBoxImage.Error);
                box.CancelButton.Visibility = Visibility.Collapsed;
                Opacity = 0.4;
                box.ShowDialog();
                Opacity = 1;
                return;
            }
            else
            {
                box = new CustomMessageBox("Заполните все поля, чтобы продолжить", "Ошибка", MessageBoxImage.Error);
                box.CancelButton.Visibility = Visibility.Collapsed;
                Opacity = 0.4;
                box.ShowDialog();
                Opacity = 1;
                return;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
