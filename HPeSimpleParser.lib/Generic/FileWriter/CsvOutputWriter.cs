using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.ObjectPool;

namespace FSSystem.ContentAdapter.HPEAndHPInc.Generic.FileWriter {
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
            _pool = new DefaultObjectPool<StringBuilder>(new StringBuilderPooledObjectPolicy());
            _detailGenerator = new CsvDetailGenerator(_pool);
            _linkGenerator = new CsvLinkGenerator(_pool);
            _marketingGenerator = new CsvMarketingGenerator(_pool);
            _optionsGenerator = new CsvOptionsGenerator(_pool);
            _productGenerator = new CsvProductGenerator(_pool);
            _specificationsGenerator = new CsvSpecificationsGenerator(_pool);
            //_supplierGenerator = new CsvSupplierGenerator();

            _detailWriter = new StreamWriter(File.OpenWrite(Path.Combine(configuration.OutputPath, configuration.CsvDirectory, configuration.DetailFileName)));
            _linkWriter = new StreamWriter(File.OpenWrite(Path.Combine(configuration.OutputPath, configuration.CsvDirectory, configuration.LinkFileName)));
            _marketingWriter = new StreamWriter(File.OpenWrite(Path.Combine(configuration.OutputPath, configuration.CsvDirectory, configuration.MarketingFileName)));
            _optionsWriter = new StreamWriter(File.OpenWrite(Path.Combine(configuration.OutputPath, configuration.CsvDirectory, configuration.OptionFileName)));
            _productWriter = new StreamWriter(File.OpenWrite(Path.Combine(configuration.OutputPath, configuration.CsvDirectory, configuration.ProductFileName)));
            _specificationsWriter = new StreamWriter(File.OpenWrite(Path.Combine(configuration.OutputPath, configuration.CsvDirectory, configuration.SpecificationFileName)));
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
        private bool _disposedValue; // To detect redundant calls
        private DefaultObjectPool<StringBuilder> _pool;

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
        public void Dispose() {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}