using System;
using NUnit.Framework;

namespace ByteDev.VideoArchive.UnitTests
{
    [TestFixture]
    public class TvEpisodeFileNameTests
    {
        [TestFixture]
        public class Constructor : MovieFileNameTests
        {
            [TestCase(null)]
            [TestCase("")]
            public void WhenIsNullOrEmpty_ThenThrowException(string fileName)
            {
                Assert.Throws<ArgumentException>(() => _ = new TvEpisodeFileName(fileName));
            }

            [Test]
            public void WhenFileNameIsValid_ThenSetProperties()
            {
                const string fileName = "The Boys [S01E01] The Name of the Game.mkv";

                var sut = new TvEpisodeFileName(fileName);

                Assert.That(sut.FileName, Is.EqualTo(fileName));
            }
        }

        [TestFixture]
        public class SeriesName : TvEpisodeFileNameTests
        {
            [TestCase("[S01E01].mkv")]
            [TestCase(" [S01E01].mkv")]
            public void WhenNoTitle_ThenSetEmpty(string fileName)
            {
                var sut = new TvEpisodeFileName(fileName);

                Assert.That(sut.SeriesName, Is.Empty);
            }

            [TestCase("The Boys [S01E01].mkv")]
            [TestCase(" The Boys [S01E01].mkv")]
            [TestCase("The Boys[S01E01].mkv")]
            public void WhenTitleIsValid_ThenSetProperties(string fileName)
            {
                var sut = new TvEpisodeFileName(fileName);

                Assert.That(sut.SeriesName, Is.EqualTo("The Boys"));
            }
        }

        [TestFixture]
        public class EpisodeInfo : TvEpisodeFileNameTests
        {
            [Test]
            public void WhenValidEpisodeInfo_ThenSet()
            {
                var sut = new TvEpisodeFileName("The Boys [S01E02].mkv");

                Assert.That(sut.EpisodeNumber.SeasonNumber, Is.EqualTo(1));
                Assert.That(sut.EpisodeNumber.EpisodeNumber, Is.EqualTo(2));
            }
        }

        [TestFixture]
        public class EpisodeName : TvEpisodeFileNameTests
        {
            [TestCase("The Boys [S01E02].mkv")]
            [TestCase("The Boys [S01E02] .mkv")]
            public void WhenNoEpisodeName_ThenSetEmpty(string fileName)
            {
                var sut = new TvEpisodeFileName(fileName);

                Assert.That(sut.EpisodeName, Is.Empty);
            }

            [TestCase("The Boys [S01E01] The Name of the Game.mkv")]
            [TestCase("The Boys [S01E01]  The Name of the Game .mkv")]
            public void WhenEpisodeName_ThenSet(string fileName)
            {
                var sut = new TvEpisodeFileName(fileName);

                Assert.That(sut.EpisodeName, Is.EqualTo("The Name of the Game"));
            }
        }
    }
}