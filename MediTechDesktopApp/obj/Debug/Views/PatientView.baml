   M S B A M L   `   `   `  ����  MediTechDesktopApp*    $MediTechDesktopApp.Views.PatientView   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      -   5        .��5      � $��10q�6     � ColumnDefinitions  5   
   �� 5      $��2* �6   �� 5      $��3* �6   .+�5   
   �� $��0��6   $��0,0,10,0q�6%   .+�5      ��  �� Orientation$ 
Horizontal=�6   $	��0,5q�62   .+�5      �� $��First Name:��6   $	��100��6.   $��Center=�6:   �� 5      -   5         ��Name$ txtFirstName��5      $	��200��60   {�.  6<   5      �� 5      $ 
Horizontal=�6   $	��0,5q�62   .+�5      �� $��
Last Name:��6   $	��100��6-   $��Center=�69   �� 5      -   5        $ txtLastName��5      $	��200��6/   {�.  6;   5      �� 5"      $ 
Horizontal=�6   $	��0,5q�62   .+�5#      �� $��Date of Birth:��6   $	��100��61   $��Center=�6=   (  "System.Windows.Controls.DatePicker  5$      -   5          Name$ dpDateOfBirth��5$      $	��200��64   {�.  6@   5%      �� 5(      $ 
Horizontal=�6   $	��0,5q�62   .+�5)      �� $��Gender:��6   $	��100��6*   $��Center=�66   �� 5*      -   5         ��Name$ cbGender��5*      $	��200��6-   {�.  .@�5+      �� $
��Male��6#   �� 5,      $��Female��6#   �� 5-      $��Other��6#   5.      5/      �� 52      $ 
Horizontal=�6   $	��0,5q�62   .+�53      �� $��Phone:��6   $	��100��6)   $��Center=�65   �� 54      -   5        $ txtPhone��54      $	��200��6,   {�.  68   55      �� 58      $ 
Horizontal=�6   $	��0,5q�62   .+�59      �� $��Email:��6   $	��100��6)   $��Center=�65   �� 5:      -   5        $ txtEmail��5:      $	��200��6,   {�.  68   5;      �� 5>      $ 
Horizontal=�6   $	��0,5q�62   .+�5?      �� $��Address:��6   $	��100��6+   $��Center=�67   �� 5@      -   5        $ 
txtAddress��5@      $	��200��6.   {�.  6:   5A      �� 5D      $ 
Horizontal=�6   $
��0,15q�62   $
��Left=�6@   .+�5E      �� -	   5         ��Name$ btnNew��5E      $	��New��63   $��60��6E   $��0,0,5,0q�6P   6a   �� 5F      -
   5        $ btnSave��5F      $
��Save��63   $��60��6E   $��0,0,5,0q�6P   {�.  6}   �� 5G      -   5        $ btnEdit��5G      $
��Edit��63   $��60��6E   $��0,0,5,0q�6P   {�.  6}   �� 5H      -   5        $ 	btnDelete��5H      $��Delete��63   $��60��6E   $��0,0,5,0q�6P   {�.  6}   �� 5I      -   5        $ 
btnRefresh��5I      $��Refresh��63   $��60��6E   6\   5J      5K      &   System.Windows.Controls.DataGrid  5N   
   -   5          Name$ 
dgPatients��5N      $��1��5O         AutoGenerateColumns .     
IsReadOnly . 	   CanUserAddRows	 .  
   CanUserDeleteRows
 .     SelectionUnit$ FullRow=�5T         SelectionMode$ Single=�5U      $��0,0,0,10q�5W         Columns 5X      0  *System.Windows.Controls.DataGridTextColumn  5Y      ,  &System.Windows.Controls.DataGridColumn   Header$ ID��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$
 Auto 6a   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6=   �� *	PatientId+6a     5Z      $ 
First Name��6%   $ * 6a    6=   �� *	FirstName+6a     5[      $ 	Last Name��6%   $ * 6a    6=   �� *
LastName+6a     5\      $	 DOB��6%   $ * 6|    6=   �� *DateOfBirth+ �� StringFormat$ {0:yyyy-MM-dd}��6|     5]      $ Gender��6%   $
 Auto 6a    6=   �� *Gender+6a     5^      $ Phone��6%   $ * 6a    6=   �� *ContactPhone+6a     5_      $ Email��6%   $ 2* 6a    6=   �� *ContactEmail+6a     5`      $ Address��6%   $ 2* 6a    6=   �� *	Address+6a   5b      5c      5d      