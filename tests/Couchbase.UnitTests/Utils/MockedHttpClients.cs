using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Couchbase.Analytics;
using Couchbase.Core;
using Couchbase.Core.Diagnostics.Metrics;
using Couchbase.Core.Diagnostics.Tracing;
using Couchbase.Core.IO.HTTP;
using Couchbase.Core.IO.Serializers;
using Couchbase.Core.Logging;
using Couchbase.Query;
using Couchbase.Search;
using Couchbase.UnitTests.Helpers;
using Couchbase.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;

namespace Couchbase.UnitTests.Utils
{
    internal static class MockedHttpClients
    {
        public static IQueryClient QueryClient([NotNull] Queue<Task<HttpResponseMessage>> responses,
            bool enableEnhancedPreparedStatements)
        {
            var handlerMock = new Mock<HttpMessageHandler>();
            handlerMock.Protected().Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()).Returns(responses.Dequeue);

            var httpClientFactory = new MockHttpClientFactory(() => new HttpClient(handlerMock.Object, false)
            {
                BaseAddress = new Uri("http://localhost:8091")
            });

            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging(builder => builder
                .AddFilter(level => level >= LogLevel.Debug)
            );
            var loggerFactory = serviceCollection.BuildServiceProvider().GetService<ILoggerFactory>();
            loggerFactory.AddFile("Logs/myapp-{Date}.txt", LogLevel.Debug);

            var mockServiceUriProvider = new Mock<IServiceUriProvider>();
            mockServiceUriProvider
                .Setup(m => m.GetRandomQueryUri())
                .Returns(new Uri("http://localhost:8093"));

            var serializer = new DefaultSerializer();
            return new QueryClient(httpClientFactory, mockServiceUriProvider.Object, serializer,
                new Mock<ILogger<QueryClient>>().Object, NoopRequestTracer.Instance)
            {
                EnhancedPreparedStatementsEnabled = enableEnhancedPreparedStatements
            };
        }

        internal static IAnalyticsClient AnalyticsClient([NotNull] Queue<Task<HttpResponseMessage>> responses)
        {
            var handlerMock = new Mock<HttpMessageHandler>();
            handlerMock.Protected().Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()).Returns(responses.Dequeue);

            var httpClientFactory = new MockHttpClientFactory(() => new HttpClient(handlerMock.Object, false)
            {
                BaseAddress = new Uri("http://localhost:8091")
            });

            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging(builder => builder
                .AddFilter(level => level >= LogLevel.Debug)
            );
            var loggerFactory = serviceCollection.BuildServiceProvider().GetService<ILoggerFactory>();
            loggerFactory.AddFile("Logs/myapp-{Date}.txt", LogLevel.Debug);

            var mockServiceUriProvider = new Mock<IServiceUriProvider>();
            mockServiceUriProvider
                .Setup(m => m.GetRandomAnalyticsUri())
                .Returns(new Uri("http://localhost:8095"));

            var serializer = new DefaultSerializer();
            return new AnalyticsClient(httpClientFactory, mockServiceUriProvider.Object, serializer,
                new Mock<ILogger<AnalyticsClient>>().Object, NoopRequestTracer.Instance);
        }

        internal static ISearchClient SearchClient([NotNull] Queue<Task<HttpResponseMessage>> responses)
        {
            var handlerMock = new Mock<HttpMessageHandler>();
            handlerMock.Protected().Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()).Returns(responses.Dequeue);

            var httpClientFactory = new MockHttpClientFactory(() => new HttpClient(handlerMock.Object, false)
            {
                BaseAddress = new Uri("http://localhost:8091")
            });

            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging(builder => builder
                .AddFilter(level => level >= LogLevel.Debug)
            );
            var loggerFactory = serviceCollection.BuildServiceProvider().GetService<ILoggerFactory>();
            loggerFactory.AddFile("Logs/myapp-{Date}.txt", LogLevel.Debug);

            var mockServiceUriProvider = new Mock<IServiceUriProvider>();
            mockServiceUriProvider
                .Setup(m => m.GetRandomSearchUri())
                .Returns(new Uri("http://localhost:8094"));

            return new SearchClient(httpClientFactory, mockServiceUriProvider.Object,
                new Mock<ILogger<SearchClient>>().Object, NoopRequestTracer.Instance);
        }

        internal static IViewClient ViewClient([NotNull] Queue<Task<HttpResponseMessage>> responses)
        {
            var handlerMock = new Mock<HttpMessageHandler>();
            handlerMock.Protected().Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()).Returns(responses.Dequeue);

            var httpClientFactory = new MockHttpClientFactory(() => new HttpClient(handlerMock.Object, false)
            {
                BaseAddress = new Uri("http://localhost:8091")
            });

            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging(builder => builder
                .AddFilter(level => level >= LogLevel.Debug)
            );
            var loggerFactory = serviceCollection.BuildServiceProvider().GetService<ILoggerFactory>();
            loggerFactory.AddFile("Logs/myapp-{Date}.txt", LogLevel.Debug);

            var serializer = new DefaultSerializer();
            return new ViewClient(httpClientFactory, serializer, new Mock<ILogger<ViewClient>>().Object,
                new Mock<IRedactor>().Object, NoopRequestTracer.Instance);
        }
    }
}
