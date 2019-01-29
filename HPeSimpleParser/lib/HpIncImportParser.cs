using System.Threading.Tasks;
using HPeSimpleParser.lib.Generic.FileWriter;
using HPeSimpleParser.lib.Parser;

namespace HPeSimpleParser.lib {
    public class HpIncImportParser : Runner {
        private readonly WriterConfiguration _configuration;

        public HpIncImportParser(WriterConfiguration configuration) {
            _configuration = configuration;
        }

        public override async Task Execute() {
            VerifyPaths(_configuration);
            await Import(_configuration, new HPIncParserDefinition());

        }

    }
}