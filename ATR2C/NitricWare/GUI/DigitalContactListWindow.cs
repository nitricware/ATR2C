using ATCSVCreator.NitricWare.AnyTone;
using ATCSVCreator.NitricWare.DigitalContactList;
using Terminal.Gui;

namespace ATCSVCreator.NitricWare.GUI; 

public class DigitalContactListWindow : Window {
    public DigitalContactListWindow() {
        X = 5;
        Y = 5;
        Width = 60;
        Height = 6;
        Title = "Digital Contact List";
        // TODO: Add export path setting
        List<string> exportTypes = Settings.exportTypes;
        
        ComboBox exportTypeComboBox = new ComboBox {
            Width = Dim.Fill(),
        };
        
        exportTypeComboBox.SetSource(exportTypes);
        exportTypeComboBox.SelectedItem = 0;
        exportTypeComboBox.Height = exportTypes.Count + 1;

        Button btnGenerateDigitalContactList = new() {
            Text = "Generate",
            Width = Dim.Fill(),
            Y = Pos.Top(exportTypeComboBox) + 2
        };

        btnGenerateDigitalContactList.Clicked += GenerateDigitalContactList;
        
        Add(exportTypeComboBox,btnGenerateDigitalContactList);
    }

    private void GenerateDigitalContactList() {
        RadioIdDigitalContactFileHandler radioIdDigitalContactFileHandler = new RadioIdDigitalContactFileHandler();
        AnyToneD878UVIIPlusDigitalContactListParser anyToneD878UviiPlusDigitalContactListParser =
            new AnyToneD878UVIIPlusDigitalContactListParser(radioIdDigitalContactFileHandler.RadioIdDigitalContacts);
        AnyToneCsvCreator anyToneCsvCreator = new AnyToneCsvCreator() {
            DigitalContactList = anyToneD878UviiPlusDigitalContactListParser.anyToneDigitalContacts
        };
        anyToneCsvCreator.CreateDigitalContactList();
        MessageBox.Query("Result", $"Digital Contact List created with {anyToneD878UviiPlusDigitalContactListParser.anyToneDigitalContacts.Count} contacts.", "OK");
    }
}