using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace VSSCRemove
{
    internal class Remover
    {
        private readonly string[] _sccProps = new[]
        {
            "SccNumberOfProjects",
            "SccLocalPath",
            "CanCheckoutShared",
            "SccProjectUniqueName",
            "SccProjectFilePathRelativizedFromConnection",
            "SccProjectName",
            "SccAuxPath",
            "SccProvider"
        };

        public void Remove(string filename)
        {
            if (filename.EndsWith(".sln", StringComparison.InvariantCultureIgnoreCase))
            {
                var content = Process(File.ReadLines(filename));
                File.WriteAllLines(filename, content);
            }
            else if (filename.EndsWith(".vspscc", StringComparison.InvariantCultureIgnoreCase) ||
                filename.EndsWith(".vssscc", StringComparison.InvariantCultureIgnoreCase))
            {
            	File.Delete(filename);
            }
            else
            {
                var doc = XDocument.Load(filename);
                Process(doc);
                doc.Save(filename);
            }
        }

        private void Process(XDocument doc)
        {
            foreach (var prop in _sccProps)
            {
                var elements = doc.Elements().ToArray();
                foreach (var element in elements)
                {
                    Process(element, prop);
                }
            }
        }

        private void Process(XElement element, string prop)
        {
            if (element.Name.LocalName.Equals(prop, StringComparison.InvariantCultureIgnoreCase))
            {
            	element.Remove();
            }
            else
            {
            	var attrs = element.Attributes().ToArray();
                foreach (var attr in attrs)
                {
                    if (attr.Name.LocalName.Equals(prop, StringComparison.InvariantCultureIgnoreCase))
                    {
                        attr.Remove();
                    }
                }

                var children = element.Elements().ToArray();
                foreach (var child in children)
                {
                    Process(child, prop);
                }
            }
        }

        private const string TextLineExp = @"^.*{0}\d*\s*=.*$";
        private IEnumerable<string> Process(IEnumerable<string> content)
        {
            var lines = content;
            foreach (var prop in _sccProps)
            {
                var regex = new Regex(string.Format(TextLineExp, prop));
                lines = lines.Where(l => !regex.IsMatch(l)).ToArray();
            }

            return lines;
        }
    }
}