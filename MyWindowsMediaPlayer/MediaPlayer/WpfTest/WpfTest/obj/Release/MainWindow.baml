   M S B A M L   `   `   `  ���� B  >WpfTest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WpfTestWpfTest      WpfTest.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     'localclr-namespace:WpfTest     -     �Name$  MainWindow1�� � Title$ 
MainWindow��$	��480��$	��800��.��� $	��-10q����� .+�� $��25��$��
10,10,10,0q�$	��Top=�.+�]� .+�o� -    o�Name$
 menu��.@�m� $��_Média��$��25��$��50��.@�m� -   $��_Ouvrir���� m� -   $��_Quitter��m� $
��_Vue��$��25��$��50��.@�m� -   $��_Liste de lecture���� � -    �Name$ 
media_grid��$��	0,40,0,65q�.+��� 
���   �r� -    r�Name$ mediaElement��$��8,0,10,0q� r� LoadedBehavior$ Manual=� J� OpacityMask
 �   �� -   $ controle��$��51��$��
10,0,10,10q�$��Bottom=�.+��� 
�������._��� -	    ��Name$ button1��$��>��$
��Left=�$��170,20,0,10q�$
��30.4���� -
   $ stop_button��$
��Stop��$
��Left=�$��110,20,0,10q�$��50���� -   $ button��$��<��$
��Left=�$��
70,20,0,10q�$
��31.4���� -   $ play_button��$��Lecture��$
��Left=�$��50��$��
10,20,0,10q��� -   $��	lblVolume��$��20��$
��34.6��$��
0,21,88,10q�$��Right=�$��RightToLeft=� J� RenderTransformOrigin$ 0.523,-0.818&�%100�� -   	 ��Name$	 volume_slider��$��20��$��80��$��Center=�
 � Value$	
 0.5Q� � Minimum$ 0Q� � Maximum$ 1Q� �� TickFrequency$	 0.1Q�$��Right=�$��0,9,5,0q��� -   $��	lblStatus��$��15��$��60��$��
0,-1,9.6,0q�$��Right=�$	��Top=�$��RightToLeft=�
00:00:00�� -   $	 
pos_slider��$��20��$��10,0.5,53.2,0q�$	��Top=� �� IsMoveToPointEnabled . �� -    ��Name$ lblspeed��$��20��$
��34.6��$��0,21,207.6,10q�$��Right=�$��RightToLeft=�$ 0.523,-0.818&�x 1�� -   $	 speed_slider��$��80��$
 1Q�$
 0.25Q�$ 2Q�$
 0.25Q� �� IsSnapToTickEnabled . $��Right=�$��0,21,127.6,10q�