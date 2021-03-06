﻿using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using ProcessManager.DataObjects.Comparers;

namespace ProcessManager.DataObjects
{
	public class Group : IIDObject, IXmlSerializable
	{
		public Group()
		{
			Applications = new List<Guid>();
		}

		public Group(string name) : this()
		{
			ID = Guid.NewGuid();
			Name = name;
			Path = string.Empty;
		}

		public Group(XmlReader reader) : this()
		{
			ReadXml(reader);
		}

		#region Properties

		public Guid ID { get; set; }
		public string Name { get; set; }
		public string Path { get; set; }
		public List<Guid> Applications { get; }

		#endregion

		public Group Clone()
		{
			Group group = new Group() { ID = ID, Name = Name, Path = Path };
			group.Applications.AddRange(Applications);
			return group;
		}

		public Group Copy(string name = null)
		{
			Group group = Clone();
			group.ID = Guid.NewGuid();
			if (name != null) group.Name = name;
			return group;
		}

		#region Equality

		public bool Equals(string name)
		{
			return Comparer.GroupsEqual(this, name);
		}

		public override bool Equals(object obj)
		{
			Group group = obj as Group;
			return @group != null && Comparer.GroupsEqual(this, @group);
		}

		public override int GetHashCode()
		{
			return Comparer.GetHashCode(this);
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
