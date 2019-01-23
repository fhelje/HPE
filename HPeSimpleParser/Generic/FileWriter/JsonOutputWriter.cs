using System;
using System.IO;
using Newtonsoft.Json;

namespace HPeSimpleParser.Generic.FileWriter {
    public class JsonOutputWriter : IDisposable {
        private readonly WriterConfiguration _configuration;
        private JsonWriter _jsonWriter;
        readonly JsonSerializer _serializer = new JsonSerializer();
        private StreamWriter _textWriter;

        public JsonOutputWriter(WriterConfiguration configuration) {
            _configuration = configuration;
        }

        public void Open() {
            _textWriter = new StreamWriter(File.OpenWrite(Path.Combine(_configuration.OutputPath, "all.json")));
            _jsonWriter = new JsonTextWriter(_textWriter) {
                AutoCompleteOnClose = true,
                CloseOutput = true,
                Formatting = Formatting.None
            };
        }

        public void Close() {
            _jsonWriter.Close();
        }

        public void Write(Model.Item item) {
            _serializer.Serialize(_jsonWriter, item);
        }

        public void Dispose() {
        }
    }
}