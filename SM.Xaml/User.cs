﻿using SM;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
using System.Windows.Threading;
using System.Collections.ObjectModel;
using System.Windows.Markup;
using SM.Wpf;

namespace SM.Xaml
{
    //[ContentPropertyAttribute("Descriptors")]
    public class User : Layer, IDescriptor
    {
        public string Id { get; set; }
        public float DesignZoom
        {
            get { return (float)GetValue(DesignZoomProperty); }
            set { SetValue(DesignZoomProperty, value); }
        }
        public static readonly DependencyProperty DesignZoomProperty =
            DependencyProperty.Register("DesignZoom", typeof(float), typeof(User), new PropertyMetadata(1f));
    }
}
