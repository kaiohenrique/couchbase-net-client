using System;
using Couchbase.Core.IO.Connections.Channels;
using Couchbase.Core.Logging;
using Microsoft.Extensions.Logging;

#nullable enable

namespace Couchbase.Core.IO.Connections.Channels
{
    /// <summary>
    /// Default implementation of <see cref="IConnectionPoolFactory"/>.
    /// </summary>
    internal class ChannelConnectionPoolFactory : IConnectionPoolFactory
    {
        private readonly IConnectionFactory _connectionFactory;
        private readonly ClusterOptions _clusterOptions;
        private readonly IConnectionPoolScaleControllerFactory _scaleControllerFactory;
        private readonly IRedactor _redactor;
        private readonly ILogger<ChannelConnectionPool> _channelPoolLogger;

        public ChannelConnectionPoolFactory(IConnectionFactory connectionFactory, ClusterOptions clusterOptions,
            IConnectionPoolScaleControllerFactory scaleControllerFactory, IRedactor redactor,
            ILogger<ChannelConnectionPool> channelPoolLogger)
        {
            _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
            _clusterOptions = clusterOptions ?? throw new ArgumentNullException(nameof(clusterOptions));
            _scaleControllerFactory = scaleControllerFactory ?? throw new ArgumentNullException(nameof(scaleControllerFactory));
            _redactor = redactor ?? throw new ArgumentNullException(nameof(redactor));
            _channelPoolLogger = channelPoolLogger ?? throw new ArgumentNullException(nameof(channelPoolLogger));
        }

        /// <inheritdoc />
        public IConnectionPool Create(ClusterNode clusterNode)
        {
            if (_clusterOptions.NumKvConnections <= 1 && _clusterOptions.MaxKvConnections <= 1)
            {
                _channelPoolLogger.LogInformation("Using the SingleConnectionPool.");

                return new SingleConnectionPool(clusterNode, _connectionFactory);
            }
            else
            {
                _channelPoolLogger.LogInformation("Using the ChannelConnectionPool.");

                var scaleController = _scaleControllerFactory.Create();

                return new ChannelConnectionPool(clusterNode, _connectionFactory, scaleController,
                    _redactor, _channelPoolLogger, (int) _clusterOptions.KvSendQueueCapacity)
                {
                    MinimumSize = _clusterOptions.NumKvConnections,
                    MaximumSize = _clusterOptions.MaxKvConnections
                };
            }
        }
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
