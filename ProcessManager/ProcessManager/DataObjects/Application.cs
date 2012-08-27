using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace ProcessManager.DataObjects
{
	public class Application : IIDObject, IXmlSerializable
	{
		public Application()
		{
			Sources = new List<DistributionSource>();
		}

		public Application(string name) : this()
		{
			ID = Guid.NewGuid();
			Name = name;
			RelativePath = string.Empty;
			Arguments = string.Empty;
		}

		public Application(XmlReader reader) : this()
		{
			ReadXml(reader);
		}

		#region Properties

		public Guid ID { get; set; }
		public string Name { get; set; }
		public string RelativePath { get; set; }
		public string Arguments { get; set; }
		public List<DistributionSource> Sources { get; set; }

		#endregion

		public Application Clone()
		{
			Application application = new Application() { ID = ID, Name = Name, RelativePath = RelativePath, Arguments = Arguments };
			application.Sources.AddRange(Sources.Select(source => source.Clone()));
			return application;
		}

		#region Equality

		public bool Equals(string name)
		{
			return (Name != null && name != null && name.Equals(Name, StringComparison.CurrentCultureIgnoreCase));
		}

		#endregion

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
			if (reader.IsEmptyElement)
				return;

			while (reader.Read())
			{
				if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "Application")
					break;

				if (reader.NodeType == XmlNodeType.Element && reader.Name == "Source")
					Sources.Add(new DistributionSource(reader));

				reader.MoveToElement();
			}
		}

		public void WriteXml(XmlWriter writer)
		{
			if (ID == Guid.Empty) ID = Guid.NewGuid();

			writer.WriteStartElement("Application");
			writer.WriteAttributeString("ID", ID.ToString());
			writer.WriteAttributeString("Name", Name);
			writer.WriteAttributeString("RelativePath", RelativePath);
			writer.WriteAttributeString("Arguments", Arguments);
			foreach (DistributionSource source in Sources)
				source.WriteXml(writer);
			writer.WriteEndElement();
		}

		#endregion
	}
}
