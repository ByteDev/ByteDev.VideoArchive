using System;
using NUnit.Framework;

namespace ByteDev.VideoArchive.UnitTests
{
    [TestFixture]
    public class TvEpisodeNumberTests
    {
        [TestFixture]
        public class Constructor_String : TvEpisodeNumberTests
        {
            [Test]
            public void WhenIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => _ = new TvEpisodeNumber(null));
            }

            [TestCase("01E02")]
            [TestCase("S0102")]
            [TestCase("S01E001")]
            [TestCase("0102")]
            public void WhenIsInvalidFormat_ThenThrowException(string info)
            {
                Assert.Throws<FormatException>(() => _ = new TvEpisodeNumber(info));
            }
        }

        [TestFixture]
        public class Constructor_Ints : TvEpisodeNumberTests
        {
            [TestCase(-1)]
            [TestCase(100)]
            public void WhenSeasonNumberIsInvalid_ThenThrowException(int seasonNumber)
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => _ = new TvEpisodeNumber(seasonNumber, 0));
            }

            [TestCase(-1)]
            [TestCase(100)]
            public void WhenEpisodeNumberIsInvalid_ThenThrowException(int episodeNumber)
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => _ = new TvEpisodeNumber(0, episodeNumber));
            }

            [TestCase(0, 0)]
            [TestCase(1, 1)]
            [TestCase(99, 99)]
            public void WhenValidArgs_ThenSetProperties(int seasonNumber, int episodeNumber)
            {
                var sut = new TvEpisodeNumber(seasonNumber, episodeNumber);

                Assert.That(sut.SeasonNumber, Is.EqualTo(seasonNumber));
                Assert.That(sut.EpisodeNumber, Is.EqualTo(episodeNumber));
            }
        }

        [TestFixture]
        public class ToStringOverride : TvEpisodeNumberTests
        {
            [TestCase("S00E00")]
            [TestCase("S01E02")]
            [TestCase("S99E99")]
            public void WhenCalled_ThenReturnString(string info)
            {
                var sut = new TvEpisodeNumber(info);

                var result = sut.ToString();

                Assert.That(result, Is.EqualTo(info));
            }
        }
    }
}