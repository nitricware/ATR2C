using CsvHelper.Configuration.Attributes;

namespace ATCSVCreator.NitricWare.AnyTone; 

public class AnyToneZone {
    [Name("No.")]
    public string Id { get; set; }
    [Name("Zone Name")]
    public string ZoneName { get; set; }
    [Name("Zone Channel Member")]
    public string ZoneChannelMember { get; set; }
    [Name("Zone Channel Member RX Frequency")]
    public string ZoneChannelMemberRxFrequency { get; set; }
    [Name("Zone Channel Member TX Frequency")]
    public string ZoneChannelMemberTxFrequency { get; set; }
    [Name("A Channel")]
    public string AChannel { get; set; }
    [Name("A Channel RX Frequency")]
    public string AChannelRxFrequency { get; set; }
    [Name("A Channel TX Frequency")]
    public string AChannelTxFrequency { get; set; }
    [Name("B Channel")]
    public string BChannel { get; set; }
    [Name("B Channel RX Frequency")]
    public string BChannelRxFrequency { get; set; }
    [Name("B Channel TX Frequency")]
    public string BChannelTxFrequency { get; set; }
    [Name("Zone Hide ")]
    public string ZoneHide { get; set; } = "0";
}