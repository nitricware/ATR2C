using ATCSVCreator.NitricWare.AnyTone;
using ATCSVCreator.NitricWare.DigitalContactList;
using ATCSVCreator.NitricWare.Helper;
using Terminal.Gui;

namespace ATCSVCreator.NitricWare.GUI; 

public class DigitalContactListWindow : Window {
    private List<View> _views = new();
    private TextField _exportDirPathTextField;
    public DigitalContactListWindow() {
        X = 5;
        Y = 5;
        Width = 60;
        Height = 8;
        Title = "Digital Contact List";
        
        List<string> exportTypes = Settings.exportTypes;
        
        Label exportTypeLabel = new() {
            Text = "Export Type",
        };
        
        _views.Add(exportTypeLabel);
        
        ComboBox exportTypeComboBox = new ComboBox {
            Width = Dim.Fill(15),
            X = Pos.Right(exportTypeLabel) +6
        };
        
        exportTypeComboBox.SetSource(exportTypes);
        exportTypeComboBox.SelectedItem = 0;
        exportTypeComboBox.Height = exportTypes.Count + 1;
        
        _views.Add(exportTypeComboBox);
        
        Label exportDirPathLabel = new() {
            Text = "Path to /export/",
            Y = Pos.Top(exportTypeComboBox) +2
        };
        
        _views.Add(exportDirPathLabel);
        
        _exportDirPathTextField = new TextField(Path.Combine(Directory.GetCurrentDirectory(),"export")) {
            // Position text field adjacent to the label
            X = Pos.Left (exportTypeComboBox),
            Y = Pos.Top (exportTypeComboBox) + 2,
            // Fill remaining horizontal space
            Width = Dim.Fill(15)
        };
        
        _views.Add(_exportDirPathTextField);
        
        Button btnSelectExportDir = GuiElementHelper.GenerateFilePickerButton(_exportDirPathTextField, true);
        
        _views.Add(btnSelectExportDir);
        
        Button btnGenerateDigitalContactList = new() {
            Text = "Generate",
            Width = Dim.Percent(50.0f),
            Y = Pos.Bottom(btnSelectExportDir) + 1,
            IsDefault = true
        };

        btnGenerateDigitalContactList.Clicked += GenerateDigitalContactList;
        
        _views.Add(btnGenerateDigitalContactList);
        
        Button btnClose = new() {
            Text = "Close",
            Width = Dim.Percent(50.0f),
            X = Pos.Right(btnGenerateDigitalContactList),
            Y = Pos.Top(btnGenerateDigitalContactList) 
        };

        btnClose.Clicked += () => {
            this.RequestStop();
        };
        
        _views.Add(btnClose);
        
        Add(_views.ToArray());
    }

    private void GenerateDigitalContactList() {
        RadioIdDigitalContactFileHandler radioIdDigitalContactFileHandler;
        try {
            radioIdDigitalContactFileHandler = new RadioIdDigitalContactFileHandler();
        } catch (Exception e) {
            MessageBox.ErrorQuery("Error", $"Error fetching current Radio ID database.", "OK");
            return;
        }
        AnyToneD878UVIIPlusDigitalContactListParser anyToneD878UviiPlusDigitalContactListParser =
            new AnyToneD878UVIIPlusDigitalContactListParser(radioIdDigitalContactFileHandler.RadioIdDigitalContacts);
        AnyToneCsvCreator anyToneCsvCreator = new AnyToneCsvCreator() {
            ExportDir = _exportDirPathTextField.Text.ToString(),
            DigitalContactList = anyToneD878UviiPlusDigitalContactListParser.anyToneDigitalContacts
        };
        anyToneCsvCreator.CreateDigitalContactList();
        MessageBox.Query("Result", $"Digital Contact List created with {anyToneD878UviiPlusDigitalContactListParser.anyToneDigitalContacts.Count} contacts.", "OK");
    }
}