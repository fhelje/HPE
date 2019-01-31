using System;
using System.IO;
using FSSystem.ContentAdapter.Model;
using Newtonsoft.Json;

namespace FSSystem.ContentAdapter.GenericOutput.FileWriter {
    public class JsonOutputWriter : IDisposable {
        private readonly JsonWriter _jsonWriter;
        private readonly JsonSerializer _serializer = new JsonSerializer();

        public JsonOutputWriter(WriterConfiguration configuration) {
            var configuration1 = configuration;
            var textWriter = new StreamWriter(File.OpenWrite(Path.Combine(configuration1.OutputPath,
                configuration1.JsonDirectory, configuration1.JsonFileName)));
            _jsonWriter = new JsonTextWriter(textWriter) {
                AutoCompleteOnClose = true,
                CloseOutput = true,
                Formatting = Formatting.Indented
            };
            _jsonWriter.WriteStartArray();
        }

        public void Dispose() {
            _jsonWriter.WriteEndArray();
            _jsonWriter.Close();
        }

        public void Write(Item item) {
            _serializer.Serialize(_jsonWriter, item);
        }
    }
}