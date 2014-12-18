﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SM.Wpf
{
    public abstract class BasicControl : UserControl, IEntity
    {
        public string Id { get; set; }
        static int i = 0;
        private static string GetAutoGenerateName()
        {
            return string.Format("_{0:000}", i++);
        }

        public BasicControl()
        {
            Id = GetAutoGenerateName();
        }
    }
       
}
