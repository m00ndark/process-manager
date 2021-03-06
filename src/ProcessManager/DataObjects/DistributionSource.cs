﻿using System;
using System.Globalization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace ProcessManager.DataObjects
{
	public class DistributionSource : IIDObject, IXmlSerializable
	{
		public DistributionSource()
		{
			ID = Guid.NewGuid();
			Path = null;
			Filter = "*";
			Recursive = false;
			Inclusive = true;
		}

		public DistributionSource(XmlReader reader)
		{
			ReadXml(reader);
		}

		#region Properties

		public Guid ID { get; set; }
		public string Path { get; set; }
		public string Filter { get; set; }
		public bool Recursive { get; set; }
		public bool Inclusive { get; set; }

		#endregion

		public DistributionSource Clone()
		{
			return new DistributionSource() { ID = ID, Path = Path, Filter = Filter, Recursive = Recursive, Inclusive = Inclusive };
		}

		public DistributionSource Copy()
		{
			DistributionSource distributionSource = Clone();
			distributionSource.ID = Guid.NewGuid();
			return distributionSource;
		}

		public void CopyTo(DistributionSource source)
		{
			source.ID = ID;
			source.Path = Path;
			source.Filter = Filter;
			source.Recursive = Recursive;
			source.Inclusive = Inclusive;
		}

		#region Implementation of IXmlSerializable

		public XmlSchema GetSchema()
		{
			return null;
		}

		public void ReadXml(XmlReader reader)
		{
			if (!reader.IsStartElement())
				reader.ReadStartElement("Source");

			while (reader.MoveToNextAttribute())
			{
				switch (reader.Name)
				{
					case "ID":
						ID = new Guid(reader.Value);
						break;
					case "Path":
						Path = reader.Value;
						break;
					case "Recursive":
						Recursive = bool.Parse(reader.Value);
						break;
					case "Filter":
						Filter = reader.Value;
						break;
					case "Inclusive":
						Inclusive = bool.Parse(reader.Value);
						break;
				}
			}

			reader.MoveToElement();
		}

		public void WriteXml(XmlWriter writer)
		{
			writer.WriteStartElement("Source");
			writer.WriteAttributeString("ID", ID.ToString());
			writer.WriteAttributeString("Path", Path);
			writer.WriteAttributeString("Recursive", Recursive.ToString(CultureInfo.InvariantCulture));
			writer.WriteAttributeString("Filter", Filter);
			writer.WriteAttributeString("Inclusive", Inclusive.ToString(CultureInfo.InvariantCulture));
			writer.WriteEndElement();
		}

		#endregion
	}
}
