using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace ProcessManager.DataObjects
{
	public class Group : IXmlSerializable
	{
		public Group()
		{
			Applications = new List<Guid>();
		}

		public Group(XmlReader reader) : this()
		{
			ReadXml(reader);
		}

		#region Properties

		public Guid ID { get; set; }
		public string Name { get; set; }
		public string Path { get; set; }
		public List<Guid> Applications { get; private set; }

		#endregion

		#region Implementation of IXmlSerializable

		public XmlSchema GetSchema()
		{
			return null;
		}

		public void ReadXml(XmlReader reader)
		{
			if (!reader.IsStartElement())
				reader.ReadStartElement("Group");

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
					case "Path":
						Path = reader.Value;
						break;
				}
			}

			reader.MoveToElement();
			if (reader.IsEmptyElement)
				return;

			while (reader.Read())
			{
				if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "Group")
					break;

				if (reader.NodeType != XmlNodeType.Element || reader.Name != "Application" || !reader.MoveToNextAttribute() || reader.Name != "ID")
					continue;

				Applications.Add(new Guid(reader.Value));
				reader.MoveToElement();
			}
		}

		public void WriteXml(XmlWriter writer)
		{
			writer.WriteStartElement("Group");
			writer.WriteAttributeString("ID", ID.ToString());
			writer.WriteAttributeString("Name", Name);
			writer.WriteAttributeString("Path", Path);
			foreach (Guid applicationID in Applications)
			{
				writer.WriteStartElement("Application");
				writer.WriteAttributeString("ID", applicationID.ToString());
				writer.WriteEndElement();
			}
			writer.WriteEndElement();
		}

		#endregion
	}
}
