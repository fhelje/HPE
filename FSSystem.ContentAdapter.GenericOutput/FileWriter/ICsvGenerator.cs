namespace FSSystem.ContentAdapter.GenericOutput.FileWriter {
    public interface ICsvGenerator<in T> {
        bool TryGenerateLine(T item, out string line);
    }
}