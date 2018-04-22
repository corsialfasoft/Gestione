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
}