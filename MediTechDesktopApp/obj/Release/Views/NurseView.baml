   M S B A M L   `   `   `  ����   MediTechDesktopApp(    "MediTechDesktopApp.Views.NurseView   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     -   .��� $��10q�  � ColumnDefinitions  �� $��2* ��� $��3* �.+��� $��0��$��0,0,10,0q�.+���  �� Orientation$ 
Horizontal=�$	��0,5q�.+��� $��First Name:��$	��100��$��Center=��� -    ��Name$ txtFirstName��$	��200��{�.  �� $ 
Horizontal=�$	��0,5q�.+��� $��
Last Name:��$	��100��$��Center=��� -   $ txtLastName��$	��200��{�.  �� $ 
Horizontal=�$	��0,5q�.+��� $��Department:��$	��100��$��Center=��� -    ��Name$ cbDepartment��$	��200��{�.  �� $ 
Horizontal=�$	��0,5q�.+��� $��Phone:��$	��100��$��Center=��� -   $ txtPhone��$	��200��{�.  �� $ 
Horizontal=�$	��0,5q�.+��� $��Email:��$	��100��$��Center=��� -   $ txtEmail��$	��200��{�.  �� $ 
Horizontal=�$
��0,15q�$
��Left=�.+��� -    ��Name$ btnNew��$	��New��$��60��$��0,0,5,0q��� -   $ btnSave��$
��Save��$��60��$��0,0,5,0q�{�.  �� -	   $ btnEdit��$
��Edit��$��60��$��0,0,5,0q�{�.  �� -
   $ 	btnDelete��$��Delete��$��60��$��0,0,5,0q�{�.  �� -   $ 
btnRefresh��$��Refresh��$��60��&   System.Windows.Controls.DataGrid  -     Name$ dgNurses��$��1��   AutoGenerateColumns .     
IsReadOnly .    CanUserAddRows .  	   CanUserDeleteRows	 .  
   SelectionUnit$
 FullRow=�   SelectionMode$ Single=�$��0,0,0,10q�   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ ID��   Width5  /System.Windows.Controls.DataGridLengthConverter$
 Auto 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *	NurseId+  $ 
First Name��$ *  �� *	FirstName+  $ 	Last Name��$ *  �� *
LastName+  $ 
Department��$ *  �� *DepartmentName+  $ Phone��$
 Auto  �� *ContactPhone+  $ Email��$ 2*  �� *ContactEmail+