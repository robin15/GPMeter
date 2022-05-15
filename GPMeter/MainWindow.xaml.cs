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
using System.IO;
using System.Windows.Threading;

namespace GPMeter
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            GPMeter.Orientation = Orientation.Vertical;
            SetupTimer();
        }
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //マウスボタン押下状態でなければ何もしない  
            if (e.ButtonState != MouseButtonState.Pressed) return;

            this.DragMove();
        }

        private void MyTimerMethod(object sender, EventArgs e)
        {
            using (var sr = new StreamReader("TextFile.txt"))
            {
                var val = Int32.Parse(sr.ReadToEnd()) * 100 / 5000;
                GPMeter.Value = val;
                this.DataContext = val;
                if (GPMeter.Value >= 0 && GPMeter.Value <= 40)
                {
                    GPMeter.Foreground = Brushes.Green;
                }
                else if (GPMeter.Value > 30 && GPMeter.Value <= 70)
                {
                    GPMeter.Foreground = Brushes.Yellow;
                }
                else
                {
                    GPMeter.Foreground = Brushes.Red;
                }
            }
        }

        // タイマのインスタンス
        private DispatcherTimer _timer;

        // タイマを設定する
        private void SetupTimer()
        {
            // タイマのインスタンスを生成
            _timer = new DispatcherTimer(); // 優先度はDispatcherPriority.Background
                                            // インターバルを設定
            _timer.Interval = new TimeSpan(0, 0, 1);
            // タイマメソッドを設定
            _timer.Tick += new EventHandler(MyTimerMethod);
            // タイマを開始
            _timer.Start();
        }

    }
}
