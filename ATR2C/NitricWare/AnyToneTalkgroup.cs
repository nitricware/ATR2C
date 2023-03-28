namespace ATCSVCreator.NitricWare; 

public class AnyToneTalkgroup {
    public string ID { get; set; }
    public string RadioID { get; set; }
    public string Name { get; set; }
    public string CallType { get; set; } = "Group Call";
    public string CallAlert { get; set; } = "None";
}