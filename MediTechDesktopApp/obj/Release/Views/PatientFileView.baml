   M S B A M L   `   `   `  ����   MediTechDesktopApp.    (MediTechDesktopApp.Views.PatientFileView   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     -   .��� $��10q�  � ColumnDefinitions  �� $��2* ��� $��3* �.+��� $��0��$��0,0,10,0q�.+���  �� Orientation$ 
Horizontal=�$	��0,5q�.+��� $��Patient:��$��80��$��Center=��� -    ��Name$ 
cbPatients��$	��200���� $ 
Horizontal=�$	��0,5q�.+��� $��
File Name:��$��80��$��Center=��� -    ��Name$ txtFileName��$	��200��{�.  �� $ 
Horizontal=�$	��0,5q�.+��� -    ��Name$ 	btnBrowse��$��	Browse…��$��80���� -    ��Name$ txtFilePath��$	��200��$��10,0,0,0q�$
��Wrap=��� $ 
Horizontal=�$
��0,15q�$
��Left=�.+��� -   $ btnNew��$	��New��$��60��$��0,0,5,0q��� -   $ btnSave��$
��Save��$��60��$��0,0,5,0q�{�.  �� -   $ 	btnDelete��$��Delete��$��60��$��0,0,5,0q�{�.  �� -	   $ 
btnRefresh��$��Refresh��$��60��&   System.Windows.Controls.DataGrid  -
     Name$ dgPatientFiles��$��1��   AutoGenerateColumns .     
IsReadOnly . 	   CanUserAddRows	 .  
   CanUserDeleteRows
 .     SelectionUnit$ FullRow=�   SelectionMode$ Single=�$��0,0,0,10q�   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ ID��   Width5  /System.Windows.Controls.DataGridLengthConverter$
 Auto 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *FileId+  $ Patient��$ 2*  �� *PatientName+  $ 	File Name��$ 3*  �� *
FileName+  $ 	File Type��$ 2*  �� *
FileType+  $ Size (bytes)��$ *  �� *FileSizeBytes+  $ Uploaded At��$ 2*  �� *
UploadedAt+ �� StringFormat$ {0:yyyy-MM-dd HH:mm}��