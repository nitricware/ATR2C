using ATCSVCreator.NitricWare;

var repeater = new RepeaterFileHandler();
var talkgroups = new TalkGroupFileHandler();

var talkGroupList = new List<AnyToneTalkgroup>();

// Load latest repeaters from CSV
repeater.LoadRepeaterCSV();

// Load talkgroups from CSV

talkgroups.LoadTalkgroupCSV();

// Create channels per repeater

repeater.TalkGroups = talkgroups.TalkGroupList;
repeater.CreateDigitalChannels();
foreach (var talkGroup in talkgroups.TalkGroupList) {
    talkGroupList.Add(talkGroup.ToAnyToneTalkgroup());
}

// create analog contacts (Echo Link DTMF tones)

/*Console.WriteLine("There are " + repeater.RepeaterList.Count + " Repeaters in total.");
var repCount = 0;
foreach (var rep in repeater.RepeaterList) {
    if (!rep.NeedsZone) {
        continue;
    }

    repCount++;
    Console.WriteLine(rep.ZoneName);
    Console.WriteLine(rep.Band + " " + rep.Type + "-Repeater " + rep.Callsign + " Receives on " + rep.Rx + 1.0 +
                      " and transmits on " + rep.Tx + " it requires CC" + rep.ColorCode);
    Console.WriteLine("Location: " + rep.Site);
    Console.WriteLine();
}

Console.WriteLine("There are " + (repeater.RepeaterList.Count - repCount) + " more analog FM repeaters.");*/

var CsvCreator = new CSVCreator(repeater.RepeaterList);
CsvCreator.AnalogAddressBook = repeater.AnyToneAnalogContacts;
CsvCreator.CreateAnalogAddressBookFile();

CsvCreator.CreateDigitalZones();
CsvCreator.CreateAnalogZones();

CsvCreator.CreateZonesFile();
CsvCreator.CreateChannelsFile();

CsvCreator.Talkgroups = talkGroupList;

CsvCreator.CreateTalkgroupsFile();

CsvCreator.MergeDefaults();