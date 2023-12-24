#region license
/*
Copyright 2005 - 2023 Advantage Solutions, s. r. o.

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

using System.Collections;
using Microsoft.Extensions.Configuration;

namespace Origam.Service.Core;

// Interface for authentication providers that can authenticate requests outgoing to external services. 
public interface IClientAuthenticationProvider
{
    // Implementations should return false if they cannot authenticate request to the url.
    // If the request can be authenticated the headers should be modified accordingly.  
    bool TryAuthenticate(string url, Hashtable headers);
    
    // Receives IConfiguration parsed from the appsettings. There are no constraints on what should be read
    // from the configuration. Write what you need to the appsettings.json and read it here.
    // The configuration section for a class implementing this interface should have the same name as the class.
    void Configure(IConfiguration configuration);
}