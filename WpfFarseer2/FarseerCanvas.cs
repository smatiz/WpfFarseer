using FarseerPhysics.Dynamics;
using System;
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

namespace WpfFarseer
{
    public class FarseerCanvas : Canvas
    {
        World _world;
        double _elapsedStep = 0;
        public FarseerCanvas()
        {
            Loaded += (s, e) =>
            {
                if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this)) return;
                _world = new World(new Microsoft.Xna.Framework.Vector2(0, 10));
                foreach (var child in Children)
                {
                    var body = child as BodyControl;
                    if (body != null)
                    {
                        body.Initialize(_world);
                    }
                }
            };
        }

        private void update()
        {
            if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this)) return;
            foreach (var child in Children)
            {
                var bodyControl = child as BodyControl;
                if (bodyControl != null)
                {
                    bodyControl.Update(_world);
                }
            }
        }

        private void clear()
        {
            if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this)) return;
            _world = new World(new Microsoft.Xna.Framework.Vector2(0, 10));
            
            foreach (var child in Children)
            {
                var body = child as BodyControl;
                if (body != null)
                {
                    body.Initialize(_world);
                }
            }

            //_world.Clear();
            //_world = new World(new Microsoft.Xna.Framework.Vector2(0, 10));
            _elapsedStep = 0;
            update();
        }

        public void Step(float dt)
        {

            if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this)) return;
            if (dt >= 0)
            {
                _elapsedStep += dt;
                _world.Step(dt);
            }
            else
            {
                /*
                var step = (float)(_elapsedStep + dt);
                if (step >= 0)
                {
                    clear();
                    while (step > 100)
                    {
                        Step(100);
                        step -= 100;
                    }
                    Step(step);
                }*/
            }
            update();
        }

        public StepViewModel StepViewModel
        {
            get { return (StepViewModel)GetValue(StepViewModelProperty); }
            set { SetValue(StepViewModelProperty, value); }
        }
        public static readonly DependencyProperty StepViewModelProperty =
            DependencyProperty.Register("StepViewModel", typeof(StepViewModel), typeof(FarseerCanvas), new PropertyMetadata(null, OnStepViewModelChanged));
        private static void OnStepViewModelChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var _this = (FarseerCanvas)dependencyObject;
            _this.StepViewModel.FarseerCanvas = _this;
        }
       
    }
}

