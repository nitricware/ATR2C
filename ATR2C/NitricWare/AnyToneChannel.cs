namespace ATCSVCreator.NitricWare; 

public class AnyToneChannel {
    public string ID { get; set; }
    public string ChannelName { get; set; }
    public string ReceiveFrequency { get; set; }
    public string TransmitFrequency { get; set; }
    public string ChannelType { get; set; }
    public string TransmitPower { get; set; } = "Mid";
    public string BandWidth { get; set; } = "12.5K";
    public string CTCSSDecode { get; set; } = "Off";
    public string CTCSSEncode { get; set; } = "Off";
    public string Contact { get; set; }
    public string ContactCallType { get; set; } = "Group Call";
    public string ContactTG { get; set; }
    public string RadioID { get; set; } = Settings.HamCallSign;
    public string BusyLock { get; set; } = "Always";
    public string SquelchMode { get; set; } = "Carrier";
    public string OptionalSignal { get; set; } = "Off";
    public string DTMFID { get; set; } = "1";
    public string ToneID2 { get; set; } = "1";
    public string ToneID5 { get; set; } = "1";
    public string PTTID { get; set; } = "Off";
    public string ColorCode { get; set; }
    public string Slot { get; set; }
    public string ScanList { get; set; } = "None";
    public string ReceiveGroupList { get; set; } = "None";
    public string PTTProhibit { get; set; } = "Off";
    public string Reverse { get; set; } = "Off";
    public string SimplexTDMA { get; set; } = "Off";
    public string SlotSuit { get; set; } = "Off";
    public string AESDigitalEncryption { get; set; } = "Normal Encryption";
    public string DigitalEncryption { get; set; } = "Off";
    public string CallConfirmation { get; set; } = "Off";
    public string TalkAround { get; set; } = "Off";
    public string WorkAlone { get; set; } = "Off";
    public string CustomCTCSS { get; set; } = "131.8";
    public string ToneDecode2 { get; set; } = "1";
    public string Ranging { get; set; } = "Off";
    public string ThroughMode { get; set; } = "Off";
    public string APRSRX { get; set; } = "Off";
    public string AnalogAPRSPTTMode { get; set; } = "Off";
    public string DigitalAPRSPTTMode { get; set; } = "Off";
    public string APRSReportType { get; set; } = "Digital";
    public string DigitalAPRSReportChannel { get; set; } = "1";
    public string CorrectFrequency { get; set; } = "0";
    public string SMSConfirmation { get; set; } = "Off";
    public string ExcludeChannelFromRoaming { get; set; } = "0";
    public string DMRMode { get; set; } = "1";
    public string DataACKDisable { get; set; } = "0";
    public string R5ToneBot { get; set; } = "0";
    public string R5ToneEot { get; set; } = "0";
    public string AutoScan { get; set; } = "0";
    public string AnaAPRSMute { get; set; } = "0";
    public string SendTalkerAlias { get; set; } = "0";
    public string AnaAPRSTXPath { get; set; } = "0";
}