   M S B A M L   `   `   `  ����M  IMediTechDesktopApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:MediTechDesktopApp.ViewsMediTechDesktopApp.Views  5      -    'MediTechDesktopApp.Views.AdminStaffView   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      8local&clr-namespace:MediTechDesktopApp.Views     5      -   5        .��5
      � $��10q�6     � ColumnDefinitions  5   
   �� 5      $��2* �6   �� 5      $��3* �6   .+�5   
   �� $��0��6   $��0,0,10,0q�6%   .+�5      ��  �� Orientation$ 
Horizontal=�6   $	��0,5q�62   .+�5      �� $��First Name:��6   $	��100��6.   $��Center=�6:   �� 5      -   5         ��Name$ txtFirstName��5      $	��200��60   {�.  6<   5      �� 5      $ 
Horizontal=�6   $	��0,5q�62   .+�5      �� $��
Last Name:��6   $	��100��6-   $��Center=�69   �� 5      -   5        $ txtLastName��5      $	��200��6/   {�.  6;   5      �� 5!      $ 
Horizontal=�6   $	��0,5q�62   .+�5"      �� $��Role:��6   $	��100��6(   $��Center=�64   �� 5#      -   5         ��Name$ cbRole��5#      $	��200��6+   {�.   �� DisplayMemberPath$
 Name��5$       �� SelectedValuePath$ RoleId��64   5%      �� 5(      $ 
Horizontal=�6   $	��0,5q�62   .+�5)      �� $��Phone:��6   $	��100��6)   $��Center=�65   �� 5*      -   5        $ txtPhone��5*      $	��200��6,   {�.  68   5+      �� 5.      $ 
Horizontal=�6   $	��0,5q�62   .+�5/      �� $��Email:��6   $	��100��6)   $��Center=�65   �� 50      -   5        $ txtEmail��50      $	��200��6,   {�.  68   51      �� 54      $ 
Horizontal=�6   $
��0,15q�62   $
��Left=�6@   .+�55      �� -   5         ��Name$ btnNew��55      $	��New��6-   $��60��6?   $��0,0,5,0q�6J   6[   �� 56      -   5        $ btnSave��56      $
��Save��6-   $��60��6?   $��0,0,5,0q�6J   {�.  6t   �� 57      -	   5        $ btnEdit��57      $
��Edit��6-   $��60��6?   $��0,0,5,0q�6J   {�.  6t   �� 58      -
   5        $ 	btnDelete��58      $��Delete��6-   $��60��6?   $��0,0,5,0q�6J   {�.  6t   �� 59      -   5        $ 
btnRefresh��59      $��Refresh��6-   $��60��6?   6X   5:      5;      &   System.Windows.Controls.DataGrid  5>   
   -   5          Name$ dgAdminStaff��5>      $��1��5?         AutoGenerateColumns .  	   
IsReadOnly	 . 
   CanUserAddRows
 .     CanUserDeleteRows .     SelectionMode$ Single=�5D         SelectionUnit$ FullRow=�5E      $��0,0,0,10q�5G         Columns 5H      0  *System.Windows.Controls.DataGridTextColumn  5I      ,  &System.Windows.Controls.DataGridColumn   Header$ ID��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$
 Auto 6Y   1  +System.Windows.Controls.DataGridBoundColumn   Binding 68   �� *	StaffId+6Y     5J      $ 
First Name��6%   $ * 6Z    69   �� *	FirstName+6Z     5K      $ 	Last Name��6%   $ * 6Y    68   �� *
LastName+6Y     5L      $
 Role��6%   $ * 6Y    68   �� *
RoleName+6Y     5M      $ Phone��6%   $ * 6Y    68   �� *ContactPhone+6Y     5N      $ Email��6%   $ 2* 6Y    68   �� *ContactEmail+6Y   5P      5Q      5R      