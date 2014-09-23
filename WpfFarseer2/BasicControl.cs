﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WpfFarseer
{
    public class BasicControl : Canvas
    {
        public TimeSpan Delay
        {
            get { return (TimeSpan)GetValue(DelayProperty); }
            set { SetValue(DelayProperty, value); }
        }
        public static readonly DependencyProperty DelayProperty =
            DependencyProperty.Register("Delay", typeof(TimeSpan), typeof(BasicControl), new PropertyMetadata(new TimeSpan(0)));



        public string Id { get; set; }
        //{
        //    get { return (string)GetValue(Id {get; private set;}Property); }
        //    set { SetValue(Id {get; private set;}Property, value); }
        //}
        //public static readonly DependencyProperty Id {get; private set;}Property =
        //    DependencyProperty.Register("Id {get; private set;}", typeof(string), typeof(BasicControl), new PropertyMetadata(null));

        
    }
}
