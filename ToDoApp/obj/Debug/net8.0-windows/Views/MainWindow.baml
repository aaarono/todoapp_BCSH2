   M S B A M L   `   `   `  ����B  >ToDoApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:ToDoApp.ViewsToDoApp.Views  5   	   7 clr-namespace:ToDoApp.ViewModelsToDoApp.ViewModels  63       ToDoApp.Views.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:ToDoApp.Views     5   	   7
viewmodels clr-namespace:ToDoApp.ViewModels     63     � Title$  ToDoApp��5   	   $	��600��6   $	��800��6&    � 	Resources 5	       �� 
TargetType �� Property �� Value   HeaderTextBlock��%�  '��       &	      �� 5
   
   " ��6   .�5      ��  � ��$ 5q�6'   �� 5       � ��$	 5,2q�6(   �� 5       � �� �	#FF4444446+   �� 5       � ��
 �����6+   �� 5       � ��
 �   �6,   �� 5       � ��$ 1q�60   5      �� 5   
   " ��6   .�5      ��  � ��$ 20(�6)   �� 5       � ��
 �����6+   �� 5       � ��$ 10q�6'   5      5      .��5      � ��5   
   �� 5       �� 
StartPoint$	 0,0&�6"    �� EndPoint$	 1,1&�63   .��5      �  � Color$ 	#FF303030��6    � Offset$	 0.0Q�61   � 5      $ 	#FF202020��6   $	 1.0Q�61   5      	 � ColumnDefinitions	 5"   
   �� 5#      $	��250 �6   �� 5$      $��* �6   .+�5'   
   �� $��0��6   ���	#FF383838$��Stretch=�6<   .+�5(      �� #��[  69   ��6   �� *GreetingMessage+69   �� 5*      
 �� DisplayMemberPath$
 CategoryName��69   $��10q�5+   @   ���	#FF2C2C2C
���������5*      �� *
Categories+ �� SelectedItem 5+      �� *SelectedCategory+5,   -   �� 5.       �� Orientation$ 
Horizontal=�6   $��Center=�62   .+�5/      �� $	��Add��6   $��60��6N   ��6'   �� *AddCategoryCommand+6N   �� 50      $
��Edit��6   $��60��6P   ��6(   �� *EditCategoryCommand+6P   �� 51      $��Delete��6   $��60��6T   ��6*   �� *DeleteCategoryCommand+6T   52      �� 54      $��Logout��6   $��10q�6H   $��Center=�6T   $	��200��6q   ��6&   �� *LogoutCommand+6q   55      �� 57   
   $��1��6   .+�58      �� $��Tasks��6   #��[  6%   �� 59      ���	#FF2C2C2C
�������$��10q�6^   ��6   �� *Tasks+��5:      �� 5;      .R�5<      �� $ 
Horizontal=�6%   .+�5=      �� $��Center=�5>   '   $	��5,0q�5?   '    � ToolTip$ Mark as completed��5@   '   ��5=   '   �� *
TaskName+ d� 	IsChecked 6D   �� *IsCompleted+ �� Mode$ TwoWay=���5A   '   �� *!DataContext.CompleteTaskCommand+ �� RelativeSource ��  �� AncestorType ListBox��5B   '   �� �� 5C      $
��Edit��6%   ��5D   %   �� *DataContext.EditTaskCommand+ ��  ListBox��5E   %   �� �� 5F      $��Delete��6%   ��5G   %   �� *DataContext.DeleteTaskCommand+ ��  ListBox��5H   %   �� 5I      5J      5L      �� 5M      $��Add Task��6   $��10q�6K   $
��Left=�6W   ��6(   �� *AddTaskCommand+6W   5N      5O      5P      