public delegate TOutput DaoCall<TInput, TOutput>(TInput input );
		public TOutput Try<TInput, TOutput>(DaoCall<TInput, TOutput> prova, TInput k) {
			try{
				
				TOutput res =  prova(k);
				return res;
			}catch(Exception e){
				throw e;
			}
		}