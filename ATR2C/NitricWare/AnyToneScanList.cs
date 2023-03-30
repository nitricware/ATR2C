using CsvHelper.Configuration;

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

public sealed class AnyToneScanListClassMap : ClassMap<AnyToneScanList>
{
    public AnyToneScanListClassMap()
    {
        Map(m => m.ID).Name("No.");
        Map(m => m.ScanListName).Name("Scan List Name");
        Map(m => m.ScanChannelMember).Name("Scan Channel Member");
        Map(m => m.ScanChannelMemberRXFrequency).Name("Scan Channel Member RX Frequency");
        Map(m => m.ScanChannelMemberTXFrequency).Name("Scan Channel Member TX Frequency");
        Map(m => m.ScanMode).Name("Scan Mode");
        Map(m => m.PriorityChannelSelect).Name("Priority Channel Select");
        Map(m => m.PriorityChannel1).Name("Priority Channel 1");
        Map(m => m.PriorityChannel1RXFrequency).Name("Priority Channel 1 RX Frequency");
        Map(m => m.PriorityChannel1TXFrequency).Name("Priority Channel 1 TX Frequency");
        Map(m => m.PriorityChannel2).Name("Priority Channel 2");
        Map(m => m.PriorityChannel2RXFrequency).Name("Priority Channel 2 RX Frequency");
        Map(m => m.PriorityChannel2TXFrequency).Name("Priority Channel 2 TX Frequency");
        Map(m => m.RevertChannel).Name("Revert Channel");
        Map(m => m.LookBackTimeA).Name("Look Back Time A[s]");
        Map(m => m.LookBackTimeB).Name("Look Back Time B[s]");
        Map(m => m.DropoutDelayTime).Name("Dropout Delay Time[s]");
        Map(m => m.DwellTime).Name("Dwell Time[s]");
    }
}