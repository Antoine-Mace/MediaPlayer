   M S B A M L   `   `   `  ����  MyWindowsMediaPlayer,    &MyWindowsMediaPlayer.View.ExplorerView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      $	��200��5      .��5      � $��5,0,0,0q�6   
�������  J� Opacity$	  0.8Q�6/    � RowDefinitions 5   
   �� 5	      $
��Auto �6   �� 5
      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� $��Center=�6   $	��Top=�61   
���   �$��20��6\   
EXPLORER6r   �� 5   
   $��20��6   $��20��6   $��Right=�6(   $	��Top=�6D   
������ ��5      �� 5      ��5      �� 5      .��5      �� $��/Images/close.png��6   5      �� 5   
   -   5         ��Name$ plName��5      $��	5,20,5,20q�6q   $��20��6�   ��6#   �� *
SearchText+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�.��6�   ��5      �� 5      .��5   "   ��5      �� 5      .��5   #   5      �� 5    
   -   5         ��Name$ filter��5       $��5,40,5,0q�6#   $	��Top=�65   $��20��6M    �� SelectedItem 6Y   �� *SelectedFilter+��5!      �� 5"      ��5$      �� 5%      .@�5'      �� $��Title��6   �� 5(      $��Category��6   �� 5)      $��Artist��6   5*      �� 5,   
   -   5         ��Name$ 	listMedia��5/      $��1��5,      $ �� IsSynchronizedWithCurrentItem$
 TrueY�5/      	 �� DisplayMemberPath$	 	toDisplay��67   
 �� SelectedValuePath$

 Path��6U   $��5,0,5,0q�6n   ��5-      �� *
Explorer+ �� SelectedValue 5.      �� *SelectedMedia+��50      �� 51      ��53      �� 54      56      A� 58   
   $��2��6    A� Columns$ 2��6#   $��50��6/   .+�59      �� $	��Add��6   ���	#FF707070��6#   �� *
AddCommand+6B   �� 5:      $��Remove��6   ���	#FF707070��6&   �� *RemoveCommand+6H   5;      5<      5>      