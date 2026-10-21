
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using EMP_record_management.Models;
using EMP_record_management.Services;

namespace EMP_record_management
{
    public partial class MainWindow : Window
    {
        private readonly EmployeeManager employeeManager =
            new EmployeeManager();

        public MainWindow()
        {
            InitializeComponent();
            employeeManager.Init();
            RefreshEmployeeList();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameTextBox.Text) ||
                !int.TryParse(AgeTextBox.Text, out int age) ||
                !decimal.TryParse(
                    SalaryTextBox.Text,
                    NumberStyles.Number,
                    CultureInfo.CurrentCulture,
                    out decimal salary) ||
                age <= 0 || salary < 0)
            {
                MessageBox.Show("Enter valid employee details.");
                return;
            }

            emp employee = new emp
            {
                Name = NameTextBox.Text.Trim(),
                Age = age,
                Salary = salary
            };

            employeeManager.Add(employee);
            RefreshEmployeeList();

            NameTextBox.Clear();
            AgeTextBox.Clear();
            SalaryTextBox.Clear();

        }
        private void InitButton_Click(object sender, RoutedEventArgs e)
        {
            employeeManager.Init();
            RefreshEmployeeList();
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            employeeManager.Clear();
            RefreshEmployeeList();
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string filePath = FilePathTextBox.Text.Trim();

                if (FormatComboBox.SelectedItem is not ComboBoxItem selectedItem)
                {
                    MessageBox.Show("Select a file format.");
                    return;
                }

                string format = selectedItem.Content.ToString()!;

                employeeManager.Save(filePath, format);

                MessageBox.Show("Employee records saved successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Save failed: {ex.Message}");
            }
        }

        private void RestoreButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string filePath = FilePathTextBox.Text.Trim();

                employeeManager.Restore(filePath);
                RefreshEmployeeList();

                MessageBox.Show("Employee records restored successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Restore failed: {ex.Message}");
            }
        }

        private void RefreshEmployeeList()
        {
            EmployeeListView.ItemsSource = null;
            EmployeeListView.ItemsSource = employeeManager.Employees;
        }
    }
}
