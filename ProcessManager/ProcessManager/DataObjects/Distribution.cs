using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace ProcessManager.DataObjects
{
	public class Distribution : IIDObject, IXmlSerializable
	{
		public Distribution()
		{
			Sources = new List<DistributionSource>();
		}

		public Distribution(string name) : this()
		{
			ID = Guid.NewGuid();
			Name = name;
		}

		public Distribution(XmlReader reader) : this()
		{
			ReadXml(reader);
		}

		#region Properties

		public Guid ID { get; set; }
		public string Name { get; set; }
		public List<DistributionSource> Sources { get; set; }
		public string DestinationPath { get; set; }

		#endregion

		public Distribution Clone()
		{
			Distribution distribution = new Distribution() { ID = ID, Name = Name, DestinationPath = DestinationPath };
			distribution.Sources.AddRange(Sources.Select(source => source.Clone()));
			return distribution;
		}

		#region ToString

		public override string ToString()
		{
			return Name;
		}

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
					case "DestinationPath":
						DestinationPath = reader.Value;
						break;
				}
			}

			reader.MoveToElement();
			if (reader.IsEmptyElement)
				return;

			while (reader.Read())
			{
				if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "Distribution")
					break;

				if (reader.NodeType == XmlNodeType.Element && reader.Name == "Source")
					Sources.Add(new DistributionSource(reader));

				reader.MoveToElement();
			}
		}

		public void WriteXml(XmlWriter writer)
		{
			writer.WriteStartElement("Distribution");
			writer.WriteAttributeString("ID", ID.ToString());
			writer.WriteAttributeString("Name", Name);
			writer.WriteAttributeString("DestinationPath", DestinationPath);
			foreach (DistributionSource source in Sources)
				source.WriteXml(writer);
			writer.WriteEndElement();
		}

		#endregion
	}
}
