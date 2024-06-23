using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace WpfUIApplication
{
    public class MainViewModel:INotifyPropertyChanged
    {
        private string _musicName = "重生之我在异乡为异客（女生独唱版）-周深深     重生之我在异乡为异客（女生独唱版）-周深深    ";

        public string MusicName
        {
            get { return _musicName; }
            set
            {
                _musicName = value;
                OnPropertyChange(nameof(MusicName));
            }
        }


        private CancellationTokenSource _textToken;
        private Task _textTask;

        public MainViewModel()
        {
            _textToken = new CancellationTokenSource();
        }

        private void timer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            var tempName = MusicName;
            MusicName = tempName.Substring(1) + tempName[0];
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChange([CallerMemberName] string name ="")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
