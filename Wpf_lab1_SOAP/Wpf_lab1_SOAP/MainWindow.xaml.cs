using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Diagnostics;
using System.Windows.Navigation;
using Wpf_lab1_SOAP.CBR;

namespace Wpf_lab1_SOAP
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ViewModel();
        }
      
    }
}
