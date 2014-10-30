using SM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace  SM.Farseer
{
    public class StepViewModel : NotifyObjectViewer
    {

        FarseerWorldManager _worldManager;

        public StepViewModel(FarseerWorldManager worldManager)
        {
            _worldManager = worldManager;
        }

        void notifyCommands()
        {
            NotifyPropertyChanged(() => PauseCommand);
            NotifyPropertyChanged(() => PlayCommand);
            NotifyPropertyChanged(() => BackCommand);
        }

        void play()
        {
            //_worldManager_old.Play();
            _worldManager.Build();
       
            _worldManager.Play();
            notifyCommands();
        }

        void pause()
        {
            //_worldManager_old.Pause();
            _worldManager.Pause();
            notifyCommands();
        }

        void back()
        {
            notifyCommands();
        }

        void save()
        {
        }
        void load()
        {
        }
        void restart()
        {
            /*if (_worldManager == null) return;
            _worldManager.Restart();*/
        }

        int _dt=0;
        public ICommand PlayCommand { get { return new BasicCommand(play, () => _dt == 0); } }
        public ICommand PauseCommand { get { return new BasicCommand(pause, () => _dt == 0); } }
        public ICommand RestartCommand { get { return new BasicCommand(restart, () => _dt == 0); } }
        public ICommand BackCommand { get { return new BasicCommand(back, () => _dt == 0); } }
        
        public ICommand VoidCommand { get { return new BasicCommand(() => { }, () => false); } }

       

    }
}
