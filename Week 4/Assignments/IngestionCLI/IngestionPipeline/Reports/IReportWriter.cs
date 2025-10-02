    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingestion.Pipeline.Reports
{
    /// <summary>
    /// Report writer contract. Implementations produce output (file, stream) for a collection of T.
    /// </summary>
    public interface IReportWriter<T>
    {
        void WriteReport(T report, string path);
    }
}
