namespace ATCSVCreator.NitricWare; 

public static class Settings {
    public static readonly string HamCallSign = "OE3ZZZ";
    // TODO: implement functionality 
    public static bool CreateDigitalScanLists = true;
    public static bool CreateAnalogScanLists = true;
    public static List<string> exportTypes = new() {
        "AnyTone AT-D878UVII Plus",
        "CHIRP"
    };
}