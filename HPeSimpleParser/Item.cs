namespace HPeSimpleParser {
    internal static class Item {
        public const string NodeName = "item";
        internal static class Content {
            public const string NodeName = "content";

            internal static class System {
                public const string NodeName = "system";

                internal static class DeleteThese {
                    public const string NodeName = "deletethese";
                    public const string PhotoJpeg400x400 = "photojpeg400x400";
                    public const string PhotoJpeg190x170 = "photojpeg190x170";
                    public const string PhotoJpeg70x100 = "photojpeg70x100";
                }
                internal static class Metadata {
                    public const string NodeName = "metadata";
                    public const string ProdClassFlag = "prodclassflag";
                    public const string FeaturedProductSmb = "featuredproductsmb";
                    public const string ProdSequencing = "prodsequencing";
                    public const string Mpd = "mpd";
                    public const string TangibleFlag = "tangibleflag";
                    public const string CarePackRegistrationFlag = "carepackregistrationflag";
                }
                internal static class FacetsFacetsUrl {
                    public const string NodeName = "facetsfacets_url";
                    public const string FacetTieringSupportUrl = "facet_tiering_support_url";
                }
                internal static class Coreinformation {
                    public const string NodeName = "coreinformation";
                    public const string ProdNum = "prodnum";
                    public const string CustFacingDes = "custfacingdes";
                    public const string ProdLongName = "prodlongname";
                    public const string ProdName = "prodname";
                }
                internal static class Webservice {
                    public const string NodeName = "webservice";
                    public const string WebAddressUrl = "webaddressurl";
                    public const string CallToActionUrl = "calltoactionurl";
                    public const string CtaText = "ctatext";
                    public const string QrcLink = "qrclink";
                    public const string MarqueeTitle = "marqueetitle";
                    public const string MarqueeDesc = "marqueedesc";
                    public const string WebListTemplate = "weblisttemplate";
                    public const string MarqueeImage = "marqueeimage";
                    public const string LandingPageUrl = "landingpageurl";
                    public const string ProdNameUrl = "prodnameurl";
                }
                internal static class DataSheetSupportLinks {
                    public const string NodeName = "datasheet_supportlinks";
                    public const string DataSheetUrl = "datasheeturl";
                    public const string DigitalDataSheet = "digitaldatasheet";
                    public const string RelatedLinkDataSheet = "relatedlink_datasheet";
                    public const string DigitalDataSheetUrl = "digitaldatasheeturl";
                    public const string TechnicalSupportUrl = "technicalsupporturl";
                    public const string RelatedLinkTsManuals = "relatedlink_tsmanuals";
                }
            }
            internal static class TechSpecs {
                public const string NodeName = "tech_specs";
                internal static class Technicalspecificationsseries {
                    public const string NodeName = "technicalspecificationsseries";
                    public const string MemMax = "memmax";
                    public const string ProcessorFamily = "processorfamily";
                    public const string Processor = "processor";
                    public const string MemProtect = "memprotect";
                    public const string DriveType = "drivetype";
                    public const string Workload = "workload";
                    public const string MemSlots = "memslots";
                }
                internal static class TechnicalSpecifications {
                    public const string NodeName = "technicalspecifications";
                    public const string RecTech = "rectech";
                    public const string ChassisFan = "chassisfan";
                    public const string Cores = "cores";
                    public const string Addfeats = "addfeats";
                    public const string Pwrsupply = "pwrsupply";
                    public const string Raidlevels = "raidlevels";
                    public const string Chassistype = "chassistype";
                    public const string Raidsupport = "raidsupport";
                    public const string Ports = "ports";
                    public const string Numserverport = "numserverport";
                    public const string Nodatatracks = "nodatatracks";
                    public const string Snapshotsuppt = "snapshotsuppt";
                    public const string Sysconfig = "sysconfig";
                    public const string Trayblanks = "trayblanks";
                    public const string Productcolour = "productcolour";
                    public const string Routingswitchcap = "routingswitchcap";
                    public const string Mediatypetech = "mediatypetech";
                    public const string Stackingdesc = "stackingdesc";
                    public const string Wavelength = "wavelength";
                    public const string Indandbuttons = "indandbuttons";
                    public const string System = "system";
                    public const string Channels = "channels";
                    public const string Technote = "technote";
                    public const string Writeiops = "writeiops";
                    public const string Maglaycoercivity = "maglaycoercivity";
                    public const string Height = "height";
                    public const string Cat5cable = "cat5cable";
                    public const string Diskdrivedes = "diskdrivedes";
                    public const string Hdnumincluded = "hdnumincluded";
                    public const string Processornum = "processornum";
                    public const string Network = "network";
                    public const string Expanslots = "expanslots";
                    public const string Tieringsupp = "tieringsupp";
                    public const string Plugtype = "plugtype";
                    public const string Wifiantenna = "wifiantenna";
                    public const string Dimmtypecomp = "dimmtypecomp";
                    public const string Switchcap = "switchcap";
                    public const string Modelavail = "modelavail";
                    public const string Drivemedcompat = "drivemedcompat";
                    public const string Replicationfeat = "replicationfeat";
                    public const string Ratedlinecurr = "ratedlinecurr";
                    public const string Battertdes = "battertdes";
                    public const string Arrayplatform = "arrayplatform";
                    public const string Nvdimmcap = "nvdimmcap";
                    public const string Powerconsump = "powerconsump";
                    public const string Fibertype = "fibertype";
                    public const string Voltage = "voltage";
                    public const string Performance = "performance";
                    public const string Interface = "interface";
                    public const string Hddcntrlr = "hddcntrlr";
                    public const string Dimmtype = "dimmtype";
                    public const string Dimmrank = "dimmrank";
                    public const string Memband4board = "memband4board";
                    public const string Mounting = "mounting";
                    public const string Connectortype = "connectortype";
                    public const string Radiocover = "radiocover";
                    public const string Backupps = "backupps";
                    public const string Ratinpcurr = "ratinpcurr";
                    public const string Architectfeat = "architectfeat";
                    public const string Virtappliance = "virtappliance";
                    public const string Managefeatures = "managefeatures";
                    public const string Hardwaresupp = "hardwaresupp";
                    public const string Ratinpfreq = "ratinpfreq";
                    public const string Wrntystdstmt = "wrntystdstmt";
                    public const string Dimenmet = "dimenmet";
                    public const string Memory = "memory";
                    public const string Peaksingpreflopointperf = "peaksingpreflopointperf";
                    public const string Netprocessor = "netprocessor";
                    public const string Dimenus = "dimenus";
                    public const string Compnodes = "compnodes";
                    public const string Maxnumnvdimms = "maxnumnvdimms";
                    public const string Ossuppted = "ossuppted";
                    public const string Drivemoddesc = "drivemoddesc";
                    public const string Remotemgmtsw = "remotemgmtsw";
                    public const string Processorname = "processorname";
                    public const string Powtype = "powtype";
                    public const string Memorytypeecc = "memorytypeecc";
                    public const string Cabletype = "cabletype";
                    public const string Outletype = "outletype";
                    public const string Opticaldrivetype = "opticaldrivetype";
                    public const string Nvdimmrank = "nvdimmrank";
                    public const string Memprocessor = "memprocessor";
                    public const string Netinterfacecontroller = "netinterfacecontroller";
                    public const string Memstd = "memstd";
                    public const string Connectivity = "connectivity";
                    public const string Encryptsupport = "encryptsupport";
                    public const string Maxdriveperenclosure = "maxdriveperenclosure";
                    public const string Datarate = "datarate";
                    public const string Ratinppow = "ratinppow";
                    public const string Techno = "techno";
                    public const string Acceleratorspercard = "acceleratorspercard";
                    public const string Tapetypeemulated = "tapetypeemulated";
                    public const string Numofserversmax = "numofserversmax";
                    public const string Pwrsupplyname = "pwrsupplyname";
                    public const string Tempopcent = "tempopcent";
                    public const string Mountingoptions = "mountingoptions";
                    public const string Mediatype = "mediatype";
                    public const string Throughput = "throughput";
                    public const string Upgradability = "upgradability";
                    public const string Msbf = "msbf";
                    public const string Basefilm = "basefilm";
                    public const string Latency = "latency";
                    public const string Powspec = "powspec";
                    public const string Tapemaxemulated = "tapemaxemulated";
                    public const string Graphics = "graphics";
                    public const string Gsname01 = "gsname_01";
                    public const string Processorspd = "processorspd";
                    public const string Jdports = "jdports";
                    public const string Powdistrib = "powdistrib";
                    public const string Dimmcap = "dimmcap";
                    public const string Portspdrate = "portspdrate";
                    public const string Chassisdes = "chassisdes";
                    public const string Weightmet = "weightmet";
                    public const string Capacity = "capacity";
                    public const string Pwrcapacity = "pwrcapacity";
                    public const string Bladesperenclosure = "bladesperenclosure";
                    public const string Processorsnum = "processorsnum";
                    public const string Pwrsupplypoe = "pwrsupplypoe";
                    public const string Dimmnatspeed = "dimmnatspeed";
                    public const string Memsizeperboard = "memsizeperboard";
                    public const string Poweravail = "poweravail";
                    public const string Transcformfact = "transcformfact";
                    public const string Numofsimusersmax = "numofsimusersmax";
                    public const string Protocolsupp = "protocolsupp";
                    public const string Mediaformat = "mediaformat";
                    public const string Networking = "networking";
                    public const string Heatdissipation = "heatdissipation";
                    public const string Bufcredits = "bufcredits";
                    public const string Accelrttype = "accelrttype";
                    public const string Maxstorage = "maxstorage";
                    public const string Boot = "boot";
                    public const string Cpu = "cpu";
                    public const string Lifetimewrites = "lifetimewrites";
                    public const string Powerwatts = "powerwatts";
                    public const string Cartridgemaxemulated = "cartridgemaxemulated";
                    public const string Bustype = "bustype";
                    public const string Elecinputconn = "elecinputconn";
                    public const string Processorcache = "processorcache";
                    public const string Bladetypesupp = "bladetypesupp";
                    public const string Switchbandwidthagg = "switchbandwidthagg";
                    public const string Connectandcabling = "connectandcabling";
                    public const string Wrntyfeatures = "wrntyfeatures";
                    public const string Powersupplytype = "powersupplytype";
                    public const string Certifcompli = "certifcompli";
                    public const string Tapewidthmet = "tapewidthmet";
                    public const string Diskarraysupt = "diskarraysupt";
                    public const string Cartridgerobust = "cartridgerobust";
                    public const string Storageexp = "storageexp";
                    public const string Maxsrcappliance = "maxsrcappliance";
                    public const string Maxdistance = "maxdistance";
                    public const string Server = "server";
                    public const string Connectstd = "connectstd";
                    public const string Ieecompliance = "ieecompliance";
                    public const string Nonvolatilemedia = "nonvolatilemedia";
                    public const string Systemfanfeat = "systemfanfeat";
                    public const string Deduplication = "deduplication";
                    public const string Efficiency = "efficiency";
                    public const string Bitdensity = "bitdensity";
                    public const string Servertypesupp = "servertypesupp";
                    public const string Cartslotbase = "cartslotbase";
                    public const string Storage = "storage";
                    public const string Suppdiskenc = "suppdiskenc";
                    public const string Endurancedwpd = "endurancedwpd";
                    public const string Medialabel = "medialabel";
                    public const string Slottype = "slottype";
                    public const string Quantityperpackage = "quantityperpackage";
                    public const string Hostinterface = "hostinterface";
                    public const string Mgmtfeatures = "mgmtfeatures";
                    public const string Weightus = "weightus";
                    public const string Powersupply = "powersupply";
                    public const string Tapethickness = "tapethickness";
                    public const string Enclosuretype = "enclosuretype";
                    public const string Highavailability = "highavailability";
                    public const string Supportedcables = "supportedcables";
                    public const string Tapelength = "tapelength";
                    public const string Archivelife = "archivelife";
                    public const string Datatrans = "datatrans";
                    public const string Powout = "powout";
                    public const string Nvdimmtype = "nvdimmtype";
                    public const string Inputvoltage = "inputvoltage";
                    public const string Dataservices = "dataservices";
                    public const string Targetbkpapp = "targetbkpapp";
                    public const string Numberofports = "numberofports";
                    public const string Formfactorfull = "formfactorfull";
                    public const string Commled = "commled";
                    public const string Numpdusticks = "numpdusticks";
                    public const string Rackspace = "rackspace";
                    public const string Hotplug = "hotplug";
                    public const string Cache = "cache";
                    public const string Accelrtapps = "accelrtapps";
                    public const string Readiops = "readiops";
                    public const string Readspdmedia = "readspdmedia";
                    public const string Processorcoreavail = "processorcoreavail";
                    public const string Humidityop = "humidityop";
                    public const string Tempopfar = "tempopfar";
                    public const string Maxsptservers = "maxsptservers";
                    public const string Ssdtype = "ssdtype";
                    public const string Softopt = "softopt";
                    public const string Ioports = "ioports";
                    public const string Humiditystrg = "humiditystrg";
                }
                internal static class DeleteThese {
                    public const string NodeName = "deletethese";
                    public const string Wrntyshort = "wrntyshort";
                    public const string Langopt = "langopt";
                    public const string Whatsinbox = "whatsinbox";
                    public const string Energystar = "energystar";
                    public const string Safety = "safety";
                }
                internal static class Technicalspecificationssku {
                    public const string NodeName = "technicalspecificationssku";
                    public const string Sortorder = "sortorder";
                    public const string Offerelements = "offerelements";
                    public const string ProddiffLong = "proddiff_long";
                }
                internal static class Coreinformation {
                    public const string NodeName = "coreinformation";
                    public const string Unspsc = "unspsc";
                    public const string Subbrandname = "subbrandname";
                }
                internal static class Quickspeclinks {
                    public const string NodeName = "quickspeclinks";
                    public const string InfoQuickspecWebWw = "info_quickspec_web_ww";
                    public const string InfoQuickspecDocWw = "info_quickspec_doc_ww";
                }
                internal static class Legalinformation {
                    public const string NodeName = "legalinformation";
                    public const string Trademark = "trademark";
                }
                internal static class Quickspecdate {
                    public const string NodeName = "quickspecdate";
                    public const string AudienceExpiryDateInfoQuickspecWebWw = "audience_expiry_date_info_quickspec_web_ww";
                    public const string AudienceReleaseDateInfoQuickspecDocWw = "audience_release_date_info_quickspec_doc_ww";
                    public const string ContentVersionInfoQuickspecWebWw = "content_version_info_quickspec_web_ww";
                    public const string ContentVersionInfoQuickspecDocWw = "content_version_info_quickspec_doc_ww";
                    public const string AudienceExpiryDateInfoQuickspecDocWw = "audience_expiry_date_info_quickspec_doc_ww";
                    public const string AudienceReleaseDateInfoQuickspecWebWw = "audience_release_date_info_quickspec_web_ww";
                }
            }
            internal static class Features {
                public const string NodeName = "features";
                internal static class Metadata {
                    public const string NodeName = "metadata";
                    public const string Desctiptionmetatag = "desctiptionmetatag";
                }
                internal static class Whatsnew {
                    public const string NodeName = "whatsnew";
                    public const string Whatnew06 = "whatnew_06";
                    public const string Whatnew04 = "whatnew_04";
                    public const string Whatnew05 = "whatnew_05";
                    public const string Whatnew01 = "whatnew_01";
                    public const string Whatnew03 = "whatnew_03";
                    public const string Whatnew02 = "whatnew_02";
                }
                internal static class Keysellingpoints {
                    public const string NodeName = "keysellingpoints";
                    public const string Ksp01Suppt02Long = "ksp_01_suppt_02_long";
                    public const string Ksp04Suppt05Long = "ksp_04_suppt_05_long";
                    public const string Ksp04Suppt03Long = "ksp_04_suppt_03_long";
                    public const string Ksp03Suppt02Long = "ksp_03_suppt_02_long";
                    public const string Ksp04Suppt01Long = "ksp_04_suppt_01_long";
                    public const string Ksp03HeadlineMedium = "ksp_03_headline_medium";
                    public const string Ksp02Suppt02Long = "ksp_02_suppt_02_long";
                    public const string Ksp02Suppt05Long = "ksp_02_suppt_05_long";
                    public const string Ksp02Suppt01Long = "ksp_02_suppt_01_long";
                    public const string Ksp02HeadlineMedium = "ksp_02_headline_medium";
                    public const string Ksp04Suppt02Long = "ksp_04_suppt_02_long";
                    public const string Ksp02Suppt04Long = "ksp_02_suppt_04_long";
                    public const string Ksp02Suppt03Long = "ksp_02_suppt_03_long";
                    public const string Ksp03Suppt04Long = "ksp_03_suppt_04_long";
                    public const string Ksp01HeadlineMedium = "ksp_01_headline_medium";
                    public const string Ksp04HeadlineMedium = "ksp_04_headline_medium";
                    public const string Ksp03Suppt05Long = "ksp_03_suppt_05_long";
                    public const string Ksp03Suppt01Long = "ksp_03_suppt_01_long";
                    public const string Ksp01Suppt01Long = "ksp_01_suppt_01_long";
                    public const string Ksp01Suppt03Long = "ksp_01_suppt_03_long";
                    public const string Ksp01Suppt04Long = "ksp_01_suppt_04_long";
                    public const string Ksp04Suppt04Long = "ksp_04_suppt_04_long";
                    public const string Ksp03Suppt03Long = "ksp_03_suppt_03_long";
                    public const string Ksp01Suppt05Long = "ksp_01_suppt_05_long";
                }
                internal static class Overview {
                    public const string NodeName = "overview";
                    public const string ProddesOverviewExtended = "proddes_overview_extended";
                }
                internal static class Footnotes {
                    public const string NodeName = "footnotes";
                    public const string Msgfootnote07 = "msgfootnote_07";
                    public const string Msgfootnote04 = "msgfootnote_04";
                    public const string Msgfootnote06 = "msgfootnote_06";
                    public const string Msgfootnote01 = "msgfootnote_01";
                    public const string Msgfootnote05 = "msgfootnote_05";
                    public const string Msgfootnote02 = "msgfootnote_02";
                    public const string Msgfootnote03 = "msgfootnote_03";
                }
                internal static class Coreinformation {
                    public const string NodeName = "coreinformation";
                    public const string Prodnameseries = "prodnameseries";
                    public const string Company = "company";
                }
                internal static class Webservice {
                    public const string NodeName = "webservice";
                    public const string Qrcimage = "qrcimage";
                }
                internal static class Techfeatures {
                    public const string NodeName = "techfeatures";
                    public const string Featurestech06Long = "featurestech_06_long";
                    public const string Featurestech01Long = "featurestech_01_long";
                    public const string Featurestech17Long = "featurestech_17_long";
                    public const string Featurestech01Medium = "featurestech_01_medium";
                    public const string Featurestech18Medium = "featurestech_18_medium";
                    public const string Featurestech14Long = "featurestech_14_long";
                    public const string Featurestech07Long = "featurestech_07_long";
                    public const string Featurestech03Long = "featurestech_03_long";
                    public const string Featurestech13Long = "featurestech_13_long";
                    public const string Featurestech10Long = "featurestech_10_long";
                    public const string Featurestech10Medium = "featurestech_10_medium";
                    public const string Featurestech12Long = "featurestech_12_long";
                    public const string Featurestech08Long = "featurestech_08_long";
                    public const string Featurestech15Medium = "featurestech_15_medium";
                    public const string Featurestech06Medium = "featurestech_06_medium";
                    public const string Featurestech14Medium = "featurestech_14_medium";
                    public const string Featurestech07Medium = "featurestech_07_medium";
                    public const string Featurestech04Medium = "featurestech_04_medium";
                    public const string Featurestech02Medium = "featurestech_02_medium";
                    public const string Featurestech05Medium = "featurestech_05_medium";
                    public const string Featurestech18Long = "featurestech_18_long";
                    public const string Featurestech11Medium = "featurestech_11_medium";
                    public const string Featurestech09Medium = "featurestech_09_medium";
                    public const string Featurestech17Medium = "featurestech_17_medium";
                    public const string Featurestech08Medium = "featurestech_08_medium";
                    public const string Featurestech11Long = "featurestech_11_long";
                    public const string Featurestech02Long = "featurestech_02_long";
                    public const string Featurestech16Medium = "featurestech_16_medium";
                    public const string Featurestech04Long = "featurestech_04_long";
                    public const string Featurestech12Medium = "featurestech_12_medium";
                    public const string Featurestech16Long = "featurestech_16_long";
                    public const string Featurestech03Medium = "featurestech_03_medium";
                    public const string Featurestech13Medium = "featurestech_13_medium";
                    public const string Featurestech05Long = "featurestech_05_long";
                    public const string Featurestech09Long = "featurestech_09_long";
                }
                internal static class Rightrail {
                    public const string NodeName = "rightrail";
                    public const string Right03Rail04Statement = "right_03_rail_04_statement";
                    public const string Right04Rail05Statement = "right_04_rail_05_statement";
                    public const string Right03Rail05Statement = "right_03_rail_05_statement";
                    public const string Right04Rail03Url = "right_04_rail_03_url";
                    public const string Rightrailhead01 = "rightrailhead_01";
                    public const string Right01Rail02Statement = "right_01_rail_02_statement";
                    public const string Right01Rail04Url = "right_01_rail_04_url";
                    public const string Right04Rail03Statement = "right_04_rail_03_statement";
                    public const string Right03Rail01Statement = "right_03_rail_01_statement";
                    public const string Right01Rail01Url = "right_01_rail_01_url";
                    public const string Right05Rail05Url = "right_05_rail_05_url";
                    public const string Right02Rail03Statement = "right_02_rail_03_statement";
                    public const string Right05Rail02Url = "right_05_rail_02_url";
                    public const string Right04Rail02Statement = "right_04_rail_02_statement";
                    public const string Right01Rail03Statement = "right_01_rail_03_statement";
                    public const string Rightrailhead02 = "rightrailhead_02";
                    public const string Right02Rail02Url = "right_02_rail_02_url";
                    public const string Right02Rail04Url = "right_02_rail_04_url";
                    public const string Right04Rail04Statement = "right_04_rail_04_statement";
                    public const string Right01Rail05Statement = "right_01_rail_05_statement";
                    public const string Right02Rail01Url = "right_02_rail_01_url";
                    public const string Right01Rail05Url = "right_01_rail_05_url";
                    public const string Right05Rail03Statement = "right_05_rail_03_statement";
                    public const string Right05Rail02Statement = "right_05_rail_02_statement";
                    public const string Right03Rail05Url = "right_03_rail_05_url";
                    public const string Right01Rail03Url = "right_01_rail_03_url";
                    public const string Right03Rail04Url = "right_03_rail_04_url";
                    public const string Right02Rail04Statement = "right_02_rail_04_statement";
                    public const string Rightrailhead03 = "rightrailhead_03";
                    public const string Right05Rail03Url = "right_05_rail_03_url";
                    public const string Right05Rail01Url = "right_05_rail_01_url";
                    public const string Right03Rail01Url = "right_03_rail_01_url";
                    public const string Right02Rail05Url = "right_02_rail_05_url";
                    public const string Right04Rail02Url = "right_04_rail_02_url";
                    public const string Right04Rail01Statement = "right_04_rail_01_statement";
                    public const string Right02Rail03Url = "right_02_rail_03_url";
                    public const string Right02Rail05Statement = "right_02_rail_05_statement";
                    public const string Right01Rail02Url = "right_01_rail_02_url";
                    public const string Right02Rail01Statement = "right_02_rail_01_statement";
                    public const string Right03Rail02Statement = "right_03_rail_02_statement";
                    public const string Right02Rail02Statement = "right_02_rail_02_statement";
                    public const string Right01Rail04Statement = "right_01_rail_04_statement";
                    public const string Right04Rail04Url = "right_04_rail_04_url";
                    public const string Right04Rail01Url = "right_04_rail_01_url";
                    public const string Right03Rail02Url = "right_03_rail_02_url";
                    public const string Right05Rail04Url = "right_05_rail_04_url";
                    public const string Rightrailhead04 = "rightrailhead_04";
                    public const string Right04Rail05Url = "right_04_rail_05_url";
                    public const string Right01Rail01Statement = "right_01_rail_01_statement";
                    public const string Right05Rail05Statement = "right_05_rail_05_statement";
                    public const string Right05Rail01Statement = "right_05_rail_01_statement";
                    public const string Rightrailhead05 = "rightrailhead_05";
                    public const string Right03Rail03Statement = "right_03_rail_03_statement";
                    public const string Right05Rail04Statement = "right_05_rail_04_statement";
                    public const string Right03Rail03Url = "right_03_rail_03_url";
                }
                internal static class Hotlinks {
                    public const string NodeName = "hotlinks";
                    public const string Url06Url = "url_06_url";
                    public const string Url06UrlStatement = "url_06_url_statement";
                    public const string Url05UrlStatement = "url_05_url_statement";
                    public const string Url07 = "url_07";
                    public const string Url03UrlStatement = "url_03_url_statement";
                    public const string Url07Url = "url_07_url";
                    public const string Url02Url = "url_02_url";
                    public const string Url01UrlStatement = "url_01_url_statement";
                    public const string Url04 = "url_04";
                    public const string Url04UrlStatement = "url_04_url_statement";
                    public const string Url02UrlStatement = "url_02_url_statement";
                    public const string Url06 = "url_06";
                    public const string Url05Url = "url_05_url";
                    public const string Url03Url = "url_03_url";
                    public const string Url01 = "url_01";
                    public const string Url03 = "url_03";
                    public const string Url04Url = "url_04_url";
                    public const string Url05 = "url_05";
                    public const string Url02 = "url_02";
                    public const string Url01Url = "url_01_url";
                    public const string Url07UrlStatement = "url_07_url_statement";
                }
                internal static class Technicalspecificationssku {
                    public const string NodeName = "technicalspecificationssku";
                    public const string Prodnameshort = "prodnameshort";
                }
                internal static class DeleteThese {
                    public const string NodeName = "deletethese";
                    public const string Ltdksp01Short = "ltdksp_01_short";
                    public const string TmktOverviewMedium = "tmkt_overview_medium";
                    public const string Ltdksp02Short = "ltdksp_02_short";
                    public const string LtdproddesMedium = "ltdproddes_medium";
                    public const string Ltdksp03Short = "ltdksp_03_short";
                }
            }
            internal static class Facets {
                public const string NodeName = "facets";
                internal static class DeleteThese {
                    public const string NodeName = "deletethese";
                    public const string FacetPerf = "facet_perf";
                    public const string FacetProdcat = "facet_prodcat";
                }
                internal static class FacetsfacetsUrl {
                    public const string NodeName = "facetsfacets_url";
                    public const string FacetTapetechUrl = "facet_tapetech_url";
                    public const string FacetChannelsUrl = "facet_channels_url";
                    public const string FacetSwitchtypeUrl = "facet_switchtype_url";
                    public const string FacetInterfaceUrl = "facet_interface_url";
                    public const string FacetWattsUrl = "facet_watts_url";
                    public const string FacetProductlineUrl = "facet_productline_url";
                    public const string FacetRecommendedprodUrl = "facet_recommendedprod_url";
                    public const string FacetPrimeappsUrl = "facet_primeapps_url";
                    public const string FacetSegmentUrl = "facet_segment_url";
                    public const string FacetUplinkspeedmediaUrl = "facet_uplinkspeedmedia_url";
                    public const string FacetCarriertypeUrl = "facet_carriertype_url";
                    public const string FacetHotplugUrl = "facet_hotplug_url";
                    public const string FacetDimmtypeUrl = "facet_dimmtype_url";
                    public const string FacetWorkloadUrl = "facet_workload_url";
                    public const string FacetFormfactorUrl = "facet_formfactor_url";
                    public const string FacetPorttypeUrl = "facet_porttype_url";
                    public const string FacetRackhghtUrl = "facet_rackhght_url";
                    public const string FacetProtocolsUrl = "facet_protocols_url";
                    public const string FacetSeriesnameUrl = "facet_seriesname_url";
                    public const string FacetRedundantPowerUrl = "facet_redundant_power_url";
                    public const string FacetUsageUrl = "facet_usage_url";
                    public const string FacetOptdriveUrl = "facet_optdrive_url";
                    public const string FacetControllertypeUrl = "facet_controllertype_url";
                    public const string FacetServergenUrl = "facet_servergen_url";
                    public const string FacetMemstdUrl = "facet_memstd_url";
                    public const string FacetProdtypeUrl = "facet_prodtype_url";
                    public const string FacetOsUrl = "facet_os_url";
                    public const string FacetBandwdthUrl = "facet_bandwdth_url";
                    public const string FacetConnectUrl = "facet_connect_url";
                    public const string FacetSsdtypeUrl = "facet_ssdtype_url";
                    public const string FacetUserAccessUrl = "facet_user_access_url";
                    public const string FacetServerfamilyUrl = "facet_serverfamily_url";
                    public const string FacetSubbrandUrl = "facet_subbrand_url";
                    public const string FacetAdapterTypeUrl = "facet_adapter_type_url";
                    public const string FacetMediaformatUrl = "facet_mediaformat_url";
                    public const string FacetTapelabeloptUrl = "facet_tapelabelopt_url";
                    public const string FacetFeatUrl = "facet_feat_url";
                    public const string FacetPortsmaxUrl = "facet_portsmax_url";
                    public const string FacetTechnologyUrl = "facet_technology_url";
                    public const string FacetCategoryUrl = "facet_category_url";
                    public const string FacetProcessortypeUrl = "facet_processortype_url";
                    public const string FacetProcessorsmaxUrl = "facet_processorsmax_url";
                    public const string FacetSubcategoryUrl = "facet_subcategory_url";
                    public const string FacetRoutingswitchingUrl = "facet_routingswitching_url";
                    public const string FacetTransrateUrl = "facet_transrate_url";
                    public const string FacetCapUrl = "facet_cap_url";
                    public const string FacetProcessorspdUrl = "facet_processorspd_url";
                    public const string FacetPortcountUrl = "facet_portcount_url";
                    public const string FacetManagementUrl = "facet_management_url";
                }
                internal static class FacetsInner {
                    public const string NodeName = "facets";
                    public const string FacetInterface = "facet_interface";
                    public const string FacetTieringSupport = "facet_tiering_support";
                    public const string FacetBandwdth = "facet_bandwdth";
                    public const string FacetSwitchtype = "facet_switchtype";
                    public const string FacetPorttype = "facet_porttype";
                    public const string FacetWorkload = "facet_workload";
                    public const string FacetProcessortype = "facet_processortype";
                    public const string FacetManagement = "facet_management";
                    public const string FacetFormfactor = "facet_formfactor";
                    public const string FacetProdtype = "facet_prodtype";
                    public const string FacetRecommendedprod = "facet_recommendedprod";
                    public const string FacetPortsmax = "facet_portsmax";
                    public const string FacetTechnology = "facet_technology";
                    public const string FacetOptdrive = "facet_optdrive";
                    public const string FacetFeat = "facet_feat";
                    public const string FacetWatts = "facet_watts";
                    public const string FacetMemstd = "facet_memstd";
                    public const string FacetCategory = "facet_category";
                    public const string FacetCarriertype = "facet_carriertype";
                    public const string FacetControllertype = "facet_controllertype";
                    public const string FacetTapelabelopt = "facet_tapelabelopt";
                    public const string FacetProcessorsmax = "facet_processorsmax";
                    public const string FacetAdapterType = "facet_adapter_type";
                    public const string FacetOs = "facet_os";
                    public const string FacetMediaformat = "facet_mediaformat";
                    public const string FacetConnect = "facet_connect";
                    public const string FacetPortcount = "facet_portcount";
                    public const string FacetDimmtype = "facet_dimmtype";
                    public const string FacetChannels = "facet_channels";
                    public const string FacetProductline = "facet_productline";
                    public const string FacetRedundantPower = "facet_redundant_power";
                    public const string FacetProcessorspd = "facet_processorspd";
                    public const string FacetRoutingswitching = "facet_routingswitching";
                    public const string FacetProtocols = "facet_protocols";
                    public const string FacetSubbrand = "facet_subbrand";
                    public const string FacetUsage = "facet_usage";
                    public const string FacetServerfamily = "facet_serverfamily";
                    public const string FacetTapetech = "facet_tapetech";
                    public const string FacetUplinkspeedmedia = "facet_uplinkspeedmedia";
                    public const string FacetPrimeapps = "facet_primeapps";
                    public const string FacetRackhght = "facet_rackhght";
                    public const string FacetSubcategory = "facet_subcategory";
                    public const string FacetSsdtype = "facet_ssdtype";
                    public const string FacetSegment = "facet_segment";
                    public const string FacetHotplug = "facet_hotplug";
                    public const string FacetTransrate = "facet_transrate";
                    public const string FacetUserAccess = "facet_user_access";
                    public const string FacetSeriesname = "facet_seriesname";
                    public const string FacetServergen = "facet_servergen";
                    public const string FacetCap = "facet_cap";
                }
            }
        }
        internal static class Msc {
            public const string NodeName = "msc";
            internal static class Asset {
                public const string NodeName = "asset";
                public const string FileSize = "file_size";
                public const string AssetId = "asset_id";
                public const string Orientation = "orientation";
                public const string BcId = "bc_id";
                public const string KeyWord = "key_word";
                public const string MediaPlayLength = "media_play_length";
                public const string TranslationInstanceId = "translation_instance_id";
                public const string HtmlScript = "html_script";
                public const string DateInserted = "date_inserted";
                public const string AssetDisclosureLevel = "asset_disclosure_level";
                public const string AssetType = "asset_type";
                public const string DerivativeGuid = "derivative_guid";
                public const string BcAccountName = "bc_account_name";
                public const string PixelHeight = "pixel_height";
                public const string AssetName = "asset_name";
                public const string SourceRevisionNum = "source_revision_num";
                public const string TranslationCollectionId = "translation_collection_id";
                public const string PixelWidth = "pixel_width";
                public const string BcReferenceId = "bc_reference_id";
                public const string BusinessUnit = "business_unit";
                public const string TaskRevisionNum = "task_revision_num";
                public const string AssetDescription = "asset_description";
                public const string IsDeleted = "is_deleted";
                public const string LanguageCode = "language_code";
                public const string TaskGuid = "task_guid";
                public const string BackGround = "back_ground";
                public const string AssetCategory = "asset_category";
                public const string PlayerId = "player_id";
                public const string RenditionId = "rendition_id";
                public const string AssetTitle = "asset_title";
                public const string BcAccountId = "bc_account_id";
                public const string PlayerKey = "player_key";
            }
        }
        internal static class Metadata {
            public const string NodeName = "metadata";
            public const string Prodsequencing = "prodsequencing";
            public const string Featuredproductsmb = "featuredproductsmb";
            public const string Desctiptionmetatag = "desctiptionmetatag";
            public const string ItemPublishers = "item_publishers";
            internal static class MarketSegments {
                public const string NodeName = "market_segments";
                public const string Opt = "opt";
                public const string Description = "description";
                public const string ContentData = "content_data";
            }
            public const string Prodclassflag = "prodclassflag";
            internal static class Upc {
                public const string NodeName = "upc";
                public const string Opt = "opt";
                public const string Description = "description";
                public const string ContentData = "content_data";
            }
            public const string Carepackregistrationflag = "carepackregistrationflag";
            public const string Mpd = "mpd";
            public const string Tangibleflag = "tangibleflag";
        }
        internal static class Images {
            public const string NodeName = "images";
            internal static class Image {
                public const string NodeName = "image";
                public const string PlannedPublicDate = "planned_public_date";
                public const string Orientation = "orientation";
                public const string ImageUrlHttp = "image_url_http";
                public const string Action = "action";
                public const string FileName = "file_name";
                public const string DocumentType = "document_type";
                public const string ImageUrlHttps = "image_url_https";
                public const string Background = "background";
                public const string PixelHeight = "pixel_height";
                public const string PixelWidth = "pixel_width";
                public const string SearchKeyword = "search_keyword";
                public const string MasterObjectName = "master_object_name";
                public const string FullTitle = "full_title";
                public const string LanguageCode = "language_code";
                public const string CmgAcronym = "cmg_acronym";
                public const string DocumentTypeDetail = "document_type_detail";
                public const string ContentType = "content_type";
                public const string DpiResolution = "dpi_resolution";
            }
        }
        internal static class Hierarchy {
            public const string NodeName = "hierarchy";
            public const string ProductType = "product_type";
            public const string SupportSubcategory = "support_subcategory";
            public const string BigSeries = "big_series";
            public const string SmallSeries = "small_series";
            public const string Sku = "sku";
            public const string SupportName = "support_name";
            public const string SupportCategory = "support_category";
            public const string Model = "model";
            public const string MarketingSubCategory = "marketing_sub_category";
            public const string MarketingCategory = "marketing_category";
        }
        internal static class Plc {
            public const string NodeName = "plc";
            public const string AnnDate = "ann_date";
            public const string FullDate = "full_date";
            public const string ObsoleteDate = "obsolete_date";
            public const string EndOfSupportDate = "end_of_support_date";
            public const string BlindDate = "blind_date";
            public const string RemovalDate = "removal_date";
        }
        internal static class Documents {
            public const string NodeName = "documents";
            public const string Manuals = "manuals";
            internal static class Document {
                public const string NodeName = "document";
                public const string ConcentraInternalId = "concentra_internal_id";
                public const string LanguageLabel = "language_label";
                public const string MasterObjectName = "master_object_name";
                public const string Action = "action";
                public const string HpAudienceExpiryDate = "hp_audience_expiry_date";
                public const string LanguageCode = "language_code";
                public const string CreationDate = "creation_date";
                public const string SearchKeyword = "search_keyword";
                public const string CollectionFullTitle = "collection_full_title";
                public const string ContentUrl = "content_url";
                public const string PartnerAudienceExpiryDate = "partner_audience_expiry_date";
                public const string ContentUpdateDate = "content_update_date";
                public const string Url = "url";
                public const string CustomerSegmentCode = "customer_segment_code";
                public const string PubPageSize = "pub_page_size";
                public const string ObjectName = "object_name";
                public const string FullTitle = "full_title";
                public const string CustomerAudienceReleaseDate = "customer_audience_release_date";
                public const string HpAudienceReleaseDate = "hp_audience_release_date";
                public const string Description = "description";
                public const string PubFlag = "pub_flag";
                public const string PartnerAudienceReleaseDate = "partner_audience_release_date";
                public const string DocumentType = "document_type";
                public const string CustomerAudienceExpiryDate = "customer_audience_expiry_date";
                public const string DisclosureLevel = "disclosure_level";
                public const string SmartflowContentType = "smartflow_content_type";
            }
        }
        internal static class Links {
            public const string NodeName = "links";
            internal static class Link {
                public const string NodeName = "link";
                public const string ProductType = "product_type";
                public const string MarketingSubCategory = "marketing_sub_category";
                public const string FullDate = "full_date";
                public const string ObsoleteDate = "obsolete_date";
                public const string MarketingCategory = "marketing_category";
                public const string Type = "type";
                public const string Num = "num";
                public const string Order = "order";
                public const string Reco = "reco";
                public const string Pmoid = "pmoid";
                public const string Name = "name";
            }
        }
    }
}