namespace Couchbase.Core.IO.Operations
{
    /// <summary>
    /// Features that they client negotiate on a per connection basis.
    /// </summary>
    internal enum ServerFeatures : short
    {
        /// <summary>
        /// For custom data types
        /// </summary>
        Datatype = 0x01,

        /// <summary>
        /// Enable TCP nodelay
        /// </summary>
        TcpNoDelay = 0x03,

        /// <summary>
        /// Return the sequence number with every mutation
        /// </summary>
        MutationSeqno = 0x04,

        /// <summary>
        /// Disable TCP nodelay
        /// </summary>
        TcpDelay = 0x05,

        /// <summary>
        /// Perform subdocument operations on document attributes
        /// </summary>
        SubdocXAttributes = 0x06,

        /// <summary>
        /// Return extended error information for the client to use in K/V Error Mapping. Implies the client
        /// will request that information from the server to use in mapping error attributes and classes.
        /// </summary>
        XError = 0x07,

        /// <summary>
        /// Indicates if the cluster supports RBAC and if a Select_Bucket operation should
        /// be executed when opening a bucket.
        /// </summary>
        SelectBucket = 0x08,

        /// <summary>
        /// Requests that the connection use Snappy compression on request, and indicates if the server supports
        /// Snappy compression on response.
        /// </summary>
        SnappyCompression = 0x0a,

        /// <summary>
        /// Retrieve the Server Duration of the operation. This enables the server to return responds
        /// with magic <see cref="Magic.AltResponse"/>.
        /// </summary>
        ServerDuration = 0x0f,

        /// <summary>
        /// Indicates if the client can send requests that include Framing Extras encoded into the request packet.
        /// </summary>
        AlternateRequestSupport = 0x10,

        /// <summary>
        /// Indicates if requests can include synchronous replication requirements into framing extras.
        /// NOTE: Requires <see cref="AlternateRequestSupport"/> be enabled too.
        /// </summary>
        SynchronousReplication = 0x11,

        /// <summary>
        /// Indicates if the server supports scoped collections.
        /// </summary>
        Collections = 0x12,

        /// <summary>
        /// Support unordered execution of operations.
        /// </summary>
        UnorderedExecution = 0x0e,

        /// <summary>
        /// Enables the "create as deleted" flag, allowing a document to be created in a tombstoned state.
        /// </summary>
        /// <remarks>Requires Couchbase Server 6.6 or greater.</remarks>
        CreateAsDeleted = 0x17,

        /// <summary>
        /// Enables preserving expiry when updating document.
        /// </summary>
        PreserveTtl = 0x14,

        /// <summary>
        /// Enables JSON as a data type for KV range scans.
        /// </summary>
        JSON = 0x0b
    }
}

#region [ License information          ]

/* ************************************************************
 *
 *    @author Couchbase <info@couchbase.com>
 *    @copyright 2015 Couchbase, Inc.
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

#endregion
