using System.Globalization;

namespace ATCSVCreator.NitricWare; 

public class OEVSVRepeaterFileHandler {
    public List<TalkGroup> TalkGroups;
    
    public List<AnyToneAnalogContact> AnalogContacts = new();
    public List<AnyToneZone> Zones = new();
    public List<AnyToneChannel> Channels = new();
    public List<AnyToneScanList> ScanLists = new();

    public void LoadRepeaterCSV() {
        foreach (string line in File.ReadAllLines(Settings.InputFile)
                     .Skip(1)
                     .Where(SkipLine)) {
            string[] columns = line.Split(Settings.separator);
            ParseRepeater(columns);
        }
    }

    public bool SkipLine(string line) {
        string[] values = line.Split(Settings.separator);
        
        // Only parse 70cm and 2m repeaters
        if (values[(int) OEVSVRepeaterCSVColumns.Band] != "70cm" && values[(int) OEVSVRepeaterCSVColumns.Band] != "2m") {
            return false;
        }
        
        // Only parse voice repeaters; typo in csv;
        if (values[(int) OEVSVRepeaterCSVColumns.Type] != "repeater_voice" && values[(int) OEVSVRepeaterCSVColumns.Type] != "repeaer_voice") {
            return false;
        }
        
        // Only parse active repeaters
        if (values[(int) OEVSVRepeaterCSVColumns.Status] != "active") {
            return false;
        }
        
        // Ignore remaining entries that do not have neither FM nor DMR marked
        if (values[(int) OEVSVRepeaterCSVColumns.FM] != "1" && values[(int) OEVSVRepeaterCSVColumns.DMR] != "1") {
            return false;
        }

        return true;
    }

    public void ParseRepeater(string[] values) {
        // 1. check if the repeater is fm, dmr or both
        // 2. if its dmr, create a zone and add channels (BM/I2/BO) to it
        // 3. if it's fm, create a zone (location, omit if it already exists) and add the repeater as a channel.
        // 4. if it's fm add the echolink code to the address book
        
        string channelCallsign = Convert.ToString(values[Settings.RepeaterCSVColumns["callsign"]]);
        string channelBand = Convert.ToString(values[Settings.RepeaterCSVColumns["band"]]);
        string repeaterLocation = values[(int)OEVSVRepeaterCSVColumns.Callsign].Substring(0, 3);

        string channelTx = Convert.ToDouble(
            values[Settings.RepeaterCSVColumns["tx"]],
            CultureInfo.InvariantCulture)
            .ToString("0.00000")
            .Replace(",",".");
        string channelRx = Convert.ToDouble(
            values[Settings.RepeaterCSVColumns["rx"]],
            CultureInfo.InvariantCulture)
            .ToString("0.00000")
            .Replace(",",".");

        // Check if the repeater is FM, DMR or both
        if (values[(int)OEVSVRepeaterCSVColumns.FM] == "1") {
            // The repeater is FM; it may also be DMR;
            // Create channel for the repeater;
            
            string channelFMCTCSSTx = values[Settings.RepeaterCSVColumns["ctcsstx"]].Length < 1
                ? "Off"
                : values[Settings.RepeaterCSVColumns["ctcsstx"]];
            string channelFMCTCSSRx = values[Settings.RepeaterCSVColumns["ctcssrx"]].Length < 1
                ? "Off"
                : values[Settings.RepeaterCSVColumns["ctcssrx"]];
            
            string channelFMSquelchMode = channelFMCTCSSRx != "Off" || channelFMCTCSSTx != "Off" ? "CTCSS/DCS" : "Carrier";
            
            AnyToneChannel channelFM = new AnyToneChannel {
                ChannelName = $"{ channelCallsign } { channelBand }".Truncate(16),
                ReceiveFrequency = channelRx,
                TransmitFrequency = channelTx,
                ChannelType = "A-Analog",
                CTCSSDecode = channelFMCTCSSTx,
                CTCSSEncode = channelFMCTCSSRx,
                SquelchMode = channelFMSquelchMode,
                APRSReportType = "Analog",
                CustomCTCSS = "251.1",
                BusyLock = "Off",
                ColorCode = "1"
            };
            
            Channels.Add(channelFM);
            
            string channelZoneName = repeaterLocation + " FM";
            //zone = Zones.Any(x => x.ChannelName == location + " FM");
            AnyToneZone? zone = Zones.FirstOrDefault(x => x.ZoneName == channelZoneName);

            if (zone == null) {
                // Zone for repeater location does not exist yet.
                // Creating zone and adding current channel.

                zone = new AnyToneZone {
                    ZoneName = channelZoneName,
                    ZoneChannelMember = channelFM.ChannelName,
                    ZoneChannelMemberRXFrequency = channelFM.ReceiveFrequency,
                    ZoneChannelMemberTXFrequency = channelFM.TransmitFrequency,
                    AChannel = channelFM.ChannelName,
                    AChannelRXFrequency = channelFM.ReceiveFrequency,
                    AChannelTXFrequency = channelFM.TransmitFrequency,
                    BChannel = channelFM.ChannelName,
                    BChannelRXFrequency = channelFM.ReceiveFrequency,
                    BChannelTXFrequency = channelFM.TransmitFrequency
                };
                
                Zones.Add(zone);
            } else {
                // Zone for repeater location does exist.
                // Adding current channel.
                
                zone.ZoneChannelMember += $"|{channelFM.ChannelName}";
                zone.ZoneChannelMemberRXFrequency += $"|{channelFM.ReceiveFrequency}";
                zone.ZoneChannelMemberTXFrequency += $"|{channelFM.TransmitFrequency}";
            }
            
            // Add the repeater's EchoLink Address to the AddressBook
            
            if (values[(int) OEVSVRepeaterCSVColumns.EchoLink] == "1") {
                var analogContact = new AnyToneAnalogContact {
                    Name = channelCallsign,
                    Number = values[(int) OEVSVRepeaterCSVColumns.EchoLinkID]
                };

                AnalogContacts.Add(analogContact);
            }
            
            // Create a Scan List if necessary and add the repeater to it

            if (Settings.CreateAnalogScanLists) {
                string scanListName = repeaterLocation + " Analog";
                AnyToneScanList? scanList = ScanLists.FirstOrDefault(x => x.ScanListName == scanListName);
                if (scanList == null) {
                    scanList = new AnyToneScanList {
                        ScanListName = scanListName,
                        ScanChannelMember = channelFM.ChannelName,
                        ScanChannelMemberTXFrequency = channelFM.TransmitFrequency,
                        ScanChannelMemberRXFrequency = channelFM.ReceiveFrequency
                    };
                    
                    ScanLists.Add(scanList);
                } else {
                    scanList.ScanChannelMember += $"|{channelFM.ChannelName}";
                    scanList.ScanChannelMemberTXFrequency += $"|{channelFM.TransmitFrequency}";
                    scanList.ScanChannelMemberRXFrequency += $"|{channelFM.ReceiveFrequency}";
                }

                channelFM.ScanList = scanList.ScanListName;
            }
        }

        if (values[(int)OEVSVRepeaterCSVColumns.DMR] == "1") {
            // Channel is DMR
            // Create a zone for the repeater.
            // Then, create channels for selected TGs based on the
            // repeater network
            string shortCallsign = channelCallsign.Substring(3);
            DMRNetwork repeaterNetwork = DMRNetwork.BO;
            
            if (values[(int)OEVSVRepeaterCSVColumns.Brandmeister] == "1" &&
                values[(int)OEVSVRepeaterCSVColumns.IPSC2] == "1") {
                repeaterNetwork = DMRNetwork.BO;
            } else if (values[(int)OEVSVRepeaterCSVColumns.Brandmeister] == "" &&
                       values[(int)OEVSVRepeaterCSVColumns.IPSC2] == "1") {
                repeaterNetwork = DMRNetwork.I2;
            } else if (values[(int)OEVSVRepeaterCSVColumns.Brandmeister] == "1" &&
                       values[(int)OEVSVRepeaterCSVColumns.IPSC2] == "") {
                repeaterNetwork = DMRNetwork.BM;
            }

            List<AnyToneChannel> digitalChannels = new();

            foreach (var talkgroup in TalkGroups
                         .Where(tg => tg.CreateChannel)) {
                // Create a channel for each talkgroup that is marked for channel creation in talkgroups.csv
                if (repeaterNetwork != DMRNetwork.BO && repeaterNetwork != talkgroup.Network) {
                    // Skip any talkgroup that does not match the network of the repeater;
                    // Do not skip any talkgroup if the repeater supports both talkgroups;
                    continue;
                }

                AnyToneChannel digitalChannel = new AnyToneChannel {
                    ChannelName = $"{shortCallsign}-{talkgroup.Network}-{talkgroup.Name}".Truncate(16),
                    ReceiveFrequency = channelRx,
                    TransmitFrequency = channelTx,
                    ChannelType = "D-Digital",
                    Contact = talkgroup.Name,
                    ContactTG = talkgroup.DMRid.ToString(),
                    ColorCode = values[(int)OEVSVRepeaterCSVColumns.Colorcode],
                    Slot = talkgroup.TimeSlot.ToString()
                };
                
                // Create a scanlist if needed and add this repeater to if marked
                
                if (Settings.CreateAnalogScanLists && talkgroup.AddToScanList) {
                    string scanListName = repeaterLocation + " Digital";
                    AnyToneScanList? scanList = ScanLists.FirstOrDefault(x => x.ScanListName == scanListName);
                    if (scanList == null) {
                        scanList = new AnyToneScanList {
                            ScanListName = scanListName,
                            ScanChannelMember = digitalChannel.ChannelName,
                            ScanChannelMemberTXFrequency = digitalChannel.TransmitFrequency,
                            ScanChannelMemberRXFrequency = digitalChannel.ReceiveFrequency
                        };
                        
                        ScanLists.Add(scanList);
                    } else {
                        scanList.ScanChannelMember += $"|{digitalChannel.ChannelName}";
                        scanList.ScanChannelMemberTXFrequency += $"|{digitalChannel.TransmitFrequency}";
                        scanList.ScanChannelMemberRXFrequency += $"|{digitalChannel.ReceiveFrequency}";
                        
                    }

                    digitalChannel.ScanList = scanList.ScanListName;
                }
                
                digitalChannels.Add(digitalChannel);
            }
            
            // Create a zone and add the first channel to the zone
            AnyToneZone digitalZone = new AnyToneZone {
                ZoneName = $"{channelCallsign} {channelBand} {values[(int)OEVSVRepeaterCSVColumns.Site]}".Truncate(16),
                ZoneChannelMember = digitalChannels.First().ChannelName,
                ZoneChannelMemberRXFrequency = digitalChannels.First().ReceiveFrequency,
                ZoneChannelMemberTXFrequency = digitalChannels.First().TransmitFrequency,
                AChannel = digitalChannels.First().ChannelName,
                AChannelRXFrequency = digitalChannels.First().ReceiveFrequency,
                AChannelTXFrequency = digitalChannels.First().TransmitFrequency,
                BChannel = digitalChannels.First().ChannelName,
                BChannelRXFrequency = digitalChannels.First().ReceiveFrequency,
                BChannelTXFrequency = digitalChannels.First().TransmitFrequency
            };

            foreach (var digitalChannel in digitalChannels.Skip(1)) {
                // Add the remaining channels to the zone
                digitalZone.ZoneChannelMember += $"|{digitalChannel.ChannelName}";
                digitalZone.ZoneChannelMemberRXFrequency += $"|{digitalChannel.ReceiveFrequency}";
                digitalZone.ZoneChannelMemberTXFrequency += $"|{digitalChannel.TransmitFrequency}";
            }
            
            // Add the zone to the Zones List
            Zones.Add(digitalZone);
            
            // Add all created channels to the Channels list
            Channels = Channels.Concat(digitalChannels).ToList();
        }
        
        // Once it's done, sort the Zones list by name
        Zones = Zones.OrderBy(z => z.ZoneName).ToList();
    }
}
// 0    1  2      3   4               5            6            7        8              9         10    11  12       13    14          15            16     17 18        19       20       21       22          23         24  25 26    27           28                 29   30          31    32         33         34    35         36              37      38         39   40        41       42        43            44           45         46      47   48           49  50  51 52 53
// band;ch;ch_new;uid;type_of_station;frequency_tx;frequency_rx;callsign;antenna_heigth;site_name;sysop;url;hardware;mmdvm;solar_power;battery_power;status;fm;fm_wakeup;ctcss_tx;ctcss_rx;echolink;echolink_id;digital_id;dmr;cc;ipsc2;brandmeister;network_registered;c4fm;c4fm_groups;dstar;dstar_rpt1;dstar_rpt2;tetra;other_mode;other_mode_name;comment;created_at;city;longitude;latitude;sea_level;locator_short;locator_long;geo_prefix;bev_gid;geom;name_address;gkz;bkz;kg;pg;bl