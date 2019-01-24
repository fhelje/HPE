using System;
using System.IO;
using System.Linq;
using System.Text;
using HPeSimpleParser.HPE.Model;

namespace HPeSimpleParser.Generic.FileWriter
{

    //public class FileWriter : IDisposable
    //{
    //    private readonly StringBuilder _productSb;
    //    private readonly StringBuilder _optionsSb;
    //    private readonly StringBuilder _marketingSb;
    //    private readonly StringBuilder _specificationSb;
    //    //private readonly StringBuilder _hierarchySb;
    //    private readonly StringBuilder _linksSb;
    //    private readonly StringBuilder _supplierSb;

    //    private readonly StreamWriter _productStream;
    //    private readonly StreamWriter _optionStream;
    //    private readonly StreamWriter _linkStream;
    //    private readonly StreamWriter _supplierStream;
    //    private readonly StreamWriter _pureHierarchyStream;
    //    private readonly StreamWriter _marketingStream;
    //    private readonly StreamWriter _specificationStream;

    //    public FileWriter(string rootPath)
    //    {
    //        _productSb = new StringBuilder();
    //        _optionsSb = new StringBuilder();
    //        _specificationSb = new StringBuilder();
    //        //_hierarchySb = new StringBuilder();
    //        _linksSb = new StringBuilder();
    //        _supplierSb = new StringBuilder();
    //        _marketingSb = new StringBuilder();
    //        _supplierStream = CreateStream(rootPath, "hpe_supplier.txt");
    //        _pureHierarchyStream = CreateStream(rootPath, "hpe_pure_hierarchy.txt");
    //        _productStream = CreateStream(rootPath, "hpe_product.txt");
    //        _linkStream = CreateStream(rootPath, "hpe_link.txt");
    //        _marketingStream = CreateStream(rootPath, "hpe_marketing.txt");
    //        _optionStream = CreateStream(rootPath, "hpe_option.txt");
    //        _specificationStream = CreateStream(rootPath, "hpe_specification.txt");
    //    }

    //    private static StreamWriter CreateStream(string rootPath, string fileName)
    //    {
    //        return new StreamWriter(
    //            new FileStream(
    //                Path.Combine(rootPath, fileName), FileMode.Create), Encoding.UTF8);
    //    }

    //    public void Write(ProductRoot productRoot)
    //    {
    //        WriteProduct(productRoot);
    //        WriteOptions(productRoot);
    //        WriteSupplier(productRoot);
    //        WriteLink(productRoot);
    //        WriteMarketing(productRoot);
    //        WriteSpecification(productRoot);

    //    }
    //    // ReSharper disable UnusedMember.Local
    //    private enum ProductColumnV1
    //    {
    //        PartnerPartNumber,
    //        PartNumber,
    //        ManufacturerName,
    //        ManufacturerCode,
    //        CategoryID,
    //        CategoryName,
    //        Description,
    //        DescriptionLong,
    //        ProductCode,
    //        IsEol,
    //        ChangeDate,
    //        UNSPSC
    //    }

    //    private enum PureHierarchyColumnV1
    //    {
    //        PartnerPartNumber,
    //        CategoryID,
    //        CategoryName,
    //        ParentCategoryID,
    //        Level
    //    }

    //    private enum LinkColumnV1
    //    {
    //        PartnerPartNumber,
    //        PdfLinkDataSheet,
    //        PdfLinkManual,
    //        ImageLinks
    //    }

    //    private enum MarketingColumnV1
    //    {
    //        PartnerPartnumber,
    //        MarketingCode,
    //        MarketingText,
    //        LanguageId
    //    }

    //    private enum OptionColumnV1
    //    {
    //        PartnerPartNumber,
    //        Options
    //    }

    //    private enum SpecificationColumnV1
    //    {
    //        PartnerPartNumber,
    //        Specifications
    //    }

    //    private enum ManufacturerColumnV1
    //    {
    //        SupplierID,
    //        SupplierName
    //    }
    //    // ReSharper restore UnusedMember.Local

    //    private void WriteSpecification(ProductRoot productRoot)
    //    {
    //        _specificationSb.Clear();
    //        //PartnerPartnumber,  // \item[num] inclusive # XXX
    //        _specificationSb.Append(productRoot.PartnerPartNumber);
    //        _specificationSb.Append(FileSeparators.ColumnSeparator);
    //        //Specifications      //
    //        for (var i = 0; i < productRoot.Specifications.LabeledItems.Count; i++)
    //        {
    //            var spec = productRoot.Specifications.LabeledItems[i];
    //            _specificationSb.Append(spec.Name);
    //            _specificationSb.Append(FileSeparators.MultiColumnColumnSeparator);
    //            _specificationSb.Append(spec.Value);
    //            _specificationSb.Append(FileSeparators.MultiColumnColumnSeparator);
    //            _specificationSb.Append(string.Empty);
    //            if (i < productRoot.Specifications.LabeledItems.Count - 1)
    //            {
    //                _specificationSb.Append(FileSeparators.MultiColumnColumnRowSeparator);
    //            }
    //        }
    //        _specificationSb.Append(Environment.NewLine);
    //        _specificationStream.Write(_optionsSb.ToString());
    //    }

    //    private void WriteMarketing(ProductRoot productRoot)
    //    {
    //        _marketingSb.Clear();
    //        //PartnerPartnumber,  // \item[num] inclusive # XXX
    //        _marketingSb.Append(productRoot.PartnerPartNumber);
    //        _marketingSb.Append(FileSeparators.ColumnSeparator);
    //        //MarketingCode,      // LEAVE EMPTY
    //        _marketingSb.Append(productRoot.Marketing.MarketingCode);
    //        _marketingSb.Append(FileSeparators.ColumnSeparator);
    //        //MarketingText,      // \item\content\features\keysellingpoint (Parse node below)
    //        _marketingSb.Append(productRoot.Marketing.MarketingText);
    //        _marketingSb.Append(FileSeparators.ColumnSeparator);
    //        //LanguageId          // \item[culturecode]
    //        _marketingSb.Append(productRoot.LanguageId);

    //        _marketingSb.Append(Environment.NewLine);
    //        _marketingStream.Write(_optionsSb.ToString());
    //    }

    //    private void WriteLink(ProductRoot productRoot)
    //    {
    //        _linksSb.Clear();
    //        //PartnerPartNumber,  // \item[num] inclusive # XXX
    //        _linksSb.Append(productRoot.PartnerPartNumber);
    //        _linksSb.Append(FileSeparators.ColumnSeparator);
    //        //PdfLinkDataSheet,   // \item\content\tech_specs\quickspeclinks\info_quickspec_doc_ww 
    //        _linksSb.Append(productRoot.Links.PdfLinkDataSheet);
    //        _linksSb.Append(FileSeparators.ColumnSeparator);
    //        //PdfLinkManual,      //
    //        _linksSb.Append(productRoot.Links.PdfLinkManual);
    //        _linksSb.Append(FileSeparators.ColumnSeparator);
    //        //ImageLinks          // \item\images\image lista ut vilka bilder vi ska använda
    //        _linksSb.Append(string.Join(FileSeparators.MultiColumnColumnSeparator, productRoot.Links.SelectedImages.Select(x => x.ImageUrlHttp)));
    //        _linksSb.Append(Environment.NewLine);
    //        _linkStream.Write(_linksSb.ToString());

    //    }

    //    private void WriteSupplier(ProductRoot productRoot)
    //    {
    //        _supplierSb.Clear();

    //        //SupplierID,         // \item[num] inclusive # XXX
    //        _supplierSb.Append(productRoot.PartnerPartNumber);
    //        _supplierSb.Append(FileSeparators.ColumnSeparator);
    //        //SupplierName        //            
    //        _supplierSb.Append(productRoot.Product.ManufacturerCode);
    //        _supplierSb.Append(Environment.NewLine);
    //        _supplierStream.Write(_supplierSb.ToString());

    //    }


    //    private void WriteOptions(ProductRoot productRoot)
    //    {
    //        //
    //        //
    //        _optionsSb.Clear();
    //        //PartnerPartNumber,  // \item[num] inclusive # XXX
    //        _optionsSb.Append(productRoot.PartnerPartNumber);
    //        _productSb.Append(FileSeparators.ColumnSeparator);
    //        //Options             // \
    //        for (var index = 0; index < productRoot.Options.Items.Count; index++)
    //        {
    //            var link = productRoot.Options.Items[index];
    //            _optionsSb.Append(link.OptionPartnerPartNumber);
    //            _optionsSb.Append(FileSeparators.MultiColumnColumnSeparator);
    //            _optionsSb.Append(link.ManufacturerCode);
    //            _optionsSb.Append(FileSeparators.MultiColumnColumnSeparator);
    //            _optionsSb.Append(link.OptionGroupCode);
    //            _optionsSb.Append(FileSeparators.MultiColumnColumnSeparator);
    //            _optionsSb.Append(string.Empty);
    //            if (index < (productRoot.Options.Items.Count - 1))
    //            {
    //                _optionsSb.Append(FileSeparators.MultiColumnColumnRowSeparator);
    //            }
    //        }
    //        _optionsSb.Append(Environment.NewLine);
    //        _optionStream.Write(_optionsSb.ToString());
    //    }

    //    private void WriteProduct(ProductRoot productRoot)
    //    {
    //        var hierarchy = productRoot.Hierarchy.FirstOrDefault(x => x.Name == "HPE") ?? productRoot.Hierarchy.First();
    //        _productSb.Clear();
    //        //PartnerPartNumber,  // \item[num] inclusive # XXX
    //        _productSb.Append(productRoot.PartnerPartNumber);
    //        _productSb.Append(FileSeparators.ColumnSeparator);
    //        //PartNumber,         // \item[num] inclusive # XXX
    //        _productSb.Append(productRoot.PartNumber);
    //        _productSb.Append(FileSeparators.ColumnSeparator);
    //        //ManufacturerName,   // HPE
    //        _productSb.Append(productRoot.Product.ManufacturerName);
    //        _productSb.Append(FileSeparators.ColumnSeparator);
    //        //ManufacturerCode,   // HPE
    //        _productSb.Append(productRoot.Product.ManufacturerCode);
    //        _productSb.Append(FileSeparators.ColumnSeparator);
    //        //CategoryID,         // lövnode i category
    //        _productSb.Append(hierarchy.CategoryName);
    //        _productSb.Append(FileSeparators.ColumnSeparator);
    //        //CategoryName,       // lövnode i category
    //        _productSb.Append(hierarchy.CategoryName);
    //        _productSb.Append(FileSeparators.ColumnSeparator);
    //        //Description,        // \item\content\features\technicalspecificationssku\prodnameshort
    //        _productSb.Append(productRoot.Product.Description);
    //        _productSb.Append(FileSeparators.ColumnSeparator);
    //        //DescriptionLong,    // \item\content\features\technicalspecificationssku\prodnameshort
    //        _productSb.Append(productRoot.Product.DescriptionLong);
    //        _productSb.Append(FileSeparators.ColumnSeparator);
    //        //ProductCode,        // \
    //        _productSb.Append(productRoot.Product.ProductCode);
    //        _productSb.Append(FileSeparators.ColumnSeparator);
    //        //IsEol,              // HUR?
    //        _productSb.Append(string.Empty);
    //        _productSb.Append(FileSeparators.ColumnSeparator);
    //        //ChangeDate          // \item[lastupdatedate]
    //        _productSb.Append(productRoot.Product.ChangeDate);
    //        _productSb.Append(FileSeparators.ColumnSeparator);

    //        _productSb.Append(productRoot.Product.Unspsc);

    //        _productSb.Append(Environment.NewLine);
    //        _productStream.Write(_productSb.ToString());
    //    }

    //    public void Dispose()
    //    {
    //        _productStream?.Dispose();
    //        _optionStream?.Dispose();
    //        _linkStream?.Dispose();
    //        _supplierStream?.Dispose();
    //        _pureHierarchyStream?.Dispose();
    //        _marketingStream?.Dispose();
    //        _specificationStream?.Dispose();
    //    }
    //}
}