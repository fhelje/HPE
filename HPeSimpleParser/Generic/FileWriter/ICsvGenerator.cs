namespace HPeSimpleParser.Generic.FileWriter
{
    public interface ICsvGenerator<in T> 
    {
        bool TryGenerateLine(T item, out string line);
    }
}