   M S B A M L   `   `   `  ����  MediTechDesktopApp.    (MediTechDesktopApp.Views.PatientFileView   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      -   5        .��5
      � $��10q�6     � ColumnDefinitions  5   
   �� 5      $��2* �6   �� 5      $��3* �6   .+�5   
   �� $��0��6   $��0,0,10,0q�6%   .+�5      ��  �� Orientation$ 
Horizontal=�6   $	��0,5q�62   .+�5      �� $��Patient:��6   $��80��6+   $��Center=�66   �� 5      -   5         ��Name$ 
cbPatients��5      $	��200��6/   5      5      �� 5      $ 
Horizontal=�6   $	��0,5q�62   .+�5      �� $��
File Name:��6   $��80��6-   $��Center=�68   �� 5      -   5         ��Name$ txtFileName��5      $	��200��6/   {�.  6;   5      �� 5"      $ 
Horizontal=�6   $	��0,5q�62   .+�5#      �� -   5         ��Name$ 	btnBrowse��5#      $��	Browse…��6,   $��80��6>   6I   �� 5$      -   5         ��Name$ txtFilePath��5$      $	��200��61   $��10,0,0,0q�6=   $
��Wrap=�6O   5%      �� 5(      $ 
Horizontal=�6   $
��0,15q�62   $
��Left=�6@   .+�5)      �� -   5        $ btnNew��5)      $	��New��6-   $��60��6?   $��0,0,5,0q�6J   6[   �� 5*      -   5        $ btnSave��5*      $
��Save��6-   $��60��6?   $��0,0,5,0q�6J   {�.  6t   �� 5+      -   5        $ 	btnDelete��5+      $��Delete��6-   $��60��6?   $��0,0,5,0q�6J   {�.  6s   �� 5,      -	   5        $ 
btnRefresh��5,      $��Refresh��6-   $��60��6?   6V   5-      5.      &   System.Windows.Controls.DataGrid  51   
   -
   5          Name$ dgPatientFiles��51      $��1��52         AutoGenerateColumns .     
IsReadOnly . 	   CanUserAddRows	 .  
   CanUserDeleteRows
 .     SelectionUnit$ FullRow=�57         SelectionMode$ Single=�58      $��0,0,0,10q�5:         Columns 5;      0  *System.Windows.Controls.DataGridTextColumn  5<      ,  &System.Windows.Controls.DataGridColumn   Header$ ID��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$
 Auto 6[   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6:   �� *FileId+6[     5=      $ Patient��6%   $ 2* 6[    6:   �� *PatientName+6[     5>      $ 	File Name��6%   $ 3* 6[    6:   �� *
FileName+6[     5?      $ 	File Type��6%   $ 2* 6[    6:   �� *
FileType+6[     5@      $ Size (bytes)��6%   $ * 6]    6;   �� *FileSizeBytes+6]     5A      $ Uploaded At��6%   $ 2* 6~    6:   �� *
UploadedAt+ �� StringFormat$ {0:yyyy-MM-dd HH:mm}��6~   5C      5D      5E      