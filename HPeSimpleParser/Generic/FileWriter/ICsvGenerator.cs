using System;
using System.Threading.Tasks;

namespace HPeSimpleParser
{
    public interface ICsvGenerator<in T> 
    {
        bool TryGenerateLine(T item, out string line);
    }
}