   M S B A M L   `   `   `  ����B  >ToDoApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:ToDoApp.ViewsToDoApp.Views  5   	       ToDoApp.Views.LoginWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:ToDoApp.Views     5   	     � Title$  ToDoApp��5   	   $	��400��6   $	��600��6&   .��5	      � ��5
   
   �� 5       �� 
StartPoint$	 0,0&�6"    �� EndPoint$	 1,1&�63   .��5      �  � Color$ 	#FF303030��6    � Offset$	 0.0Q�61   � 5      $ 	#FF202020��6   $	 1.0Q�61   5      .+�5   
   �� $��Center=�6   $��Center=�60   .+�5      �� $��ToDoApp��6   $��30(�6'   
�������$��Center=�6H   �� 5      $	��200��6   $��5q�6"   ��5      �� *
Username+ �� UpdateSourceTrigger$ PropertyChanged=�" �� ValidatesOnNotifyDataErrors$
 True��G� 5      -   5         G�Name$ PasswordBox��5   1   $	��200��6   $��5q�6&   
 � Tag 5      �� *
Password+$ PropertyChanged=�$
 True���� 5      	 �� Orientation$	 
Horizontal=�6   $��Center=�62   .+�5      �� $��Sign In��6   $��80��6+   $��5q�66   ��5      �� *LoginCommand+��6:   �� 
 �� ElementName$
 PasswordBox���� 5      $��Sign Up��6   $��80��6+   $��5q�66   ��5      �� *RegisterCommand+��6=   �� $
 PasswordBox��5      5      5       5!      