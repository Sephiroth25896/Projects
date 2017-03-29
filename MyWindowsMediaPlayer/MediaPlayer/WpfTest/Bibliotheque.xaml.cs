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
using System.Windows.Media.Imaging;


namespace WpfTest
{
    /// <summary>
    /// Logique d'interaction pour Bibliotheque.xaml
    /// </summary>
    public partial class Bibliotheque : Window
    {
        string[] FilePaths { get; set; }
        string[] FilePaths2 { get; set; }
        string[] FilePaths3 { get; set; }
        string[] FilePaths4 { get; set; }
        public Bibliotheque(MainWindow AppMainWindow)
        {
            InitializeComponent();
            comboBox.SelectedIndex = 0;
            this.Top = AppMainWindow.Top;
            this.Left = AppMainWindow.Left;

            this.Height = AppMainWindow.ActualHeight;
            this.Width = AppMainWindow.ActualWidth;
            this.WindowState = AppMainWindow.WindowState;

            select_box();

        }

        private void go_closes(object sender, EventArgs e)
        {
            ((MainWindow)this.Owner).Show();
        }

        public void select_box()
        {
            if (comboBox.SelectedIndex == 0)
            {
                bibli_music();
            }
            else
            {
                bibli_video();
            }
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
            gridview.Columns.Add(new GridViewColumn {
                Header = "Titre", DisplayMemberBinding = new Binding("Title") });
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
            for (int i = 0; i < FilePaths.Count(); i++)
            {
                bibView.Items.Add(new Song() { Title = f[i].Tag.Title, Artiste = f[i].Tag.FirstAlbumArtist, Album = f[i].Tag.Album });
                //listView.Items.Add(System.IO.Path.GetFileNameWithoutExtension(((MainWindow)this.Owner).med.chemin[i]));
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
            for (int i = 0; i < FilePaths.Count(); i++)
            {
                bibView.Items.Add(new Video() { Title = f[i].Tag.Title, Duree = f[i].Properties.Duration.ToString("g") });
                //listView.Items.Add(System.IO.Path.GetFileNameWithoutExtension(((MainWindow)this.Owner).med.chemin[i]));
            }
        }

        private void Change_box(object sender, SelectionChangedEventArgs e)
        {
            bibView.Items.Clear();
            select_box();
        }

        private void Select_media(object sender, MouseButtonEventArgs e)
        {
            int tmp  = bibView.SelectedIndex;
            ((MainWindow)this.Owner).med.chemin.Add(FilePaths[tmp]);
            ((MainWindow)this.Owner).med.name.Add(System.IO.Path.GetFileNameWithoutExtension(FilePaths[tmp]));
            /*for (int av = 0; av < ((MainWindow)this.Owner).med.chemin.Count(); av++)
            {
                ((MainWindow)this.Owner).med.name[av] = System.IO.Path.GetFileNameWithoutExtension(((MainWindow)this.Owner).med.name[av]);
            }*/
        }
    }
}
