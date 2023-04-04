using ATCSVCreator.NitricWare.ENUM;
using CsvHelper.Configuration.Attributes;

namespace ATCSVCreator.NitricWare.TalkGroups;

public class TalkGroup {
    [Name("No.")]
    public string Id { get; set; }
    // Rename CSV column to DMRID
    [Name("Radio ID")]
    public string DmrId { get; set; }
    public string Name { get; set; }
    [Name("Call Type")]
    public string CallType { get; set; }
    [Name("Call Alert")]
    public string CallAlert { get; set; }
    [Name("Create Channel")]
    public bool CreateChannel { get; set; }
    public bool AddToList { get; set; }
    public DmrNetwork Network { get; set; }
    public string TimeSlot { get; set; }
    public bool AddToScanList { get; set; }
}