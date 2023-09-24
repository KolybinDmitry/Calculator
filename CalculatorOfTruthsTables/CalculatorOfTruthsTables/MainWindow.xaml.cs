using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
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

namespace CalculatorOfTruthsTables
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Model - класс логической переменной
        // View - dataGrid с таблицей истинности
        // ViewModel - обработка логического выражения

        public MainWindow()
        {
            InitializeComponent();
        }

        // VIEW
        private void buttonOk_Click(object sender, RoutedEventArgs e)
        {
            // Валидация
            if (int.TryParse(textBoxCount.Text, out var size) && size >= 0 && size <= 8)
            {
                // генерируем таблицу истинности
                var variables = RowInTableOfTruth.GetVariables(int.Parse(textBoxCount.Text));
                try
                {
                    // вычисляем булеву функцию
                    Calculus.Function(variables, textBoxExpression.Text);
                }
                catch (ArgumentException)
                {
                    MessageBox.Show("Логическое выражение неверно записано", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                catch
                {
                    MessageBox.Show("Произошла неизвестная ошибка", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                // выдаем результат на экран
                dataGrid.ItemsSource = variables;
            }
            else
                MessageBox.Show("Количество переменных может быть только от 0 до 8", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}
