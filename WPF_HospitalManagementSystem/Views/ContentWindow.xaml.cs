using System;
using System.Threading;
using System.Windows;

namespace WPF_HospitalManagementSystem.Views;

/// <summary>
/// Interaction logic for ContentWindow.xaml
/// </summary>
public partial class ContentWindow : Window
{
    public void InformationalBox(string messageText, string caption,
        object newContent = null, MessageBoxButton buttonType = MessageBoxButton.OK,
        MessageBoxImage imageType = MessageBoxImage.Asterisk, int duration = 200)
    {
        MyFrame.Content = null;
        MyFrame.Content = newContent;
        Thread.Sleep(duration);
        MessageBox.Show(messageText, caption, buttonType, imageType);
    }

    public ContentWindow() => InitializeComponent();
    private void ContentWindow_OnClosed(object sender, EventArgs e) => Environment.Exit(0);
    private void Branches_Click(object sender, RoutedEventArgs e) => InformationalBox("Branch Data Loaded", "Loaded", new Branches());
    private void Doctors_Click(object sender, RoutedEventArgs e) => InformationalBox("Doctor Data Loaded", "Loaded", new Doctors());
    private void Nurses_Click(object sender, RoutedEventArgs e) => InformationalBox("Nurse Data Loaded", "Loaded", new Nurses());
    private void Clear_Click(object sender, RoutedEventArgs e) => InformationalBox("Screen is Cleared", "Clear", duration: 150);
}