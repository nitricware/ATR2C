using ATCSVCreator.NitricWare.CPSObjects;
using CsvHelper.Configuration.Attributes;

namespace ATCSVCreator.NitricWare.AnyTone; 

public class AnyToneAnalogContact : IAnalogContact {
    [Name("No.")]
    public string Id { get; set; }
    public string Number { get; set; }
    public string Name { get; set; }
}