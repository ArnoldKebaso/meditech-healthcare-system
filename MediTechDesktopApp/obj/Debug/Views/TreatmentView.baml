   M S B A M L   `   `   `  ����  MediTechDesktopApp,    &MediTechDesktopApp.Views.TreatmentView   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      -   5        .��5	      � $��10q�6     � ColumnDefinitions  5
   
   �� 5      $��2* �6   �� 5      $��3* �6   .+�5   
   �� $��0��6   $��0,0,10,0q�6%   .+�5      ��  �� Orientation$ 
Horizontal=�6   $	��0,5q�62   .+�5      �� $��Name:��6   $	��100��6(   $��Center=�64   �� 5      -   5         ��Name$ txtName��5      $	��200��6+   {�.  67   5      �� 5      $ Vertical=�6   $	��0,5q�60   .+�5      �� $��Description:��6   $	��100��6/   $��Center=�6;   �� 5      -   5        $ txtDescription��5      $	��300��62   $��80��6>    �� TextWrapping$
 Wrap=�5       �� AcceptsReturn . {�.  6C   5      �� 5      $ 
Horizontal=�6   $
��0,15q�62   $
��Left=�6@   .+�5       �� -   5         ��Name$ btnNew��5       $	��New��6-   $��60��6?   $��0,0,5,0q�6J   6[   �� 5!      -   5        $ btnSave��5!      $
��Save��6-   $��60��6?   $��0,0,5,0q�6J   {�.  6t   �� 5"      -   5        $ btnEdit��5"      $
��Edit��6-   $��60��6?   $��0,0,5,0q�6J   {�.  6t   �� 5#      -   5        $ 	btnDelete��5#      $��Delete��6-   $��60��6?   $��0,0,5,0q�6J   {�.  6t   �� 5$      -   5        $ 
btnRefresh��5$      $��Refresh��6-   $��60��6?   6X   5%      5&      &   System.Windows.Controls.DataGrid  5)   
   -	   5          Name$ dgTreatments��5)      $��1��5*         AutoGenerateColumns .     
IsReadOnly . 	   CanUserAddRows	 .  
   CanUserDeleteRows
 .     SelectionUnit$ FullRow=�5/         SelectionMode$ Single=�50      $��0,0,0,10q�52         Columns 53      0  *System.Windows.Controls.DataGridTextColumn  54      ,  &System.Windows.Controls.DataGridColumn   Header$ ID��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$
 Auto 6[   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6;   �� *TreatmentId+6[     55      $
 Name��6%   $ 2* 6[    6;   �� *Name+6[     56      $ Description��6%   $ 3* 6[    6;   �� *Description+6[     57      $ 
Created At��6%   $ * 6~    6;   �� *	CreatedAt+ �� StringFormat$ {0:yyyy-MM-dd HH:mm}��6~   59      5:      5;      