   M S B A M L   `   `   `  ���� F  BBatchRename, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:BatchRenameBatchRename  )clr-namespace:BatchRenameBatchRename  U QSystem.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3aG4clr-namespace:System.Drawing;assembly=System.DrawingSystem.Drawing     BatchRename.MainWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     +localclr-namespace:BatchRename     -controlclr-namespace:BatchRename     Hdrawing4clr-namespace:System.Drawing;assembly=System.Drawing     -     �Name$  BatchRenameWindow�� � Title$ Batch Rename Application��$	��510��$
��1010��
�������.��� -   $��MainGrid��.+�]� .+�`� $��40��$��0,0,0,5q�$	��Top=�.�c� .@��� -   $��RefreshButton��.����  �� Orientation$ 
Horizontal=�.+��� $��/Images/Refresh.ico��$��30��$��30��$��5,0,5,0q� �� BitmapScalingMode$ HighQuality=��� $��Refresh��$��Center=��� -   $��
OpenButton��.���� $ 
Horizontal=�.+��� $��/Images/Open.png��$��30��$��30��$��5,0,5,0q�$ HighQuality=��� $
��Open��$��Center=��� -   $��
SaveButton��.���� $ 
Horizontal=�.+��� $��/Images/Save.jpg��$��30��$��30��$��5,0,5,0q�$ HighQuality=��� $
��Save��$��Center=��� -   $��ArgumentTabControl��$	��250��$��0,0,40,0q�$
��Left=�
������$	��429��$	��Top=�.@��� ���� $ 
Horizontal=�.+��� $��/Images/Rules.png��$��40��$��40��$��0,0,5,0q�$ HighQuality=��� $��Rules��$��Center=�.��]� .+�� -   $��UserControlGrid��$	��160��$��Bottom=�$��0,10,0,0q��� -   $��RulesListView��$	��Top=� � 	Resources  �� 
TargetType �� Triggers P� Property P� Value	 �� Property
 �� Value��%�   '��       �� " �� P�  �@J�IsKeyboardFocusWithin . . ��� 	 �@��
IsSelected
 . P�  �@��
IsSelected . . ��� 	 � ��$

 Bold"��� 	 � ��

 �  ���� 	 � ��

 �   ����� .R��� -	   $	��230��$
��Left=�
����� �.���� $ 
Horizontal=�.+��� -
   $��20��$��0,0,10,0q��� $��Center=����� *Description+&    BatchRename.NameToTitleConverter   Instance �� 	Converter# Z �� ���� $ 
Horizontal=�.+��� $��/Images/Choice.png��$��30��$��30��$��0,0,5,0q�$ HighQuality=��� $��Choiced��$��Center=�.��]� .+��� -   $��AddListListView��$	��Top=� � ContextMenu �� .@�m� -   $��Remove������ .R��� ���� *Description+�� -   $��ResultTabControl��$��Right=�
�������.@��� ���� $ 
Horizontal=�.+��� $��/Images/File.ico��$��40��$��40��$��0,0,5,0q�$ HighQuality=��� $��Files��$��Center=�.��]� .+��� $ 
Horizontal=�$��Right=�$��Bottom=�$��Bottom=�$��0,10,0,0q�.+��� -   $	��120��$��40��$��0,0,10,0q�$��Right=�.���� $ 
Horizontal=�.+��� $��/Images/Add.png��$��30��$��30��$��0,0,5,0q�$ HighQuality=��� $	��Add��$��Center=��� -   $	��120��$��40��$��0,0,10,0q�$��Right=�.���� $��Preview��$��Center=��� -   $	��120��$��40��$��Right=�.���� $ 
Horizontal=�.+��� $��/Images/Batch.jpg��$��30��$��30��$��0,0,5,0q�$ HighQuality=��� $��Batch��$��Center=��� -   $��FileListView��$	��Top=� �� .@�m� -   $��Remove�� �� View �� .P��� $��Filename�� �� Width$	 120�� �� DisplayMemberBinding �� *Value+�� $��New filename��$	 150�� �� *NewFilename+�� $
��Path��$	 150�� �� *Path+�� $��Batch State��$	 150�� �� *
BatchState+�� ���� $ 
Horizontal=�.+��� $��/Images/Folder.ico��$��40��$��40��$��0,0,5,0q�$ HighQuality=��� $��Folders��$��Center=�.��]� .+��� $ 
Horizontal=�$��Right=�$��Bottom=�$��Bottom=�$��0,10,0,0q�.+��� -   $	��120��$��40��$��0,0,10,0q�$��Right=�.���� $ 
Horizontal=�.+��� $��/Images/Add.png��$��30��$��30��$��0,0,5,0q�$ HighQuality=��� $	��Add��$��Center=��� -   $	��120��$��40��$��0,0,10,0q�$��Right=�.���� $ 
Horizontal=�.+��� $��30��$��30��$��0,0,5,0q�$ HighQuality=����� *Images.PreviewIcon[0].Value+�� $��Preview��$��Center=��� -   $	��120��$��40��$��Right=�.���� $ 
Horizontal=�.+��� $��/Images/Batch.jpg��$��30��$��30��$��0,0,5,0q�$ HighQuality=��� $��Batch��$��Center=��� -   $��FolderListView��$	��Top=� �� .@�m� -   $��Remove�� �� .P��� $��
Foldername��$	 120�� �� *Value+�� $��New foldername��$	 150�� �� *NewFoldername+�� $
��Path��$	 150�� �� *Path+�� $��Batch State��$	 150�� �� *
BatchState+