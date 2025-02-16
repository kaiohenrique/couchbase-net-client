using Couchbase.Core.Compatibility;
using System;

#nullable enable

namespace Couchbase
{
    /// <summary>
    /// Settings to enable various experiments. These experiments may improve performance, but also
    /// may have stability issues. If successful, they will become the standard approach.
    /// </summary>
    public class ExperimentalOptions
    {
        /// <summary>
        /// Use System.Threading.Channels for connection pool distribution.
        /// </summary>
        [InterfaceStability(Level.Volatile)]
        [Obsolete("ChannelConnectionPool is now the default pool since 3.3.0. To revert back to the DataFlowConnectionPool set this to false.")]
        public bool ChannelConnectionPools { get; set; } = true;
    }
}


/* ************************************************************
 *
 *    @author Couchbase <info@couchbase.com>
 *    @copyright 2021 Couchbase, Inc.
 *
 *    Licensed under the Apache License, Version 2.0 (the "License");
 *    you may not use this file except in compliance with the License.
 *    You may obtain a copy of the License at
 *
 *        http://www.apache.org/licenses/LICENSE-2.0
 *
 *    Unless required by applicable law or agreed to in writing, software
 *    distributed under the License is distributed on an "AS IS" BASIS,
 *    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *    See the License for the specific language governing permissions and
 *    limitations under the License.
 *
 * ************************************************************/
