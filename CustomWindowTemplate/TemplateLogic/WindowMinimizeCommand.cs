﻿using System;
using System.Windows;
using System.Windows.Input;

namespace CustomWindowTemplate.TemplateLogic
{
    public class WindowMinimizeCommand :ICommand
    {     

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            var window = parameter as Window;

            if (window != null)
            {
                window.WindowState = WindowState.Minimized;
            }
        }
    }
}
