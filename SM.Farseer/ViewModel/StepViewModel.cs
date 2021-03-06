﻿using SM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
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
            _worldManager.Play();
            notifyCommands();
        }

        void pause()
        {
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
        void startXna()
        {
           // _worldManager.Build();
            string path = @"S:\ROOT\DROPBOX\Dropbox\DATI\PROGRAMMAZIONE\CSHARP\FARSEER_ALL\Farseer Physics Engine 3.5 Samples\Farseer Physics Samples 3.5\bin\x86\Debug\Samples XNA.exe";
            string pathT = @"S:\ROOT\DROPBOX\Dropbox\DATI\PROGRAMMAZIONE\CSHARP\FARSEER_ALL\Farseer Physics Engine 3.5 Samples\Farseer Physics Samples 3.5\bin\x86\Debug\script.txt";
            //CodeGenerator.Header = _worldManager.Id;
            string code = CodeGenerator.Code;
            File.WriteAllText(pathT, code);
            var process = new Process();
            process.StartInfo.FileName = path;
            process.StartInfo.Arguments = "\"" + pathT + "\"";
            process.Start();
        }
        int _dt=0;
        public ICommand PlayCommand { get { return new BasicCommand(play, () => _dt == 0); } }
        public ICommand PauseCommand { get { return new BasicCommand(pause, () => _dt == 0); } }
        public ICommand RestartCommand { get { return new BasicCommand(restart, () => _dt == 0); } }
        public ICommand BackCommand { get { return new BasicCommand(back, () => _dt == 0); } }



        public ICommand XnaCommand { get { return new BasicCommand(startXna, () => _dt == 0); } }
        
        public ICommand VoidCommand { get { return new BasicCommand(() => { }, () => false); } }

       

    }
}
