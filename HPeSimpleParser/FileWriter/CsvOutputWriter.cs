using System;
using System.IO;
using System.Threading.Tasks;

namespace HPeSimpleParser
{
    public class CsvOutputWriter : IDisposable
    {

        private readonly CsvDetailGenerator _detailGenerator;
        private readonly CsvLinkGenerator _linkGenerator;
        private readonly CsvMarketingGenerator _marketingGenerator;
        private readonly CsvOptionsGenerator _optionsGenerator;
        private readonly CsvProductGenerator _productGenerator;
        private readonly CsvSpecificationsGenerator _specificationsGenerator;
        private readonly CsvSupplierGenerator _supplierGenerator;

        private readonly TextWriter _detailWriter;
        private readonly TextWriter _linkWriter;
        private readonly TextWriter _marketingWriter;
        private readonly TextWriter _optionsWriter;
        private readonly TextWriter _productWriter;
        private readonly TextWriter _specificationsWriter;
        private readonly TextWriter _supplierWriter;

        public CsvOutputWriter(WriterConfiguration configuration)
        {
            _productGenerator = new CsvProductGenerator();
            _detailGenerator = new CsvDetailGenerator();
            _linkGenerator = new CsvLinkGenerator();
            _marketingGenerator = new CsvMarketingGenerator();
            _optionsGenerator = new CsvOptionsGenerator();
            _productGenerator = new CsvProductGenerator();
            _specificationsGenerator = new CsvSpecificationsGenerator();
            _supplierGenerator = new CsvSupplierGenerator();

            _detailWriter = new StreamWriter(File.OpenWrite(Path.Combine(configuration.OutputPath, "detail.txt")));
            _linkWriter = new StreamWriter(File.OpenWrite(Path.Combine(configuration.OutputPath, "link.txt")));
            _marketingWriter = new StreamWriter(File.OpenWrite(Path.Combine(configuration.OutputPath, "marketing.txt")));
            _optionsWriter = new StreamWriter(File.OpenWrite(Path.Combine(configuration.OutputPath, "options.txt")));
            _productWriter = new StreamWriter(File.OpenWrite(Path.Combine(configuration.OutputPath, "product.txt")));
            _specificationsWriter = new StreamWriter(File.OpenWrite(Path.Combine(configuration.OutputPath, "specifications.txt")));
            _supplierWriter = new StreamWriter(File.OpenWrite(Path.Combine(configuration.OutputPath, "supplier.txt")));


        }
        public async Task WriteAsync(FileTypes fileTypes, Model.Item item)
        {
            if (fileTypes.HasFlag(FileTypes.Detail))
            {
                await _detailWriter.WriteAsync(_detailGenerator.GenerateLine(item.Detail));
            }
            if (fileTypes.HasFlag(FileTypes.Link))
            {
                await _linkWriter.WriteAsync(_linkGenerator.GenerateLine(item.Link));
            }
            if (fileTypes.HasFlag(FileTypes.Marketing))
            {
                await _marketingWriter.WriteAsync(_marketingGenerator.GenerateLine(item.Marketing));
            }
            if (fileTypes.HasFlag(FileTypes.Option))
            {
                await _optionsWriter.WriteAsync(_optionsGenerator.GenerateLine(item.Options));
            }
            if (fileTypes.HasFlag(FileTypes.Product))
            {
                await _productWriter.WriteAsync(_productGenerator.GenerateLine(item.Product));
            }
            if (fileTypes.HasFlag(FileTypes.Specification))
            {
                await _specificationsWriter.WriteAsync(_specificationsGenerator.GenerateLine(item.Specifications));
            }
            if (fileTypes.HasFlag(FileTypes.Supplier))
            {
                await _supplierWriter.WriteAsync(_supplierGenerator.GenerateLine(item.Supplier));
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.
                _detailWriter.Dispose();
                _linkWriter.Dispose();
                _marketingWriter.Dispose();
                _optionsWriter.Dispose();
                _productWriter.Dispose();
                _specificationsWriter.Dispose();
                _supplierWriter.Dispose();

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~CsvOutputWriter() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        void IDisposable.Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}