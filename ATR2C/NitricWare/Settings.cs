namespace ATCSVCreator.NitricWare; 

public static class Settings {
    public static string separator = ",";
    public static string HamCallSign = "OE3FKG";

    public static Dictionary<string, int> TalkGroupCSVColumns = new() {
        {"dmrid", 1},
        {"name", 2},
        {"calltype", 3},
        {"alerttype", 4},
        {"createchannel", 5},
        {"addtolist", 6},
        {"network", 7},
        {"timeslot", 8}
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

public enum RepeaterCSVColumns {
    Band = 8,
    Tx,
    Rx,
    Callsign,
    Site,
    isI2
    
}