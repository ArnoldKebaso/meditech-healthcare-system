   M S B A M L   `   `   `  ����   MediTechDesktopApp*    $MediTechDesktopApp.Views.PatientView   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     -   .��� $��10q�  � ColumnDefinitions  �� $��2* ��� $��3* �.+��� $��0��$��0,0,10,0q�.+���  �� Orientation$ 
Horizontal=�$	��0,5q�.+��� $��First Name:��$	��100��$��Center=��� -    ��Name$ txtFirstName��$	��200��{�.  �� $ 
Horizontal=�$	��0,5q�.+��� $��
Last Name:��$	��100��$��Center=��� -   $ txtLastName��$	��200��{�.  �� $ 
Horizontal=�$	��0,5q�.+��� $��Date of Birth:��$	��100��$��Center=�(  "System.Windows.Controls.DatePicker  -     Name$ dpDateOfBirth��$	��200��{�.  �� $ 
Horizontal=�$	��0,5q�.+��� $��Gender:��$	��100��$��Center=��� -    ��Name$ cbGender��$	��200��{�.  .@��� $
��Male���� $��Female���� $��Other���� $ 
Horizontal=�$	��0,5q�.+��� $��Phone:��$	��100��$��Center=��� -   $ txtPhone��$	��200��{�.  �� $ 
Horizontal=�$	��0,5q�.+��� $��Email:��$	��100��$��Center=��� -   $ txtEmail��$	��200��{�.  �� $ 
Horizontal=�$	��0,5q�.+��� $��Address:��$	��100��$��Center=��� -   $ 
txtAddress��$	��200��{�.  �� $ 
Horizontal=�$
��0,15q�$
��Left=�.+��� -	    ��Name$ btnNew��$	��New��$��60��$��0,0,5,0q��� -
   $ btnSave��$
��Save��$��60��$��0,0,5,0q�{�.  �� -   $ btnEdit��$
��Edit��$��60��$��0,0,5,0q�{�.  �� -   $ 	btnDelete��$��Delete��$��60��$��0,0,5,0q�{�.  �� -   $ 
btnRefresh��$��Refresh��$��60��&   System.Windows.Controls.DataGrid  -     Name$ 
dgPatients��$��1��   AutoGenerateColumns .     
IsReadOnly . 	   CanUserAddRows	 .  
   CanUserDeleteRows
 .     SelectionUnit$ FullRow=�   SelectionMode$ Single=�$��0,0,0,10q�   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ ID��   Width5  /System.Windows.Controls.DataGridLengthConverter$
 Auto 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *	PatientId+  $ 
First Name��$ *  �� *	FirstName+  $ 	Last Name��$ *  �� *
LastName+  $	 DOB��$ *  �� *DateOfBirth+ �� StringFormat$ {0:yyyy-MM-dd}��  $ Gender��$
 Auto  �� *Gender+  $ Phone��$ *  �� *ContactPhone+  $ Email��$ 2*  �� *ContactEmail+  $ Address��$ 2*  �� *	Address+