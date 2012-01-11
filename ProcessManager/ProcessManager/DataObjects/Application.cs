using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace ProcessManager.DataObjects
{
	public class Application : IXmlSerializable
	{
		public Application() { }

		public Application(XmlReader reader) : this()
		{
			ReadXml(reader);
		}

		#region Properties

		public Guid ID { get; set; }
		public string Name { get; set; }
		public string RelativePath { get; set; }
		public string Arguments { get; set; }

		#endregion

		#region Implementation of IXmlSerializable

		public XmlSchema GetSchema()
		{
			return null;
		}

		public void ReadXml(XmlReader reader)
		{
			if (!reader.IsStartElement())
				reader.ReadStartElement("Application");

			while (reader.MoveToNextAttribute())
			{
				switch (reader.Name)
				{
					case "ID":
						ID = new Guid(reader.Value);
						break;
					case "Name":
						Name = reader.Value;
						break;
					case "RelativePath":
						RelativePath = reader.Value;
						break;
					case "Arguments":
						Arguments = reader.Value;
						break;
				}
			}

			reader.MoveToElement();
		}

		public void WriteXml(XmlWriter writer)
		{
			if (ID == Guid.Empty) ID = Guid.NewGuid();

			writer.WriteStartElement("Application");
			writer.WriteAttributeString("ID", ID.ToString());
			writer.WriteAttributeString("Name", Name);
			writer.WriteAttributeString("RelativePath", RelativePath);
			writer.WriteAttributeString("Arguments", Arguments);
			writer.WriteEndElement();
		}

		#endregion
	}
}
