namespace ATCSVCreator.NitricWare; 

public class TalkGroupFileHandler {
    public List<TalkGroup> TalkGroupList;

    public TalkGroupFileHandler(string path) {
        List<TalkGroup> values = File.ReadAllLines(path)
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
            Convert.ToInt32(values[Settings.TalkGroupCSVColumns["timeslot"]]),
            Convert.ToBoolean(values[Settings.TalkGroupCSVColumns["addtoscanlist"]])
        );
        return talkgroup;
    }
}