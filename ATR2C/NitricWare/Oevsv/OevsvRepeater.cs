using ATCSVCreator.NitricWare.CPSObjects;
using ATCSVCreator.NitricWare.ENUM;
using CsvHelper.Configuration.Attributes;

namespace ATCSVCreator.NitricWare.Oevsv; 

public class OevsvRepeater : IRepeater {
    [Name("band")]
    public RadioBand Band { get; set; }
    [Name("type_of_station")]
    public StationType Type { get; set; }
    /*
     * The ÖVSV repeater list reverses Tx and Rx
     * because it's from the repeater's POV.
     *
     * This reversal is contrary to i.e. AnyTone
     * CPS POV.
     *
     * Therefore, the Rx column is saved to the Tx
     * property of the class and vice versa.
     */
    [Name("frequency_rx")]
    public double Tx { get; set; }
    [Name("frequency_tx")]
    public double Rx { get; set; }
    [Name("callsign")]
    public string Callsign { get; set; }
    [Name("site_name")]
    public string SiteName { get; set; }
    [Name("status")]
    public RepeaterStatus Status { get; set; }
    [Name("fm")]
    public bool IsFM { get; set; }
    [Name("ctcss_tx")]
    public string CtcssTx { get; set; }
    [Name("ctcss_rx")]
    public string CtcssRx { get; set; }
    [Name("echolink")]
    public bool IsEchoLink { get; set; }
    [Name("echolink_id")]
    public string EchoLinkId { get; set; }
    [Name("digital_id")]
    public string DigitalId { get; set; }
    [Name("dmr")]
    public bool IsDmr { get; set; }
    [Name("cc")]
    public string ColorCode { get; set; }
    [Name("ipsc2")]
    public bool IsIpsc2 { get; set; }
    [Name("brandmeister")]
    public bool IsBrandmeister { get; set; }
    [Name("c4fm")]
    public bool IsC4Fm { get; set; }
    [Name("dstar")]
    public bool IsDstar { get; set; }
    [Name("tetra")]
    public bool IsTetra { get; set; }
}

/* band	ch	ch_new	uid	type_of_station	frequency_tx	frequency_rx	callsign	antenna_heigth	site_name	sysop	url	hardware
 mmdvm	solar_power	battery_power	status	fm	fm_wakeup	ctcss_tx	ctcss_rx	echolink	echolink_id	digital_id	dmr	cc	ipsc2	brandmeister	
 network_registered	c4fm	c4fm_groups	dstar	dstar_rpt1	dstar_rpt2	tetra	other_mode	other_mode_name	comment	created_at	city	
 longitude	latitude	sea_level	locator_short	locator_long	geo_prefix	bev_gid	geom	name_address	gkz	bkz	kg	pg	bl */
