using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Reflection; 

namespace Readtable
{  
	class MainClass
	{
		public static void Main (string[] args)
		{ 
			RecordGenerater.Generate("Actor", new Field[] { 
				new Field(){ type = typeof(uint), name = "id"},
				new Field(){ type = typeof(string), name = "name"},
				new Field(){ type = typeof(string), name = "desc"}, 
			});
			RecordGenerater.Generate("Monster", new Field[] { 
				new Field(){ type = typeof(uint), name = "id"},
				new Field(){ type = typeof(string), name = "name"},
				new Field(){ type = typeof(string), name = "desc"}, 
			});
			string filename = "../../test.txt"; 
			Table<uint, TestRecord> testTable = null;
			TimeConsumed ("LoadTable", delegate() {
				testTable = new Table<uint, TestRecord> (filename);
			}); 
			TimeConsumed ("Serialize", delegate() {
				testTable.Serialize ();
			}); 
			TimeConsumed ("Deserialize", delegate() {
				testTable.Deserialize (); 
			}); 
			TimeConsumed ("SaveBinary", delegate() {
				testTable.SaveBinary ();
			}); 
			TimeConsumed ("LoadBinary", delegate() {
				testTable.LoadBinary ();
			}); 
		}
		
		public delegate void Callback ();
		
		static void TimeConsumed (string name, Callback callback)
		{
			int time = System.DateTime.Now.Millisecond; 
			callback (); 
			int comsumed = System.DateTime.Now.Millisecond - time;
			System.Console.WriteLine ("TimeConsumed with: " + name + " (" + comsumed + "ms)");
		}
	}
}
