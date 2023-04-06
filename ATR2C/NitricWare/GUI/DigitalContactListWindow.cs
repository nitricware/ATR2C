using Terminal.Gui;

namespace ATCSVCreator.NitricWare.GUI; 

public class DigitalContactListWindow : Window{
    public DigitalContactListWindow() {
        X = 5;
        Y = 5;
        Width = 40;
        Height = 6;
        Title = "Digital Contact List";
        
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
        // TODO implement
        // fetch radio ID CSV
        // create exportType digital contact list csv
        throw new NotImplementedException();
    }
}