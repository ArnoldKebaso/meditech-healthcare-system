   M S B A M L   `   `   `  ����  MediTechDesktopApp-    'MediTechDesktopApp.Views.DepartmentView   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      -   5        .��5
      � $��10q�6     � ColumnDefinitions  5   
   �� 5      $��2* �6   �� 5      $��3* �6   .+�5   
   �� $��0��6   $��0,0,10,0q�6%   .+�5      ��  �� Orientation$ 
Horizontal=�6   $	��0,5q�62   .+�5      �� $��Name:��6   $	��100��6(   $��Center=�64   �� 5      -   5         ��Name$ txtName��5      $	��600��6+   {�.  67   5      �� 5      $ 
Horizontal=�6   $
��0,15q�62   $
��Left=�6@   .+�5      �� -   5         ��Name$ btnNew��5      $	��New��6-   $��60��6?   $��0,0,5,0q�6J   6[   �� 5      -   5        $ btnSave��5      $
��Save��6-   $��60��6?   $��0,0,5,0q�6J   {�.  6q   �� 5      -   5        $ btnEdit��5      $
��Edit��6-   $��60��6?   $��0,0,5,0q�6J   {�.  6q   �� 5       -   5        $ 	btnDelete��5       $��Delete��6-   $��60��6?   $��0,0,5,0q�6J   {�.  6s   �� 5!      -   5        $ 
btnRefresh��5!      $��Refresh��6-   $��60��6?   6V   5"      5#      &   System.Windows.Controls.DataGrid  5&   
   -   5          Name$ dgDepartments��5&      $��1��5'         AutoGenerateColumns .     
IsReadOnly .    CanUserAddRows .     CanUserDeleteRows .  	   SelectionMode$	 Single=�5,      
   SelectionUnit$
 FullRow=�5-      $��0,0,0,10q�5/         Columns 50      0  *System.Windows.Controls.DataGridTextColumn  51      ,  &System.Windows.Controls.DataGridColumn   Header$ ID��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$
 Auto 6T   1  +System.Windows.Controls.DataGridBoundColumn   Binding 63   �� *DepartmentId+6T     52      $
 Name��6%   $ * 6T    63   �� *Name+6T   54      55      56      