   M S B A M L   `   `   `  ����   MediTechDesktopApp.    (MediTechDesktopApp.Views.AppointmentView   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     -   .��� $��10q�  � ColumnDefinitions  �� $��2* ��� $��3* �.+��� $��0��$��0,0,10,0q�.+���  �� Orientation$ 
Horizontal=�$	��0,5q�.+��� $��Patient:��$	��100��$��Center=��� -    ��Name$ 
cbPatients��$	��200��{�.  �� $ 
Horizontal=�$	��0,5q�.+��� $��Doctor:��$	��100��$��Center=��� -   $ 	cbDoctors��$	��200��{�.  �� $ 
Horizontal=�$	��0,5q�.+��� $��Date:��$	��100��$��Center=�(  "System.Windows.Controls.DatePicker  -     Name$ dpAppointmentDate��$	��200��{�.  �� $ 
Horizontal=�$	��0,5q�.+��� $��Status:��$	��100��$��Center=��� -   $ cbStatus��$	��200��{�.  .@��� $��Pending���� $��	Confirmed���� $��	Completed���� $��Canceled���� $��Rescheduled���� $��No Show���� $ Vertical=�$	��0,5q�.+��� $��Notes:��$	��100��$��Center=��� -    ��Name$ txtNotes��$	��300��$��80�� �� TextWrapping$
 Wrap=� �� AcceptsReturn . {�.  �� $ 
Horizontal=�$
��0,15q�$
��Left=�.+��� -    ��Name$ btnNew��$	��New��$��60��$��0,0,5,0q��� -   $ btnSave��$
��Save��$��60��$��0,0,5,0q�{�.  �� -	   $ btnEdit��$
��Edit��$��60��$��0,0,5,0q�{�.  �� -
   $ 	btnDelete��$��Delete��$��60��$��0,0,5,0q�{�.  �� -   $ 
btnRefresh��$��Refresh��$��60��&   System.Windows.Controls.DataGrid  -     Name$ dgAppointments��$��1��	   AutoGenerateColumns	 .  
   
IsReadOnly
 .    SelectionMode$ Single=�   CanUserAddRows .     CanUserDeleteRows .     SelectionUnit$ FullRow=�$��0,0,0,10q�   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ ID��   Width5  /System.Windows.Controls.DataGridLengthConverter$
 Auto 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *AppointmentId+  $ Patient��$ 2*  �� *PatientName+  $ Doctor��$ 2*  �� *
DoctorName+  $
 Date��$ *  �� *AppointmentDate+ �� StringFormat$ {0:yyyy-MM-dd HH:mm}��  $ Status��$
 Auto  �� *Status+  $ Notes��$ 3*  �� *Notes+