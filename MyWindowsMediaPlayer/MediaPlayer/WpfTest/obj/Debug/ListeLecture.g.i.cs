﻿#pragma checksum "..\..\ListeLecture.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "52FA0C22CDB9537E9732C39DF0F9DAED"
//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using WpfTest;


namespace WpfTest {
    
    
    /// <summary>
    /// ListeLecture
    /// </summary>
    public partial class ListeLecture : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 9 "..\..\ListeLecture.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid page_liste;
        
        #line default
        #line hidden
        
        
        #line 10 "..\..\ListeLecture.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView listView;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\ListeLecture.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Button_Enregistrer;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\ListeLecture.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button button_load;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\ListeLecture.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button button_clean;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\ListeLecture.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox comboBox;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\ListeLecture.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBoxItem Musique;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\ListeLecture.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBoxItem Video;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\ListeLecture.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBoxItem Image;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\ListeLecture.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView bibView;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\ListeLecture.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox search;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/WpfTest;component/listelecture.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\ListeLecture.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 8 "..\..\ListeLecture.xaml"
            ((WpfTest.ListeLecture)(target)).Closed += new System.EventHandler(this.go_close);
            
            #line default
            #line hidden
            
            #line 8 "..\..\ListeLecture.xaml"
            ((WpfTest.ListeLecture)(target)).KeyDown += new System.Windows.Input.KeyEventHandler(this.Key_pressed);
            
            #line default
            #line hidden
            return;
            case 2:
            this.page_liste = ((System.Windows.Controls.Grid)(target));
            return;
            case 3:
            this.listView = ((System.Windows.Controls.ListView)(target));
            
            #line 10 "..\..\ListeLecture.xaml"
            this.listView.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.Change_media);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 17 "..\..\ListeLecture.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Add_Song);
            
            #line default
            #line hidden
            return;
            case 5:
            this.Button_Enregistrer = ((System.Windows.Controls.Button)(target));
            
            #line 18 "..\..\ListeLecture.xaml"
            this.Button_Enregistrer.Click += new System.Windows.RoutedEventHandler(this.Button_save);
            
            #line default
            #line hidden
            return;
            case 6:
            this.button_load = ((System.Windows.Controls.Button)(target));
            
            #line 19 "..\..\ListeLecture.xaml"
            this.button_load.Click += new System.Windows.RoutedEventHandler(this.button_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.button_clean = ((System.Windows.Controls.Button)(target));
            
            #line 20 "..\..\ListeLecture.xaml"
            this.button_clean.Click += new System.Windows.RoutedEventHandler(this.button_delete);
            
            #line default
            #line hidden
            return;
            case 8:
            this.comboBox = ((System.Windows.Controls.ComboBox)(target));
            
            #line 21 "..\..\ListeLecture.xaml"
            this.comboBox.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.Change_box);
            
            #line default
            #line hidden
            return;
            case 9:
            this.Musique = ((System.Windows.Controls.ComboBoxItem)(target));
            return;
            case 10:
            this.Video = ((System.Windows.Controls.ComboBoxItem)(target));
            return;
            case 11:
            this.Image = ((System.Windows.Controls.ComboBoxItem)(target));
            return;
            case 12:
            this.bibView = ((System.Windows.Controls.ListView)(target));
            
            #line 26 "..\..\ListeLecture.xaml"
            this.bibView.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.Select_media);
            
            #line default
            #line hidden
            return;
            case 13:
            this.search = ((System.Windows.Controls.TextBox)(target));
            
            #line 31 "..\..\ListeLecture.xaml"
            this.search.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.Search_button);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

