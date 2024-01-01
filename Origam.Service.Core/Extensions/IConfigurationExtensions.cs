#region license
/*
Copyright 2005 - 2024 Advantage Solutions, s. r. o.

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
using Microsoft.Extensions.Configuration;

namespace Origam.Extensions
{
    public static class IConfigurationExtensions
    {
        public static IConfigurationSection GetSectionOrThrow(this IConfiguration configuration, string key)
        {
            var section = configuration.GetSection(key);
            if (!section.Exists())
            {
                throw new ArgumentException($"Section \"{key}\" was not found in configuration. Check your appsettings.json");
            }
            return section;
        }
        
        public static string[] GetStringArrayOrThrow(this IConfiguration section)
        {
            string[] stringArray = section.Get<string[]>();
            if (stringArray == null || stringArray.Length == 0)
            {
                throw new ArgumentException($"String array in section \"{section}\" was not found in configuration or was empty. Check your appsettings.json");
            }
            return stringArray;
        }        
        
        public static string[] GetStringArrayOrEmpty(this IConfiguration section)
        {
            string[] stringArray = section.Get<string[]>();
            if (stringArray == null || stringArray.Length == 0)
            {
                return Array.Empty<string>();
            }
            return stringArray;
        }
    }
}