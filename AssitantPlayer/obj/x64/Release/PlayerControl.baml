   M S B A M L   `   `   `  ���� a  ]GongSolutions.WPF.DragDrop, Version=2.0.0.0, Culture=neutral, PublicKeyToken=91f1945125b7a587kLclr-namespace:GongSolutions.Wpf.DragDrop;assembly=GongSolutions.Wpf.DragDropGongSolutions.Wpf.DragDrop  K GYoutubePlayerLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullU<clr-namespace:YoutubePlayerLib.Cef;assembly=YoutubePlayerLibYoutubePlayerLib.Cef  AssitantPlayer"   AssitantPlayer.PlayerControl   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     V RMaterialDesignThemes.Wpf, Version=2.5.1.1284, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  Bi:http://schemas.microsoft.com/expression/2010/interactivity  UddLclr-namespace:GongSolutions.Wpf.DragDrop;assembly=GongSolutions.Wpf.DragDrop  Fcef<clr-namespace:YoutubePlayerLib.Cef;assembly=YoutubePlayerLib    � 	Resources  3  -System.Windows.Media.Effects.DropShadowEffect   ButtonEffect   Color   RenderingBias   
BlurRadius   	Direction  PlayingIcon �� 
TargetType'  !MaterialDesignThemes.Wpf.PackIcon�  �ControlzEx.PackIconBase`1[[MaterialDesignThemes.Wpf.PackIconKind, MaterialDesignThemes.Wpf, Version=2.5.1.1284, Culture=neutral, PublicKeyToken=null]] �� Property �� Value �� Triggers	 �� Binding
 �� ElementName �� Value  StreamerSongTemplate
 � Tag � ColumnDefinitions �Name 0 ,MaterialDesignFloatingActionMiniAccentButton �� BasedOn !� Visual K� ZIndex  MaterialDesignShadowBrush  OrangeBrush �� Path � ToolTip   Kind   PrimaryHueMidForegroundBrush  ChatSongTemplate � ContextMenu � Cursor �� RelativeSource �� AncestorType  MaterialDesignMenuItem �� Orientation��%G  &	        &	 4     &	 i    7  7   7  7  7  &	 R    7  7   7  7  7  7  7    $ 	#FFD67619��$ Quality=�$ 40Q�$ 0Q��� "  .���  �@ Kind$
 Play=� �� 	 �� *PlayerState+$
 Player�� playing.a���  �@ Kind$ Pause=��� 	 �� *PlayerState+$
 Player��
 paused.a���  �@ Kind$
 Play=��� 	 �� *PlayerState+$
 Player�� 	unstarted.a���  �@ Kind$
 Play=��� .R�� -   ���	#02FF0000$��0,0,0,0q�$��Stretch=� �� *Index+ �� $ 	SongIndex��$��18 ��� $ 
PlayButton��$��65 ��� $ SongName��$��* ��� $ RightSideBt1��$��40 ��� $ RightSidebt2��$��43 �.+��� $��0��$��Center=�$��5,0,0,0q�$��Center=����� *Index+�� -   $��1��$��Center=�$��10,5,0,5q� �� *Index+  ���� '��       " ��8   .���  � �� Z�  �� 	 �� *
IsSelected+ true.a���  � �� �	#70702222��  �@J�Effect8  ��!�  � .+�� $ 2��#���    ���� '�       " �.���  � y�$ 	Collapsed=� �� 	 �� *
IsSelected+ true.a���  � y�$ Visible=��� 	 �� *
IsSelected+ true.a���  � y�$ Visible=��� $ 1������ *Image+.��  8�� $��Center=�$��20��$��20��  ���� '        "  .���  �@ Kind$
 Play=���  � y�$ 	Collapsed=� �� 	 �� *
IsSelected+ true.a���  � y�$ Visible=��� 	 �� $
 Player��$ PlayerState� playing.a���  �@ Kind$ Pause=��� $��2��$
��15,0q�$��Center=�$
��Left=�$��CharacterEllipsis=� �� *Title+���� *Title+�� -   $��3��8�� $
��Left=�$��0,0,0,0q� �� *Index+��Z� ��Z� .��  $ Delete=�#���  $��Center=�$��20��$��20���� -   $��4��8�� $
��Left=�$��0,0,0,0q� �� *Index+��Z� ��Z� .��  $ ContentCopy=�#���  $��Center=�$��20��$��20���� .R�� -   ���	#02FF0000$��Stretch=� �� *Index+ �� $ 	SongIndex��$��18 ��� $ 
PlayButton��$��65 ��� $ SongName��$��* ��� $ RightSideBt1��$��40 ��� $ RightSidebt2��$��43 �.+��� $��0��$��Center=�$��5,0,0,0q�$��Center=����� *Index+�� -   $��1��$��Center=�$��10,5,0,5q� �� *Index+  ���� '��       " ��8   .���  � �� Z�  �� 	 �� *
IsSelected+ true.a���  � �� �	#70702222��  �@J�Effect8  ��!�  � .+�� $ 2��#���    ���� '�       " �.���  � y�$ 	Collapsed=� �� 	 �� *
IsSelected+ true.a���  � y�$ Visible=��� 	 �� *
IsSelected+ true.a���  � y�$ Visible=��� $ 1������ *Image+.��  8�� $��Center=�$��20��$��20��  ���� '        "  .���  �@ Kind$
 Play=���  � y�$ 	Collapsed=� �� 	 �� *
IsSelected+ true.a���  � y�$ Visible=��� 	 �� $
 Player��$ PlayerState� playing.a���  �@ Kind$ Pause=��� $��2��$
��15,0q�$��Center=�$
��Left=�$��CharacterEllipsis=� �� *Title+���� *Title+�� -   $��3��8�� $
��Left=�$��0,0,0,0q� �� *Index+��Z� ��Z� .��  $
 Plus=�#���  $��Center=�$��20��$��20���� -   8�� $��4��$��0,0,0,0q� �� *Index+��Z� ��Z�  �� .@�m� $
 Hand�� �� $ PlacementTarget.Tag� �� $ ContextMenuL����� " m�8  .�-	   ���� $��0,0,0,0q�$
 Hand��$��Center=�$ 
Horizontal=�.+��� .��Delete  $ Delete=�#���  $��Center=�$��20��$��20��m� $
 Hand�� �� $ PlacementTarget.Tag� �� $ ContextMenuL����� " m�8  .�-
   ���� $��0,0,0,0q�$
 Hand��$��Center=�$ 
Horizontal=�.+��� .��Link  $ ContentCopy=�$��13,0,0,0q�#���  $��Center=�$��20��$��20��m� $
 Hand�� �� *Index+���� $��0,0,0,0q�$
 Hand��$��Center=�$ 
Horizontal=�.+�  $ Account=�#���  $��Center=�$��20��$��20���� .��:�� ���� *Viewer+.��  $ DotsHorizontal=�#���  $��Center=�$��20��$��20��.���  	 MaterialDesignPaper#��� 	  �� $
��3.5* ��� $��6* �.+�� $��0�� � RowDefinitions �� $��3* ��� $��2* �.+�� $��0��.+��� $	��Top=�$��Center=�$��0,5,0,0q�$ 
Horizontal=�.+��� $��Center=�#��[ $
��Bold"�$��5,0,0,0q�$��16(�.��Streamer playlist�� $ Save playlist to file��#��[ $
��Left=�$��0,0,0,0q����� *#!SaveStreamerPlaylistInFileCommand+��Z� ��Z� .��  #��[ $ Download=�$��Center=�$��25��$��25���� $ Load playlist from file��#��[ $
��Left=�$��0,0,0,0q����� *%#LoadStreamerPlaylistFromFileCommand+��Z� ��Z� .��  #��[ $ Upload=�$��Center=�$��25��$��30���� -   $ Clear playlist��#��[ $
��Left=�$��0,0,0,0q���Z� ��Z� .��  #��[ $ PlaylistRemove=�$��Center=�$��25��$��30���� -   $ Add song to playlist��$��0,0,0,0q�#��[ $
��Left=�$��0,0,0,0q���Z� ��Z� .��  #��[ $
 Plus=�$��Center=�$��25��$��30��#  MaterialDesignThemes.Wpf.Card  $��Stretch=�$��0,40,0,0q�.���� )   #GongSolutions.Wpf.DragDrop.DragDrop   IsDragSource .    IsDropTarget . $	��100��#��[ $	��Top=�$��4,5,5,5q�
����İ� �� SelectedItem �� *LastStreamerSong+���� *StreamerPlaylist+   
 MaterialDesignListBoxItem  MaterialDesignSelection��%�   '��       7 
 �� " ��8   .���  � ��$ Stretch=���  � ��$ 0q� �� 	 �� *
IsSelected+ true.a���  � ��# �  � $��1��.+��� $	��Top=�$��Center=�$ 
Horizontal=�.+��� #��[ $��Center=�$
��Bold"�$��16(�.��Chat playlist�� -   $ Clear playlist��#��[ $
��Left=�$��0,0,0,0q���Z� ��Z� .��  #��[ $ PlaylistRemove=�$��Center=�$��25��$��30���� $ Player settings��#��[ $
��Left=�$��0,0,0,0q����� *ShowPlayerSettingsCommand+��Z� ��Z� .��  #��[ $ Settings=�$��Center=�$��25��$��25��  $��Stretch=�$��0,35,0,0q�.���� $	��100�� .  . #��[ $	��Top=�$��4,5,5,5q�
����İ� �� *LastChatSong+���� *ChatPlayList+  ��%�   '��       7 
 �� " ��8   .���  � ��$ Stretch=���  � ��$ 0q� �� 	 �� *
IsSelected+ true.a���  � ��# �  � $��1��.+�]� $��
15,15,5,15q�.+�� $��Bottom=�.+��� $��Center=�$ 
Horizontal=�$��Center=�$��
15,15,15,0q�.+��� #��[ $
��Left=����� *RepeatSongCommand+��Z� ��Z� .��  #��[ $ RotateRight=�$��Center=�$��30��$��30���� -    ��Name$ Anterior��#��[ $
��Left=�$��	25,0,50,0q����� *PlayPreviousSongCommand+��Z� ��Z� .��  #��[ $ ChevronLeft=�$��Center=�$��30��$��30���� #��[ ���	#00000000���	#70702222$��Center=����� *StartStopPlayerCommand+ J� Effect   $ 	#FFD67619��$ Quality=�$ 40Q�$ 0Q�.��  #��[ #���  $��Center=�$��30��$��30���� #��[ $��Right=�$��	50,0,25,0q����� *PlayNextSongCommand+��Z� ��Z� .��  #���  $ ChevronRight=�$��Center=�$��30��$��30���� #��[ $��90��$��Center=�  � Minimum$  0Q�! � Maximum$	! 100Q�$
 Hand��" � Value" �� *Volume+$
 Player��� $	��Top=�.+�  $��Stretch=�.��/  )YoutubePlayerLib.Cef.CefYoutubeController  -   $��Player��$��3q�#   AutoPlay# �� *
Autoplay+$   VideoId$ �� *CurrentSong.Id+)  #MaterialDesignThemes.Wpf.DialogHost  -   $��AddSongsDialog��$y�Visible=�#��[ $��2q�$��2��#��[ %   CloseOnClickAway% . &   DialogContent& � $��-2,-2,-2,-2q�#��[	 .+��� .+�� #��[ .+��� '   CloseDialogCommand#��Z' #��[ $��	10,5,10,5q�$��Right=�$	��Top=���Z� ��Z� .��  $
 Hand��# [  #��[ $ Close=�$��25��$��25���� $��	40,0,60,0q�   MaterialDesignDarkForeground#���  $
��Bold"�$��Center=�$��Center=�$��16(�!Enter Song id or youtube link !�� -   $��SongLingOrId�����	#FF939393#���  $��Center=�$
��Bold"�$��0,25,0,0q�$	��240���� -   $��	0,30,0,20q�$��Center=�.��Ok!  -   $!��ClearStreamerPlaylistDialog��$y�Visible=�#��[ $��2q�$��2��#��[ % . & � $��-2,-2,-2,-2q�#��[	 .+��� .+�� #��[ .+��� #��Z' #��[ $��	10,5,10,5q�$��Right=�$	��Top=���Z� ��Z� .��  $
 Hand��# [  #��[ $ Close=�$��25��$��25���� $��	40,0,60,0q�#���  $
��Bold"�$��Center=�$��Center=�$��16(�You want delete all songs ?�� $��30q�$��Center=�$ 
Horizontal=�.+��� -   $��Center=�.���� $��0,0,0,0q�$ 
Horizontal=�.+��� $��0,-2,0,0q�$��16(�#���  $
��Bold"�.��Yes  $��5,0,0,0q�$ Check=�$��26��$��26���� #��Z' $��15,0,0,0q�.���� $��0,0,0,0q�$ 
Horizontal=�.+��� $��0,-2,0,0q�$��16(�#���  $
��Bold"�.��No  -   $��ClearChatPlayListDialog��$y�Visible=�#��[ $��2q�$��2��#��[ % . & � $��-2,-2,-2,-2q�#��[	 .+��� .+�� #��[ .+��� #��Z' #��[ $��	10,5,10,5q�$��Right=�$	��Top=���Z� ��Z� .��  $
 Hand��# [  #��[ $ Close=�$��25��$��25���� $��	40,0,60,0q�#���  $
��Bold"�$��Center=�$��Center=�$��16(�You want delete all songs ?�� $��30q�$��Center=�$ 
Horizontal=�.+��� -   $��Center=�.���� $��0,0,0,0q�$ 
Horizontal=�.+��� $��0,-2,0,0q�$��16(�#���  $
��Bold"�.��Yes  $��5,0,0,0q�$ Check=�$��26��$��26���� #��Z' $��15,0,0,0q�.���� $��0,0,0,0q�$ 
Horizontal=�.+��� $��0,-2,0,0q�$��16(�#���  $
��Bold"�.��No