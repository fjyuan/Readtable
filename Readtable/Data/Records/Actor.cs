using System; 
using System.Reflection; 
using System.IO; 


namespace Readtable
{
	[Serializable]
	public class Actor: IRecord<UInt32>
	{
		public System.UInt32 id;
		public System.String name;
		public System.String desc;

		public bool Parse (string record)
		{
			string[] fields = record.Split ('\t');
			if (fields.Length != GetType ().GetFields ().Length) { 
				Logger.D ("Failed to parse a record with: " + record);
				return false;
			}
			id=System.UInt32.Parse(fields[0]);
			name=fields[1];
			desc=fields[2];
			Logger.D (string.Format ("Parse a record with: " + id + "," + name + "," + desc));
			return true;
		}

		public void Save (ref BinaryWriter writer)
		{
			writer.Write(id);
			writer.Write(name);
			writer.Write(desc);
		}

		public void Read (ref BinaryReader reader)
		{
			id = reader.ReadUInt32();
			name = reader.ReadString();
			desc = reader.ReadString();
		}

		public UInt32 Key ()
		{
			return id;
		}
	}
}
