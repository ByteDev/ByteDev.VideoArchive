using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ByteDev.VideoArchive
{
    /// <summary>
    /// Represents a movie file.
    /// </summary>
    public class MovieFileName : VideoFileName
    {
        /// <summary>
        /// Title name.
        /// </summary>
        public string Title { get; }

        /// <summary>
        /// Year of release.
        /// </summary>
        public int Year { get; }

        /// <summary>
        /// Any extra details. If none then will return empty.
        /// </summary>
        public IEnumerable<string> ExtraDetails { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ByteDev.VideoArchive.MovieFile" /> class.
        /// </summary>
        /// <param name="fileName">Movie file name. A valid movie file name will have the format: "Title [YYYY] (Extra Info 1) (Extra Info n).FileExtension".</param>
        /// <exception cref="T:System.ArgumentException"><paramref name="fileName" /> was null or empty.</exception>
        /// <exception cref="T:System.FormatException">Year could not be determined.</exception>
        /// <exception cref="T:System.FormatException">Invalid file name format. Missing opening square bracket ([).</exception>
        /// <exception cref="T:System.FormatException">Invalid file name format. Missing closing square bracket (]).</exception>
        public MovieFileName(string fileName) : base(fileName)
        {
            Title = GetName(fileName);
            Year = GetYear(fileName);
            ExtraDetails = GetExtraDetails(fileName);
        }

        private string GetName(string fileName)
        {
            return fileName.Substring(0, IndexOpenSqBracket).Trim();
        }

        private int GetYear(string fileName)
        {
            var year = fileName.Substring(IndexOpenSqBracket + 1, IndexCloseSqBracket - IndexOpenSqBracket - 1).Trim();

            if (int.TryParse(year, out var result))
                return result;

            throw new FormatException("Year could not be determined. Text between open and close square bracket is not a valid integer.");
        }

        private IEnumerable<string> GetExtraDetails(string fileName)
        {
            var extraDetails = Path.GetFileNameWithoutExtension(fileName)
                .Substring(IndexCloseSqBracket + 1)
                .Trim();

            if (!extraDetails.Contains('(') || !extraDetails.Contains(')'))
                return Enumerable.Empty<string>();
            
            return extraDetails.Split('(', ')')
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .Select(s => s.Trim())
                .ToList();
        }
    }
}