
using System;
using System.Windows;
namespace SM.Xaml
{
    public partial class  WheelJointControl : BasicJointControl, IWheelJoint
    {
                  
                public Single MotorSpeed
                {
                    get { return (Single)GetValue(MotorSpeedProperty); }
                    set { SetValue(MotorSpeedProperty, value); }
                }
                public static readonly DependencyProperty MotorSpeedProperty =
                    DependencyProperty.Register("MotorSpeed", typeof(Single), typeof(WheelJointControl));

                              
                public Single MaxMotorTorque
                {
                    get { return (Single)GetValue(MaxMotorTorqueProperty); }
                    set { SetValue(MaxMotorTorqueProperty, value); }
                }
                public static readonly DependencyProperty MaxMotorTorqueProperty =
                    DependencyProperty.Register("MaxMotorTorque", typeof(Single), typeof(WheelJointControl));

                              
                public Single Frequency
                {
                    get { return (Single)GetValue(FrequencyProperty); }
                    set { SetValue(FrequencyProperty, value); }
                }
                public static readonly DependencyProperty FrequencyProperty =
                    DependencyProperty.Register("Frequency", typeof(Single), typeof(WheelJointControl));

                              
                public Single DampingRatio
                {
                    get { return (Single)GetValue(DampingRatioProperty); }
                    set { SetValue(DampingRatioProperty, value); }
                }
                public static readonly DependencyProperty DampingRatioProperty =
                    DependencyProperty.Register("DampingRatio", typeof(Single), typeof(WheelJointControl));

                              
                public Single JointTranslatio
                {
                    get { return (Single)GetValue(JointTranslatioProperty); }
                    set { SetValue(JointTranslatioProperty, value); }
                }
                public static readonly DependencyProperty JointTranslatioProperty =
                    DependencyProperty.Register("JointTranslatio", typeof(Single), typeof(WheelJointControl));

                              
                public Single JointSpeed
                {
                    get { return (Single)GetValue(JointSpeedProperty); }
                    set { SetValue(JointSpeedProperty, value); }
                }
                public static readonly DependencyProperty JointSpeedProperty =
                    DependencyProperty.Register("JointSpeed", typeof(Single), typeof(WheelJointControl));

                              
                public Boolean MotorEnabled
                {
                    get { return (Boolean)GetValue(MotorEnabledProperty); }
                    set { SetValue(MotorEnabledProperty, value); }
                }
                public static readonly DependencyProperty MotorEnabledProperty =
                    DependencyProperty.Register("MotorEnabled", typeof(Boolean), typeof(WheelJointControl));

                }

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

 