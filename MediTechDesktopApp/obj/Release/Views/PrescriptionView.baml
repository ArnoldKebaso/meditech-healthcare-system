   M S B A M L   `   `   `  ����   MediTechDesktopApp/    )MediTechDesktopApp.Views.PrescriptionView   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     -   .��� $��10q�  � ColumnDefinitions  �� $��2* ��� $��3* �.+��� $��0��$��0,0,10,0q�.+���  �� Orientation$ 
Horizontal=�$	��0,5q�.+��� $��Record:��$	��100��$��Center=��� -    ��Name$ 	cbRecords��$	��200��{�.  �� $ 
Horizontal=�$	��0,5q�.+��� $��Medication:��$	��100��$��Center=��� -    ��Name$ txtMedicationName��$	��200��{�.  �� $ 
Horizontal=�$	��0,5q�.+��� $��Dosage:��$	��100��$��Center=��� -   $ 	txtDosage��$	��200��{�.  �� $ 
Horizontal=�$	��0,5q�.+��� $��
Frequency:��$	��100��$��Center=��� -   $ cbFrequency��$	��200��{�.  �� $ 
Horizontal=�$	��0,5q�.+��� $��Start Date:��$	��100��$��Center=�(  "System.Windows.Controls.DatePicker  -     Name$ dpStartDate��$	��200��{�.  �� $ 
Horizontal=�$	��0,5q�.+��� $��	End Date:��$	��100��$��Center=�  -   $ 	dpEndDate��$	��200��{�.  �� $ Vertical=�$	��0,5q�.+��� $��Instructions:��$	��100��$��Center=��� -   $ txtInstructions��$	��300��$��60�� �� TextWrapping$
 Wrap=� �� AcceptsReturn . {�.  �� $ 
Horizontal=�$
��0,15q�$
��Left=�.+��� -	    ��Name$ btnNew��$	��New��$��60��$��0,0,5,0q��� -
   $ btnSave��$
��Save��$��60��$��0,0,5,0q�{�.  �� -   $ btnEdit��$
��Edit��$��60��$��0,0,5,0q�{�.  �� -   $ 	btnDelete��$��Delete��$��60��$��0,0,5,0q�{�.  �� -   $ 
btnRefresh��$��Refresh��$��60��&   System.Windows.Controls.DataGrid  -     Name$ dgPrescriptions��$��1��	   AutoGenerateColumns	 .  
   
IsReadOnly
 .    CanUserAddRows .     CanUserDeleteRows .     SelectionUnit$ FullRow=�   SelectionMode$ Single=�$��0,0,0,10q�   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ ID��   Width5  /System.Windows.Controls.DataGridLengthConverter$
 Auto 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *PrescriptionId+  $ Record��$ 2*  �� *DisplayRecord+  $ Med Name��$ 2*  �� *MedicationName+  $ Dosage��$ *  �� *Dosage+  $ 	Frequency��$ *  �� *FrequencyName+  $ 
Start Date��$ *  �� *	StartDate+ �� StringFormat$ {0:yyyy-MM-dd}��  $ End Date��$ *  �� *	EndDate+$ {0:yyyy-MM-dd}��  $ Instructions��$ 3*  �� *Instructions+