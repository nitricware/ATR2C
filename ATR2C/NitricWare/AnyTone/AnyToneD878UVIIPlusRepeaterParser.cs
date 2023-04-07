using System.Globalization;
using System.Transactions;
using ATCSVCreator.NitricWare.CPSObjects;
using ATCSVCreator.NitricWare.DigitalContactList;
using ATCSVCreator.NitricWare.ENUM;
using ATCSVCreator.NitricWare.Helper;
using ATCSVCreator.NitricWare.TalkGroups;

namespace ATCSVCreator.NitricWare.AnyTone; 

public class AnyToneD878UVIIPlusRepeaterParser<T> where T : IRepeater {
    public List<AnyToneAnalogContact> AnalogContacts = new();
    public List<AnyToneZone> Zones = new();
    public List<AnyToneChannel> Channels = new();
    public List<AnyToneScanList> ScanLists = new();
    public List<AnyToneTalkGroup> TalkGroups = new();

    private List<T> _repeaterList;
    private List<TalkGroup> _talkGroups;

    private string _hamCallsign;

    public AnyToneD878UVIIPlusRepeaterParser(List<T> repeaterList, List<TalkGroup> talkGroups, string hamCallsign) {
        _repeaterList = repeaterList;
        _talkGroups = talkGroups;
        _hamCallsign = hamCallsign;
        
        foreach (T repeater in _repeaterList) {
            if (SkipRepeater(repeater)) {
                continue;
            }
            ParseRepeater(repeater);
        }

        TalkGroups = _talkGroups
            .Where(x => x.AddToList)
            .Select(TalkGroupToAnyToneTalkGroup)
            .ToList();
    }

    private AnyToneTalkGroup TalkGroupToAnyToneTalkGroup(TalkGroup talkGroup) {
        Console.WriteLine( $"Generate: {talkGroup.Network.ToString()}-{talkGroup.DmrId}-{talkGroup.Name}");
        return new() {
            Name = $"{talkGroup.Network.ToString()}-{talkGroup.DmrId}-{talkGroup.Name}",
            RadioId = talkGroup.DmrId,
            CallAlert = talkGroup.CallAlert,
            CallType = talkGroup.CallType
        };
    }

    private bool SkipRepeater(T repeater) {
        // Only parse 70cm and 2m repeaters
        if (repeater.Band != RadioBand.CM70 && repeater.Band != RadioBand.M2) {
            return true;
        }
        
        // Only parse voice repeaters; typo in csv;
        if (repeater.Type != StationType.RepeaterVoice) {
            return true;
        }
        
        // Only parse active repeaters
        if (repeater.Status != RepeaterStatus.Active) {
            return true;
        }
        
        // Ignore remaining entries that do not have neither FM nor DMR marked
        if (!repeater.IsFM && !repeater.IsDmr) {
            return true;
        }

        return false;
    }

    private void CreateAnalog(T repeater, string channelRx, string channelTx, string channelBand, string channelCallsign, string repeaterLocation) {
        // The repeater is FM; it may also be DMR;
        // Create channel for the repeater;
        
        string channelCtcssTx = repeater.CtcssTx.Length < 1
            ? "Off"
            : repeater.CtcssTx;
        string channelCtcssRx = repeater.CtcssRx.Length < 1
            ? "Off"
            : repeater.CtcssRx;
        
        string channelSquelchMode = channelCtcssRx != "Off" || channelCtcssTx != "Off" ? "CTCSS/DCS" : "Carrier";
        
        AnyToneChannel channelFm = new AnyToneChannel {
            ChannelName = $"{ channelCallsign } { channelBand }".Truncate(16),
            ReceiveFrequency = channelRx,
            TransmitFrequency = channelTx,
            ChannelType = "A-Analog",
            CtcssDecode = channelCtcssTx,
            CtcssEncode = channelCtcssRx,
            SquelchMode = channelSquelchMode,
            AprsReportType = "Analog",
            CustomCtcss = "251.1",
            BusyLock = "Off",
            ColorCode = "1",
            RadioId = _hamCallsign
        };
        
        Channels.Add(channelFm);
        
        string channelZoneName = repeaterLocation + " FM";
        //zone = Zones.Any(x => x.ChannelName == location + " FM");
        AnyToneZone? zone = Zones.FirstOrDefault(x => x.ZoneName == channelZoneName);

        if (zone == null) {
            // Zone for repeater location does not exist yet.
            // Creating zone and adding current channel.

            zone = new AnyToneZone {
                ZoneName = channelZoneName,
                ZoneChannelMember = channelFm.ChannelName,
                ZoneChannelMemberRxFrequency = channelFm.ReceiveFrequency,
                ZoneChannelMemberTxFrequency = channelFm.TransmitFrequency,
                AChannel = channelFm.ChannelName,
                AChannelRxFrequency = channelFm.ReceiveFrequency,
                AChannelTxFrequency = channelFm.TransmitFrequency,
                BChannel = channelFm.ChannelName,
                BChannelRxFrequency = channelFm.ReceiveFrequency,
                BChannelTxFrequency = channelFm.TransmitFrequency
            };
            
            Zones.Add(zone);
        } else {
            // Zone for repeater location does exist.
            // Adding current channel.
            
            zone.ZoneChannelMember += $"|{channelFm.ChannelName}";
            zone.ZoneChannelMemberRxFrequency += $"|{channelFm.ReceiveFrequency}";
            zone.ZoneChannelMemberTxFrequency += $"|{channelFm.TransmitFrequency}";
        }
        
        // Add the repeater's EchoLink Address to the AddressBook
        
        if (repeater.IsEchoLink) {
            var analogContact = new AnyToneAnalogContact {
                Name = channelCallsign,
                Number = repeater.EchoLinkId
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
                    ScanChannelMember = channelFm.ChannelName,
                    ScanChannelMemberTxFrequency = channelFm.TransmitFrequency,
                    ScanChannelMemberRxFrequency = channelFm.ReceiveFrequency
                };
                
                ScanLists.Add(scanList);
            } else {
                scanList.ScanChannelMember += $"|{channelFm.ChannelName}";
                scanList.ScanChannelMemberTxFrequency += $"|{channelFm.TransmitFrequency}";
                scanList.ScanChannelMemberRxFrequency += $"|{channelFm.ReceiveFrequency}";
            }

            channelFm.ScanList = scanList.ScanListName;
        }
    }

    private void CreateDigital(T repeater, string channelRx, string channelTx, string channelBand, string channelCallsign, string repeaterLocation) {
        // Channel is DMR
        // Create a zone for the repeater.
        // Then, create channels for selected TGs based on the
        // repeater network
        string shortCallsign = channelCallsign.Substring(2);
        DmrNetwork repeaterNetwork = DmrNetwork.Bo;
        
        string channelCtcssTx = repeater.CtcssTx.Length < 1
            ? "Off"
            : repeater.CtcssTx;
        string channelCtcssRx = repeater.CtcssRx.Length < 1
            ? "Off"
            : repeater.CtcssRx;
        
        string channelSquelchMode = channelCtcssRx != "Off" || channelCtcssTx != "Off" ? "CTCSS/DCS" : "Carrier";
        
        if (repeater.IsBrandmeister && repeater.IsIpsc2) {
            repeaterNetwork = DmrNetwork.Bo;
        } else if (!repeater.IsBrandmeister && repeater.IsIpsc2) {
            repeaterNetwork = DmrNetwork.I2;
        } else if (repeater.IsBrandmeister && repeater.IsIpsc2) {
            repeaterNetwork = DmrNetwork.Bm;
        }

        List<AnyToneChannel> digitalChannels = new();

        foreach (var talkgroup in _talkGroups
                     .Where(tg => tg.CreateChannel)) {
            // Create a channel for each talkgroup that is marked for channel creation in talkgroups.csv
            if (repeaterNetwork != DmrNetwork.Bo && repeaterNetwork != talkgroup.Network) {
                // Skip any talkgroup that does not match the network of the repeater;
                // Do not skip any talkgroup if the repeater supports both talkgroups;
                continue;
            }

            AnyToneChannel digitalChannel = new AnyToneChannel {
                ChannelName = $"{shortCallsign}-{talkgroup.Network}-{talkgroup.Name}".Truncate(16),
                ReceiveFrequency = channelRx,
                TransmitFrequency = channelTx,
                ChannelType = "D-Digital",
                Contact = $"{talkgroup.Network.ToString()}-{talkgroup.DmrId}-{talkgroup.Name}",
                ContactTg = talkgroup.DmrId.ToString(),
                ColorCode = repeater.ColorCode,
                Slot = talkgroup.TimeSlot.ToString(),
                RadioId = _hamCallsign,
                CtcssDecode = channelCtcssTx,
                CtcssEncode = channelCtcssRx,
                SquelchMode = channelSquelchMode,
            };
            
            // Create a scanlist if needed and add this repeater to if marked
            
            if (Settings.CreateAnalogScanLists && talkgroup.AddToScanList) {
                string scanListName = repeaterLocation + " Digital";
                AnyToneScanList? scanList = ScanLists.FirstOrDefault(x => x.ScanListName == scanListName);
                if (scanList == null) {
                    scanList = new AnyToneScanList {
                        ScanListName = scanListName,
                        ScanChannelMember = digitalChannel.ChannelName,
                        ScanChannelMemberTxFrequency = digitalChannel.TransmitFrequency,
                        ScanChannelMemberRxFrequency = digitalChannel.ReceiveFrequency
                    };
                    
                    ScanLists.Add(scanList);
                } else {
                    scanList.ScanChannelMember += $"|{digitalChannel.ChannelName}";
                    scanList.ScanChannelMemberTxFrequency += $"|{digitalChannel.TransmitFrequency}";
                    scanList.ScanChannelMemberRxFrequency += $"|{digitalChannel.ReceiveFrequency}";
                    
                }

                digitalChannel.ScanList = scanList.ScanListName;
            }
            
            digitalChannels.Add(digitalChannel);
        }
        
        // Create a zone and add the first channel to the zone
        AnyToneZone digitalZone = new AnyToneZone {
            ZoneName = $"{channelCallsign} {repeaterNetwork} {repeater.SiteName}".Truncate(16),
            ZoneChannelMember = digitalChannels.First().ChannelName,
            ZoneChannelMemberRxFrequency = digitalChannels.First().ReceiveFrequency,
            ZoneChannelMemberTxFrequency = digitalChannels.First().TransmitFrequency,
            AChannel = digitalChannels.First().ChannelName,
            AChannelRxFrequency = digitalChannels.First().ReceiveFrequency,
            AChannelTxFrequency = digitalChannels.First().TransmitFrequency,
            BChannel = digitalChannels.First().ChannelName,
            BChannelRxFrequency = digitalChannels.First().ReceiveFrequency,
            BChannelTxFrequency = digitalChannels.First().TransmitFrequency
        };

        foreach (var digitalChannel in digitalChannels.Skip(1)) {
            // Add the remaining channels to the zone
            digitalZone.ZoneChannelMember += $"|{digitalChannel.ChannelName}";
            digitalZone.ZoneChannelMemberRxFrequency += $"|{digitalChannel.ReceiveFrequency}";
            digitalZone.ZoneChannelMemberTxFrequency += $"|{digitalChannel.TransmitFrequency}";
        }
        
        // Add the zone to the Zones List
        Zones.Add(digitalZone);
        
        // Add all created channels to the Channels list
        Channels = Channels.Concat(digitalChannels).ToList();
    }
    
    private void ParseRepeater(T repeater) {
        // 1. check if the repeater is fm, dmr or both
        // 2. if its dmr, create a zone and add channels (BM/I2/BO) to it
        // 3. if it's fm, create a zone (location, omit if it already exists) and add the repeater as a channel.
        // 4. if it's fm add the echolink code to the address book
        string channelCallsign = repeater.Callsign;
        string channelBand;
        switch (repeater.Band) {
            case RadioBand.CM70:
                channelBand = "70cm";
                break;
            case RadioBand.M2:
                channelBand = "2m";
                break;
            default:
                channelBand = "0m";
                break;
        }
        string repeaterLocation = repeater.Callsign.Substring(0, 3);

        string channelTx = repeater.Tx.ToString("0.00000").Replace(",",".");
        string channelRx = repeater.Rx.ToString("0.00000").Replace(",",".");

        // Check if the repeater is FM, DMR or both
        if (repeater.IsFM) {
            CreateAnalog(repeater, channelRx, channelTx, channelBand, channelCallsign, repeaterLocation);
        }

        if (repeater.IsDmr) {
            CreateDigital(repeater, channelRx, channelTx, channelBand, channelCallsign, repeaterLocation);
        }
        
        // Once it's done, sort the Zones list by name
        Zones = Zones.OrderBy(z => z.ZoneName).ToList();
    }
}