   M S B A M L   `   `   `  ����  MediTechDesktopApp3    -MediTechDesktopApp.Views.PatientInsuranceView   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      -   5        .��5
      � $��10q�6     � ColumnDefinitions  5   
   �� 5      $��2* �6   �� 5      $��3* �6   .+�5   
   �� $��0��6   $��0,0,10,0q�6%   .+�5      ��  �� Orientation$ 
Horizontal=�6   $	��0,5q�62   .+�5      �� $��Patient:��6   $	��100��6+   $��Center=�67   �� 5      -   5         ��Name$ 	cbPatient��5      $	��200��6.    �� DisplayMemberPath$
 Name��5       �� SelectedValuePath$ Id��5      5      �� 5      $ 
Horizontal=�6   $	��0,5q�62   .+�5      �� $��Policy:��6   $	��100��6*   $��Center=�66   �� 5      -   5        $ cbPolicy��5      $	��200��6-   $
 Name��5       $ Id��5!      5"      �� 5%      $ 
Horizontal=�6   $
��0,15q�62   $
��Left=�6@   .+�5&      �� -   5         ��Name$ btnNew��5&      $	��New��6-   $��60��6?   $��0,0,5,0q�6J   6[   �� 5'      -   5        $ btnSave��5'      $
��Save��6-   $��60��6?   $��0,0,5,0q�6J   {�.  6t   �� 5(      -   5        $ 	btnDelete��5(      $��Delete��6-   $��60��6?   $��0,0,5,0q�6J   {�.  6t   �� 5)      -   5        $ 
btnRefresh��5)      $��Refresh��6-   $��60��6?   6X   5*      5+      &   System.Windows.Controls.DataGrid  5.   
   -   5          Name$ dgPatientInsurance��5.      $��1��5/         AutoGenerateColumns .     
IsReadOnly . 	   CanUserAddRows	 .  
   CanUserDeleteRows
 .     SelectionUnit$ FullRow=�54         SelectionMode$ Single=�55      $��0,0,0,10q�57         Columns 58      0  *System.Windows.Controls.DataGridTextColumn  59      ,  &System.Windows.Controls.DataGridColumn   Header$ Patient��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ 2* 6\   1  +System.Windows.Controls.DataGridBoundColumn   Binding 68   �� *PatientFullName+6\     5:      $ Policy #��6%   $ 2* 6\    68   �� *PolicyNumber+6\     5;      $ Coverage��6%   $ 3* 6\    68   �� *CoverageDetails+6\     5<      $ 
Start Date��6%   $
 Auto 6v    69   �� *	StartDate+ �� StringFormat$ {0:yyyy-MM-dd}��6v     5=      $ End Date��6%   $
 Auto 6v    69   �� *	EndDate+$ {0:yyyy-MM-dd}��6v   5?      5@      5A      