   M S B A M L   `   `   `  ����   MediTechDesktopApp*    $MediTechDesktopApp.Views.PaymentView   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     -   .��� $��10q�  � ColumnDefinitions  �� $��2* ��� $��3* �.+��� $��0��$��0,0,10,0q�.+���  �� Orientation$ 
Horizontal=�$	��0,5q�.+��� $��Invoice:��$	��100��$��Center=��� -    ��Name$ 	cbInvoice��$	��200���� $ 
Horizontal=�$	��0,5q�.+��� $��Date:��$	��100��$��Center=�(  "System.Windows.Controls.DatePicker  -     Name$ dpPaymentDate��$	��200���� $ 
Horizontal=�$	��0,5q�.+��� $��Amount Paid:��$	��100��$��Center=��� -    ��Name$ txtAmountPaid��$	��200���� $ 
Horizontal=�$	��0,5q�.+��� $��Method:��$	��100��$��Center=��� -   $ cbMethod��$	��200��.@��� $
��Cash���� $��Credit Card���� $��	Insurance���� $��Other���� $ 
Horizontal=�$
��0,15q�$
��Left=�.+��� -    ��Name$ btnNew��$	��New��$��60��$��0,0,5,0q��� -   $ btnSave��$
��Save��$��60��$��0,0,5,0q�{�.  �� -   $ 	btnUpdate��$��Update��$��60��$��0,0,5,0q�{�.  �� -	   $ 	btnDelete��$��Delete��$��60��$��0,0,5,0q�{�.  �� -
   $ 
btnRefresh��$��Refresh��$��60��&   System.Windows.Controls.DataGrid  -     Name$ 
dgPayments��$��1��   AutoGenerateColumns .     
IsReadOnly . 	   CanUserAddRows	 .  
   CanUserDeleteRows
 .     SelectionMode$ Single=�   SelectionUnit$ FullRow=�$��0,0,0,10q�   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ Invoice��   Width5  /System.Windows.Controls.DataGridLengthConverter$ 2* 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *InvoiceDisplay+  $
 Date��$ *  �� *PaymentDate+ �� StringFormat$ {0:yyyy-MM-dd}��  $ Amount��$ *  �� *
AmountPaid+$ C��  $ Method��$ *  �� *Method+