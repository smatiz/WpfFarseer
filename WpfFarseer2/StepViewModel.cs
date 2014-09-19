using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfFarseer
{
    public class StepViewModel : NotifyObjectViewer
    {
        float _dt = 0f;
        //float _step = 0f;

        System.Windows.Forms.Timer _timer = new System.Windows.Forms.Timer();

        public StepViewModel()
        {

            _timer.Interval = 25;
            _timer.Tick += (s, e) =>
            {
                if (FarseerCanvas != null)
                {
                    //if (_velocity > 0)
                    {
                        FarseerCanvas.Step(_dt);
                    }
                }
            };
            _timer.Start();
        }

        public int TickInterval
        {
            set 
            {
                _timer.Interval = value;
            }
        }

        void notifyCommands()
        {
            NotifyPropertyChanged(() => PauseCommand);
            NotifyPropertyChanged(() => PlayCommand);
            NotifyPropertyChanged(() => BackCommand);

        }

        void play()
        {
            _dt = _timer.Interval * 0.001f;// _timer.Interval;
            notifyCommands();
        }

        void pause()
        {
            _dt = 0;
            notifyCommands();
        }

        void back()
        {
            _dt = - _timer.Interval;
            notifyCommands();
        }

        public ICommand PlayCommand { get { return new BasicCommand(play, () => _dt == 0); } }
        public ICommand PauseCommand { get { return new BasicCommand(pause, () => _dt != 0); } }
        public ICommand BackCommand { get { return new BasicCommand(back, () => _dt == 0); } }

        public ICommand VoidCommand { get { return new BasicCommand(() => { }, () => false); } }


        public FarseerCanvas FarseerCanvas { private get; set; }
    }
}
