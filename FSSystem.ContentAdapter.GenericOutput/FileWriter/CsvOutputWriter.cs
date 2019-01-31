using System;
using System.IO;
using System.Text;
using FSSystem.ContentAdapter.Model;
using Microsoft.Extensions.ObjectPool;

namespace FSSystem.ContentAdapter.GenericOutput.FileWriter {
    public class CsvOutputWriter : IDisposable {
        private readonly CsvDetailGenerator _detailGenerator;
        //private readonly CsvSupplierGenerator _supplierGenerator;

        private readonly TextWriter _detailWriter;
        private readonly CsvLinkGenerator _linkGenerator;
        private readonly TextWriter _linkWriter;
        private readonly CsvMarketingGenerator _marketingGenerator;
        private readonly TextWriter _marketingWriter;
        private readonly CsvOptionsGenerator _optionsGenerator;
        private readonly TextWriter _optionsWriter;
        private readonly CsvProductGenerator _productGenerator;
        private readonly TextWriter _productWriter;
        private readonly CsvSpecificationsGenerator _specificationsGenerator;
        private readonly TextWriter _specificationsWriter;
        // private readonly TextWriter _supplierWriter;

        public CsvOutputWriter(WriterConfiguration configuration) {
            var pool = new DefaultObjectPool<StringBuilder>(new StringBuilderPooledObjectPolicy());
            _detailGenerator = new CsvDetailGenerator(pool);
            _linkGenerator = new CsvLinkGenerator(pool);
            _marketingGenerator = new CsvMarketingGenerator(pool);
            _optionsGenerator = new CsvOptionsGenerator(pool);
            _productGenerator = new CsvProductGenerator(pool);
            _specificationsGenerator = new CsvSpecificationsGenerator(pool);
            //_supplierGenerator = new CsvSupplierGenerator();

            _detailWriter = new StreamWriter(File.OpenWrite(Path.Combine(configuration.OutputPath,
                configuration.CsvDirectory, configuration.DetailFileName)));
            _linkWriter = new StreamWriter(File.OpenWrite(Path.Combine(configuration.OutputPath,
                configuration.CsvDirectory, configuration.LinkFileName)));
            _marketingWriter = new StreamWriter(File.OpenWrite(Path.Combine(configuration.OutputPath,
                configuration.CsvDirectory, configuration.MarketingFileName)));
            _optionsWriter = new StreamWriter(File.OpenWrite(Path.Combine(configuration.OutputPath,
                configuration.CsvDirectory, configuration.OptionFileName)));
            _productWriter = new StreamWriter(File.OpenWrite(Path.Combine(configuration.OutputPath,
                configuration.CsvDirectory, configuration.ProductFileName)));
            _specificationsWriter = new StreamWriter(File.OpenWrite(Path.Combine(configuration.OutputPath,
                configuration.CsvDirectory, configuration.SpecificationFileName)));
            //_supplierWriter = new StreamWriter(File.OpenWrite(Path.Combine(configuration.OutputPath, "supplier.txt")));
        }

        public void Write(FileTypes fileTypes, Item item) {
            if ((fileTypes & FileTypes.Detail) != 0
                && _detailGenerator.TryGenerateLine(item.Detail, out var detailLine)) {
                _detailWriter.Write(detailLine);
            }

            if ((fileTypes & FileTypes.Link) != 0 && _linkGenerator.TryGenerateLine(item.Link, out var linkLine)) {
                _linkWriter.Write(linkLine);
            }

            if ((fileTypes & FileTypes.Marketing) != 0
                && _marketingGenerator.TryGenerateLine(item.Marketing, out var marketingLine)) {
                _marketingWriter.Write(marketingLine);
            }

            if ((fileTypes & FileTypes.Option) != 0
                && _optionsGenerator.TryGenerateLine(item.Options, out var optionLine)) {
                _optionsWriter.Write(optionLine);
            }

            if ((fileTypes & FileTypes.Product) != 0
                && _productGenerator.TryGenerateLine(item.Product, out var productLine)) {
                _productWriter.Write(productLine);
            }

            if ((fileTypes & FileTypes.Specification) != 0
                && _specificationsGenerator.TryGenerateLine(item.Specifications, out var specLine)) {
                _specificationsWriter.Write(specLine);
            }

            //if (fileTypes.HasFlag(FileTypes.Supplier))
            //{
            //    await _supplierWriter.Write(_supplierGenerator.GenerateLine(item.Supplier));
            //}
        }

        #region IDisposable Support

        private bool _disposedValue; // To detect redundant calls

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