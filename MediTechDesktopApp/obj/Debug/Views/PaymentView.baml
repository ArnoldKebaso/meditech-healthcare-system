   M S B A M L   `   `   `  ����  MediTechDesktopApp*    $MediTechDesktopApp.Views.PaymentView   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      -   5        .��5
      � $��10q�6     � ColumnDefinitions  5   
   �� 5      $��2* �6   �� 5      $��3* �6   .+�5   
   �� $��0��6   $��0,0,10,0q�6%   .+�5      ��  �� Orientation$ 
Horizontal=�6   $	��0,5q�62   .+�5      �� $��Invoice:��6   $	��100��6+   $��Center=�67   �� 5      -   5         ��Name$ 	cbInvoice��5      $	��200��6.   5      �� 5      $ 
Horizontal=�6   $	��0,5q�62   .+�5      �� $��Date:��6   $	��100��6(   $��Center=�64   (  "System.Windows.Controls.DatePicker  5      -   5          Name$ dpPaymentDate��5      $	��200��64   5      �� 5!      $ 
Horizontal=�6   $	��0,5q�62   .+�5"      �� $��Amount Paid:��6   $	��100��6/   $��Center=�6;   �� 5#      -   5         ��Name$ txtAmountPaid��5#      $	��200��61   5$      �� 5'      $ 
Horizontal=�6   $	��0,5q�62   .+�5(      �� $��Method:��6   $	��100��6*   $��Center=�66   �� 5)      -   5        $ cbMethod��5)      $	��200��6-   .@�5*      �� $
��Cash��6#   �� 5+      $��Credit Card��6#   �� 5,      $��	Insurance��6#   �� 5-      $��Other��6#   5.      5/      �� 52      $ 
Horizontal=�6   $
��0,15q�62   $
��Left=�6@   .+�53      �� -   5         ��Name$ btnNew��53      $	��New��6-   $��60��6?   $��0,0,5,0q�6J   6[   �� 54      -   5        $ btnSave��54      $
��Save��6-   $��60��6?   $��0,0,5,0q�6J   {�.  6t   �� 55      -   5        $ 	btnUpdate��55      $��Update��6-   $��60��6?   $��0,0,5,0q�6J   {�.  6t   �� 56      -	   5        $ 	btnDelete��56      $��Delete��6-   $��60��6?   $��0,0,5,0q�6J   {�.  6t   �� 57      -
   5        $ 
btnRefresh��57      $��Refresh��6-   $��60��6?   6X   58      59      &   System.Windows.Controls.DataGrid  5<   
   -   5          Name$ 
dgPayments��5<      $��1��5=         AutoGenerateColumns .     
IsReadOnly . 	   CanUserAddRows	 .  
   CanUserDeleteRows
 .     SelectionMode$ Single=�5B         SelectionUnit$ FullRow=�5C      $��0,0,0,10q�5E         Columns 5F      0  *System.Windows.Controls.DataGridTextColumn  5I      ,  &System.Windows.Controls.DataGridColumn   Header$ Invoice��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ 2* 6^   1  +System.Windows.Controls.DataGridBoundColumn   Binding 68   �� *InvoiceDisplay+6^     5J      $
 Date��6%   $ * 6w    68   �� *PaymentDate+ �� StringFormat$ {0:yyyy-MM-dd}��6w     5K      $ Amount��6%   $ * 6g    68   �� *
AmountPaid+$ C��6g     5L      $ Method��6%   $ * 6_    68   �� *Method+6_   5N      5O      5P      