﻿using System.Windows;

namespace MailSender
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnOpenSchedulerClick(object sender, RoutedEventArgs e)
        {
            MainTabConlrol.SelectedItem = SchedulerTab;
        }
    }
}
