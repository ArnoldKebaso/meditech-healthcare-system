   M S B A M L   `   `   `  ����  MediTechDesktopApp0    *MediTechDesktopApp.Views.MedicalRecordView   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      -   5        .��5	      � $��10q�6     � ColumnDefinitions  5
   
   �� 5      $��2* �6   �� 5      $��3* �6   .+�5   
   �� $��0��6   $��0,0,10,0q�6%   .+�5      ��  �� Orientation$ 
Horizontal=�6   $	��0,5q�62   .+�5      �� $��Appointment:��6   $	��100��6/   $��Center=�6;   �� 5      -   5         ��Name$ cbAppointments��5      $	��200��63   {�.  6?   5      �� 5      $ Vertical=�6   $	��0,5q�60   .+�5      �� $��
Diagnosis:��6   $	��100��6-   $��Center=�69   �� 5      -   5         ��Name$ txtDiagnosis��5      $	��300��60   $��60��6<    �� TextWrapping$
 Wrap=�5       �� AcceptsReturn . {�.  6C   5      �� 5      $ Vertical=�6   $	��0,5q�60   .+�5       �� $��Visit Summary:��6   $	��100��61   $��Center=�6=   �� 5!      -   5        $ txtVisitSummary��5!      $	��300��63   $��60��6?   $
 Wrap=�5"       . {�.  6C   5#      �� 5%      $ Vertical=�6   $	��0,5q�60   .+�5&      �� $��Doctor Notes:��6   $	��100��60   $��Center=�6<   �� 5'      -   5        $ txtDoctorNotes��5'      $	��300��62   $��60��6>   $
 Wrap=�5(       . {�.  6C   5)      �� 5,      $ 
Horizontal=�6   $
��0,15q�62   $
��Left=�6@   .+�5-      �� -   5         ��Name$ btnNew��5-      $	��New��6-   $��60��6?   $��0,0,5,0q�6J   6[   �� 5.      -   5        $ btnSave��5.      $
��Save��6-   $��60��6?   $��0,0,5,0q�6J   {�.  6t   �� 5/      -   5        $ btnEdit��5/      $
��Edit��6-   $��60��6?   $��0,0,5,0q�6J   {�.  6t   �� 50      -	   5        $ 	btnDelete��50      $��Delete��6-   $��60��6?   $��0,0,5,0q�6J   {�.  6t   �� 51      -
   5        $ 
btnRefresh��51      $��Refresh��6-   $��60��6?   6X   52      53      &   System.Windows.Controls.DataGrid  56   
   -   5          Name$ dgMedicalRecords��56      $��1��57         AutoGenerateColumns .  	   
IsReadOnly	 . 
   CanUserAddRows
 .     CanUserDeleteRows .     SelectionUnit$ FullRow=�5<         SelectionMode$ Single=�5=      $��0,0,0,10q�5?         Columns 5@      0  *System.Windows.Controls.DataGridTextColumn  5A      ,  &System.Windows.Controls.DataGridColumn   Header$ 	Record ID��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$
 Auto 6^   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6;   �� *
RecordId+6^     5B      $ Appt # → Patient��6%   $ 2* 6f    6?   �� *DisplayAppointment+6f     5C      $ 	Diagnosis��6%   $ 2* 6^    6<   �� *	Diagnosis+6^     5D      $ Visit Summary��6%   $ 3* 6_    6<   �� *VisitSummary+6_     5E      $ Doctor Notes��6%   $ 3* 6_    6<   �� *DoctorNotes+6_     5F      $ 
Created At��6%   $ * 6    6<   �� *	CreatedAt+ �� StringFormat$ {0:yyyy-MM-dd HH:mm}��6   5H      5I      5J      