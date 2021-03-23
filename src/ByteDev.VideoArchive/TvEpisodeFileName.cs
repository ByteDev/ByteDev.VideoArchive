using System.IO;

namespace ByteDev.VideoArchive
{
    /// <summary>
    /// Represents a TV series episode file.
    /// </summary>
    public class TvEpisodeFileName : VideoFileName
    {
        /// <summary>
        /// Series name.
        /// </summary>
        public string SeriesName { get; }
    
        /// <summary>
        /// Episode number information.
        /// </summary>
        public TvEpisodeNumber EpisodeNumber { get; }

        /// <summary>
        /// Episode name.
        /// </summary>
        public string EpisodeName { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ByteDev.VideoArchive.TvEpisodeFile" /> class.
        /// </summary>
        /// <param name="fileName">File name.</param>
        /// <exception cref="T:System.ArgumentException"><paramref name="fileName" /> was null or empty.</exception>
        /// <exception cref="T:System.FormatException">Invalid file name format. Missing opening square bracket ([).</exception>
        /// <exception cref="T:System.FormatException">Invalid file name format. Missing closing square bracket (]).</exception>
        public TvEpisodeFileName(string fileName) : base(fileName)
        {
            SeriesName = GetSeriesName(fileName);
            EpisodeNumber = GetEpisodeNumberInfo(fileName);
            EpisodeName = GetEpisodeName(fileName);
        }

        private string GetSeriesName(string fileName)
        {
            return fileName.Substring(0, IndexOpenSqBracket).Trim();
        }

        private TvEpisodeNumber GetEpisodeNumberInfo(string fileName)
        {
            var episodeInfo = fileName
                .Substring(IndexOpenSqBracket + 1, IndexCloseSqBracket - IndexOpenSqBracket - 1)
                .Trim();

            return new TvEpisodeNumber(episodeInfo);
        }

        private string GetEpisodeName(string fileName)
        {
            return Path.GetFileNameWithoutExtension(fileName)
                .Substring(IndexCloseSqBracket + 1)
                .Trim();
        }
    }
}