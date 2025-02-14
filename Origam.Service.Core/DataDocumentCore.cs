#region license
/*
Copyright 2005 - 2021 Advantage Solutions, s. r. o.

This file is part of ORIGAM (http://www.origam.org).

ORIGAM is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

ORIGAM is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with ORIGAM. If not, see <http://www.gnu.org/licenses/>.
*/
#endregion

using System.Data;
using System.Xml;

namespace Origam.Service.Core
{
    public class DataDocumentCore : IDataDocument
    {
        private DataSet dataSet;
        private bool initializedAsDataSet = false;

        public DataDocumentCore()
        {
            dataSet = new DataSet();
        }

        public DataDocumentCore(DataSet dataSet)
        {
            this.dataSet = dataSet;
            initializedAsDataSet = true;
        }

        public DataDocumentCore(XmlDocument xmlDocument) : this()
        {
            WriteToDataSet(xmlDocument);
        }

        private void WriteToDataSet(XmlDocument xmlDocument)
        {
            dataSet.Clear();
            using (XmlReader xmlReader = new XmlNodeReader(xmlDocument))
            {
                Load(xmlReader,true);
            }
        }

        public XmlDocument Xml
        {
            get
            {
                XmlDocument xmlDocument = new XmlDocument();
                using var writer = new XmlCharacterEscapingWriter(
                           xmlDocument.CreateNavigator().AppendChild());
                dataSet.WriteXml(writer);
                return xmlDocument;
                /*
                XmlDocument xmlDocument = new XmlDocument();
                var navigator = xmlDocument.CreateNavigator();
                using (var writer = navigator.AppendChild())
                {
                    dataSet.WriteXml(writer);
                }
                return xmlDocument;
                */
            }
        }

        public DataSet DataSet => dataSet;
        public void AppendChild(XmlNodeType element, string prefix, string name)
        {
            XmlDocument newDocument = Xml;
            XmlNode node = newDocument.CreateNode(element, prefix, name);
            newDocument.AppendChild(node);
            WriteToDataSet(newDocument);
        }

        public void AppendChild(XmlElement documentElement, bool deep)
        {
            XmlDocument newDocument = Xml;
            XmlNode node = newDocument.ImportNode(documentElement, deep);
            newDocument.AppendChild(node);
            WriteToDataSet(newDocument);
        }

        public void DocumentElementAppendChild(XmlNode node)
        {
            XmlDocument newDocument = Xml;
            XmlNode newNode = newDocument.ImportNode(node, true);
            newDocument.DocumentElement.AppendChild(newNode);
            WriteToDataSet(newDocument);
        }

        public void Load(XmlReader xmlReader, bool doProcessing)
        {
            XmlReadMode mode = XmlReadMode.Auto;
            if (initializedAsDataSet)
            {
                mode = XmlReadMode.IgnoreSchema;
            }
            bool enforceConstraints = dataSet.EnforceConstraints;
            dataSet.EnforceConstraints = false;
            dataSet.ReadXml(doProcessing ? new XmlReaderCore(xmlReader) : xmlReader, mode);
            if (enforceConstraints)
            {
                dataSet.EnforceConstraints = true;
            }
        }

        public void LoadXml(string xmlString)
        {
            XmlDocument newDocument = Xml;
            newDocument.LoadXml(xmlString);
            WriteToDataSet(newDocument);
        }

        public object Clone()
        {
            return new DataDocumentCore(dataSet);
        }

        public override string ToString()
        {
            return Xml.OuterXml;
        }
    }
}
