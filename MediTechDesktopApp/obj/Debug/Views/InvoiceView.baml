   M S B A M L   `   `   `  ����  MediTechDesktopApp*    $MediTechDesktopApp.Views.InvoiceView   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      -   5        .��5
      � $��10q�6     � ColumnDefinitions  5   
   �� 5      $��2* �6   �� 5      $��3* �6   .+�5   
   �� $��0��6   $��0,0,10,0q�6%   .+�5      ��  �� Orientation$ 
Horizontal=�6   $	��0,5q�62   .+�5      �� $��Patient:��6   $	��100��6+   $��Center=�67   �� 5      -   5         ��Name$ 	cbPatient��5      $	��200��6.   5      �� 5      $ 
Horizontal=�6   $	��0,5q�62   .+�5      �� $��Invoice Date:��6   $	��100��60   $��Center=�6<   (  "System.Windows.Controls.DatePicker  5      -   5          Name$ dpInvoiceDate��5      $	��200��64   5      �� 5!      $ 
Horizontal=�6   $	��0,5q�62   .+�5"      �� $��Total Amount:��6   $	��100��60   $��Center=�6<   �� 5#      -   5         ��Name$ txtTotalAmount��5#      $	��200��62   5$      �� 5'      $ 
Horizontal=�6   $	��0,5q�62   .+�5(      �� $��Status:��6   $	��100��6*   $��Center=�66   �� 5)      -   5        $ cbStatus��5)      $	��200��6-   .@�5*      �� $��Pending��6#   �� 5+      $
��Paid��6#   �� 5,      $��Overdue��6#   5-      5.      �� 51      $ 
Horizontal=�6   $
��0,15q�62   $
��Left=�6@   .+�52      �� -   5         ��Name$ btnNew��52      $	��New��6-   $��60��6?   $��0,0,5,0q�6J   6[   �� 53      -   5        $ btnSave��53      $
��Save��6-   $��60��6?   $��0,0,5,0q�6J   {�.  6t   �� 54      -   5        $ 	btnUpdate��54      $��Update��6-   $��60��6?   $��0,0,5,0q�6J   {�.  6t   �� 55      -	   5        $ 	btnDelete��55      $��Delete��6-   $��60��6?   $��0,0,5,0q�6J   {�.  6t   �� 56      -
   5        $ 
btnRefresh��56      $��Refresh��6-   $��60��6?   6X   57      58      &   System.Windows.Controls.DataGrid  5;   
   -   5          Name$ 
dgInvoices��5;      $��1��5<         AutoGenerateColumns .     
IsReadOnly . 	   CanUserAddRows	 .  
   CanUserDeleteRows
 .     SelectionUnit$ FullRow=�5A         SelectionMode$ Single=�5B      $��0,0,0,10q�5D         Columns 5E      0  *System.Windows.Controls.DataGridTextColumn  5F      ,  &System.Windows.Controls.DataGridColumn   Header$ Patient��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ 2* 6_   1  +System.Windows.Controls.DataGridBoundColumn   Binding 68   �� *PatientFullName+6_     5G      $ Invoice Date��6%   $ * 6z    6;   �� *InvoiceDate+ �� StringFormat$ {0:yyyy-MM-dd}��6z     5H      $ Amount��6%   $ * 6h    68   �� *TotalAmount+$ C��6h     5I      $ Status��6%   $ * 6`    68   �� *Status+6`   5K      5L      5M      