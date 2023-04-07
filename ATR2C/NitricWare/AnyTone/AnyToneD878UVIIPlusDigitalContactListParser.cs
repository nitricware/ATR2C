using ATCSVCreator.NitricWare.DigitalContactList;

namespace ATCSVCreator.NitricWare.AnyTone; 

public class AnyToneD878UVIIPlusDigitalContactListParser {
    public List<AnyToneDigitalContact> anyToneDigitalContacts = new();

    public AnyToneD878UVIIPlusDigitalContactListParser(List<RadioIdDigitalContact> digitalContacts) {
        foreach (RadioIdDigitalContact digitalContact in digitalContacts) {
            anyToneDigitalContacts.Add(new AnyToneDigitalContact {
                DmrId = digitalContact.DmrId,
                Callsign = digitalContact.Callsign,
                Name = $"{digitalContact.FirstName} {digitalContact.LastName}",
                City = digitalContact.City,
                State = digitalContact.State,
                Country = digitalContact.Country
            });
        }
    }
}