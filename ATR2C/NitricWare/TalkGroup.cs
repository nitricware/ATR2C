namespace ATCSVCreator.NitricWare; 

public class TalkGroup {
    public DMRNetwork Network;
    public int DMRid;
    public string Name;
    public DMRCallType CallType;
    public string CallAlert;
    public bool CreateChannel;
    public bool AddToList;
    public int TimeSlot;
    public bool AddToScanList;

    public TalkGroup(
        string network,
        int dmrid,
        string name,
        string callType,
        string callAlert,
        bool createChannel,
        bool addToList,
        int timeSlot,
        bool addToScanList
    ) {
        this.DMRid = dmrid;
        this.Name = name;
        this.CallAlert = callAlert;
        this.CreateChannel = createChannel;
        this.AddToList = addToList;
        this.TimeSlot = timeSlot;
        AddToScanList = addToScanList;
        
        // parse network

        this.Network = DMRNetwork.NO;

        if (network == "BM") {
            this.Network = DMRNetwork.BM;
        }
        
        if (network == "I2") {
            this.Network = DMRNetwork.I2;
        }

        // parse calltype

        this.CallType = DMRCallType.GroupCall;

        if (callType == "Private Call") {
            this.CallType = DMRCallType.PrivateCall;
        }
    }

    public AnyToneTalkGroup ToAnyToneTalkgroup() {
        AnyToneTalkGroup tg = new AnyToneTalkGroup();
        tg.RadioID = DMRid.ToString();
        tg.Name = Name;

        return tg;
    }
}