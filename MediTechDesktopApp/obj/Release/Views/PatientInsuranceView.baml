   M S B A M L   `   `   `  ����   MediTechDesktopApp3    -MediTechDesktopApp.Views.PatientInsuranceView   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     -   .��� $��10q�  � ColumnDefinitions  �� $��2* ��� $��3* �.+��� $��0��$��0,0,10,0q�.+���  �� Orientation$ 
Horizontal=�$	��0,5q�.+��� $��Patient:��$	��100��$��Center=��� -    ��Name$ 	cbPatient��$	��200�� �� DisplayMemberPath$
 Name�� �� SelectedValuePath$ Id���� $ 
Horizontal=�$	��0,5q�.+��� $��Policy:��$	��100��$��Center=��� -   $ cbPolicy��$	��200��$
 Name��$ Id���� $ 
Horizontal=�$
��0,15q�$
��Left=�.+��� -    ��Name$ btnNew��$	��New��$��60��$��0,0,5,0q��� -   $ btnSave��$
��Save��$��60��$��0,0,5,0q�{�.  �� -   $ 	btnDelete��$��Delete��$��60��$��0,0,5,0q�{�.  �� -   $ 
btnRefresh��$��Refresh��$��60��&   System.Windows.Controls.DataGrid  -     Name$ dgPatientInsurance��$��1��   AutoGenerateColumns .     
IsReadOnly . 	   CanUserAddRows	 .  
   CanUserDeleteRows
 .     SelectionUnit$ FullRow=�   SelectionMode$ Single=�$��0,0,0,10q�   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ Patient��   Width5  /System.Windows.Controls.DataGridLengthConverter$ 2* 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *PatientFullName+  $ Policy #��$ 2*  �� *PolicyNumber+  $ Coverage��$ 3*  �� *CoverageDetails+  $ 
Start Date��$
 Auto  �� *	StartDate+ �� StringFormat$ {0:yyyy-MM-dd}��  $ End Date��$
 Auto  �� *	EndDate+$ {0:yyyy-MM-dd}��