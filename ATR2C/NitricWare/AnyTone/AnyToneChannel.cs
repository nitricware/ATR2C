using CsvHelper.Configuration.Attributes;

namespace ATCSVCreator.NitricWare.AnyTone; 

public class AnyToneChannel {
    [Name("No.")]
    public string Id { get; set; }
    [Name("Channel Name")]
    public string ChannelName { get; set; }
    [Name("Receive Frequency")]
    public string ReceiveFrequency { get; set; }
    [Name("Transmit Frequency")]
    public string TransmitFrequency { get; set; }
    [Name("Channel Type")]
    public string ChannelType { get; set; }
    [Name("Transmit Power")]
    public string TransmitPower { get; set; } = "Mid";
    [Name("Band Width")]
    public string BandWidth { get; set; } = "12.5K";
    [Name("CTCSS/DCS Decode")]
    public string CtcssDecode { get; set; } = "Off";
    [Name("CTCSS/DCS Encode")]
    public string CtcssEncode { get; set; } = "Off";
    public string Contact { get; set; }
    [Name("Contact Call Type")]
    public string ContactCallType { get; set; } = "Group Call";
    [Name("Contact TG/DMR ID")]
    public string ContactTg { get; set; }
    [Name("Radio ID")]
    public string RadioId { get; set; }
    [Name("Busy Lock/TX Permit")]
    public string BusyLock { get; set; } = "Always";
    [Name("Squelch Mode")]
    public string SquelchMode { get; set; } = "Carrier";
    [Name("Optional Signal")]
    public string OptionalSignal { get; set; } = "Off";
    [Name("DTMF ID")]
    public string Dtmfid { get; set; } = "1";
    [Name("2Tone ID")]
    public string ToneId2 { get; set; } = "1";
    [Name("5Tone ID")]
    public string ToneId5 { get; set; } = "1";
    [Name("PTT ID")]
    public string Pttid { get; set; } = "Off";
    [Name("Color Code")]
    public string ColorCode { get; set; }
    public string Slot { get; set; }
    [Name("Scan List")]
    public string ScanList { get; set; } = "None";
    [Name("Receive Group List")]
    public string ReceiveGroupList { get; set; } = "None";
    [Name("PTT Prohibit")]
    public string PttProhibit { get; set; } = "Off";
    public string Reverse { get; set; } = "Off";
    [Name("Simplex TDMA")]
    public string SimplexTdma { get; set; } = "Off";
    [Name("Slot Suit")]
    public string SlotSuit { get; set; } = "Off";
    [Name("AES Digital Encryption")]
    public string AesDigitalEncryption { get; set; } = "Normal Encryption";
    [Name("Digital Encryption")]
    public string DigitalEncryption { get; set; } = "Off";
    [Name("Call Confirmation")]
    public string CallConfirmation { get; set; } = "Off";
    [Name("Talk Around(Simplex)")]
    public string TalkAround { get; set; } = "Off";
    [Name("Work Alone")]
    public string WorkAlone { get; set; } = "Off";
    [Name("Custom CTCSS")]
    public string CustomCtcss { get; set; } = "131.8";
    [Name("2TONE Decode")]
    public string ToneDecode2 { get; set; } = "1";
    public string Ranging { get; set; } = "Off";
    [Name("Through Mode")]
    public string ThroughMode { get; set; } = "Off";
    [Name("APRS RX")]
    public string Aprsrx { get; set; } = "Off";
    [Name("Analog APRS PTT Mode")]
    public string AnalogAprspttMode { get; set; } = "Off";
    [Name("Digital APRS PTT Mode")]
    public string DigitalAprspttMode { get; set; } = "Off";
    [Name("APRS Report Type")]
    public string AprsReportType { get; set; } = "Digital";
    [Name("Digital APRS Report Channel")]
    public string DigitalAprsReportChannel { get; set; } = "1";
    [Name("Correct Frequency[Hz]")]
    public string CorrectFrequency { get; set; } = "0";
    [Name("SMS Confirmation")]
    public string SmsConfirmation { get; set; } = "Off";
    [Name("Exclude channel from roaming")]
    public string ExcludeChannelFromRoaming { get; set; } = "0";
    [Name("DMR MODE")]
    public string DmrMode { get; set; } = "1";
    [Name("DataACK Disable")]
    public string DataAckDisable { get; set; } = "0";
    [Name("R5toneBot")]
    public string R5ToneBot { get; set; } = "0";
    [Name("R5toneEot")]
    public string R5ToneEot { get; set; } = "0";
    [Name("Auto Scan")]
    public string AutoScan { get; set; } = "0";
    [Name("Ana Aprs Mute")]
    public string AnaAprsMute { get; set; } = "0";
    [Name("Send Talker Alias")]
    public string SendTalkerAlias { get; set; } = "0";
    [Name("AnaAprsTxPath")]
    public string AnaAprstxPath { get; set; } = "0";
}