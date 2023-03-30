using CsvHelper.Configuration;

namespace ATCSVCreator.NitricWare; 

public class AnyToneZone {
    public string ID { get; set; }
    public string ZoneName { get; set; }
    public string ZoneChannelMember { get; set; }
    public string ZoneChannelMemberRXFrequency { get; set; }
    public string ZoneChannelMemberTXFrequency { get; set; }
    public string AChannel { get; set; }
    public string AChannelRXFrequency { get; set; }
    public string AChannelTXFrequency { get; set; }
    public string BChannel { get; set; }
    public string BChannelRXFrequency { get; set; }
    public string BChannelTXFrequency { get; set; }
    public string ZoneHide { get; set; } = "0";
}

public sealed class AnyToneZoneClassMap : ClassMap<AnyToneZone>
{
    public AnyToneZoneClassMap()
    {
        Map(m => m.ID).Name("No.");
        Map(m => m.ZoneName).Name("Zone Name");
        Map(m => m.ZoneChannelMember).Name("Zone Channel Member");
        Map(m => m.ZoneChannelMemberRXFrequency).Name("Zone Channel Member RX Frequency");
        Map(m => m.ZoneChannelMemberTXFrequency).Name("Zone Channel Member TX Frequency");
        Map(m => m.AChannel).Name("A Channel");
        Map(m => m.AChannelRXFrequency).Name("A Channel RX Frequency");
        Map(m => m.AChannelTXFrequency).Name("A Channel TX Frequency");
        Map(m => m.BChannel).Name("B Channel");
        Map(m => m.BChannelRXFrequency).Name("B Channel RX Frequency");
        Map(m => m.BChannelTXFrequency).Name("B Channel TX Frequency");
        Map(m => m.ZoneHide).Name("Zone Hide ");
    }
}