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

namespace SM.WpfView
{
    public partial class CrossControl : Canvas//, IPointControl
    {
        public CrossControl()
        {
            InitializeComponent();
        }

        //public float X { get { return (float)Canvas.GetLeft(this); } }
        //public float Y { get { return (float)Canvas.GetTop(this); } }

        //public string ParentId { get { return ((BasicControl)Parent).Id; } }
        //public string Id { get; set; }
    }
}