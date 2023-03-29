using System.Globalization;
using CsvHelper;

namespace ATCSVCreator.NitricWare; 

public class CSVCreator {
    public List<Repeater> Repeater;
    public List<AnyToneZone> Zones;
    public List<AnyToneChannel> Channels;
    public List<AnyToneTalkgroup> Talkgroups;
    public List<AnyToneAnalogContact> AnalogAddressBook;

    /*public CSVCreator(
        List<Repeater> repeater
        ) {
        Repeater = repeater;
        Zones = new List<AnyToneZone>();
        Channels = new List<AnyToneChannel>();

        Directory.CreateDirectory("export");
    }*/

    public void CreateAnalogZones() {
        foreach (var repeater in Repeater) {
            if (repeater.Type == RepeaterType.DMR) {
                continue;
            }
            
            // Create the analog channel
            var channel = new AnyToneChannel();
            channel.ChannelName = repeater.Callsign + " " + repeater.Band;
            channel.ReceiveFrequency = repeater.Rx.ToString("0.00000").Replace(",",".");
            channel.TransmitFrequency = repeater.Tx.ToString("0.00000").Replace(",",".");
            channel.ChannelType = "A-Analog";
            channel.Contact = "";
            channel.ContactTG = "";
            channel.ColorCode = "";
            channel.Slot = "";
            channel.BandWidth = "12.5K";
            // TODO: ÖVSV repeater list lists Tx and Rx always from a repeater point of view; add a setting to let the user specify.
            channel.CTCSSDecode = repeater.CTSSTX == 0 ? "Off" : repeater.CTSSTX.ToString("0.0").Replace(",",".");
            channel.CTCSSEncode = repeater.CTSSRX == 0 ? "Off" : repeater.CTSSRX.ToString("0.0").Replace(",",".");
            channel.SquelchMode = (repeater.CTSSRX != 0 || repeater.CTSSTX != 0) ? "CTCSS/DCS" : "Carrier";
            channel.ColorCode = "0";
            channel.Slot = "1";
            channel.DigitalAPRSPTTMode = "Off";
            channel.APRSReportType = "Analog";
            channel.DMRMode = "0";
            channel.BusyLock = "Off";
            
            Channels.Add(channel);
            
            // Add the analog channel to the analog zone;
            // Create if it does not exist yet.
            var location = channel.ChannelName.Substring(0, 3);
            var fmZoneName = location + " FM";
            // https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1.find?view=net-8.0
            var fmZone = Zones.Find(zone => zone.ChannelName.Equals(fmZoneName));
            
            if(fmZone == null) {
                // Zone for location does not exist yet.
                // Create a zone, add this repeater as the A and B Channel
                var zone = new AnyToneZone();
                zone.ChannelName = location + " FM";
                
                zone.AChannel = channel.ChannelName;
                zone.AChannelRXFrequency = channel.ReceiveFrequency;
                zone.AChannelTXFrequency = channel.TransmitFrequency;
                zone.BChannel = zone.AChannel;
                zone.BChannelRXFrequency = zone.AChannelRXFrequency;
                zone.BChannelTXFrequency = zone.AChannelTXFrequency;

                // Add this channel to the zone's channel list
                
                zone.ZoneChannelMember = zone.AChannel;
                zone.ZoneChannelMemberRXFrequency = zone.AChannelRXFrequency;
                zone.ZoneChannelMemberTXFrequency = zone.AChannelTXFrequency;
                
                Zones.Add(zone);
            } else {
                // Zone does exist already.
                // Add this channel as one of its channels
                fmZone.ZoneChannelMember += "|"+channel.ChannelName;
                fmZone.ZoneChannelMemberRXFrequency += "|"+channel.ReceiveFrequency;
                fmZone.ZoneChannelMemberTXFrequency += "|"+channel.TransmitFrequency;
            }
        }
    }
    public void CreateDigitalZones() {
        foreach (var repeater in Repeater) {
            if (!repeater.NeedsZone) {
                continue;
            }
            var zone = new AnyToneZone();
            zone.ChannelName = repeater.ZoneName;

            List<string> zoneChannelMember = new List<string>();
            List<string> zoneChannelMemberRXFrequency = new List<string>();
            List<string> zoneChannelMemberTXFrequency = new List<string>();

            AnyToneChannel mainChannel = repeater.ChannelList.First();
            foreach (var channel in repeater.ChannelList) {
                zoneChannelMember.Add(channel.ChannelName);
                zoneChannelMemberRXFrequency.Add(channel.ReceiveFrequency);
                zoneChannelMemberTXFrequency.Add(channel.TransmitFrequency);
                
                Channels.Add(channel);
            }
            zone.ZoneChannelMember = string.Join("|",zoneChannelMember);
            zone.ZoneChannelMemberRXFrequency = string.Join("|",zoneChannelMemberRXFrequency);
            zone.ZoneChannelMemberTXFrequency = string.Join("|",zoneChannelMemberTXFrequency);
            
            zone.AChannel = mainChannel.ChannelName;
            zone.AChannelRXFrequency = mainChannel.ReceiveFrequency;
            zone.AChannelTXFrequency = mainChannel.TransmitFrequency;
            zone.BChannel = zone.AChannel;
            zone.BChannelRXFrequency = zone.AChannelRXFrequency;
            zone.BChannelTXFrequency = zone.AChannelTXFrequency;
            
            Zones.Add(zone);
        }
    }

    public void CreateAllFiles() {
        CreateZonesFile();
        CreateChannelsFile();
        CreateAnalogAddressBookFile();
        CreateTalkgroupsFile();
        
        MergeDefaults();
    }
    public void CreateZonesFile() {
        using (var writer = new StreamWriter("./export/Zone.csv"))
        using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
        {
            csv.WriteRecords(Zones);
        }
    }

    public void CreateChannelsFile() {
        using (var writer = new StreamWriter("./export/Channel.csv"))
        using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
        {
            csv.WriteRecords(Channels);
        }
    }

    public void CreateTalkgroupsFile() {
        using (var writer = new StreamWriter("./export/TalkGroups.csv"))
        using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
        {
            csv.WriteRecords(Talkgroups);
        }
    }
    
    public void CreateAnalogAddressBookFile() {
        using (var writer = new StreamWriter("./export/AnalogAddressBook.csv"))
        using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
        {
            csv.WriteRecords(AnalogAddressBook);
        }
    }

    public void MergeDefaults() {
        MergeFile("Channel.csv");
        MergeFile("Zone.csv");
        MergeFile("TalkGroups.csv");
    }

    private void MergeFile(string path) {
        if (!File.Exists("./defaults/" + path)) {
            return;
        }
        
        var generatedFile = File.Open("./export/"+path, FileMode.Append ,FileAccess.Write);
        var defaultsFile = File.OpenRead("./defaults/"+path);
        
        defaultsFile.CopyTo(generatedFile);
        generatedFile.Close();
        defaultsFile.Close();
    }
}