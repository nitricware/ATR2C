using System.Numerics;

namespace ATCSVCreator.NitricWare.GUI; 
using Terminal.Gui;

public class MainWindow : Window {
    public TextField repeaterPathTextField;
    private readonly string _repeaterPath = Path.Combine(Directory.GetCurrentDirectory(),"input","repeater.csv");
    public TextField talkgroupPathTextField;
    private readonly string _talkgroupPath = Path.Combine(Directory.GetCurrentDirectory(),"input","talkgroups.csv");
    public TextField defaultsDirPathTextField;
    private readonly string _defaultsDirPath = Path.Combine(Directory.GetCurrentDirectory(),"defaults");
    public TextField exportDirPathTextField;
    private readonly string _exportDirPath = Path.Combine(Directory.GetCurrentDirectory(),"export");
    public TextField hamCallsignTextField;
    private readonly string _hamCallsign = Path.Combine(Settings.HamCallSign);
    
    public MainWindow() {
        Title = "ATR2C (CTRL + Q to quit)";
        
        Label hamCallsignLabel = new() {
            Text = "Callsign"
        };

        hamCallsignTextField = new TextField(_hamCallsign) {
            // Position text field adjacent to the label
            X = Pos.Right (hamCallsignLabel) + 15,
            // Fill remaining horizontal space
            Width = Dim.Fill (15)
        };

        Label hamCallsignExplanationLabel = new() {
            Text = "Must match a Radio ID in CPS.",
            X = Pos.Left(hamCallsignLabel),
            Y = Pos.Bottom(hamCallsignTextField),
            Width = Dim.Fill()
        };
        
        Label repeaterPathLabel = new() {
            Text = "Path to repeater.csv",
            X = Pos.Left(hamCallsignLabel),
            Y = Pos.Bottom(hamCallsignExplanationLabel) + 1
        };

        repeaterPathTextField = new TextField(_repeaterPath) {
            // Position text field adjacent to the label
            X = Pos.Right (repeaterPathLabel) + 3,
            Y = Pos.Bottom(hamCallsignExplanationLabel) +1,
            // Fill remaining horizontal space
            Width = Dim.Fill (15)
        };
        
        Label talkgroupPathLabel = new() {
            Text = "Path to talkgroups.csv",
            X = Pos.Left (repeaterPathLabel),
            Y = Pos.Bottom (repeaterPathTextField) +1
        };

        talkgroupPathTextField = new TextField(_talkgroupPath) {
            // Position text field adjacent to the label
            X = Pos.Left (repeaterPathTextField),
            Y = Pos.Bottom (repeaterPathTextField) + 1,
            // Fill remaining horizontal space
            Width = Dim.Fill(15)
        };

        Label defaultsDirPathLabel = new() {
            Text = "Path to /defaults/",
            X = Pos.Left(talkgroupPathLabel),
            Y = Pos.Bottom(talkgroupPathTextField) +1
        };
        
        defaultsDirPathTextField = new TextField(_defaultsDirPath) {
            // Position text field adjacent to the label
            X = Pos.Left (talkgroupPathTextField),
            Y = Pos.Top (talkgroupPathTextField) + 2,
            // Fill remaining horizontal space
            Width = Dim.Fill(15)
        };
        
        Label defaultsDirPathExplanationLabel = new() {
            Text = "Select the directory in which the default .csv files\nthat are merged with the final product reside.\nCan contain none, any or all defaults files.",
            X = Pos.Left(defaultsDirPathLabel),
            Y = Pos.Bottom(defaultsDirPathTextField),
            Width = Dim.Fill()
        };
        
        Label exportDirPathLabel = new() {
            Text = "Path to /export/",
            X = Pos.Left(defaultsDirPathExplanationLabel),
            Y = Pos.Bottom(defaultsDirPathExplanationLabel) +1
        };
        
        exportDirPathTextField = new TextField(_exportDirPath) {
            // Position text field adjacent to the label
            X = Pos.Left (talkgroupPathTextField),
            Y = Pos.Bottom (defaultsDirPathExplanationLabel) + 1,
            // Fill remaining horizontal space
            Width = Dim.Fill(15)
        };

        Button btnSelectRepeaterPath = GenerateFilePickerButton(repeaterPathTextField, false);
        Button btnSelectTalkgroupsPath = GenerateFilePickerButton(talkgroupPathTextField, false);
        Button btnSelectDefaultsDir = GenerateFilePickerButton(defaultsDirPathTextField, true);
        Button btnSelectExportDir = GenerateFilePickerButton(exportDirPathTextField, true);

        Button btnGenerate = new () {
            Text = "Generate CSV Files",
            Y = Pos.Bottom(exportDirPathLabel) + 2,
            // center the login button horizontally
            X = Pos.Center (),
            IsDefault = true,
            Width = Dim.Fill()
        };
        
        btnGenerate.Clicked += generateFiles;

        Add(
            hamCallsignLabel,
            hamCallsignTextField,
            hamCallsignExplanationLabel,
            repeaterPathTextField,
            repeaterPathLabel, 
            btnSelectRepeaterPath,  
            talkgroupPathTextField, 
            talkgroupPathLabel,
            btnSelectTalkgroupsPath,
            defaultsDirPathLabel,
            defaultsDirPathTextField,
            btnSelectDefaultsDir,
            defaultsDirPathExplanationLabel,
            exportDirPathLabel,
            exportDirPathTextField,
            btnSelectExportDir,
            btnGenerate
            );
    }

    private Button GenerateFilePickerButton(TextField parent, bool dirSelect) {
        Button btnChoseFile = new() {
            Text = dirSelect ? "Find Dir" : "Find File",
            Y = Pos.Top(parent),
            X = Pos.Right(parent) + 1
        };

        btnChoseFile.Clicked += () => {
            OpenDialog openDialog = new("Open", dirSelect ? "Select a directory" : "Select a CSV file") {
                AllowsMultipleSelection = false,
                AllowedFileTypes = new[] { ".csv" },
                CanChooseFiles = !dirSelect,
                CanChooseDirectories = dirSelect,
                DirectoryPath = Path.GetDirectoryName(parent.Text.ToString())
            };
            
            Application.Run(openDialog);

            if (!openDialog.Canceled) {
                parent.Text = openDialog.FilePath.ToString();
            }
        };

        return btnChoseFile;
    }

    public void updatePathField(string newPath) {
        
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
        
        OEVSVRepeaterFileHandler oevsvRepeaterFileHandler = new OEVSVRepeaterFileHandler(
            repeaterPathTextField.Text.ToString(), 
            talkGroupFileHandler.TalkGroupList,
            hamCallsignTextField.Text.ToString()
            );

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
            ScanLists = oevsvRepeaterFileHandler.ScanLists,
            DefaultsDir = defaultsDirPathTextField.Text.ToString(),
            ExportDir = exportDirPathTextField.Text.ToString() 
        };

        csvCreator.CreateAllFiles();

        MessageBox.Query("Success", "The CSV files were created successfully", "OK");
    }
}