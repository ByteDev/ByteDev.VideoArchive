using System;
using System.Linq;
using ByteDev.Collections;
using NUnit.Framework;

namespace ByteDev.VideoArchive.UnitTests
{
    [TestFixture]
    public class MovieFileNameTests
    {
        [TestFixture]
        public class Title : MovieFileNameTests
        {
            [TestCase("[2008].mkv")]
            [TestCase(" [2008].mkv")]
            public void WhenNoTitle_ThenSetEmpty(string fileName)
            {
                var sut = new MovieFileName(fileName);

                Assert.That(sut.Title, Is.Empty);
            }

            [TestCase("The Dark Knight [2008].mkv")]
            [TestCase(" The Dark Knight  [2008].mkv")]
            [TestCase("The Dark Knight[2008].mkv")]
            public void WhenTitleIsValid_ThenSetProperties(string fileName)
            {
                var sut = new MovieFileName(fileName);

                Assert.That(sut.Title, Is.EqualTo("The Dark Knight"));
            }
        }

        [TestFixture]
        public class Year : MovieFileNameTests
        {
            [TestCase("The Dark Knight.mkv")]
            [TestCase("The Dark Knight (IMAX) (UHD).mkv")]
            [TestCase("The Dark Knight [] (IMAX) (UHD).mkv")]
            [TestCase("The Dark Knight [A] (IMAX) (UHD).mkv")]
            [TestCase("The Dark Knight 2008] (IMAX) (UHD).mkv")]
            [TestCase("The Dark Knight [2008 (IMAX) (UHD).mkv")]
            public void WhenYearIsInvalid_ThenThrowException(string fileName)
            {
                Assert.Throws<FormatException>(() => _ = new MovieFileName(fileName));
            }

            [TestCase("The Dark Knight [2008].mkv")]
            [TestCase("The Dark Knight [ 2008 ] (IMAX) (UHD).mkv")]
            public void WhenYearIsValid_ThenSetYear(string fileName)
            {
                var sut = new MovieFileName(fileName);

                Assert.That(sut.Year, Is.EqualTo(2008));
            }
        }

        [TestFixture]
        public class ExtraDetails : MovieFileNameTests
        {
            [TestCase("The Dark Knight [2008].mkv")]
            [TestCase("The Dark Knight [2008] .mkv")]
            [TestCase("The Dark Knight [2008] UHD.mkv")]
            [TestCase("The Dark Knight [2008] (UHD.mkv")]
            [TestCase("The Dark Knight [2008] UHD).mkv")]
            public void WhenHasNoExtraDetails_ThenSetEmpty(string fileName)
            {
                var sut = new MovieFileName(fileName);

                Assert.That(sut.Title, Is.EqualTo("The Dark Knight"));
                Assert.That(sut.Year, Is.EqualTo(2008));
                Assert.That(sut.ExtraDetails, Is.Empty);
                Assert.That(sut.FileName, Is.EqualTo(fileName));
                Assert.That(sut.FileExtension, Is.EqualTo("mkv"));
            }

            [TestCase("The Dark Knight [2008] (IMAX).mkv")]
            [TestCase("The Dark Knight [2008]  ( IMAX) .mkv")]
            public void WhenHasOneExtraDetail_ThenSet(string fileName)
            {
                var sut = new MovieFileName(fileName);

                Assert.That(sut.ExtraDetails.Single(), Is.EqualTo("IMAX"));
            }

            [TestCase("The Dark Knight [2008] (IMAX) (UHD).mkv")]
            [TestCase("The Dark Knight [2008]  ( IMAX )  ( UHD ) .mkv")]
            public void WhenHasTwoExtraDetails_ThenSet(string fileName)
            {
                var sut = new MovieFileName(fileName);

                Assert.That(sut.ExtraDetails.First(), Is.EqualTo("IMAX"));
                Assert.That(sut.ExtraDetails.Second(), Is.EqualTo("UHD"));
            }
        }
    }
}