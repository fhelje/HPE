using System;

namespace HPeSimpleParser
{
    [Flags]
    public enum FileTypes
    {
        Product = 0,
        Option = 1,
        Detail = 2,
        Link = 4,
        Supplier = 8,
        Pure_Hierarchy = 16,
        Marketing = 32,
        Specification = 64,
        Price = 128,
        Stock = 256,
    }
}