using ATCSVCreator.NitricWare.DigitalContactList;
using ATCSVCreator.NitricWare.Helper;

namespace ATCSVCreator.NitricWare.AnyTone; 

public class AnyToneD878UVIIPlusDigitalContactListParser {
    public List<AnyToneDigitalContact> anyToneDigitalContacts = new();

    public AnyToneD878UVIIPlusDigitalContactListParser(List<RadioIdDigitalContact> digitalContacts) {
        foreach (RadioIdDigitalContact digitalContact in digitalContacts) {
            anyToneDigitalContacts.Add(new AnyToneDigitalContact {
                DmrId = digitalContact.DmrId,
                Callsign = digitalContact.Callsign,
                Name = $"{digitalContact.FirstName} {digitalContact.LastName}".Truncate(16),
                City = digitalContact.City,
                State = digitalContact.State,
                Country = digitalContact.Country
            });
        }
    }
}