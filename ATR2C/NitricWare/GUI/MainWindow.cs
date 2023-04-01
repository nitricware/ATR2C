namespace ATCSVCreator.NitricWare.GUI; 
using Terminal.Gui;

public class MainWindow : Window {
    public TextField repeaterPathTextField;
    private readonly string _repeaterPath = Path.Combine(Directory.GetCurrentDirectory(),"input","repeater.csv");
    public TextField talkgroupPathTextField;
    private readonly string _talkgroupPath = Path.Combine(Directory.GetCurrentDirectory(),"input","talkgroups.csv");

    public MainWindow() {
        Title = "ATR2C (CTRL + Q to quit)";
        Label repeaterPathLabel = new() {
            Text = "Path to repeater.csv"
        };

        repeaterPathTextField = new TextField(_repeaterPath) {
            // Position text field adjacent to the label
            X = Pos.Right (repeaterPathLabel) + 3,
            // Fill remaining horizontal space
            Width = Dim.Fill (),
        };
        
        Label talkgroupPathLabel = new() {
            Text = "Path to talkgroups.csv",
            X = Pos.Left (repeaterPathLabel),
            Y = Pos.Bottom (repeaterPathLabel)
        };

        talkgroupPathTextField = new TextField(_talkgroupPath) {
            // Position text field adjacent to the label
            X = Pos.Left (repeaterPathTextField),
            Y = Pos.Top (repeaterPathTextField) + 1,
            // Fill remaining horizontal space
            Width = Dim.Fill (),
        };
        
        Button btnGenerate = new () {
            Text = "Generate CSV Files",
            Y = Pos.Bottom(talkgroupPathLabel) + 1,
            // center the login button horizontally
            X = Pos.Center (),
            IsDefault = true,
        };
        
        btnGenerate.Clicked += generateFiles;

        Add(
            repeaterPathTextField,
            repeaterPathLabel, 
            talkgroupPathTextField, 
            talkgroupPathLabel,
            btnGenerate
            );
    }

    public void generateFiles()  {
        if (
            !File.Exists(repeaterPathTextField.Text.ToString()) || 
            !File.Exists(talkgroupPathTextField.Text.ToString())
        ) {
            MessageBox.ErrorQuery("Error", "One of the specified files was not found.", "OK");
            return;
        }

        // Create export director if it does not exist yet.
        string exportPath = Path.Combine(Directory.GetCurrentDirectory(), "export");
        Directory.CreateDirectory(exportPath);
        
        TalkGroupFileHandler talkGroupFileHandler = new TalkGroupFileHandler(talkgroupPathTextField.Text.ToString());
        
        // TODO error handling if empty talkgroup list
        
        OEVSVRepeaterFileHandler oevsvRepeaterFileHandler = new OEVSVRepeaterFileHandler(repeaterPathTextField.Text.ToString(), talkGroupFileHandler.TalkGroupList);

        List<AnyToneTalkGroup> anyToneTalkgroups = new();
        foreach (var talkGroup in talkGroupFileHandler.TalkGroupList.Where(tg => tg.AddToList || tg.CreateChannel)) {
            anyToneTalkgroups.Add(talkGroup.ToAnyToneTalkgroup());
        }

        oevsvRepeaterFileHandler.TalkGroups = talkGroupFileHandler.TalkGroupList;

        // Arm the CSVCreator with all created objects
        CSVCreator csvCreator = new CSVCreator {
            Zones = oevsvRepeaterFileHandler.Zones,
            Channels = oevsvRepeaterFileHandler.Channels,
            Talkgroups = anyToneTalkgroups,
            AnalogAddressBook = oevsvRepeaterFileHandler.AnalogContacts,
            ScanLists = oevsvRepeaterFileHandler.ScanLists
        };

        csvCreator.CreateAllFiles();
        
        MessageBox.Query("Success", "The CSV files were created successfully", "OK");
    }
}