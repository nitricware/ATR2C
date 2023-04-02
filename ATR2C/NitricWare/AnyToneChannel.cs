using CsvHelper.Configuration;

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
    public string RadioID { get; set; }
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

public sealed class AnyToneChannelClassMap : ClassMap<AnyToneChannel>
{
    public AnyToneChannelClassMap()
    {
        Map(m => m.ID).Name("No.");
        Map(m => m.ChannelName).Name("Channel Name");
        Map(m => m.ReceiveFrequency).Name("Receive Frequency");
        Map(m => m.TransmitFrequency).Name("Transmit Frequency");
        Map(m => m.ChannelType).Name("Channel Type");
        Map(m => m.TransmitPower).Name("Transmit Power");
        Map(m => m.BandWidth).Name("Band Width");
        Map(m => m.CTCSSDecode).Name("CTCSS/DCS Decode");
        Map(m => m.CTCSSEncode).Name("CTCSS/DCS Encode");
        Map(m => m.Contact);
        Map(m => m.ContactCallType).Name("Contact Call Type");
        Map(m => m.ContactTG).Name("Contact TG/DMR ID");
        Map(m => m.RadioID).Name("Radio ID");
        Map(m => m.BusyLock).Name("Busy Lock/TX Permit");
        Map(m => m.SquelchMode).Name("Squelch Mode");
        Map(m => m.OptionalSignal).Name("Optional Setting");
        Map(m => m.DTMFID).Name("DTMF ID");
        Map(m => m.ToneID2).Name("2Tone ID");
        Map(m => m.ToneID5).Name("5Tone ID");
        Map(m => m.PTTID).Name("PTT ID");
        Map(m => m.ColorCode).Name("Color Code");
        Map(m => m.Slot);
        Map(m => m.ScanList).Name("Scan List");
        Map(m => m.ReceiveGroupList).Name("Receive Group List");
        Map(m => m.PTTProhibit).Name("PTT Prohibit");
        Map(m => m.Reverse);
        Map(m => m.SimplexTDMA).Name("Simplex TDMA");
        Map(m => m.SlotSuit).Name("Slot Suit");
        Map(m => m.AESDigitalEncryption).Name("AES Digital Encryption");
        Map(m => m.DigitalEncryption).Name("Digital Encryption");
        Map(m => m.CallConfirmation).Name("Call Confirmation");
        Map(m => m.TalkAround).Name("Talk Around(Simplex)");
        Map(m => m.WorkAlone).Name("Work Alone");
        Map(m => m.CustomCTCSS).Name("Custom CTCSS");
        Map(m => m.ToneDecode2).Name("2TONE Decode");
        Map(m => m.Ranging);
        Map(m => m.ThroughMode).Name("Through Mode");
        Map(m => m.APRSRX).Name("APRS RX");
        Map(m => m.AnalogAPRSPTTMode).Name("Analog APRS PTT Mode");
        Map(m => m.DigitalAPRSPTTMode).Name("Digital APRS PTT Mode");
        Map(m => m.APRSReportType).Name("APRS Report Type");
        Map(m => m.DigitalAPRSReportChannel).Name("Digital APRS Report Channel");
        Map(m => m.CorrectFrequency).Name("Correct Frequency[Hz]");
        Map(m => m.SMSConfirmation).Name("SMS Confirmation");
        Map(m => m.ExcludeChannelFromRoaming).Name("Exclude channel from roaming");
        Map(m => m.DMRMode).Name("DMR MODE");
        Map(m => m.DataACKDisable).Name("DataACK Disable");
        Map(m => m.R5ToneBot).Name("R5toneBot");
        Map(m => m.R5ToneEot).Name("R5toneEot");
        Map(m => m.AutoScan).Name("Auto Scan");
        Map(m => m.AnaAPRSMute).Name("Ana Aprs Mute");
        Map(m => m.SendTalkerAlias).Name("Send Talker Alias");
        Map(m => m.AnaAPRSTXPath).Name("AnaAprsTxPath");
    }
}