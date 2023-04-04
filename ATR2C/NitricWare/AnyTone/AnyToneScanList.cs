using CsvHelper.Configuration.Attributes;

namespace ATCSVCreator.NitricWare.AnyTone; 

public class AnyToneScanList {
    [Name("No.")]
    public string Id { get; set; }
    [Name("Scan List Name")]
    public string ScanListName { get; set; }
    [Name("Scan Channel Member")]
    public string ScanChannelMember { get; set; }
    [Name("Scan Channel Member RX Frequency")]
    public string ScanChannelMemberRxFrequency { get; set; }
    [Name("Scan Channel Member TX Frequency")]
    public string ScanChannelMemberTxFrequency { get; set; }
    [Name("Scan Mode")]
    public string ScanMode { get; set; } = "Off";
    [Name("Priority Channel Select")]
    public string PriorityChannelSelect { get; set; } = "Off";
    [Name("Priority Channel 1")]
    public string PriorityChannel1 { get; set; } = "Off";
    [Name("Priority Channel 1 RX Frequency")]
    public string PriorityChannel1RxFrequency { get; set; } = "";
    [Name("Priority Channel 1 TX Frequency")]
    public string PriorityChannel1TxFrequency { get; set; } = "";
    [Name("Priority Channel 2")]
    public string PriorityChannel2 { get; set; } = "Off";
    [Name("Priority Channel 2 RX Frequency")]
    public string PriorityChannel2RxFrequency { get; set; } = "";
    [Name("Priority Channel 2 TX Frequency")]
    public string PriorityChannel2TxFrequency { get; set; } = "";
    [Name("Revert Channel")]
    public string RevertChannel { get; set; } = "Selected";
    [Name("Look Back Time A[s]")]
    public string LookBackTimeA { get; set; } = "5.0";
    [Name("Look Back Time B[s]")]
    public string LookBackTimeB { get; set; } = "5.0";
    [Name("Dropout Delay Time[s]")]
    public string DropoutDelayTime { get; set; } = "5.0";
    [Name("Dwell Time[s]")]
    public string DwellTime { get; set; } = "5.0";
}