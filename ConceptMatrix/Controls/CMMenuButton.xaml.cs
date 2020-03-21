﻿using System;
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

namespace ConceptMatrix.Controls
{
    /// <summary>
    /// Interaction logic for CMMenuButton.xaml
    /// </summary>
    public partial class CMMenuButton : UserControl
    {
        public CMMenuButton()
        {
            InitializeComponent();
        }
        public double IconFontSize
        {
            get
            {
                return iconBox.FontSize;
            }
            set
            {
                iconBox.FontSize = value;
            }
        }

        public string IconText
        {
            get
            {
                return iconBox.Text;
            }
            set
            {
                iconBox.Text = value;
            }
        }

        public string ContentText
        {
            get
            {
                return contentBox.Text;
            }
            set
            {
                contentBox.Text = value;
            }
        }

        private void wholePanel_MouseEvent(object sender, MouseEventArgs e)
        {
            if (wholePanel.IsMouseOver)
            {
                VisualStateManager.GoToElementState(wholePanel, "MouseEnter", true);
            }
            else
            {
                VisualStateManager.GoToElementState(wholePanel, "MouseLeave", true);
            }
        }
    }
}
