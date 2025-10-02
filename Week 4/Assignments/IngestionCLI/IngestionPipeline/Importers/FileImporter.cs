using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingestion.Pipeline.Importers
{
    /// <summary>
    /// Base abstraction for importing a file into an enumerable of T.
    /// </summary>
    public abstract class FileImporter<T>
    {
        /// <summary>
        /// Imports the file at <paramref name="path"/> and returns sequence of T.
        /// Implementations should throw meaningful exceptions on error.
        /// </summary>
        public abstract IEnumerable<T> Import(string path);
    }
}
