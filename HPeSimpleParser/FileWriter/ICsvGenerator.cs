using System;
using System.Threading.Tasks;

namespace HPeSimpleParser
{
    public interface ICsvGenerator<T> 
    {
        string GenerateLine(T item);
    }
}