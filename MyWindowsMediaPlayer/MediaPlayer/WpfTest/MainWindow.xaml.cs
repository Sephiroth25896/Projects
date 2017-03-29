using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WpfTest
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        [DllImport("user32.dll")]
        private static extern uint GetDoubleClickTime();
        public MainWindow AppMainWindow { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            pos_slider.Maximum = 1;
            //  Timer Video
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
        }

        private bool userIsDraggingSlider = false;
        private bool fullscreen = false;
        private bool shuffle = false;

        private Random rand = new Random();
        private DispatcherTimer DoubleClickTimer = new DispatcherTimer();
        public MyMedia med = new MyMedia();
        string extension = "";
        public int i = 0;
        
        public state play_state = state.stop;
        loop repeat_mode = loop.off;

        public enum state
        {
            play,
            pause,
            stop
        };

        enum loop
        {
            on,
            off,
            once
        };

        public void play()
        {
            MainWindow1.Title = System.IO.Path.GetFileNameWithoutExtension(med.chemin[i]);
            mediaElement.Play();
            play_state = state.play;
            play_button.Content = WebUtility.HtmlDecode("&#xE769;");
        }

        private void pause()
        {
            mediaElement.Pause();
            play_state = state.pause;
            play_button.Content = WebUtility.HtmlDecode("&#xE768;");
        }

        private void stop()
        {
            mediaElement.Stop();
            play_state = state.stop;
            play_button.Content = WebUtility.HtmlDecode("&#xE768;");
        }

        private void Open_click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openvideo = new OpenFileDialog();
            openvideo.Multiselect = true;
            if (openvideo.ShowDialog() == true)
            {
                i = 0;
                stop();
                
                med.chemin.AddRange(openvideo.FileNames);
                med.name.AddRange(openvideo.FileNames);
                for (int av = 0; av < med.chemin.Count(); av++)
                {
                    med.name[av] = System.IO.Path.GetFileNameWithoutExtension(med.name[av]);
                }
                extension = System.IO.Path.GetExtension(med.chemin[i]);
                mediaElement.Source = new Uri(med.chemin[i]);
                play();
            }
        }

        private void play_click(object sender, RoutedEventArgs e)
        {

            if (play_state == state.stop && med.chemin.Count() != 0)
            {
                mediaElement.Source = new Uri(med.chemin[i]);
                play();
            }
            else if (play_state == state.pause)
                play();
            else if (play_state == state.play)
                pause();
        }

        private void Stop_click(object sender, RoutedEventArgs e)    
        {
            if (play_state == state.pause || play_state == state.play)
                stop();
        }

        private void Element_MediaOpened(object sender, EventArgs e)
        {
            if (mediaElement.NaturalDuration.ToString() != "Automatic")
            pos_slider.Maximum = mediaElement.NaturalDuration.TimeSpan.TotalMilliseconds;
            else
            {
            }
        }

        private void Element_MediaEnded(object sender, EventArgs e)
        {
            if (extension != ".gif")
            {
                mediaElement.Stop();
                play_state = state.stop;
                play_button.Content = WebUtility.HtmlDecode("&#xE768;");
            }
            else
            {
                pos_slider.Maximum = 0;
                pos_slider.Value = 0;
                mediaElement.Position = new TimeSpan(0, 0, 1);
                play();
            }
            if (repeat_mode == loop.off)
            {
                if (med.chemin.Count() > i + 1)
                {
                    i++;
                    mediaElement.Source = new Uri(med.chemin[i]);
                    play();
                    MainWindow1.Title = System.IO.Path.GetFileNameWithoutExtension(med.chemin[i]);
                }
                else
                    i = 0;
            }
            else if (repeat_mode == loop.once)
            {
                if (med.chemin.Count() > i + 1)
                    play();
            }
            else
            {
                if (med.chemin.Count() > i + 1)
                    i++;
                else
                    i = 0;
                mediaElement.Source = new Uri(med.chemin[i]);
                play();
                MainWindow1.Title = System.IO.Path.GetFileNameWithoutExtension(med.chemin[i]);
            }

        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if ((mediaElement.Source != null) && (mediaElement.NaturalDuration.HasTimeSpan) && (!userIsDraggingSlider))
            {
                pos_slider.Minimum = 0;
                pos_slider.Maximum = mediaElement.NaturalDuration.TimeSpan.TotalSeconds;
                pos_slider.Value = mediaElement.Position.TotalSeconds;
            }
        }

        private void pos_slider_DragStarted(object sender, DragStartedEventArgs e)
        {
            userIsDraggingSlider = true;
        }

        private void pos_slider_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            userIsDraggingSlider = false;
            mediaElement.Position = TimeSpan.FromSeconds(pos_slider.Value);
        }

        private void pos_slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            lblStatus.Text = TimeSpan.FromSeconds(pos_slider.Value).ToString(@"hh\:mm\:ss");
        }

        private void Grid_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            mediaElement.Volume += (e.Delta > 0) ? 0.005 : -0.005;
            if (mediaElement.Volume > 1)
                mediaElement.Volume = 1;
            else if (mediaElement.Volume < 0)
                mediaElement.Volume = 0;
            volume_slider.Value = mediaElement.Volume;
            int tmp = (int)(volume_slider.Value * 200);
            lblVolume.Text = "%" + tmp.ToString();
        }


        private void ChangeMediaVolume(object sender, RoutedPropertyChangedEventArgs<double> args)
        {
            mediaElement.Volume = volume_slider.Value;
            int tmp = (int)(volume_slider.Value * 200);
            lblVolume.Text = "%" + tmp.ToString();

        }

        private void quit_click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void speed_change(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            mediaElement.SpeedRatio = speed_slider.Value;
            double tmp = (speed_slider.Value);
            lblspeed.Text = "x " + tmp.ToString();
        }

        public void putFullscreen()
        {
            controle.Visibility = Visibility.Hidden;
            Thickness margin = media_grid.Margin;
            margin.Bottom = 0;
            margin.Top = 0;
            media_grid.Margin = margin;
            fullscreen_button.Content = WebUtility.HtmlDecode("&#59199;");

            this.WindowStyle = WindowStyle.None;
            this.WindowState = WindowState.Maximized;
            
        }

        public void removeFullscreen()
        {
            controle.Visibility = Visibility.Visible;
            Thickness margin = media_grid.Margin;
            margin.Bottom = 65;
            margin.Top = 40;
            media_grid.Margin = margin;
            fullscreen_button.Content = WebUtility.HtmlDecode("&#59200;");

            this.WindowStyle = WindowStyle.SingleBorderWindow;
            this.WindowState = WindowState.Normal;
        }

        System.Timers.Timer timeClick = new System.Timers.Timer((int)GetDoubleClickTime())
        {
            AutoReset = false
        };

        private void full_screen(object sender, MouseButtonEventArgs e)
        {
            if (!timeClick.Enabled)
            {
                timeClick.Enabled = true;
                return;
            }
            if (timeClick.Enabled)
            {
                if (!fullscreen)
                    putFullscreen();
                else
                    removeFullscreen();
                fullscreen = !fullscreen;
            }
        }

        private void Key_pressed(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape && fullscreen)
            {
                removeFullscreen();
                fullscreen = !fullscreen;
            }
            if (e.Key == Key.Space && play_state != state.stop)
            {
                if (play_state == state.pause)
                    play();
                else
                    pause();
            }
            if (e.Key == Key.L && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                openList();
            }
            if (e.Key == Key.O && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                OpenFileDialog openvideo = new OpenFileDialog();
                openvideo.Multiselect = true;
                if (openvideo.ShowDialog() == true)
                {
                    i = 0;
                    stop();

                    med.chemin.AddRange(openvideo.FileNames);
                    med.name.AddRange(openvideo.FileNames);
                    for (int av = 0; av < med.chemin.Count(); av++)
                    {
                        med.name[av] = System.IO.Path.GetFileNameWithoutExtension(med.name[av]);
                    }
                    extension = System.IO.Path.GetExtension(med.chemin[i]);
                    mediaElement.Source = new Uri(med.chemin[i]);
                    play();
                }
            }
        }

        private void pos_mouse(object sender, MouseEventArgs e)
        {
            if (fullscreen)
            {
                double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
                if (Mouse.GetPosition(media_grid).Y > screenHeight - 100)
                    controle.Visibility = Visibility.Visible;
                else
                    controle.Visibility = Visibility.Hidden;

            }
        }

        private void prev_click(object sender, RoutedEventArgs e)
        {
            if (!shuffle)
            {
                if (i - 1 >= 0)
                {
                    i--;
                    mediaElement.Source = new Uri(med.chemin[i]);
                    play();
                    MainWindow1.Title = System.IO.Path.GetFileNameWithoutExtension(med.chemin[i]);
                }
            }
            else
                shuffle_on();
        }

        private void click_next(object sender, RoutedEventArgs e)
        {
            if (!shuffle)
            {
                if (med.chemin == null)
                    return;
                if (med.chemin.Count() > i + 1)
                {
                    i++;
                    mediaElement.Source = new Uri(med.chemin[i]);
                    play();
                    MainWindow1.Title = System.IO.Path.GetFileNameWithoutExtension(med.chemin[i]);
                }
            }
            else
                shuffle_on();
        }

        private void openList()
        {
            ListeLecture window_list = new ListeLecture(this);
            window_list.Owner = this;
            window_list.Show();
            this.Hide();
        }

        private void listeLecture_click(object sender, RoutedEventArgs e)
        {
            openList();
        }

        private void openBib()
        {
            ListeLecture window_bib = new ListeLecture(this);
            window_bib.Owner = this;
            window_bib.Show();
            this.Hide();
        }

        private void Bibliotheque_click(object sender, RoutedEventArgs e)
        {
            openBib();
        }

        private void shuffle_click(object sender, RoutedEventArgs e)
        {
            shuffle = !shuffle;
        }

        private void shuffle_on()
        {
            int tmp = rand.Next(0, med.chemin.Count());

            while (tmp == i)
                tmp = rand.Next(0, med.chemin.Count());
            i = tmp;
            if (mediaElement.Source != null && med.chemin.Count() > i)
            {
                mediaElement.Source = new Uri(med.chemin[i]);
                play();
                MainWindow1.Title = System.IO.Path.GetFileNameWithoutExtension(med.chemin[i]);
            }
        }

        private void repeat_click(object sender, RoutedEventArgs e)
        {
            if (repeat_mode == loop.off)
            {
                repeat_mode = loop.on;
                repeat_button.Foreground = Brushes.Black;
            }
            else if (repeat_mode == loop.on)
            {
                repeat_mode = loop.once;
                repeat_button.Content = WebUtility.HtmlDecode("&#59629;");
                repeat_button.Foreground = Brushes.Black;
            }
            else
            {
                repeat_mode = loop.off;
                repeat_button.Content = WebUtility.HtmlDecode("&#59630;");
                repeat_button.Foreground = Brushes.Red;
            }
        }

        private void fullscreen_click(object sender, RoutedEventArgs e)
        {
            if (!fullscreen)
                putFullscreen();
            else
                removeFullscreen();
            fullscreen = !fullscreen;
        }
    }
}
