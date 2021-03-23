using System;
using System.IO;

namespace ByteDev.VideoArchive
{
    /// <summary>
    /// Represents a video file.
    /// </summary>
    public abstract class VideoFileName
    {
        protected readonly int IndexOpenSqBracket;
        protected readonly int IndexCloseSqBracket;

        /// <summary>
        /// Video file name.
        /// </summary>
        public string FileName { get; }
        
        /// <summary>
        /// Video file extension.
        /// </summary>
        public string FileExtension { get; }

        protected VideoFileName(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentException("File name was null or empty.", nameof(fileName));

            FileName = fileName;
            FileExtension = GetFileExtension(fileName);

            IndexOpenSqBracket = GetOpenSqBracketIndex(fileName);
            IndexCloseSqBracket = GetCloseSqBracketIndex(fileName, IndexOpenSqBracket);
        }

        public override string ToString()
        {
            return FileName;
        }

        private static string GetFileExtension(string fileName)
        {
            string exten = Path.GetExtension(fileName);

            return string.IsNullOrEmpty(exten) ? 
                string.Empty : 
                exten.Replace(".", string.Empty).ToLower();
        }

        private static int GetOpenSqBracketIndex(string fileName)
        {
            var index = fileName.IndexOf("[", StringComparison.Ordinal);

            if (index < 0)
                throw new FormatException("Invalid file name format. Missing opening square bracket ([).");
            
            return index;
        }

        private static int GetCloseSqBracketIndex(string fileName, int indexOpenSqBracket)
        {
            var index = fileName.IndexOf("]", indexOpenSqBracket, StringComparison.Ordinal);

            if (index < 0)
                throw new FormatException("Invalid file name format. Missing closing square bracket (]).");

            return index;
        }
    }
}