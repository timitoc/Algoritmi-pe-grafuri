   M S B A M L   `   `   `  ����R  NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089   Kruskal_Teorie  �� EndPoint �� 
StartPoint � Color � Offset 	 Enunt  Prim_Teorie  Disjunct�� 5      D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     :x,http://schemas.microsoft.com/winfx/2006/xaml      5      %J  &	        &	 v    &	 �    &	 �    /� 5      .T�5   
   J� $
��Bold"�6   $��18(�6&   .*�65   ��5      �� 5      $  0.5,1&�6&   $ 0.5,0&�67   .��5	      � $ Black��6#   $ 0Q�61   � 5
      $ 	#FF14085F��6#   $ 1Q�65   5      .*�5   $   Algoritmul lui Kruskal5      J� 5   
   .*�6   /-Algoritmul funcționează în felul următor:6?   �� 5   
   .<�5      �� .9�5      J� .*�6   a_creează o pădure F (o mulțime de arbori), unde fiecare vârf din graf este un arbore separat5      5      �� 5      .9�5      J� .*�6   =;creează o mulțime S care conține toate muchiile din graf5      5      �� 5      .9�5      J� .*�6    atât timp cât S este nevidă69   �� 5      .<�5      �� .9�5      J� .*�6$   '%elimină o muchie de cost minim din S5!      5"      �� 5#      .9�5$      J� .*�6$   ��dacă acea muchie conectează doi arbori distincți, atunci adaugă muchia în pădure, combinând cei doi arbori într-unul singur6�   5%      �� 5&      .9�5'      J� .*�6$   altfel, ignoră muchia6;   5(      5)      5*      5+      J� 5,   
   .*�6   }{La sfârșitul algoritmului, pădurea are doar o componentă care reprezintă un arbore parțial de cost minim al grafului.6�   J� 5-   
   .*�6   :8Acest algoritm a fost scris de Joseph Kruskal, în 1956.6M   J� 5/   
   6   50      /� 52      .T�53   
   J� .*�6   a_Se da un graf conex neorientat G cu N noduri si M muchii, fiecare muchie avand asociat un cost.6u   J� 54   
   .*�6   ��Se cere sa se determine un subgraf care cuprinde toate nodurile si o parte din muchii, astfel incat subgraful determinat sa aiba structura de arbore si suma costurilor muchiilor care il formeaza sa fie minim posibila.6�   J� 55   
   .*�6   b`Subgraful cu proprietatile de mai sus se va numi arbore partial de cost minim pentru graful dat.6v   56      /� 58      .T�59   
   J� $
��Bold"�6   $��18(�6&   .*�65   ��5:      �� 5;      $  0.5,1&�6&   $ 0.5,0&�67   .��5<      � $ Black��6#   $ 0Q�61   � 5=      $ 	#FF14085F��6#   $ 1Q�65   5>      .*�5?   $   Algoritmul lui Prim5@      J� 5B   
   .*�6   RPAlgoritmul lui Prim folosește tot o abordare greedy, de data aceasta pe noduri.6e   J� 5C   
   .*�6   ��Pentru aceasta este folosită structura de date MINHEAP, prin care se execută selecarea în timp optim a nodului din care există muchia cu cel mai mic cost către arborele parțial curent.6�   J� 5D   
   .*�6   +)Acesta funcționează în felul următor:6;   �� 5F   
   .<�5G      �� .9�5H      J� .*�6   ?=Intrare: Un graf conex ponderat cu nodurile V și muchiile E.5J      5K      �� 5L      .9�5M      J� .*�6   ZXInitializare: Vnou = {x}, unde x este un nod arbitrar (punct de plecare) din V, Enou= {}5O      5P      �� 5Q      .9�5R      J� .*�6   repetă până când Vnou=V:66   �� 5T      .<�5U      �� .9�5V      J� .*�6$   ��Alege muchia (u,v) din E de cost minim astfel încât u este în Vnou și v nu e (dacă există mai multe astfel de muchii, se alege arbitrar)5X      5Y      �� 5Z      .9�5[      J� .*�6$   %#Se adaugă v la Vnou, (u,v) la Enou6H   5\      5]      5^      �� 5_      .9�5`      J� .*�6   A?Ieșire: Vnou și Enou descriu arborele parțial de cost minim.5b      5c      5d      J� 5f   
   .*�6   ��Algoritmul a fost descoperit în 1930 de către matematicianul Vojtěch Jarník și apoi, independent, de informaticienii Robert C. Prim în 1957 și redescoperit de Edsger Dijkstra în 1959. De aceea mai este numit Algoritmul DJP, algoritmul Jarník sau algoritmul Prim-Jarník.6#  J� 5h   
   6   5i      /� 5k      .T�5l   
   J� $
��Bold"�6   $��18(�6&   .*�65   ��5m      �� 5n      $  0.5,1&�6&   $ 0.5,0&�67   .��5o      � $ Black��6#   $ 0Q�61   � 5p      $ 	#FF14085F��6#   $ 1Q�65   5q      .*�5r   $   Păduri de mulțimi disjunte5s      J� 5t   
   .*�6   FDAceastă structură de date rezolvă eficient următoarele probleme:6V   �� 5u   
   .<�5v      �� .9�5w      J� .*�6   USÎn ce mulțime se află nodul x? Fac nodurile x și y parte din aceeași mulțime?5y      5z      �� 5{      .9�5|      J� .*�6   Reuniunea a două mulțimi.5~      5      5�      J� 5�   
   .*�6   97Arborii vor fi reprezentați printr-un vector de tați.6K   J� 5�   
   .*�6   zxPentru a verifica dacă două elemente fac parte din aceeași mulțime, verificăm dacă rădăcinile lor sunt identice.6�   J� 5�   
   .*�6   ��Obținerea rădăcinii elementului x se face prin iterarea succesivă a tatălui nodului curent, apoi a tatălui acestuia și așa mai departe, până ajung la rădăcină6�   J� 5�   
   .*�6   \ZAceastă abordare are complexitatea O(h), unde h este nivelul la care se afla x in arbore.6o   J� 5�   
   .*�6   ��Vom face o imbunătățire, care se bazează pe faptul ca pe noi ne interesează doar rădăcina arborelui: Vom compresa drumurile6�   J� 5�   
   .*�6   ljCompresia drumurilor constă în legarea fiilor direct de rădăcină după fiecare parcurgere a acestora.6z   J� 5�   
   .*�6   omPentru reuniunea a două mulțimi vom adăuga rădăcina unuia dintre arbori ca fiu al rădăcinii celuilalt.6|   5�      5�      