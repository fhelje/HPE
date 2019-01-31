using System;
using Nest;

namespace FSSystem.ContentAdapter.HPEAndHPInc.Generic.FileWriter
{
    public interface ICsvGenerator<in T> 
    {
        bool TryGenerateLine(T item, out string line);
    }
    public interface ICsvGenerator2<in T> {
        void Start();
        void Close();
        int GenerateLine(T item, string[] variants);
        void CopyTo(Span<char> buffer);
    }
}