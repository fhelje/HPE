using System;
using System.IO;
using FSSystem.ContentAdapter.HPEAndHPInc.Generic.Model;
using Newtonsoft.Json;

namespace FSSystem.ContentAdapter.HPEAndHPInc.Generic.FileWriter {
    public class JsonOutputWriter : IDisposable {
        private readonly WriterConfiguration _configuration;
        private readonly JsonWriter _jsonWriter;
        private readonly JsonSerializer _serializer = new JsonSerializer();
        private readonly StreamWriter _textWriter;

        public JsonOutputWriter(WriterConfiguration configuration) {
            _configuration = configuration;
            _textWriter = new StreamWriter(File.OpenWrite(Path.Combine(_configuration.OutputPath,
                _configuration.JsonDirectory, _configuration.JsonFileName)));
            _jsonWriter = new JsonTextWriter(_textWriter) {
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