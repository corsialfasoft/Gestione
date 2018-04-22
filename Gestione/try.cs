using System;
namespace tre{
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
}