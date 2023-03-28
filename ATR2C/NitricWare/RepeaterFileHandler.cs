using System.Runtime.CompilerServices;

namespace ATCSVCreator.NitricWare; 

public class RepeaterFileHandler {
    private readonly string _pathToRepeaterCsv = "./input/repeater.csv";
    public List<Repeater> RepeaterList;
    public List<TalkGroup> TalkGroups;
    public List<AnyToneAnalogContact> AnyToneAnalogContacts = new List<AnyToneAnalogContact>();

    public void LoadRepeaterCSV() {
        List<Repeater> values = File.ReadAllLines(_pathToRepeaterCsv)
            .Skip(1)
            .Where(v => SkipLine(v))
            .Select(v => GetRepeater(v))
            .ToList();
        RepeaterList = values;
    }

    public bool SkipLine(string line) {
        string[] values = line.Split(Settings.separator);
        
        // Only parse 70cm and 2m repeaters
        if (values[0] != "70cm" && values[0] != "2m") {
            return false;
        }
        
        // Only parse voice repeaters; typo in csv;
        if (values[4] != "repeater_voice" && values[4] != "repeaer_voice") {
            return false;
        }
        
        // Only parse active repeaters
        if (values[16] != "active") {
            return false;
        }

        if (values[Settings.RepeaterCSVColumns["isFM"]] != "1" && values[Settings.RepeaterCSVColumns["isDMR"]] != "1") {
            return false;
        }
        
        // Ignore remaining entries that do not have either FM or DMR marked
        if (values[17] != "1" && values[24] != "1") {
            return false;
        }

        return true;
    }

    public Repeater GetRepeater(string csvLine) {
        string[] values = csvLine.Split(Settings.separator);
        // TODO: ÖVSV repeater list lists Tx and Rx always from a repeater point of view; add a setting to let the user specify.
        Repeater repeater = new Repeater(
            Convert.ToString(values[Settings.RepeaterCSVColumns["band"]]),
            // The OEVSV CSV has Tx and Rx reversed (i.e. another point of view.)
            Convert.ToDouble(values[Settings.RepeaterCSVColumns["tx"]].Length < 1
                ? "0.0"
                : values[Settings.RepeaterCSVColumns["tx"]].Replace(".", ",")),
            Convert.ToDouble(values[Settings.RepeaterCSVColumns["rx"]].Length < 1
                ? "0.0"
                : values[Settings.RepeaterCSVColumns["rx"]].Replace(".", ",")),
            Convert.ToString(values[Settings.RepeaterCSVColumns["callsign"]]),
            Convert.ToString(values[Settings.RepeaterCSVColumns["site"]]),
            Convert.ToBoolean(values[Settings.RepeaterCSVColumns["isI2"]] == "1" ? "true" : "false"),
            Convert.ToBoolean(values[Settings.RepeaterCSVColumns["isBM"]] == "1" ? "true" : "false"),
            Convert.ToInt32(values[Settings.RepeaterCSVColumns["colorcode"]] == "" ? "0" : values[25]),
            Convert.ToBoolean(values[Settings.RepeaterCSVColumns["isDMR"]] == "1" ? "true" : "false"),
            Convert.ToBoolean(values[Settings.RepeaterCSVColumns["isFM"]] == "1" ? "true" : "false"),
            Convert.ToDouble(values[Settings.RepeaterCSVColumns["ctcsstx"]].Length < 1
                ? "0.0"
                : values[Settings.RepeaterCSVColumns["ctcsstx"]].Replace(".", ",")),
            Convert.ToDouble(values[Settings.RepeaterCSVColumns["ctcssrx"]].Length < 1
                ? "0.0"
                : values[Settings.RepeaterCSVColumns["ctcssrx"]].Replace(".", ","))
        );

        // Add the repeater's EchoLink address to the Analog Address Book
        if (repeater.Type == RepeaterType.FM && values[Settings.RepeaterCSVColumns["hasEchoLink"]] == "1") {
            var analogContact = new AnyToneAnalogContact();
            analogContact.Name = repeater.Callsign;
            analogContact.Number = values[Settings.RepeaterCSVColumns["echolinkID"]];
            
            AnyToneAnalogContacts.Add(analogContact);
        }
        
        return repeater;
    }

    public void CreateDigitalChannels() {
        foreach (var repeater in RepeaterList) {
            Console.WriteLine(repeater.Callsign+repeater.NeedsZone);
            if (!repeater.NeedsZone) {
                continue;
            }
            foreach (var talkGroup in TalkGroups) {
                if (!talkGroup.CreateChannel) {
                    continue;
                }
                
                // Ignore talkgroups for a different network than the repeater network
                // Add channels for IPSC2 and Brandmeister talkgroups if the repeater
                // supports both.
                
                if (repeater.Network != DMRNetwork.BO && talkGroup.Network != repeater.Network) {
                    continue;
                }
                var channel = new AnyToneChannel();
                channel.ChannelName = repeater.Callsign.Substring(2) + talkGroup.TimeSlot + "-" + talkGroup.Name.Replace("TG", "");
                channel.ReceiveFrequency = repeater.Rx.ToString().Replace(",",".").PadRight(9,'0');
                channel.TransmitFrequency = repeater.Tx.ToString().Replace(",",".").PadRight(9,'0');
                channel.ChannelType = "D-Digital";
                channel.Contact = talkGroup.Name;
                channel.ContactTG = talkGroup.DMRid.ToString();
                channel.ColorCode = repeater.ColorCode.ToString();
                channel.Slot = talkGroup.TimeSlot.ToString();

                repeater.ChannelList.Add(channel);
            }
        }
    }
}
// 0    1  2      3   4               5            6            7        8              9         10    11  12       13    14          15            16     17 18        19       20       21       22          23         24  25 26    27           28                 29   30          31    32         33         34    35         36              37      38         39   40        41       42        43            44           45         46      47   48           49  50  51 52 53
// band;ch;ch_new;uid;type_of_station;frequency_tx;frequency_rx;callsign;antenna_heigth;site_name;sysop;url;hardware;mmdvm;solar_power;battery_power;status;fm;fm_wakeup;ctcss_tx;ctcss_rx;echolink;echolink_id;digital_id;dmr;cc;ipsc2;brandmeister;network_registered;c4fm;c4fm_groups;dstar;dstar_rpt1;dstar_rpt2;tetra;other_mode;other_mode_name;comment;created_at;city;longitude;latitude;sea_level;locator_short;locator_long;geo_prefix;bev_gid;geom;name_address;gkz;bkz;kg;pg;bl