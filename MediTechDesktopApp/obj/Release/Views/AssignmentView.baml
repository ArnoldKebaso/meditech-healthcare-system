   M S B A M L   `   `   `  ����   MediTechDesktopApp-    'MediTechDesktopApp.Views.AssignmentView   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     -   .��� $��10q�  � ColumnDefinitions  �� $��2* ��� $��3* �.+��� $��0��$��0,0,10,0q�.+���  �� Orientation$ 
Horizontal=�$	��0,5q�.+��� $��Patient:��$	��100��$��Center=��� -    ��Name$ 	cbPatient��$	��200��{�.   �� DisplayMemberPath$ FullName�� �� SelectedValuePath$ 	PatientId���� $ 
Horizontal=�$	��0,5q�.+��� $��
Treatment:��$	��100��$��Center=��� -   $ cbTreatment��$	��200��{�.  $
 Name��$ TreatmentId���� $ 
Horizontal=�$	��0,5q�.+��� $��Doctor:��$	��100��$��Center=��� -   $ cbDoctor��$	��200��{�.  $ FullName��$ DoctorId���� $ 
Horizontal=�$	��0,5q�.+��� $��Nurse:��$	��100��$��Center=��� -   $ cbNurse��$	��200��{�.  $ FullName��$ NurseId���� $ 
Horizontal=�$	��0,5q�.+��� $��Date:��$	��100��$��Center=�(  "System.Windows.Controls.DatePicker  -     Name$ dpDate��$	��200��{�.  �� $ Vertical=�$	��0,5q�.+��� $��Notes:��$	��100��$��Center=��� -    ��Name$ txtNotes��$	��300��$��80�� �� TextWrapping$
 Wrap=� �� AcceptsReturn . {�.  �� $ 
Horizontal=�$
��0,15q�$
��Left=�.+��� -   	 ��Name$	 btnNew��$	��New��$��60��$��0,0,5,0q��� -	   $	 btnSave��$
��Save��$��60��$��0,0,5,0q�{�.  �� -
   $	 btnEdit��$
��Edit��$��60��$��0,0,5,0q�{�.  �� -   $	 	btnDelete��$��Delete��$��60��$��0,0,5,0q�{�.  �� -   $	 
btnRefresh��$��Refresh��$��60��&   System.Windows.Controls.DataGrid  -   
  Name$
 dgAssignments��$��1��   AutoGenerateColumns .     
IsReadOnly .    CanUserAddRows .     CanUserDeleteRows .     SelectionMode$ Single=�$��0,0,0,10q�   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ Patient��   Width5  /System.Windows.Controls.DataGridLengthConverter$ 2* 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *PatientFullName+  $ 	Treatment��$ 2*  �� *TreatmentName+  $
 Date��$ *  �� *AssignmentDate+ �� StringFormat$ {0:yyyy-MM-dd}��  $ Doctor��$ 2*  �� *DoctorFullName+  $ Nurse��$ 2*  �� *NurseFullName+  $ Notes��$ 3*  �� *Notes+