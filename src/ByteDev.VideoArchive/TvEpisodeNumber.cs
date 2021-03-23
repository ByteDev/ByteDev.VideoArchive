using System;

namespace ByteDev.VideoArchive
{
    /// <summary>
    /// Represents episode number information.
    /// </summary>
    public class TvEpisodeNumber
    {
        /// <summary>
        /// Season number.
        /// </summary>
        public int SeasonNumber { get; }

        /// <summary>
        /// Episode number.
        /// </summary>
        public int EpisodeNumber { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ByteDev.VideoArchive.EpisodeInfo" /> class.
        /// </summary>
        /// <param name="episodeInfo">Episode info. Must be in the format: S00E00 (e.g. S01E02).</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="episodeInfo" /> is null.</exception>
        /// <exception cref="T:System.FormatException">Invalid format episode info.</exception>
        public TvEpisodeNumber(string episodeInfo)
        {
            if (episodeInfo == null)
                throw new ArgumentNullException(nameof(episodeInfo));

            if (episodeInfo.Length != 6 ||
                episodeInfo.Substring(0, 1) != "S" ||
                episodeInfo.Substring(3, 1) != "E")
                throw new FormatException("Invalid format episode info.");

            try
            {
                SeasonNumber = int.Parse(episodeInfo.Substring(1, 2));
                EpisodeNumber = int.Parse(episodeInfo.Substring(4, 2));
            }
            catch (Exception ex)
            {
                throw new FormatException("Invalid format episode info.", ex);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ByteDev.VideoArchive.TvEpisodeInfo" /> class.
        /// </summary>
        /// <param name="seasonNumber">Season number.</param>
        /// <param name="episodeNumber">Episode number.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="seasonNumber" /> must be equal or greater than zero.</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="episodeNumber" /> must be equal or greater than zero.</exception>
        public TvEpisodeNumber(int seasonNumber, int episodeNumber)
        {
            if (seasonNumber < 0 || seasonNumber > 99)
                throw new ArgumentOutOfRangeException(nameof(seasonNumber), "Season number must between 0 and 99.");

            if (episodeNumber < 0 || episodeNumber > 99)
                throw new ArgumentOutOfRangeException(nameof(seasonNumber), "Episode number must between 0 and 99.");

            SeasonNumber = seasonNumber;
            EpisodeNumber = episodeNumber;
        }

        public override string ToString()
        {
            return "S" + SeasonNumber.ToString("00") + "E" + EpisodeNumber.ToString("00");
        }
    }
}