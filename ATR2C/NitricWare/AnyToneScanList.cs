namespace ATCSVCreator.NitricWare; 

public class AnyToneScanList {
    public string ID { get; set; }
    public string ScanListName { get; set; }
    public string ScanChannelMember { get; set; }
    public string ScanChannelMemberRXFrequency { get; set; }
    public string ScanChannelMemberTXFrequency { get; set; }
    public string ScanMode { get; set; } = "Off";
    public string PriorityChannelSelect { get; set; } = "Off";
    public string PriorityChannel1 { get; set; } = "Off";
    public string PriorityChannel1RXFrequency { get; set; } = "";
    public string PriorityChannel1TXFrequency { get; set; } = "";
    public string PriorityChannel2 { get; set; } = "Off";
    public string PriorityChannel2RXFrequency { get; set; } = "";
    public string PriorityChannel2TXFrequency { get; set; } = "";
    public string RevertChannel { get; set; } = "Selected";
    public string LookBackTimeA { get; set; } = "5.0";
    public string LookBackTimeB { get; set; } = "5.0";
    public string DropoutDelayTime { get; set; } = "5.0";
    public string DwellTime { get; set; } = "5.0";
}