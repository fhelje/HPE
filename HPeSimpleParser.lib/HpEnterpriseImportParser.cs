using System.Threading.Tasks;
using FSSystem.ContentAdapter.HPEAndHPInc.Enums;
using FSSystem.ContentAdapter.HPEAndHPInc.Generic.FileWriter;
using FSSystem.ContentAdapter.HPEAndHPInc.Parser;

namespace FSSystem.ContentAdapter.HPEAndHPInc {
    public class HpEnterpriseImportParser : Runner {
        private readonly WriterConfiguration _configuration;

        public HpEnterpriseImportParser(WriterConfiguration configuration) {
            _configuration = configuration;
        }

        public override async Task Execute() {
            VerifyPaths(_configuration);
            await Import(_configuration, new HPEParserDefinition(), VariantType.HPE).ConfigureAwait(false);
        }
    }
}