using ATCSVCreator.NitricWare.ENUM;

namespace ATCSVCreator.NitricWare.CPSObjects; 

public interface IRepeater {
    public RadioBand Band { get; set; }
    public StationType Type { get; set; }
    public double Tx { get; set; }
    public double Rx { get; set; }
    public string Callsign { get; set; }
    public string SiteName { get; set; }
    public RepeaterStatus Status { get; set; }
    public bool IsFM { get; set; }
    public string CtcssTx { get; set; }
    public string CtcssRx { get; set; }
    public bool IsEchoLink { get; set; }
    public string EchoLinkId { get; set; }
    public string DigitalId { get; set; }
    public bool IsDmr { get; set; }
    public string ColorCode { get; set; }
    public bool IsIpsc2 { get; set; }
    public bool IsBrandmeister { get; set; }
    public bool IsC4Fm { get; set; }
    public bool IsDstar { get; set; }
    public bool IsTetra { get; set; } 
}

