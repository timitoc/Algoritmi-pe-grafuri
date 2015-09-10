Algoritmi pe grafuri, fostul "Arbore Partial de cost Minim", nume care nu mai corespunde cu continutul softului.

Fisierele text si imaginile folosite(bucket1.txt, bucket2.txt, Reworked (acolada|part1|part2|part3).png) 
sunt realizate de noi(Ardelean Andrei Timotei sau Ardelean Razvan Mitel).

Pentru explicatia teoretica a algoritmilor am folosit ca sursa: http://www.wikipedia.com/wiki/
Pentru enuntul problemei APM: http://www.infoarena.ro

Am folosit cateva "code snippets" din surse deschise pe care le mentionez
(Referintele sunt puse si in codul sursa al aplicatiei):
----------------------------------------------------------------------------------------
Clasa AlgorithmUI.xaml.cs, functia GetEncoding
	/**
         * Method for getting text-file encoding
         * Source: http://stackoverflow.com/questions/3825390/effective-way-to-find-any-files-encoding
         */
        public static Encoding GetEncoding(string filename)[...]
----------------------------------------------------------------------------------------
Clasa Algorithm.cs, functia BuildAssembly
 	/**
         * Method for building Assembly at runtime
         * Source: http://stackoverflow.com/questions/21013572/compile-assembly-in-runtime-and-save-dll-in-a-folder
         */
        public static Assembly BuildAssembly(string code)[...]
----------------------------------------------------------------------------------------
Dictionarele de resurse BaraMeniu si Brushes
 	https://msdn.microsoft.com/en-us/library/ms747082%28v=vs.85%29.aspx?f=255&MSPPError=-2147217396
----------------------------------------------------------------------------------------

Distributia contributiei partilor:

Ardelean Andrei Timotei:- Realizarea claselor: Algorithm.cs, AlgorithmUI.xaml, LineCounter.cs, Universal.xaml, Prim.xaml
			- Ajustar/Modificari/Update-uri semnificative aduse clasei MainWindow.xaml
			

Ardelean Razvan Mitel:  - Realizarea claselor Graf.cs, GrafControl.xaml, Nod.cs, Muchie.xaml, Kruskal.xaml
			- Realizarea stadiului initial al clasei MainWindow.xaml
			- Proiectarea interfetei cu utilizatorul
			

