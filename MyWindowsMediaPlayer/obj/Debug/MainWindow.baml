   M S B A M L   `   `   `  ����O  KMyWindowsMediaPlayer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:MyWindowsMediaPlayer.ViewMyWindowsMediaPlayer.View  5   	   %    MyWindowsMediaPlayer.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   8View'clr-namespace:MyWindowsMediaPlayer.View     5   	     � Title$  MyWindowsMediaPlayer��5   	   $	��300��6&   $
��1000��63   $	��187��6@   $	��850��6P    � Icon$ ./Images/favicon.png��6_   .��5      � 
���   �.+�5   
   ]� .+�5      o� $	��Top=�6   
������� J� Opacity$	 0.8Q�6;   .@�5	      m� $
��Open��6   
���   �.@�5
      m� -   5        $
��File��5
      
���   �6@   m� 5      -   5        $��	Directory��5      
���   �6E   5      m� 5      $��Display��6   
���   �.@�5      m� -   5        $��	Playlists��5      
���   �6F   m� 5      -   5        $��Explorer��5      
���   �6E   m� 5      -   5        $��
Fullscreen��5      
���   �6G   5      5      �� 5      6   5      � 5   
    � ColumnDefinitions 5      �� 5      $
��auto �6#   �� 5      $��* �6#   �� 5      $
��auto �6#   5      ,   &MyWindowsMediaPlayer.View.PlaylistView  5   
   -   5          Name$ PlaylistView��5      $
��left=�62   $��1��6M   $��1��6]   $��0,23,0,5q�6k   �� 5   
   -   5        $��20��5      $��20��6   $
��Left=�6(   $	��Top=�6C   
������ $��
175,23,0,0q�6t   ��5      �� 5       ��5"      �� 5#      .��5%      �� $��/Images/close.png��6   5&      ,   &MyWindowsMediaPlayer.View.ExplorerView  5'   
   -   5          Name$ ExplorerView��5'      $��right=�62   $��3��6N   $��1��6^   $��0,23,0,5q�6l   �� 5(   
   -	   5        $��20��5(      $��20��6   $��Right=�6(   $	��Top=�6D   
������ $��0,23,0,0q�6u   ��5)      �� 5*      ��5,      �� 5-      .��5/      �� $��/Images/close.png��6   50      *   $MyWindowsMediaPlayer.View.ScreenView  51   
   -
   5          Name$ 
ScreenView��51      *   $MyWindowsMediaPlayer.View.ScreenView   MouseDoubleClick$ doubleClickEvent��6�   $��Stretch=�6.   $��2��6S   
���   �$��200,23,200,5q�6v   6�   +   %MyWindowsMediaPlayer.View.ControlView  52   
   -   5          Name$ ControlView��52      +   %MyWindowsMediaPlayer.View.ControlView	   
MouseEnter$	 mouseControlEventEnter��6�   
   
MouseLeave$
 mouseControlEventLeave��6�   $��Stretch=�60    � Cursor$
 Hand��6U   $	 0.8Q�6c   $	��top=�6q   $��200,23,200,0q�6�   6�   53      55      