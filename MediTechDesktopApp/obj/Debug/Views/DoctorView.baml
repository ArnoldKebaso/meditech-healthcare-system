   M S B A M L   `   `   `  ����  MediTechDesktopApp)    #MediTechDesktopApp.Views.DoctorView   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      -   5        .��5      � $��10q�6     � ColumnDefinitions  5   
   �� 5      $��2* �6   �� 5      $��3* �6   .+�5   
   �� $��0��6   $��0,0,10,0q�6%   .+�5      ��  �� Orientation$ 
Horizontal=�6   $	��0,5q�62   .+�5      �� $��First Name:��6   $	��100��6.   $��Center=�6:   �� 5      -   5         ��Name$ txtFirstName��5      $	��200��60   {�.  6<   5      �� 5      $ 
Horizontal=�6   $	��0,5q�62   .+�5      �� $��
Last Name:��6   $	��100��6-   $��Center=�69   �� 5      -   5        $ txtLastName��5      $	��200��6/   {�.  6;   5      �� 5!      $ 
Horizontal=�6   $	��0,5q�62   .+�5"      �� $��Specialization:��6   $	��100��62   $��Center=�6>   �� 5#      -   5         ��Name$ cbSpecialization��5#      $	��200��65   {�.  6A   5$      �� 5'      $ 
Horizontal=�6   $	��0,5q�62   .+�5(      �� $��Phone:��6   $	��100��6)   $��Center=�65   �� 5)      -   5        $ txtPhone��5)      $	��200��6,   {�.  68   5*      �� 5-      $ 
Horizontal=�6   $	��0,5q�62   .+�5.      �� $��Email:��6   $	��100��6)   $��Center=�65   �� 5/      -   5        $ txtEmail��5/      $	��200��6,   {�.  68   50      �� 53      $ 
Horizontal=�6   $
��0,15q�62   $
��Left=�6@   .+�54      �� -   5         ��Name$ btnNew��54      $	��New��6/   $��60��6A   $��0,0,5,0q�6L   6]   �� 55      -   5        $ btnSave��55      $
��Save��6/   $��60��6A   $��0,0,5,0q�6L   {�.  6w   �� 56      -	   5        $ btnEdit��56      $
��Edit��6/   $��60��6A   $��0,0,5,0q�6L   {�.  6w   �� 57      -
   5        $ 	btnDelete��57      $��Delete��6/   $��60��6A   $��0,0,5,0q�6L   {�.  6w   �� 58      -   5        $ 
btnRefresh��58      $��Refresh��6/   $��60��6A   6X   59      5:      &   System.Windows.Controls.DataGrid  5=   
   -   5          Name$ 	dgDoctors��5=      $��1��5>         AutoGenerateColumns .     
IsReadOnly .    CanUserAddRows .  	   CanUserDeleteRows	 .  
   SelectionUnit$
 FullRow=�5C         SelectionMode$ Single=�5D      $��0,0,0,10q�5F         Columns 5G      0  *System.Windows.Controls.DataGridTextColumn  5H      ,  &System.Windows.Controls.DataGridColumn   Header$ ID��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$
 Auto 6e   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6=   �� *
DoctorId+6e     5I      $ 
First Name��6%   $ * 6e    6=   �� *	FirstName+6e     5J      $ 	Last Name��6%   $ * 6e    6=   �� *
LastName+6e     5K      $ Specialization��6%   $ * 6f    6=   �� *SpecializationName+6f     5L      $ Phone��6%   $
 Auto 6e    6=   �� *ContactPhone+6e     5M      $ Email��6%   $ 2* 6e    6=   �� *ContactEmail+6e   5O      5P      5Q      