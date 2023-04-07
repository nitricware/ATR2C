using System.Globalization;
using System.Net;
using System.Net.Sockets;
using ATCSVCreator.NitricWare.ENUM;
using ATCSVCreator.NitricWare.Helper;
using ATCSVCreator.NitricWare.Oevsv;
using CsvHelper;

namespace ATCSVCreator.NitricWare.DigitalContactList; 

public class RadioIdDigitalContactFileHandler {
    public List<RadioIdDigitalContact> RadioIdDigitalContacts;
    public RadioIdDigitalContactFileHandler(string? contactListUrl = null) {
        contactListUrl ??= Settings.radioIdDigitalContactListUrl;
        WebClient client = new WebClient();
        Stream stream = client.OpenRead(contactListUrl);
        StreamReader reader = new StreamReader(stream);
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture)) {
            RadioIdDigitalContacts = csv.GetRecords<RadioIdDigitalContact>().ToList();
        }
    }
}