using System;
using NUnit.Framework;

namespace ByteDev.VideoArchive.UnitTests
{
    [TestFixture]
    public class VideoFileNameTests
    {
        [TestFixture]
        public class Constructor : VideoFileNameTests
        {
            [TestCase(null)]
            [TestCase("")]
            public void WhenIsNullOrEmpty_ThenThrowException(string fileName)
            {
                Assert.Throws<ArgumentException>(() => _ = new MovieFileName(fileName));
            }

            [Test]
            public void WhenFileNameIsValid_ThenSetProperties()
            {
                const string fileName = "The Dark Knight [2008] (IMAX) (UHD).mkv";

                var sut = new MovieFileName(fileName);

                Assert.That(sut.FileName, Is.EqualTo(fileName));
            }
        }

        [TestFixture]
        public class FileExtension : VideoFileNameTests
        {
            [Test]
            public void WhenHasFileNameExtension_ThenSet()
            {
                var sut = new MovieFileName("The Dark Knight [2008] (IMAX) (UHD).mkv");

                Assert.That(sut.FileExtension, Is.EqualTo("mkv"));
            }

            [Test]
            public void WhenHasNoFileNameExtension_ThenSetEmpty()
            {
                var sut = new MovieFileName("The Dark Knight [2008] (IMAX) (UHD)");

                Assert.That(sut.FileExtension, Is.Empty);
            }
        }

        [TestFixture]
        public class ToStringOverride : VideoFileNameTests
        {
            [Test]
            public void WhenCalled_ThenReturnString()
            {
                const string fileName = "The Dark Knight [2008] (IMAX) (UHD).mkv";

                var sut = new MovieFileName(fileName);

                var result = sut.ToString();

                Assert.That(result, Is.EqualTo(fileName));
            }
        }
    }
}