using System;
using System.IO;
using System.Threading.Tasks;

namespace HPeSimpleParser.Generic.FileWriter {
    public class CsvOutputWriter : IDisposable {

        private readonly CsvDetailGenerator _detailGenerator;
        private readonly CsvLinkGenerator _linkGenerator;
        private readonly CsvMarketingGenerator _marketingGenerator;
        private readonly CsvOptionsGenerator _optionsGenerator;
        private readonly CsvProductGenerator _productGenerator;
        private readonly CsvSpecificationsGenerator _specificationsGenerator;
        //private readonly CsvSupplierGenerator _supplierGenerator;

        private readonly TextWriter _detailWriter;
        private readonly TextWriter _linkWriter;
        private readonly TextWriter _marketingWriter;
        private readonly TextWriter _optionsWriter;
        private readonly TextWriter _productWriter;
        private readonly TextWriter _specificationsWriter;
        // private readonly TextWriter _supplierWriter;

        public CsvOutputWriter(WriterConfiguration configuration) {
            _detailGenerator = new CsvDetailGenerator();
            _linkGenerator = new CsvLinkGenerator();
            _marketingGenerator = new CsvMarketingGenerator();
            _optionsGenerator = new CsvOptionsGenerator();
            _productGenerator = new CsvProductGenerator();
            _specificationsGenerator = new CsvSpecificationsGenerator();
            //_supplierGenerator = new CsvSupplierGenerator();

            _detailWriter = new StreamWriter(File.OpenWrite(Path.Combine(configuration.OutputPath, "detail.txt")));
            _linkWriter = new StreamWriter(File.OpenWrite(Path.Combine(configuration.OutputPath, "link.txt")));
            _marketingWriter = new StreamWriter(File.OpenWrite(Path.Combine(configuration.OutputPath, "marketing.txt")));
            _optionsWriter = new StreamWriter(File.OpenWrite(Path.Combine(configuration.OutputPath, "option.txt")));
            _productWriter = new StreamWriter(File.OpenWrite(Path.Combine(configuration.OutputPath, "product.txt")));
            _specificationsWriter = new StreamWriter(File.OpenWrite(Path.Combine(configuration.OutputPath, "specification.txt")));
            //_supplierWriter = new StreamWriter(File.OpenWrite(Path.Combine(configuration.OutputPath, "supplier.txt")));


        }
        public async Task WriteAsync(FileTypes fileTypes, Model.Item item) {
            if (fileTypes.HasFlag(FileTypes.Detail)) {
                if (_detailGenerator.TryGenerateLine(item.Detail, out var detailLine)) {
                    await _detailWriter.WriteAsync(detailLine);
                }
            }
            if (fileTypes.HasFlag(FileTypes.Link)) {
                if (_linkGenerator.TryGenerateLine(item.Link, out var linkLine)) {
                    await _linkWriter.WriteAsync(linkLine);
                }
            }
            if (fileTypes.HasFlag(FileTypes.Marketing)) {
                if (_marketingGenerator.TryGenerateLine(item.Marketing, out var marketingLine)) {
                    await _marketingWriter.WriteAsync(marketingLine);
                }
            }
            if (fileTypes.HasFlag(FileTypes.Option)) {
                if (_optionsGenerator.TryGenerateLine(item.Options, out var optionLine)) {
                    await _optionsWriter.WriteAsync(optionLine);
                }
            }
            if (fileTypes.HasFlag(FileTypes.Product)) {
                if (_productGenerator.TryGenerateLine(item.Product, out var productLine)) {
                    await _productWriter.WriteAsync(productLine);
                }
            }
            if (fileTypes.HasFlag(FileTypes.Specification)) {
                if (_specificationsGenerator.TryGenerateLine(item.Specifications, out var specLine)) {
                    await _specificationsWriter.WriteAsync(specLine);
                }
            }
            //if (fileTypes.HasFlag(FileTypes.Supplier))
            //{
            //    await _supplierWriter.WriteAsync(_supplierGenerator.GenerateLine(item.Supplier));
            //}
        }

        #region IDisposable Support
        private bool _disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing) {
            if (!_disposedValue) {
                if (disposing) {
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
                //_supplierWriter.Dispose();

                _disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~CsvOutputWriter() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        void IDisposable.Dispose() {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}