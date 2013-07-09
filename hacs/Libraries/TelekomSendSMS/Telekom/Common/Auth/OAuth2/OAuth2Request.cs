// This file is part of the Telekom .NET SDK
// Copyright 2010 Deutsche Telekom AG
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telekom.Common.Model;
using Telekom.Common.Model.Validation;

//! Internal classes for OAuth 2.0 handshake
namespace Telekom.Common.Auth.OAuth2
{
    /// <summary>
    /// Base class for OAuth requests
    /// </summary>
    internal class OAuth2Request : TelekomRequest
    {
        [HttpParameter("client_id")]
        [Required]
        public string ClientId { get; set; }

        [HttpParameter("client_secret")]
        public string ClientSecret { get; set; }
    }
}
