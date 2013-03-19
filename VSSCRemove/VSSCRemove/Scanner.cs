using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace VSSCRemove
{
    internal class Scanner
    {
        private readonly string _directory;

        private readonly string[] _patterns = new[] { "*.sln", "*.csproj", "*.vcproj", "*.vbproj", "*.vspscc", "*.vssscc" };

        public Scanner(string directory)
        {
            _directory = directory;
        }

        private IEnumerable<string> SearchFor(string pattern)
        {
        	return Directory.EnumerateFiles(_directory, pattern, SearchOption.AllDirectories);
        }

        public IEnumerable<string> Scan()
        {
            IEnumerable<string> filenames = null;

            for (int i = 0; i < _patterns.Length; i++)
            {
                if (i == 0)
                {
                	filenames = SearchFor(_patterns[i]);
                }
                else
                {
                	filenames = filenames.Concat(SearchFor(_patterns[i]));
                }
            }
 
            foreach (var filename in filenames)
            {
                yield return filename;
            }
        }
    }
}