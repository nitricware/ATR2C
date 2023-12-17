using ATCSVCreator.NitricWare.CHIRP;
using ATCSVCreator.NitricWare.CPSObjects;
using ATCSVCreator.NitricWare.ENUM;

namespace ATCSVCreator.NitricWare.Generic_5_Character; 

public class Generic5CharacterRepeaterParser<T> where T : IRepeater {
    public List<Generic5CharacterListItem> ListItems = new();
    private List<T> _repeaterList;
    
    public Generic5CharacterRepeaterParser(List<T> repeaterList) {
        _repeaterList = repeaterList;
        
        foreach (T repeater in _repeaterList) {
            if (SkipRepeater(repeater)) {
                continue;
            }
            ParseRepeater(repeater);
        }
    }
    
    private void ParseRepeater(T repeater) {
        CreateListItem(repeater);
        
        // Once it's done, sort the Zones list by name
        ListItems = ListItems.OrderBy(z => z.Name).ToList();
    }
    
    private void CreateListItem(T repeater) {
        string channelBand;
        switch (repeater.Band) {
            case RadioBand.CM70:
                channelBand = "7";
                break;
            case RadioBand.M2:
                channelBand = "2";
                break;
            default:
                channelBand = "0";
                break;
        }
        string name = repeater.Callsign.Substring(2, 4) + "-" + channelBand;

        string channelTx = repeater.Tx.ToString("0.00000").Replace(",",".");
        string channelRx = repeater.Rx.ToString("0.00000").Replace(",",".");
        
        string channelCtcssTx = repeater.CtcssTx.Length < 1
            ? ""
            : repeater.CtcssTx;
        string channelCtcssRx = repeater.CtcssRx.Length < 1
            ? ""
            : repeater.CtcssRx;

        Generic5CharacterListItem listItem = new Generic5CharacterListItem {
            Name = name,
            Rx = channelRx,
            Tx = channelTx,
            CtcssRx = channelCtcssRx,
            CtcssTx = channelCtcssTx
        };
        
        ListItems.Add(listItem);
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
        
        // TODO: implement some feature that lets users decide on this part
        // Ignore remaining entries that are not FM
        if (!repeater.IsFM) {
            return true;
        }
        
        if (repeater.IsDmr) {
            return true;
        }

        return false;
    }
}