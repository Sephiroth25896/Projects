using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace WpfTest
{
    /// <summary>
    /// Logique d'interaction pour ListeLecture.xaml
    /// </summary>
    public partial class ListeLecture : Window
    {

        string[] FilePaths { get; set; }
        string[] FilePaths2 { get; set; }
        string[] FilePaths3 { get; set; }
        string[] FilePaths4 { get; set; }

        public ListeLecture(MainWindow AppMainWindow)
        {
            InitializeComponent();
            comboBox.SelectedIndex = 0;
            this.Top = AppMainWindow.Top;
            this.Left = AppMainWindow.Left;

            this.Height = AppMainWindow.ActualHeight;
            this.Width = AppMainWindow.ActualWidth;
            this.WindowState = AppMainWindow.WindowState;
            listView.Items.Clear();
            select_box();
            if (AppMainWindow.med.chemin != null)
            {
                for (int i = 0; i < AppMainWindow.med.chemin.Count(); ++i)
                {
                    listView.Items.Add((AppMainWindow.med.name[i]));
                    //listView.Items.Add(System.IO.Path.GetFileNameWithoutExtension(AppMainWindow.med.chemin[i]));
                }
            }
        }

        private void go_close(object sender, EventArgs e)
        {
            ((MainWindow)this.Owner).Show();
        }

        private void Key_pressed(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.L && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                this.Close();
                ((MainWindow)this.Owner).Show();
            }
        }

        private void Button_Add_Song(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openvideo = new OpenFileDialog();
            openvideo.Multiselect = true;
            if (openvideo.ShowDialog() == true)
            {
                ((MainWindow)this.Owner).med.chemin.AddRange(openvideo.FileNames);
                ((MainWindow)this.Owner).med.name.AddRange(openvideo.FileNames);
                for (int av = 0; av < ((MainWindow)this.Owner).med.chemin.Count(); av++)
                {
                    ((MainWindow)this.Owner).med.name[av] = System.IO.Path.GetFileNameWithoutExtension(((MainWindow)this.Owner).med.name[av]);
                }
              }
            listView.Items.Clear();
            if (((MainWindow)this.Owner).med.name != null)
                for (int i = 0; i < ((MainWindow)this.Owner).med.name.Count(); i++)
                {
                    listView.Items.Add(((MainWindow)this.Owner).med.name[i]);
                    //listView.Items.Add(System.IO.Path.GetFileNameWithoutExtension(((MainWindow)this.Owner).med.chemin[i]));
                }
        }


        private void Change_media(object sender, MouseButtonEventArgs e)
        {
            int tmp = 0;
            if (((MainWindow)this.Owner).med.name == null)
                return;
            if (listView.SelectedIndex < 0)
                return;
            while (((MainWindow)this.Owner).med.name[tmp] != listView.SelectedItem.ToString())
                tmp++;
            ((MainWindow)this.Owner).mediaElement.Source = new Uri(((MainWindow)this.Owner).med.chemin[tmp]);
            ((MainWindow)this.Owner).play();
            ((MainWindow)this.Owner).MainWindow1.Title = System.IO.Path.GetFileNameWithoutExtension(((MainWindow)this.Owner).med.chemin[tmp]);
            ((MainWindow)this.Owner).i = tmp;
        }

        private void Button_save(object sender, RoutedEventArgs e)
        {
            XmlSerializer xs = new XmlSerializer(typeof(MyMedia));
            using (StreamWriter wr = new StreamWriter("Playlist.xml"))
            {
                xs.Serialize(wr, ((MainWindow)this.Owner).med);
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            XmlSerializer xs = new XmlSerializer(typeof(MyMedia));
            if (File.Exists("playlist.xml") == false)
                return ;
            using (StreamReader rd = new StreamReader("playlist.xml"))
            {
                MyMedia med = xs.Deserialize(rd) as MyMedia;
                listView.Items.Clear();
                for (int i = 0; i < med.name.Count(); i++)
                {
                    listView.Items.Add(med.name[i]);
                }
                ((MainWindow)this.Owner).med = med;
            }
        }

        private void button_delete(object sender, RoutedEventArgs e)
        {
            int tmp = 0;
            if (((MainWindow)this.Owner).med.name.Count() == 0)
                return;
            /*while (((MainWindow)this.Owner).med.name[tmp] != ((MainWindow)this.Owner).MainWindow1.Title)
                tmp++;*/
            listView.Items.Clear();
            if ((((MainWindow)this.Owner).play_state) == MainWindow.state.stop)
            {
                ((MainWindow)this.Owner).i = 0;
                ((MainWindow)this.Owner).med.name.Clear();
                ((MainWindow)this.Owner).med.chemin.Clear();
            }
            else
            {
                string tempo = ((MainWindow)this.Owner).med.chemin[tmp];
                ((MainWindow)this.Owner).med.name.Clear();
                ((MainWindow)this.Owner).med.chemin.Clear();
                ((MainWindow)this.Owner).med.name.Add(((MainWindow)this.Owner).MainWindow1.Title);
                ((MainWindow)this.Owner).med.chemin.Add(tempo);
                ((MainWindow)this.Owner).i = 0;
            }
        }

        public void select_box()
        {
            if (comboBox.SelectedIndex == 0)
            {
                bibli_music();
            }
            else if (comboBox.SelectedIndex == 1)
            {
                bibli_video();
            }
            else
                bibli_picture();
        }

        public void bibli_music()
        {
            string path = (@"C:\Users\" + Environment.UserName + @"\Music");

            FilePaths = Directory.GetFiles(path, "*.mp3", SearchOption.AllDirectories);
            FilePaths2 = Directory.GetFiles(path, "*.flac", SearchOption.AllDirectories);
            FilePaths3 = Directory.GetFiles(path, "*.wav", SearchOption.AllDirectories);
            FilePaths4 = Directory.GetFiles(path, "*.ogg", SearchOption.AllDirectories);

            string[] z;
            z = new string[FilePaths.Count() + FilePaths2.Count()];
            FilePaths.CopyTo(z, 0);
            FilePaths2.CopyTo(z, FilePaths.Count());
            FilePaths = z;

            z = new string[FilePaths.Count() + FilePaths3.Count()];
            FilePaths.CopyTo(z, 0);
            FilePaths3.CopyTo(z, FilePaths.Count());
            FilePaths = z;

            z = new string[FilePaths.Count() + FilePaths4.Count()];
            FilePaths.CopyTo(z, 0);
            FilePaths4.CopyTo(z, FilePaths.Count());
            FilePaths = z;

            TagLib.File[] f;
            f = new TagLib.File[FilePaths.Count()];
            for (int i = 0; i < FilePaths.Count(); i++)
            {
                f[i] = TagLib.File.Create(FilePaths[i]);
            }
            var gridview = new GridView();
            this.bibView.View = gridview;
            gridview.Columns.Add(new GridViewColumn
            {
                Header = "Titre",
                DisplayMemberBinding = new Binding("Title")
            });
            gridview.Columns.Add(new GridViewColumn
            {
                Header = "Artiste",
                DisplayMemberBinding = new Binding("Artiste")
            });
            gridview.Columns.Add(new GridViewColumn
            {
                Header = "Album",
                DisplayMemberBinding = new Binding("Album")
            });
            bibView.Items.Clear();
            for (int i = 0; i < FilePaths.Count(); i++)
            {
                if (f[i].Tag.Title == null)
                    bibView.Items.Add(new Song() { Title = System.IO.Path.GetFileNameWithoutExtension(FilePaths[i]), Artiste = f[i].Tag.FirstAlbumArtist, Album = f[i].Tag.Album });
                else
                    bibView.Items.Add(new Song() { Title = f[i].Tag.Title, Artiste = f[i].Tag.FirstAlbumArtist, Album = f[i].Tag.Album });
            }

            //

        }

        public void bibli_video()
        {
            string path = (@"C:\Users\" + Environment.UserName + @"\Videos");

            FilePaths = Directory.GetFiles(path, "*.mp4", SearchOption.AllDirectories);
            FilePaths2 = Directory.GetFiles(path, "*.mkv", SearchOption.AllDirectories);
            FilePaths3 = Directory.GetFiles(path, "*.avi", SearchOption.AllDirectories);
            FilePaths4 = Directory.GetFiles(path, "*.m4v", SearchOption.AllDirectories);
            string[] z;
            z = new string[FilePaths.Count() + FilePaths2.Count()];
            FilePaths.CopyTo(z, 0);
            FilePaths2.CopyTo(z, FilePaths.Count());
            FilePaths = z;

            z = new string[FilePaths.Count() + FilePaths3.Count()];
            FilePaths.CopyTo(z, 0);
            FilePaths3.CopyTo(z, FilePaths.Count());
            FilePaths = z;

            z = new string[FilePaths.Count() + FilePaths4.Count()];
            FilePaths.CopyTo(z, 0);
            FilePaths4.CopyTo(z, FilePaths.Count());
            FilePaths = z;

            TagLib.File[] f;
            f = new TagLib.File[FilePaths.Count()];
            for (int i = 0; i < FilePaths.Count(); i++)
            {
                f[i] = TagLib.File.Create(FilePaths[i]);
            }
            var gridview = new GridView();
            this.bibView.View = gridview;
            gridview.Columns.Add(new GridViewColumn
            {
                Header = "Titre",
                DisplayMemberBinding = new Binding("Title")
            });
            gridview.Columns.Add(new GridViewColumn
            {
                Header = "Durée",
                DisplayMemberBinding = new Binding("Duree")
            });
            bibView.Items.Clear();
            for (int i = 0; i < FilePaths.Count(); i++)
            {
                if (f[i].Tag.Title == null)
                    bibView.Items.Add(new Video() { Title = System.IO.Path.GetFileNameWithoutExtension(FilePaths[i]), Duree = f[i].Properties.Duration.ToString("g") });
                else
                    bibView.Items.Add(new Video() { Title = f[i].Tag.Title, Duree = f[i].Properties.Duration.ToString("g") });
            }
        }

        public void bibli_picture()
        {
            string path = (@"C:\Users\" + Environment.UserName + @"\Pictures");

            FilePaths = Directory.GetFiles(path, "*.jpg", SearchOption.AllDirectories);
            FilePaths2 = Directory.GetFiles(path, "*.png", SearchOption.AllDirectories);
            FilePaths3 = Directory.GetFiles(path, "*.gif", SearchOption.AllDirectories);
            FilePaths4 = Directory.GetFiles(path, "*.bmp", SearchOption.AllDirectories);
            string[] z;
            z = new string[FilePaths.Count() + FilePaths2.Count()];
            FilePaths.CopyTo(z, 0);
            FilePaths2.CopyTo(z, FilePaths.Count());
            FilePaths = z;

            z = new string[FilePaths.Count() + FilePaths3.Count()];
            FilePaths.CopyTo(z, 0);
            FilePaths3.CopyTo(z, FilePaths.Count());
            FilePaths = z;

            z = new string[FilePaths.Count() + FilePaths4.Count()];
            FilePaths.CopyTo(z, 0);
            FilePaths4.CopyTo(z, FilePaths.Count());
            FilePaths = z;

            TagLib.File[] f;
            f = new TagLib.File[FilePaths.Count()];
            for (int i = 0; i < FilePaths.Count(); i++)
            {
                f[i] = TagLib.File.Create(FilePaths[i]);
            }
            var gridview = new GridView();
            this.bibView.View = gridview;
            gridview.Columns.Add(new GridViewColumn
            {
                Header = "Titre",
                DisplayMemberBinding = new Binding("Title")
            });
            bibView.Items.Clear();
            for (int i = 0; i < FilePaths.Count(); i++)
            {
                bibView.Items.Add(new Video() { Title = System.IO.Path.GetFileNameWithoutExtension(FilePaths[i]) });
            }
        }

        private void Change_box(object sender, SelectionChangedEventArgs e)
        {
            bibView.Items.Clear();
            select_box();
        }

        private void Select_media(object sender, MouseButtonEventArgs e)
        {
            int tmp = bibView.SelectedIndex;
            if (tmp < 0)
                return;
            ((MainWindow)this.Owner).med.chemin.Add(FilePaths[tmp]);
            ((MainWindow)this.Owner).med.name.Add(System.IO.Path.GetFileNameWithoutExtension(FilePaths[tmp]));
            listView.Items.Clear();
            for (int i = 0; i < ((MainWindow)this.Owner).med.chemin.Count(); ++i)
            {
                listView.Items.Add((((MainWindow)this.Owner).med.name[i]));
            }

        }

        private void Search_button(object sender, TextChangedEventArgs e)
        {
            TextBox objtext = (TextBox)sender;
            string txt = objtext.Text;
            bibView.Items.Clear();
            TagLib.File[] f;
            f = new TagLib.File[FilePaths.Count()];
            for (int i = 0; i < FilePaths.Count(); i++)
            {
                f[i] = TagLib.File.Create(FilePaths[i]);
            }
            var gridview = new GridView();
            this.bibView.View = gridview;
            if (comboBox.SelectedIndex == 0)
            {
                gridview.Columns.Add(new GridViewColumn
                {
                    Header = "Titre",
                    DisplayMemberBinding = new Binding("Title")
                });
                gridview.Columns.Add(new GridViewColumn
                {
                    Header = "Artiste",
                    DisplayMemberBinding = new Binding("Artiste")
                });
                gridview.Columns.Add(new GridViewColumn
                {
                    Header = "Album",
                    DisplayMemberBinding = new Binding("Album")
                });
                if (this.Owner != null)
                {
                    for (int i = 0; i < FilePaths.Count(); ++i)
                    {
                        if (System.IO.Path.GetFileNameWithoutExtension(FilePaths[i]).Contains(txt) || txt.Equals(""))
                            bibView.Items.Add(new Song() { Title = System.IO.Path.GetFileNameWithoutExtension(FilePaths[i]), Artiste = f[i].Tag.FirstAlbumArtist, Album = f[i].Tag.Album });

                    }

                }
            }
            else if (comboBox.SelectedIndex == 1)
            {
                gridview.Columns.Add(new GridViewColumn
                {
                    Header = "Titre",
                    DisplayMemberBinding = new Binding("Title")
                });
                gridview.Columns.Add(new GridViewColumn
                {
                    Header = "Durée",
                    DisplayMemberBinding = new Binding("Duree")
                });
                if (this.Owner != null)
                {
                    for (int i = 0; i < FilePaths.Count(); ++i)
                    {
                        if (System.IO.Path.GetFileNameWithoutExtension(FilePaths[i]).Contains(txt) || txt.Equals(""))
                            bibView.Items.Add(new Video() { Title = System.IO.Path.GetFileNameWithoutExtension(FilePaths[i]), Duree = f[i].Properties.Duration.ToString("g") });
                    }

                }
            }
            else
            {
                gridview.Columns.Add(new GridViewColumn
                {
                    Header = "Titre",
                    DisplayMemberBinding = new Binding("Title")
                });
                if (this.Owner != null)
                {
                    for (int i = 0; i < FilePaths.Count(); ++i)
                    {
                        if (System.IO.Path.GetFileNameWithoutExtension(FilePaths[i]).Contains(txt) || txt.Equals(""))
                            bibView.Items.Add(new Video() { Title = System.IO.Path.GetFileNameWithoutExtension(FilePaths[i]) });

                    }

                }
            }

            if (txt.Equals(""))
                select_box();

        }
    }
}
