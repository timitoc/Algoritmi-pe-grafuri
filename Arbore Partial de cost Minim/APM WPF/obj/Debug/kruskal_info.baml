   M S B A M L   `   `   `  ����B  >APM WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:APM_WPFAPM_WPF  5   Q   /� 6   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    %localclr-namespace:APM_WPF    6Q   -   5          �Name$  KruskalInfo��5   E    /� ColumnWidth$	 400��6    /� FontSize$ 14(�6!    /� 
FontFamily$ Georgia)�6/   .T�5   
   J� .*�6   /-Algoritmul funcționează în felul următor:6?   �� 5   
   .<�5      �� .9�6   J� .*�6"   a_creează o pădure F (o mulțime de arbori), unde fiecare vârf din graf este un arbore separat5      6   �� 5	   
   .9�5
      J� .*�6   =;creează o mulțime S care conține toate muchiile din graf5      5      �� 5   
   .9�5      J� .*�6    atât timp cât S este nevidă65   �� 5      .<�5      �� .9�5      J� .*�6$   %#elimină o muchie de cost minim din5      5      �� 5      .9�5      J� .*�6$   ��dacă acea muchie conectează doi arbori distincți, atunci adaugă muchia în pădure, combinând cei doi arbori într-unul singur6�   5      �� 5      .9�5      J� .*�6$   altfel, ignoră muchia6;   5      5      5      5       J� 5!   
   .*�6   }{La sfârșitul algoritmului, pădurea are doar o componentă care reprezintă un arbore parțial de cost minim al grafului.6�   J� 5"   
   .*�6   :8Acest algoritm a fost scris de Joseph Kruskal, în 1956.6M   J� 5$   
   6   5&      