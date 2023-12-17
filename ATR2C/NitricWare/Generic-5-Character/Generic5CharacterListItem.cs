using CsvHelper.Configuration.Attributes;

namespace ATCSVCreator.NitricWare.CHIRP; 

public class Generic5CharacterListItem {
    [Name("Name")]
    public string Name { get; set; }
    [Name("RX")]
    public string Rx { get; set; }
    [Name("TX")]
    public string Tx { get; set; }
    [Name("CTCSS-RX")]
    public string CtcssRx { get; set; }
    [Name("CTCSS-TX")]
    public string CtcssTx { get; set; }
}