   M S B A M L   `   `   `  ����O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  5       APM WPF   APM_WPF.Muchie   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      9System&clr-namespace:System;assembly=mscorlib     5      -   5          >�Name$  MuchieControl��5	       � 	Resources 5         ButtonControlTemplate1 �� 
TargetType��%�   &	        �� 5      " ��63   .R�5      � .+�5      �� $
��Left=�6   $��23.25��6+   $	��Top=�6L   $��148.625��6d   ��6:   Z� ��6t   Z� 5      5      5       � Triggers 5      :� 5       :� RoutedEvent$ FrameworkElement.Loaded��6   .��5      � -   5         �Name$ 
LayoutRoot��5      '  !System.Windows.VisualStateManager   VisualStateGroups 5      %  System.Windows.VisualStateGroup  5      -   5        Default��   States. 5         System.Windows.VisualState  -   5        
normal��5        5      -   5        selected��   
Storyboard. 5      �� .��5      �� 	 �� TargetProperty$*	 $(Shape.Fill).(SolidColorBrush.Color)�6%   
 �� 
TargetName$

 path��6f   .g�5   	   8  2System.Windows.Media.Animation.EasingColorKeyFrame   �� KeyTime$ 0��6    �� Value$ Black��6)   5   	   �� 5      $,	 &(Shape.Stroke).(SolidColorBrush.Color)�6%   $

 path��6h   .g�5    	     $ 0��6   $ 	#FF5784FB��6)   5!   	   U� 5"      $	 (UIElement.Opacity)�6&   $

 path��6V   .\�5#   	   9  3System.Windows.Media.Animation.EasingDoubleKeyFrame   O� KeyTime$ 0��6    O� Value$ 1Q�6*   5$   	   T� 5%      $	 (Border.BorderBrush)�6&   $
 textBox��6W   ..�5&   	   i�  R� KeyTime$ 0��6     R� Value 5'   
   �� 5(       �� Color$ 	#FF5784FB��6   5*   
   5+   	   5,      5-      5.        5/      -   5        flip��. 50        -   5        	normalpos��50        51      -   5        
reversepos��. 52      �� .��53      U� $V	 P(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleY)�6&   $

 path��6�   .\�54   	     $ 0��6   $ 1Q�6*   55   	   56      57      58      .+�5:      F� -	   5         F�Name$
 path��5:   	   ����@�[	@(��@�-�.�		���	#FFF4F4F5 �� Stretch$
 Fill=�6A   
���   � J� RenderTransformOrigin$ 0.5,0.5&�6_   z�5;      V� 5<      .~�5=      ��  �� ScaleY$ -1Q�6    �� ScaleX$ 1Q�6"   �� 5>       �� AngleY$ 0Q�6    �� AngleX$ 0Q�6    �� 5?       �� Angle$ 0Q�6   T� 5@      5A      5C      �� 5D      -
   5         ��Name$ contentControl��5D      $��Center=�6+   $��Center=�6H   $��0,0,0,0q�6c   .��5E      �� -   5         ��Name$ textBox��5E      $��20��6   $
��34.5��6*    �� TextWrapping$
 Wrap=�67   $ 0.5,0.5&�6�   $��Center=�6�   $��0,0,0,0q�6�   $��Center=�6�   ��6K   �� *DataContext+ �� ElementName$ contentControl��.��6�   z�5F      V� 5G      .~�5H      �� �� 5I      �� 5J      $ -0.175Q�6   T� 5K      5L      .��5M      5N      5O      5P      5Q      