namespace ATCSVCreator.NitricWare; 

public class TalkGroupFileHandler {
    private readonly string _pathToTalkGroupCsv = "./input/talkgroups.csv";
    public List<TalkGroup> TalkGroupList;

    public void LoadTalkgroupCSV() {
        List<TalkGroup> values = File.ReadAllLines(_pathToTalkGroupCsv)
            .Skip(1)
            .Select(v => GetTalkGroup(v))
            .ToList();
        TalkGroupList = values;
    }

    private TalkGroup GetTalkGroup(string line) {
        string[] values = line.Split(Settings.separator);
        TalkGroup talkgroup = new TalkGroup(
            values[Settings.TalkGroupCSVColumns["network"]],
            Convert.ToInt32(values[Settings.TalkGroupCSVColumns["dmrid"]]),
            Convert.ToString(values[Settings.TalkGroupCSVColumns["name"]]),
            values[Settings.TalkGroupCSVColumns["calltype"]],
            values[Settings.TalkGroupCSVColumns["alerttype"]],
            Convert.ToBoolean(values[Settings.TalkGroupCSVColumns["createchannel"]]),
            Convert.ToBoolean(values[Settings.TalkGroupCSVColumns["addtolist"]]),
            Convert.ToInt32(values[Settings.TalkGroupCSVColumns["timeslot"]])
        );
        return talkgroup;
    }
}