   M S B A M L   `   `   `  ����  MyWindowsMediaPlayer,    &MyWindowsMediaPlayer.View.PlaylistView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      $	��200��5      .��5      � $��0,0,5,0q�6   
�������  J� Opacity$	  0.8Q�6/    � RowDefinitions 5   
   �� 5	      $
��Auto �6   �� 5
      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� $��Center=�6   $��10,0,0,0q�62   
���   �	PLAYLISTS6b   �� 5   
   -   5         ��Name$ plName��5      $��5,20,5,0q�6D   $��20��6V   ��6#   �� *PlaylistSetName+.��6b   ��5      �� 5      .��5       ��5      �� 5      .��5      5      �� 5   
   $��1��6   $ �� IsSynchronizedWithCurrentItem$
 TrueY�5      $��5,0,5,0q�6;   ��5      �� *playlists_names+ �� SelectedItem 5      �� *getSelectedPlaylist+��5      �� 5      ��5      �� 5      5!      A� 5"   
   $��3��6    A� Columns$ 2��6#   $��50��6/   .+�5#      �� $	��Add��6   ���	#FF707070��6#   �� *AddPlaylistCommand+6J   �� 5$      $��Remove��6   ���	#FF707070��6&   �� *RemovePlaylistCommand+6P   5%      5&      5'      