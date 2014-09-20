using FarseerPhysics.Common;
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

        System.Windows.Forms.Timer _timer = new System.Windows.Forms.Timer();

        public StepViewModel()
        {

            _timer.Interval = 25;
            _timer.Tick += (s, e) =>
            {
                if (_farseerCanvas != null)
                {
                    //if (_velocity > 0)
                    {
                        _farseerCanvas.Update();
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
            NotifyPropertyChanged(() => SaveCommand);
            NotifyPropertyChanged(() => LoadCommand);

        }

        void play()
        {
            _farseerCanvas.Play();
            notifyCommands();
        }

        void pause()
        {
            _farseerCanvas.Pause();
            notifyCommands();
        }

        void back()
        {
            notifyCommands();
        }

        void save()
        {
            if (_farseerCanvas == null) return;
            _farseerCanvas.Save();
        }
        void load()
        {
            if (_farseerCanvas == null) return;
            _farseerCanvas.Load();
        }

        int _dt=0;
        public ICommand PlayCommand { get { return new BasicCommand(play, () => _dt == 0); } }
        public ICommand PauseCommand { get { return new BasicCommand(pause, () => _dt == 0); } }
        public ICommand BackCommand { get { return new BasicCommand(back, () => _dt == 0); } }
        public ICommand SaveCommand { get { return new BasicCommand(save, () => _farseerCanvas != null && _dt == 0 && _farseerCanvas.Savable); } }
        public ICommand LoadCommand { get { return new BasicCommand(load, () => _farseerCanvas != null && _dt == 0); } }
        
        public ICommand VoidCommand { get { return new BasicCommand(() => { }, () => false); } }

        FarseerCanvas _farseerCanvas;
        public FarseerCanvas FarseerCanvas
        {
            set
            {
                _farseerCanvas = value;
                notifyCommands();
            }
        }
    }
}
