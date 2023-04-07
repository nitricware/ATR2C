using CsvHelper.Configuration.Attributes;

namespace ATCSVCreator.NitricWare.DigitalContactList; 

public class RadioIdDigitalContact {
    [Name("RADIO_ID")]
    public string DmrId { get; set; }
    [Name("CALLSIGN")]
    public string Callsign { get; set; }  
    [Name("FIRST_NAME")]
    public string FirstName { get; set; }
    [Name("LAST_NAME")]
    public string LastName { get; set; }
    [Name("CITY")]
    public string City { get; set; }
    [Name("STATE")]
    public string State { get; set; }
    [Name("COUNTRY")]
    public string Country { get; set; }
}