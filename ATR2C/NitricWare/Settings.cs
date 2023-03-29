namespace ATCSVCreator.NitricWare; 

public static class Settings {
    public static string separator = ",";
    public static string HamCallSign = "OE3FKG";
    public static string InputFile = "./input/repeater.csv";
    public static bool CreateDigitalScanLists = true;
    public static bool CreateAnalogScanLists = true;

    public static Dictionary<string, int> TalkGroupCSVColumns = new() {
        {"dmrid", 1},
        {"name", 2},
        {"calltype", 3},
        {"alerttype", 4},
        {"createchannel", 5},
        {"addtolist", 6},
        {"network", 7},
        {"timeslot", 8},
        {"addtoscanlist", 9}
    };

    public static Dictionary<string, int> RepeaterCSVColumns = new() {
        { "band", 0 },
        { "tx", 6 },
        { "rx", 5 },
        { "callsign", 7 },
        { "site", 9 },
        { "isI2", 26 },
        { "isBM", 27 },
        { "colorcode", 25 },
        { "isDMR", 24 },
        { "isFM", 17 },
        { "ctcsstx", 19},
        { "ctcssrx", 20},
        { "hasEchoLink", 21 },
        { "echolinkID", 22 },
        { "c4fm", 29 },
        { "dstar", 31 },
        { "tetra", 34 }
    };
}

public enum OEVSVRepeaterCSVColumns {
    Band = 0,
    Type = 4,
    Tx = 6,
    Rx = 5,
    Callsign = 7,
    Site = 9,
    Status = 16,
    IPSC2 = 26,
    Brandmeister = 27,
    Colorcode = 25,
    DMR = 24,
    FM = 17,
    CTCSSTx = 19,
    CTCSSRx = 20,
    EchoLink = 21,
    EchoLinkID = 22,
    C4FM = 29,
    DStar = 31,
    Tetra = 34
}