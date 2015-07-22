using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace APM_WPF
{
    public class Algorithm
    {
        Graf graf;
        String code;
        private object algoInstance;
        private Type algoType, muhType;
        object watchMuchii, watchSelected;

        public List<muchie> muchii;
        
        public class muchie
        {
            public int x, y, dist;
            public muchie(int xx = 0, int yy = 0, int dd = 0)
            {
                x = xx;
                y = yy;
                dist = dd;
            }
            public string Xstr { get { return x.ToString(); } }
            public string Ystr { get { return y.ToString(); } }
            public string Diststr { get { return dist.ToString(); } }
        }

        public Algorithm(Graf graf, String code)
        {
            this.graf = graf;
            //code = System.IO.File.ReadAllText(codePath);
            Compile(code, "algoSpace", "algoClass");
            muhType = getField("muh").GetValue(algoInstance).GetType();

            setMembers();
            accesMethod("start");
        }

        /**
         * Method for building Assembly at runtime
         * Source: http://stackoverflow.com/questions/21013572/compile-assembly-in-runtime-and-save-dll-in-a-folder
         */
        public static Assembly BuildAssembly(string code)
        {
            Microsoft.CSharp.CSharpCodeProvider provider =
               new CSharpCodeProvider();
            ICodeCompiler compiler = provider.CreateCompiler();
            CompilerParameters compilerparams = new CompilerParameters();
            compilerparams.GenerateExecutable = false;
            compilerparams.GenerateInMemory = false;

            CompilerResults results =
               compiler.CompileAssemblyFromSource(compilerparams, code);
      
            if (results.Errors.HasErrors)
            {
                StringBuilder errors = new StringBuilder("Compiler Errors :\r\n");
                foreach (CompilerError error in results.Errors)
                {
                    errors.AppendFormat("Line {0},{1}\t: {2}\n",
                           error.Line, error.Column, error.ErrorText);
                }
                throw new Exception(errors.ToString());
            }
            else
            {
                return results.CompiledAssembly;
            }
        }
       
        public void Compile(string code, string namespacename, string classname)
        {
            Assembly asm = BuildAssembly(code);
            algoInstance = asm.CreateInstance(namespacename + "." + classname);
            algoType = algoInstance.GetType();
        }

        public FieldInfo getField(String fieldName)
        {
            FieldInfo prop = algoType.GetField(fieldName);
            if (prop == null)
                throw new Exception(fieldName + "Doesn't exist or is not public");
            return prop;
        }

        public object accesMethod(string methodName, params object[] args)
        {
            object returnval;

            MethodInfo method = algoType.GetMethod(methodName);

            returnval = method.Invoke(algoInstance, args);
            return returnval;
        }

        public void setValue(String fieldName, object value)
        {
            getField(fieldName).SetValue(algoInstance, value);
        }

        public object getValue(String fieldName)
        {
            return getField(fieldName).GetValue(algoInstance);
        }

        public muchie createMuchie(object muhInstance)
        {
            muchie toR = new muchie((int)muhType.GetField("x").GetValue(muhInstance), 
                                    (int)muhType.GetField("y").GetValue(muhInstance),
                                    (int)muhType.GetField("dist").GetValue(muhInstance));
            return toR;
        }

        public object createMuh(muchie fromMuchie)
        {
            object toR = Activator.CreateInstance(muhType, fromMuchie.x, fromMuchie.y, fromMuchie.dist);
            return toR;
        }

        public void setMembers()
        {
            Type listType = typeof(List<>).MakeGenericType(new[] { muhType });

            setValue("n", graf.noduri.Count);
            object debugn = getValue("n");

            #region Set Graph

            Array muhs = Array.CreateInstance(listType, graf.noduri.Count+1);            

            for (int i = 0; i < graf.noduri.Count; i++)
            {
                IList muh = (IList)Activator.CreateInstance(listType);
                for (int j = 0; j < graf.noduri[i].vec.Count; j++)
                {
                    Muchie vMuchie = graf.noduri[i].vec[j];  
                    muchie crt;
                    if (i + 1 == vMuchie.x.num())
                        crt = new muchie(vMuchie.x.num(), vMuchie.y.num(), vMuchie.dist);
                    else
                        crt = new muchie(vMuchie.y.num(), vMuchie.x.num(), vMuchie.dist);
                    muh.Add(createMuh(crt));
                }    
                muhs.SetValue(Convert.ChangeType(muh, listType), i+1);
            }            
            setValue("graf", muhs);

            #endregion

            watchMuchii = Activator.CreateInstance(listType);
            setValue("watchMuchii", watchMuchii);          

            watchSelected = Activator.CreateInstance(listType);
            setValue("watchSelected", watchSelected);

            int x = 8;
            Array nodesInfo = Array.CreateInstance(x.GetType(), graf.noduri.Count + 1);
            for (int i = 1; i <= graf.noduri.Count; i++)
                nodesInfo.SetValue(i, i);
           
            setValue("nodesInfo", nodesInfo);

        }

        private List<muchie> createList(object muhList)
        {
            List<muchie> toR = new List<muchie>();
            IList adapter = (IList)muhList;
            for (int i = 0; i < adapter.Count; i++)
                toR.Add(createMuchie(adapter[i]));
            return toR;
        }

        public void callExecute()
        {
            accesMethod("executa", new object[]{});
        }

        public List<muchie> getWatchMuchii()
        {
            return createList(getValue("watchMuchii"));
        }

        public List<muchie> getWatchSelected()
        {
            return createList(getValue("watchSelected"));
        }

        public String getExplicatii()
        {
            return (String)getValue("Explicatii");
        }

        public int[] getNodesInfo()
        {
            return (int[]) getValue("nodesInfo");
        }

        public bool isFinished()
        {
            return (bool) getValue("isFinished");
        }

        public List<KeyValuePair<int, int>> getColorChanges()
        {
            return (List<KeyValuePair<int, int>>)getValue("colorChanges");
        }

        public void eraseColorChanges()
        {
            ((List<KeyValuePair<int, int>>)getValue("colorChanges")).Clear();
        }

    }
}
