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

using Newtonsoft.Json;
using System;

namespace Origam.Service.Core
{
	public enum RuleExceptionSeverity
	{
		High,
		Low
	}

	/// <summary>
	/// Summary description for RuleExceptionData.
	/// </summary>
	[Serializable]
	public class RuleExceptionData
	{
		public RuleExceptionData() 
		{
		}

		public RuleExceptionData(string message) 
		{
			this.Message = message;
		}

		public RuleExceptionData(string message, RuleExceptionSeverity severity,
			string fieldName, string entityName)
		{
			this.Message = message;
			this.Severity = severity;
			this.FieldName = fieldName;
			this.EntityName = entityName;
		}

		public string FieldName = "";
		public string EntityName = "";
		public string Message = "";
        [JsonIgnore]
        public int HttpStatusCode = 400;
		public RuleExceptionSeverity Severity;

    }
}
