using ATCSVCreator.NitricWare;

OEVSVRepeaterFileHandler oevsvRepeaterFileHandler = new OEVSVRepeaterFileHandler();
TalkGroupFileHandler talkGroupFileHandler = new TalkGroupFileHandler();

// Load the talkgroups from file
// Transform the loaded talkgroups to a list for the CSVCreator
// Only add marked talkgroups (either AddToList or CreateChannel)
talkGroupFileHandler.LoadTalkgroupCSV();

List<AnyToneTalkGroup> anyToneTalkgroups = new();
foreach (var talkGroup in talkGroupFileHandler.TalkGroupList.Where(tg => tg.AddToList || tg.CreateChannel)) {
    anyToneTalkgroups.Add(talkGroup.ToAnyToneTalkgroup());
}

oevsvRepeaterFileHandler.TalkGroups = talkGroupFileHandler.TalkGroupList;
oevsvRepeaterFileHandler.LoadRepeaterCSV();

// Arm the CSVCreator with all created objects
CSVCreator csvCreator = new CSVCreator {
    Zones = oevsvRepeaterFileHandler.Zones,
    Channels = oevsvRepeaterFileHandler.Channels,
    Talkgroups = anyToneTalkgroups,
    AnalogAddressBook = oevsvRepeaterFileHandler.AnalogContacts,
    ScanLists = oevsvRepeaterFileHandler.ScanLists
};

csvCreator.CreateAllFiles();

public static class MyExtensions {
    public static string Truncate(this string s, int length) {
        if (s.Length > length)
            return s.Substring(0, length);
        return s;
    }
}