   M S B A M L   `   `   `  ����  MediTechDesktopApp.    (MediTechDesktopApp.Views.AppointmentView   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      -   5        .��5	      � $��10q�6     � ColumnDefinitions  5
   
   �� 5      $��2* �6   �� 5      $��3* �6   .+�5   
   �� $��0��6   $��0,0,10,0q�6%   .+�5      ��  �� Orientation$ 
Horizontal=�6   $	��0,5q�62   .+�5      �� $��Patient:��6   $	��100��6+   $��Center=�67   �� 5      -   5         ��Name$ 
cbPatients��5      $	��200��6/   {�.  6;   5      �� 5      $ 
Horizontal=�6   $	��0,5q�62   .+�5      �� $��Doctor:��6   $	��100��6*   $��Center=�66   �� 5      -   5        $ 	cbDoctors��5      $	��200��6.   {�.  6:   5      �� 5      $ 
Horizontal=�6   $	��0,5q�62   .+�5      �� $��Date:��6   $	��100��6(   $��Center=�64   (  "System.Windows.Controls.DatePicker  5       -   5          Name$ dpAppointmentDate��5       $	��200��68   {�.  6D   5!      �� 5#      $ 
Horizontal=�6   $	��0,5q�62   .+�5$      �� $��Status:��6   $	��100��6*   $��Center=�66   �� 5%      -   5        $ cbStatus��5%      $	��200��6-   {�.  .@�5&      �� $��Pending��6#   �� 5'      $��	Confirmed��6#   �� 5(      $��	Completed��6#   �� 5)      $��Canceled��6#   �� 5*      $��Rescheduled��6#   �� 5+      $��No Show��6#   5,      5-      �� 5/      $ Vertical=�6   $	��0,5q�60   .+�50      �� $��Notes:��6   $	��100��6)   $��Center=�65   �� 51      -   5         ��Name$ txtNotes��51      $	��300��6,   $��80��68    �� TextWrapping$
 Wrap=�52       �� AcceptsReturn . {�.  6C   53      �� 56      $ 
Horizontal=�6   $
��0,15q�62   $
��Left=�6@   .+�57      �� -   5         ��Name$ btnNew��57      $	��New��6-   $��60��6?   $��0,0,5,0q�6J   6[   �� 58      -   5        $ btnSave��58      $
��Save��6-   $��60��6?   $��0,0,5,0q�6J   {�.  6t   �� 59      -	   5        $ btnEdit��59      $
��Edit��6-   $��60��6?   $��0,0,5,0q�6J   {�.  6t   �� 5:      -
   5        $ 	btnDelete��5:      $��Delete��6-   $��60��6?   $��0,0,5,0q�6J   {�.  6t   �� 5;      -   5        $ 
btnRefresh��5;      $��Refresh��6-   $��60��6?   6X   5<      5=      &   System.Windows.Controls.DataGrid  5@   
   -   5          Name$ dgAppointments��5@      $��1��5A      	   AutoGenerateColumns	 .  
   
IsReadOnly
 .    SelectionMode$ Single=�5D         CanUserAddRows .     CanUserDeleteRows .     SelectionUnit$ FullRow=�5G      $��0,0,0,10q�5I         Columns 5J      0  *System.Windows.Controls.DataGridTextColumn  5K      ,  &System.Windows.Controls.DataGridColumn   Header$ ID��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$
 Auto 6]   1  +System.Windows.Controls.DataGridBoundColumn   Binding 67   �� *AppointmentId+6]     5L      $ Patient��6%   $ 2* 6]    67   �� *PatientName+6]     5M      $ Doctor��6%   $ 2* 6]    67   �� *
DoctorName+6]     5N      $
 Date��6%   $ * 6�    67   �� *AppointmentDate+ �� StringFormat$ {0:yyyy-MM-dd HH:mm}��6�     5O      $ Status��6%   $
 Auto 6]    67   �� *Status+6]     5P      $ Notes��6%   $ 3* 6]    67   �� *Notes+6]   5R      5S      5T      