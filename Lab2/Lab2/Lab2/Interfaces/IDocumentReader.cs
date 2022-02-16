using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.DocumentReader
{
    interface IDocumentReader
    {
        public StreamReader OpenDocument(string path);
        public string GetAllText(string text, StreamReader reader);
    }
}
