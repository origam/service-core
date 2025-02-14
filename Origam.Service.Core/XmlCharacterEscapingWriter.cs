#region license
/*
Copyright 2005 - 2025 Advantage Solutions, s. r. o.

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
using System.Xml;

namespace Origam.Service.Core;

public class XmlCharacterEscapingWriter : XmlWriter
{
    private readonly XmlWriter innerWriter;
    private static readonly char ReplacementChar = '\uE000';

    public XmlCharacterEscapingWriter(XmlWriter innerWriter)
    {
        this.innerWriter = innerWriter ?? throw new ArgumentNullException(
            nameof(innerWriter));
    }

    public override void WriteString(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            innerWriter.WriteString(text);
            return;
        }
        string transformedText = ReplaceInvalidXmlChars(text);
        innerWriter.WriteString(transformedText);
    }

    private static string ReplaceInvalidXmlChars(string text)
    {
        Span<char> buffer = text.Length <= 256 
            ? stackalloc char[text.Length] : new char[text.Length];
        int writeIndex = 0;
        bool modified = false;
        foreach (char ch in text)
        {
            if (XmlConvert.IsXmlChar(ch))
            {
                buffer[writeIndex++] = ch;
            }
            else
            {
                modified = true;
                buffer[writeIndex++] = ReplacementChar;
            }
        }
        return modified 
            ? new string(buffer.Slice(0, writeIndex).ToArray()) 
            : text;
    }

    public override void WriteSurrogateCharEntity(char lowChar, char highChar)
        => innerWriter.WriteSurrogateCharEntity(lowChar, highChar);
    
    public override void WriteStartElement(
        string prefix, string localName, string ns) 
        => innerWriter.WriteStartElement(prefix, localName, ns);

    public override void WriteEndDocument() => innerWriter.WriteEndDocument();

    public override void WriteEndElement() => innerWriter.WriteEndElement();
    
    public override void WriteEntityRef(string name) 
        => innerWriter.WriteEntityRef(name);
    
    public override void WriteFullEndElement() 
        => innerWriter.WriteFullEndElement();
    
    public override void WriteStartAttribute(
        string prefix, string localName, string ns) 
        => innerWriter.WriteStartAttribute(prefix, localName, ns);
    
    public override void WriteStartDocument() 
        => innerWriter.WriteStartDocument();
    
    public override void WriteStartDocument(bool standalone)
        => innerWriter.WriteStartDocument(standalone);
    
    public override void WriteEndAttribute() => innerWriter.WriteEndAttribute();
    
    public override void WriteCData(string text) 
        => innerWriter.WriteCData(text);
    
    public override void WriteCharEntity(char ch)
        => innerWriter.WriteCharEntity(ch);

    public override void WriteChars(char[] buffer, int index, int count)
        => innerWriter.WriteChars(buffer, index, count);

    public override void WriteComment(string text) 
        => innerWriter.WriteComment(text);

    public override void WriteDocType(
        string name, string pubid, string sysid, string subset)
        => innerWriter.WriteDocType(name, pubid, sysid, subset);

    public override void WriteProcessingInstruction(string name, string text) 
        => innerWriter.WriteProcessingInstruction(name, text);

    public override void WriteRaw(char[] buffer, int index, int count)
        => innerWriter.WriteRaw(buffer, index, count);

    public override void WriteRaw(string data) => innerWriter.WriteRaw(data);
    
    public override void WriteBase64(byte[] buffer, int index, int count) 
        => innerWriter.WriteBase64(buffer, index, count);
    public override void Flush() => innerWriter.Flush();
    
    public override string LookupPrefix(string ns) 
        => innerWriter.LookupPrefix(ns);

    public override void WriteWhitespace(string ws) 
        => innerWriter.WriteWhitespace(ws);

    public override WriteState WriteState => innerWriter.WriteState;
    
    public override void Close() => innerWriter.Close();
}