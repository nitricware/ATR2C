namespace ATCSVCreator.NitricWare; 

public class Repeater {
    public string Band;
    public double Tx;           // Tx of the repeater, not Tx of the HT to send to the repeater
    public double Rx;           // Rx of the repeater, not Rx of the HT to receive from the repeater
    public string Callsign;
    public string Site;
    public int ColorCode;
    public RepeaterType Type;
    public DMRNetwork Network;

    public string ZoneName;
    public bool NeedsZone;

    public double CTSSTX;
    public double CTSSRX;

    public List<AnyToneChannel> ChannelList = new List<AnyToneChannel>();
    public Repeater(
        string band = "?",
        double tx = 0.0,
        double rx = 0.0,
        string callsign = "OE0AAA",
        string site = "?",
        bool isIPSC2 = false,
        bool isBrandMeister = false,
        int colorCode = 1,
        bool isDMR = false,
        bool isFM = false,
        double ctssTx = 0.0,
        double ctssRx = 0.0
        ) {
        // TODO: ÖVSV repeater list lists Tx and Rx always from a repeater point of view; add a setting to let the user specify.
        this.Band = band;
        this.Tx = tx;
        this.Rx = rx;
        this.Callsign = callsign;
        this.Site = site;
        this.ColorCode = colorCode;
        
        this.Network = DMRNetwork.NO;
        this.Type = RepeaterType.DUO;

        this.NeedsZone = true;

        this.CTSSTX = ctssTx;
        this.CTSSRX = ctssRx;
        
        if (isDMR && !isFM) {
            this.Type = RepeaterType.DMR;
            this.NeedsZone = true;
        }
        
        if (!isDMR && isFM) {
            this.Type = RepeaterType.FM;
            this.NeedsZone = false;
        }

        if (isBrandMeister) {
            this.Network = DMRNetwork.BM;
        }

        if (isIPSC2) {
            this.Network = DMRNetwork.I2;
        }

        // Assume that any Repeater with no network assigned is part of both
        // Mark any repeater with both networks assigned
        if ((isIPSC2 && isBrandMeister) || (isDMR && (!isIPSC2 && !isBrandMeister))) {
            this.Network = DMRNetwork.BO;
        }

        this.ZoneName = callsign + " " + (Network == DMRNetwork.NO ? "" : Network + " ") + site;
    }
}