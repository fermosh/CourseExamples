using System;
using System.Linq;

using System.Threading;
					
public class Program
{
	class Whatever : IDisposable{
        public int ID {get;private set;}
        public Whatever(int id) => ID = id;
        Guid[] arr = Enumerable.Range(1,10000).Select(x=>Guid.NewGuid()).ToArray();
		public void Dispose(){
			Dispose(true);
            GC.SuppressFinalize(this);
		}
        public void Dispose(bool disposing){
            Console.WriteLine("I release managed resources here...");
            if(disposing)
                Console.WriteLine($"Disposing {ID} (with pattern)...");
        }
		~Whatever(){
			Console.WriteLine($"Finalizing {ID}...");
            Dispose(false);
		}
	}
	public static void Main()
	{
		var w = new Whatever(0);
		
		Console.WriteLine("Hello World");
		w = null;
        GC.Collect();
		for(int i = 1 ; i < 2000; i++){
			Thread.Sleep(100);
             using(var test =  new Whatever(i) ){
                Console.Write(".");
             }
		}
	}
}