   M S B A M L   `   `   `  ����   TwitchAssistant?    9TwitchAssistant.UserInterface.Views.CoinSystemControlView   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     V RMaterialDesignThemes.Wpf, Version=2.5.1.1280, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  .��� .+��� $��Center=�$��Center=�$��	0,-15,0,0q�.+��� $��Center=�  �� Orientation$  
Horizontal=�$��0,0,0,0q�.+��� ���	#FF905CEE$
��Bold"�$��16(�.��Coin system on :�� -   $��CoinSystemStateCheckBox�� � Cursor$
 Hand��$��Center=�$��5,5,0,0q� d� 	IsChecked �� *CoinSystemOn+�� $��Center=�$  
Horizontal=�$��0,0,0,0q�.+��� ���	#FF905CEE$
��Bold"�$��16(�.��Coins name :�� $��Center=�$	��100��$
 Hand��$��Center=�    PrimaryHueMidForegroundBrush#���   $��5,5,0,0q����� *	CoinsName+�� $��Center=����	#FF905CEE$
��Bold"�$��16(�$��0,5,0,0q�.��Coin system Mode�� $  
Horizontal=�$	��Top=�$��Center=�.+��� $��14(�.��Twitch rangsd� -   $��CoinSystemModeToggle��$��5,0,5,0q�$
 Hand���� $��14(�.��Custom rangs�  � ColumnDefinitions �� $��1* ��� $��1* �.+�� -   $��TwitchRangsMode��$��0��$��5q�.+��� $	��130��.+�]� .+��� ���	#FF905CEE$
��Bold"�.��	Rang name'  !MaterialDesignThemes.Wpf.PackIcon  $��20��$��20���  �ControlzEx.PackIconBase`1[[MaterialDesignThemes.Wpf.PackIconKind, MaterialDesignThemes.Wpf, Version=2.5.1.1280, Culture=neutral, PublicKeyToken=null]]   Kind$
 Coin=�$��Center=�$��21,3,0,0q�$��Center=�   MaterialDesignDarkForeground#���  �� $��Center=�.+�]� .+��� $
��Left=�.��
Unfollow�� -   $��
UnfollowTB��$��Center=�$��Right=�$��35��#���   ���� *
Unfollow+]� .+��� $
��Left=�.��
Follower�� -   $��	FolloweTB��$��Center=�$��Right=�$��35��#���   ���� *
Follower+]� .+��� $
��Left=�.��
Subscriber�� -   $��SubscriberTB��$��Center=�$��Right=�$��35��#���   ���� *
Subscriber+� -   $��CustomRangsMode��{�.  $��1��$��30,5,5,5q�.+��� $��Center=�.+�]� .+��� ���	#FF905CEE$
��Bold"�.��Select ranking Set�� �� $��0,5,0,0q�.+��� $��Center=����	#FF905CEE$
��Bold"�$��16(�.��Accrual timer�� $��Center=�$
��Bold"�$��14(����#3eb247.��Stream online�� $  
Horizontal=�$��Center=�.+��� ���	#FF905CEE$
��Bold"�.��Accural interval�� -   $��AcOnlineMinets��$��Center=�$��35��#���   ���� *OnlineInterval+�� .��	minets.�� $��0,5,0,0q�$��Center=�$
��Bold"�$��14(����#cf1717.��Stream offline�� $  
Horizontal=�$��Center=�.+��� ���	#FF905CEE$
��Bold"�.��Accural interval�� -	   $��AcOfflineMinets��$��Center=�$��35��#���   ���� *OfflineInterval+�� .��	minets.