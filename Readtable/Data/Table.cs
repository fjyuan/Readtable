using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Readtable
{
	public class Table<K, R> where R : IRecord<K>, new ()
	{	
		string filename, datfile, binfile;
		Dictionary<K, R> records;
		
		public Table (string path)
		{
			records = new Dictionary<K, R> (); 
			Load (path);
		}
		
		void Load (string path)
		{ 
			filename = path; 
			if (filename == null || filename == string.Empty || !File.Exists (filename)) {
				return ;
			}  
			datfile = filename.Replace (".txt", ".dat"); 
			binfile = filename.Replace (".txt", ".bin");
			Stream stream = File.Open (filename, FileMode.Open);
			TextReader reader = new StreamReader (stream);
			string fields = reader.ReadLine ();  
			Logger.D (string.Format ("Load table: {0} \nwith fields: {1}.", filename, fields)); 
			string table = reader.ReadToEnd ();  
			stream.Close ();  
			string[] strRecords = table.Split (new string[1] {"\r\n"}, StringSplitOptions.RemoveEmptyEntries);    
			foreach (string record in strRecords) {
				R r = new R ();
				if (!　r.Parse (record)) {
				}　
				records [r.Key ()] = r; 
			}
		}
		
		public R Get (K k)
		{
			return records [k];
		}
		
		public void Serialize ()
		{ 
			Stream s = File.Open (datfile, FileMode.Create);		
			BinaryFormatter formatter = new BinaryFormatter ();
			formatter.Serialize (s, records); 
			s.Close ();  
		}
		
		public void Deserialize ()
		{
			records = null;
			Stream s = File.Open (datfile, FileMode.Open);		
			BinaryFormatter formatter = new BinaryFormatter ();
			records = formatter.Deserialize (s) as Dictionary<K, R>;   
			foreach (K k in records.Keys) {
				R r = records [k]; 
			} 
			s.Close ();  
		}
		
		public void SaveBinary ()
		{
			Stream s = File.Open (binfile, FileMode.Create);
			BinaryWriter writer = new BinaryWriter (s); 
			int count = records.Count; 
			writer.Write (count); 
			Dictionary<K, R>.KeyCollection keys = records.Keys;
			foreach (K k in keys) {
				R r = records [k];
				r.Save (ref writer);
			} 
			s.Close ();
		}
		
		public void LoadBinary ()
		{
			records = null;
			records = new Dictionary<K, R> (); 
			Stream s = File.Open (binfile, FileMode.Open);
			BinaryReader reader = new BinaryReader (s); 
			int count = reader.ReadInt32 ();
			for (int i = 0; i < count; ++i) {
				R r = new R ();
				r.Read (ref reader); 
				records [r.Key ()] = r;
			}
			s.Close ();
		}
	}
}

