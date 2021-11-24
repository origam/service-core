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

using System;
using System.Data;
using System.Xml;

namespace Origam.Service.Core
{
    public class DataDocumentCore : IDataDocument
    {
        private DataSet dataSet;

        public DataDocumentCore()
        {
            dataSet = new DataSet();
        }

        public DataDocumentCore(DataSet dataSet)
        {
            this.dataSet = dataSet;
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
                // dataSet always returns xml, so it's always safe to convert to xml
                xmlDocument.LoadXml(dataSet.GetXml());                
                return xmlDocument;
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
            dataSet.ReadXml(doProcessing ? new XmlReaderCore(xmlReader) : xmlReader);
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
    }
}
