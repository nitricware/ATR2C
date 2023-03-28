namespace ATCSVCreator.NitricWare; 

public class AnyToneZone {
    public string ID { get; set; }
    public string ChannelName { get; set; }
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