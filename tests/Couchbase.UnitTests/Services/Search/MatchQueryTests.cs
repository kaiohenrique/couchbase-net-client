using System;
using Couchbase.Services.Search.Queries.Simple;
using Newtonsoft.Json;
using Xunit;

namespace Couchbase.UnitTests.Services.Search
{
    public class MatchQueryQueryTests
    {
        [Fact]
        public void Boost_ReturnsPrefixQuery()
        {
            var query = new MatchQuery("somematchquery").Boost(2.2);

            Assert.IsType<MatchQuery> (query);
        }

        [Fact]
        public void Boost_WhenBoostIsLessThanZero_ThrowsArgumentOutOfRangeException()
        {
            var query = new MatchQuery("somematchquery");

            Assert.Throws<ArgumentOutOfRangeException>(() => query.Boost(-.1));
        }

        [Fact]
        public void Ctor_WhenMatchIsNull_ThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(()=> new MatchQuery(null));
        }

        [Fact]
        public void PrefixLength_WhenLessThanZero_ThrowsArgumentOutOfRangeException()
        {
            var query = new MatchQuery("theterm");

            Assert.Throws<ArgumentOutOfRangeException>(() => query.PrefixLength(-1));
        }

        [Fact]
        public void Fuzziness_WhenLessThanZero_ThrowsArgumentOutOfRangeException()
        {
            var query = new MatchQuery("theterm");

            Assert.Throws<ArgumentOutOfRangeException>(() => query.Fuzziness(-1));
        }

        [Fact]
        public void Export_ReturnsValidJson()
        {
            var query = new MatchQuery("somematchquery")
                .Field("field")
                .PrefixLength(5)
                .Fuzziness(10)
                .Analyzer("analyzer");

            var expected = JsonConvert.SerializeObject(new
            {
                match = "somematchquery",
                prefix_length = 5,
                fuzziness = 10,
                field = "field",
                analyzer = "analyzer"
            }, Formatting.None);

            Assert.Equal(expected, query.Export().ToString(Formatting.None));
        }

        [Fact]
        public void Export_Omits_Field_If_Not_Present()
        {
            var query = new MatchQuery("somematchquery");

            var expected = JsonConvert.SerializeObject(new
            {
                match = "somematchquery",
                prefix_length = 0,
                fuzziness = 0
            }, Formatting.None);

            Assert.Equal(expected, query.Export().ToString(Formatting.None));
        }

        [Fact]
        public void Export_Omits_Analyzer_If_Not_Present()
        {
            var query = new MatchQuery("somematchquery");

            var expected = JsonConvert.SerializeObject(new
            {
                match = "somematchquery",
                prefix_length = 0,
                fuzziness = 0
            }, Formatting.None);

            Assert.Equal(expected, query.Export().ToString(Formatting.None));
        }
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
