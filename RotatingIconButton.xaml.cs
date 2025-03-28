﻿using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using System;

namespace BusNetworkSystem
{
    public partial class RotatingIconButton : UserControl
    {
        public static readonly DependencyProperty ButtonTextProperty =
            DependencyProperty.Register(
                "ButtonText",
                typeof(string),
                typeof(RotatingIconButton),
                new PropertyMetadata("Пополнить баланс"));

        public string ButtonText
        {
            get => (string)GetValue(ButtonTextProperty);
            set => SetValue(ButtonTextProperty, value);
        }

        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register(
                "Command",
                typeof(ICommand),
                typeof(RotatingIconButton));

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public RotatingIconButton()
        {
            InitializeComponent();
            MainButton.Content = ButtonText;
            MainButton.Command = Command;
        }
    }
}
