﻿using System;
using System.IO;
using Newtonsoft.Json;

namespace FSSystem.ContentAdapter.HPEAndHPInc.Generic.FileWriter {
    public class JsonOutputWriter : IDisposable {
        private readonly WriterConfiguration _configuration;
        private JsonWriter _jsonWriter;
        readonly JsonSerializer _serializer = new JsonSerializer();
        private StreamWriter _textWriter;

        public JsonOutputWriter(WriterConfiguration configuration) {
            _configuration = configuration;
            _textWriter = new StreamWriter(File.OpenWrite(Path.Combine(_configuration.OutputPath, _configuration.JsonDirectory, _configuration.JsonFileName)));
            _jsonWriter = new JsonTextWriter(_textWriter) {
                AutoCompleteOnClose = true,
                CloseOutput = true,
                Formatting = Formatting.Indented                
            };
            _jsonWriter.WriteStartArray();
        }

        public void Write(Model.Item item) {
            _serializer.Serialize(_jsonWriter, item);
        }

        public void Dispose() {
            _jsonWriter.WriteEndArray();
            _jsonWriter.Close();
        }
    }
}