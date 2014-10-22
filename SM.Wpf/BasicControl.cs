﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SM.Wpf
{
    // controlli in grado di agganciare il proprio canvas o il canvas di un altro BasicControl al canvas padre
    public class BasicControl : Visual
    {
        protected Canvas _canvas = new Canvas();
        protected UIElementCollection _parentChildrens;
        public void AddToUIElementCollection(UIElementCollection parentChildrens)
        {
            _parentChildrens = parentChildrens;
            _parentChildrens.Add(_canvas);
        }
        static int i = 0;
        public static string AutoGenerateName()
        {
            return string.Format("autogenerated_{0}", i++);
        }
        public BasicControl()
        {
            if (Id == null || Id == "")
            {
                Id = BasicControl.AutoGenerateName();
            }
            //_canvas.Tag = this;
        }

        public string Id
        {
            get;
            set;
        }

    }
}
