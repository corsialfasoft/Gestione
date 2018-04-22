/* output input
*void 1
 * void 7
 * void 2
 * void 3
 * void 4
 * output 1
 * output 2
 * */




using System;
using System.Reflection;
namespace tre{
public class doh{
public delegate void DaoCall<TInput1>(TInput1 input);
public delegate void DaoCall<TInput1,TInput2>(TInput1 input1, TInput2 input2);
public delegate void DaoCall<TInput1,TInput2, TInput3>(TInput1 input1, TInput2 input2, TInput3 input3);
public delegate void DaoCall<TInput1,TInput2, TInput3, TInput4>(TInput1 input1, TInput2 input2, TInput3 input3, TInput4 input4);
public delegate void DaoCall<TInput1,TInput2, TInput3, TInput4, TInput5>(TInput1 input1, TInput2 input2, TInput3 input3, TInput4 input4, TInput5 input5);
public delegate void DaoCall<TInput1,TInput2, TInput3, TInput4, TInput5, TInput6>(TInput1 input1, TInput2 input2, TInput3 input3, TInput4 input4, TInput5 input5, TInput6 input6);
public delegate void DaoCall<TInput1,TInput2, TInput3, TInput4, TInput5, TInput6, TInput7>(TInput1 input1, TInput2 input2, TInput3 input3, TInput4 input4, TInput5 input5, TInput6 input6, TInput7 input7);

public delegate TOutput DaoCall<TInputa,TInputb, TOutput>(TInputa inputa, TInputb inputb);
public delegate TOutput DaoCall<TInput, TOutput>(TInput input );
		public TOutput Try<TInputa,TInputb, TOutput>(DaoCall<TInputa,TInputb, TOutput> prova, TInputa k, TInputb h) {
			try{
				
				TOutput res =  prova(k,h);
				return res;
			}catch(Exception e){
				throw e;
			}
		}
		public TOutput Try<List<TInput>,TOnput>(DaoCall<List<TInput>, TOutput> prova, TInput k){}
		public TOutput Try<TInput, TOutput>(DaoCall<TInput, TOutput> prova, TInput k) {
			try{
				
				TOutput res =  prova(k);
				return res;
			}catch(Exception e){
				throw e;
			}
		}
	}
    public class doh{
        public delegate TOutput DaoCall<TInput, TOutput>(TInput input );
        public delegate TOutput DaoCall<TInputa,TInputb, TOutput>(TInputa inputa, TInputb inputb);
        public delegate TOutput DaoCall<TInputa,TInputb, TInputc, TOutput>(TInputa inputa, TInputb inputb, TInputc inputc);
        public delegate TOutput DaoCall<TInputa,TInputb, TInputc, TInputd, TOutput>(TInputa inputa, TInputb inputb, TInputc inputc, TInputd inputd);
        public delegate TOutput DaoCall<TInputa,TInputb, TInputc, TInputd, TInpute, TOutput>(TInputa inputa, TInputb inputb, TInputc inputc, TInputd inputd, TInpute inpute);
        public delegate TOutput DaoCall<TInputa, TInputb, TInputc, TInputd, TInpute, TInputf, TOutput>(TInputa inputa, TInputb inputb, TInputc inputc, TInputd inputd, TInpute inpute, TInputf inputf);
        public delegate TOutput DaoCall<TInputa, TInputb, TInputc, TInputd, TInpute, TInputf,TInputg, TOutput>(TInputa inputa, TInputb inputb, TInputc inputc, TInputd inputd, TInpute inpute, TInputf inputf,TInputg inputg);
	    public TOutput Try<TInput, TOutput>(DaoCall<TInput, TOutput> prova, TInput k) {
		    try{
			    TOutput res =  prova(k);
			    return res;
		    }catch(Exception e){
			    throw e;
		    }
	    }
        public TOutput Try<TOutput, TInputa,TInputb>(DaoCall<TInputa, TInputb, TOutput> prova, TInputa inputa, TInputb inputb) {
            try {
                TOutput res = prova(inputa, inputb);
                return res;
            } catch (Exception e) {
                throw e;
            }
        }
        public TOutput Try<TInputa, TInputb, TInputc, TOutput>(DaoCall<TInputa, TInputb, TInputc, TOutput> prova, TInputa inputa, TInputb inputb, TInputc inputc) {
            try {
                TOutput res = prova(inputa, inputb,inputc);
                return res;
            } catch (Exception e) {
                throw e;
            }
        }
        public TOutput Try<TInputa, TInputb, TInputc, TInputd, TOutput>(DaoCall<TInputa, TInputb, TInputc, TInputd, TOutput> prova, TInputa inputa, TInputb inputb, TInputc inputc, TInputd inputd) {
            try {
                TOutput res = prova(inputa, inputb, inputc,inputd);
                return res;
            } catch (Exception e) {
                throw e;
            }
        }
        public TOutput Try<TInputa, TInputb, TInputc, TInputd, TInpute, TOutput>(DaoCall<TInputa, TInputb, TInputc, TInputd, TInpute, TOutput> prova,
            TInputa inputa, TInputb inputb, TInputc inputc, TInputd inputd, TInpute inpute) {
            try {
                TOutput res = prova(inputa, inputb, inputc, inputd, inpute);
                return res;
            } catch (Exception e) {
                throw e;
            }
        }
        public TOutput Try<TInputa, TInputb, TInputc, TInputd, TInpute, TInputf, TOutput>(DaoCall<TInputa, TInputb, TInputc, TInputd, TInpute, TInputf, TOutput> prova,
           TInputa inputa, TInputb inputb, TInputc inputc, TInputd inputd, TInpute inpute, TInputf inputf) {
            try {
                TOutput res = prova(inputa, inputb, inputc, inputd, inpute, inputf);
                return res;
            } catch (Exception e) {
                throw e;
            }
        }
        public TOutput Try<TInputa, TInputb, TInputc, TInputd, TInpute, TInputf, TInputg, TOutput>(DaoCall<TInputa, TInputb, TInputc, TInputd, TInpute, TInputf, TInputg, TOutput> prova,
           TInputa inputa, TInputb inputb, TInputc inputc, TInputd inputd, TInpute inpute, TInputf inputf, TInputg inputg) {
            try {
                TOutput res = prova(inputa, inputb, inputc, inputd, inpute, inputf, inputg);
                return res;
            } catch (Exception e) {
                throw e;
            }
        }
    }
    public static class Command { 
        public static object Excecute(Object obj, string methodName, object[] parameters) {
            Type classType = obj.GetType();
            Type[] types = new Type[parameters.Length];
            for(int i=0;i<parameters.Length;i++) {
                types[i] = parameters[i].GetType();
            }
            MethodInfo method = classType.GetMethod(methodName,types);
            if (method != null) {
                try { 
                    return method.Invoke(obj,parameters);
                }catch(Exception e) { 
                    throw e;
                }
            }
            throw new Exception("Metodo non trovato");
        }
    }
}