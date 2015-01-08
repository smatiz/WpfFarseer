
using System;
using System.Windows;
namespace SM.Xaml
{
    public partial class  RopeJointControl : BasicJointControl, IRopeJoint
    {
                  
                public Single MaxLength
                {
                    get { return (Single)GetValue(MaxLengthProperty); }
                    set { SetValue(MaxLengthProperty, value); }
                }
                public static readonly DependencyProperty MaxLengthProperty =
                    DependencyProperty.Register("MaxLength", typeof(Single), typeof(RopeJointControl));

                    
                public Single MaxLengthFactor
                {
                    get { return (Single)GetValue(MaxLengthFactorProperty); }
                    set { SetValue(MaxLengthFactorProperty, value); }
                }
                public static readonly DependencyProperty MaxLengthFactorProperty =
                    DependencyProperty.Register("MaxLengthFactor", typeof(Single), typeof(RopeJointControl));

                    
                public String TargetFlagIdA
                {
                    get { return (String)GetValue(TargetFlagIdAProperty); }
                    set { SetValue(TargetFlagIdAProperty, value); }
                }
                public static readonly DependencyProperty TargetFlagIdAProperty =
                    DependencyProperty.Register("TargetFlagIdA", typeof(String), typeof(RopeJointControl));

                    
                public String TargetFlagIdB
                {
                    get { return (String)GetValue(TargetFlagIdBProperty); }
                    set { SetValue(TargetFlagIdBProperty, value); }
                }
                public static readonly DependencyProperty TargetFlagIdBProperty =
                    DependencyProperty.Register("TargetFlagIdB", typeof(String), typeof(RopeJointControl));

      }
}

 