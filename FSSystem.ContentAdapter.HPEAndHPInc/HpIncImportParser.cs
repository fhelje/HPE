﻿using System.Threading.Tasks;
using FSSystem.ContentAdapter.GenericOutput.FileWriter;
using FSSystem.ContentAdapter.HPEAndHPInc.Enums;
using FSSystem.ContentAdapter.HPEAndHPInc.Parser;

namespace FSSystem.ContentAdapter.HPEAndHPInc {
    public class HpIncImportParser : Runner {
        private readonly WriterConfiguration _configuration;

        public HpIncImportParser(WriterConfiguration configuration) {
            _configuration = configuration;
        }

        public override async Task Execute() {
            VerifyPaths(_configuration);
            await Import(_configuration, new HPIncParserDefinition(), VariantType.HPInc).ConfigureAwait(false);
        }
    }
}