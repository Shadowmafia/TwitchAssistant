   M S B A M L   `   `   `  ����  TwitchAssistant?    9TwitchAssistant.UserInterface.Views.CoinSystemControlView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      V RMaterialDesignThemes.Wpf, Version=2.5.1.1280, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      .��5	      � .+�5
   
   �� $��Center=�6   $��Center=�60   $��	0,-15,0,0q�6M   .+�5      �� $��Center=�6     �� Orientation$  
Horizontal=�66   $��0,0,0,0q�6O   .+�5      �� ���	#FF905CEE$
��Bold"�6/   $��16(�6A   .��6O   Coin system on :6a   �� 5      -   5        $��CoinSystemStateCheckBox��5   L    � Cursor$
 Hand��6>   $��Center=�6k   $��5,5,0,0q�6�    d� 	IsChecked 6   �� *CoinSystemOn+6�   5      �� 5      $��Center=�6   $  
Horizontal=�66   $��0,0,0,0q�6O   .+�5      �� ���	#FF905CEE$
��Bold"�6/   $��16(�6A   .��6O   Coins name :6]   �� 5      $��Center=�6   $	��100��6Y   $
 Hand��6e   $��Center=�6t       PrimaryHueMidForegroundBrush#���   6�   $��5,5,0,0q�6�   ��6>   �� *	CoinsName+6�   5      �� 5      $��Center=�6   ���	#FF905CEE$
��Bold"�6H   $��16(�6Z   $��0,5,0,0q�6h   .��6y   Coin system Mode6�   �� 5      $  
Horizontal=�6   $	��Top=�62   $��Center=�6J   .+�5      �� $��14(�6   .��6&   Twitch rangs64   d� 5      -   5        $��CoinSystemModeToggle��5   1   $��5,0,5,0q�6    $
 Hand��6M   6]   �� 5      $��14(�6   .��6&   Custom rangs64   5      � 5       � ColumnDefinitions 5      �� 5      $��1* �6'   64   �� 5      $��1* �6'   64   .+�5      � -   5        $��TwitchRangsMode��5      $��0��6.   $��5q�6>   .+�5      �� $	��130��6!   .+�5       ]� .+�5!      �� ���	#FF905CEE$
��Bold"�6;   .��6M   	Rang name6X   '  !MaterialDesignThemes.Wpf.PackIcon  5"      $��20��66   $��20��6A   �  �ControlzEx.PackIconBase`1[[MaterialDesignThemes.Wpf.PackIconKind, MaterialDesignThemes.Wpf, Version=2.5.1.1280, Culture=neutral, PublicKeyToken=null]]   Kind$
 Coin=�6M   $��Center=�6Y   $��21,3,0,0q�6v   $��Center=�6�      MaterialDesignDarkForeground#���  6�   6�   5#      �� 5$      $��Center=�6%   .+�5&      ]� .+�5'   "   �� $
��Left=�6)   .��6D   
Unfollow6N   �� 5(   "   -   5        $��
UnfollowTB��5(   h   $��Center=�6D   $��Right=�6z   $��35��6�   #���   6�   ��6*   �� *
Unfollow+6�   5)      ]� 5+      .+�5,   "   �� $
��Left=�6(   .��6C   
Follower6M   �� 5-   "   -   5        $��	FolloweTB��5-   h   $��Center=�6D   $��Right=�6y   $��35��6�   #���   6�   ��6*   �� *
Follower+6�   5.      ]� 50      .+�51   "   �� $
��Left=�6(   .��6C   
Subscriber6O   �� 52   "   -   5        $��SubscriberTB��52   j   $��Center=�6F   $��Right=�6~   $��35��6�   #���   6�   ��6*   �� *
Subscriber+6�   53      54      55      56      � 57      -   5        $��CustomRangsMode��57   )   {�.  $��1��6@   $��30,5,5,5q�6P   .+�58      �� $��Center=�6!   .+�59      ]� .+�5:      �� ���	#FF905CEE$
��Bold"�6;   .��6M   Select ranking Set6a   5;      �� 5<      6%   5=      5>      5?      �� 5@      $��0,5,0,0q�6   .+�5A      �� $��Center=�6   ���	#FF905CEE$
��Bold"�6L   $��16(�6^   .��6l   Accrual timer6{   �� 5B      $��Center=�6   $
��Bold"�65   $��14(�6G   ���#3eb247.��6j   Stream online6y   �� 5C      $  
Horizontal=�6   $��Center=�66   .+�5D      �� ���	#FF905CEE$
��Bold"�63   .��6E   Accural interval6W   �� 5E      -   5        $��AcOnlineMinets��5E   b   $��Center=�6>   $��35��6x   #���   6�   ��6   �� *OnlineInterval+6�   �� 5F      .��6   	minets.6%   5G      �� 5I      $��0,5,0,0q�6   $��Center=�6)   $
��Bold"�6F   $��14(�6X   ���#cf1717.��6{   Stream offline6�   �� 5J      $  
Horizontal=�6   $��Center=�66   .+�5K      �� ���	#FF905CEE$
��Bold"�63   .��6E   Accural interval6W   �� 5L      -	   5        $��AcOfflineMinets��5L   c   $��Center=�6?   $��35��6z   #���   6�   ��6   �� *OfflineInterval+6�   �� 5M      .��6   	minets.6%   5N      5O      5P      5Q      5R      