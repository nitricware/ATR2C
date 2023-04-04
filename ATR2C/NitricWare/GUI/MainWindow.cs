using ATCSVCreator.NitricWare.AnyTone;
using ATCSVCreator.NitricWare.Oevsv;
using ATCSVCreator.NitricWare.TalkGroups;

namespace ATCSVCreator.NitricWare.GUI; 
using Terminal.Gui;

public class MainWindow : Window {
    private readonly TextField _repeaterPathTextField;
    private readonly string _repeaterPath = Path.Combine(Directory.GetCurrentDirectory(),"input","repeater.csv");
    private readonly TextField _talkGroupPathTextField;
    private readonly string _talkGroupPath = Path.Combine(Directory.GetCurrentDirectory(),"input","talkgroups.csv");
    private readonly TextField _defaultsDirPathTextField;
    private readonly string _defaultsDirPath = Path.Combine(Directory.GetCurrentDirectory(),"defaults");
    private readonly TextField _exportDirPathTextField;
    private readonly string _exportDirPath = Path.Combine(Directory.GetCurrentDirectory(),"export");
    private readonly TextField _hamCallsignTextField;
    private readonly string _hamCallsign = Path.Combine(Settings.HamCallSign);
    
    public MainWindow() {
        Title = "ATR2C (CTRL + Q to quit)";
        
        Label hamCallsignLabel = new() {
            Text = "Callsign"
        };

        _hamCallsignTextField = new TextField(_hamCallsign) {
            // Position text field adjacent to the label
            X = Pos.Right (hamCallsignLabel) + 15,
            // Fill remaining horizontal space
            Width = Dim.Fill (15)
        };

        Label hamCallsignExplanationLabel = new() {
            Text = "Must match a Radio ID in CPS.",
            X = Pos.Left(hamCallsignLabel),
            Y = Pos.Bottom(_hamCallsignTextField),
            Width = Dim.Fill()
        };
        
        Label repeaterPathLabel = new() {
            Text = "Path to repeater.csv",
            X = Pos.Left(hamCallsignLabel),
            Y = Pos.Bottom(hamCallsignExplanationLabel) + 1
        };

        _repeaterPathTextField = new TextField(_repeaterPath) {
            // Position text field adjacent to the label
            X = Pos.Right (repeaterPathLabel) + 3,
            Y = Pos.Bottom(hamCallsignExplanationLabel) +1,
            // Fill remaining horizontal space
            Width = Dim.Fill (15)
        };
        
        Label talkgroupPathLabel = new() {
            Text = "Path to talkgroups.csv",
            X = Pos.Left (repeaterPathLabel),
            Y = Pos.Bottom (_repeaterPathTextField) +1
        };

        _talkGroupPathTextField = new TextField(_talkGroupPath) {
            // Position text field adjacent to the label
            X = Pos.Left (_repeaterPathTextField),
            Y = Pos.Bottom (_repeaterPathTextField) + 1,
            // Fill remaining horizontal space
            Width = Dim.Fill(15)
        };

        Label defaultsDirPathLabel = new() {
            Text = "Path to /defaults/",
            X = Pos.Left(talkgroupPathLabel),
            Y = Pos.Bottom(_talkGroupPathTextField) +1
        };
        
        _defaultsDirPathTextField = new TextField(_defaultsDirPath) {
            // Position text field adjacent to the label
            X = Pos.Left (_talkGroupPathTextField),
            Y = Pos.Top (_talkGroupPathTextField) + 2,
            // Fill remaining horizontal space
            Width = Dim.Fill(15)
        };
        
        Label defaultsDirPathExplanationLabel = new() {
            Text = "Select the directory in which the default .csv files\nthat are merged with the final product reside.\nCan contain none, any or all defaults files.",
            X = Pos.Left(defaultsDirPathLabel),
            Y = Pos.Bottom(_defaultsDirPathTextField),
            Width = Dim.Fill()
        };
        
        Label exportDirPathLabel = new() {
            Text = "Path to /export/",
            X = Pos.Left(defaultsDirPathExplanationLabel),
            Y = Pos.Bottom(defaultsDirPathExplanationLabel) +1
        };
        
        _exportDirPathTextField = new TextField(_exportDirPath) {
            // Position text field adjacent to the label
            X = Pos.Left (_talkGroupPathTextField),
            Y = Pos.Bottom (defaultsDirPathExplanationLabel) + 1,
            // Fill remaining horizontal space
            Width = Dim.Fill(15)
        };

        Button btnSelectRepeaterPath = GenerateFilePickerButton(_repeaterPathTextField, false);
        Button btnSelectTalkgroupsPath = GenerateFilePickerButton(_talkGroupPathTextField, false);
        Button btnSelectDefaultsDir = GenerateFilePickerButton(_defaultsDirPathTextField, true);
        Button btnSelectExportDir = GenerateFilePickerButton(_exportDirPathTextField, true);

        Button btnGenerate = new () {
            Text = "Generate CSV Files",
            Y = Pos.Bottom(exportDirPathLabel) + 2,
            // center the login button horizontally
            X = Pos.Center (),
            IsDefault = true,
            Width = Dim.Fill()
        };
        
        btnGenerate.Clicked += GenerateFiles;

        Add(
            hamCallsignLabel,
            _hamCallsignTextField,
            hamCallsignExplanationLabel,
            _repeaterPathTextField,
            repeaterPathLabel, 
            btnSelectRepeaterPath,  
            _talkGroupPathTextField, 
            talkgroupPathLabel,
            btnSelectTalkgroupsPath,
            defaultsDirPathLabel,
            _defaultsDirPathTextField,
            btnSelectDefaultsDir,
            defaultsDirPathExplanationLabel,
            exportDirPathLabel,
            _exportDirPathTextField,
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

    private void GenerateFiles()  {
        if (
            !File.Exists(_repeaterPathTextField.Text.ToString()) || 
            !File.Exists(_talkGroupPathTextField.Text.ToString())
        ) {
            MessageBox.ErrorQuery("Error", "One of the specified files was not found.", "OK");
            return;
        }
        
        OevsvRepeaterFileHandler oevsvRepeaterFileHandler;
        TalkGroupFileHandler talkGroupFileHandler = new TalkGroupFileHandler(_talkGroupPathTextField.Text.ToString());
        
        // Create export director if it does not exist yet.
        string exportPath = Path.Combine(Directory.GetCurrentDirectory(), "export");
        Directory.CreateDirectory(exportPath);
        
        try {
            oevsvRepeaterFileHandler = new OevsvRepeaterFileHandler(_repeaterPathTextField.Text.ToString());
        } catch (NullReferenceException e) {
            MessageBox.ErrorQuery("Error", $"There was an error reading the repeater.csv file: {e.Message}", "OK");
            return;
        }

        var anyToneD878UviiPlusParser = new AnyToneD878UVIIPlusParser<OevsvRepeater>(
            oevsvRepeaterFileHandler.OevsvRepeaters,
            talkGroupFileHandler.TalkGroupList,
            _hamCallsignTextField.Text.ToString() ?? "OE0ABC");

        // Arm the CSVCreator with all created objects
        AnyToneCsvCreator anyToneCsvCreator = new AnyToneCsvCreator {
            Zones = anyToneD878UviiPlusParser.Zones,
            Channels = anyToneD878UviiPlusParser.Channels,
            TalkGroups = anyToneD878UviiPlusParser.TalkGroups,
            AnalogAddressBook = anyToneD878UviiPlusParser.AnalogContacts,
            ScanLists = anyToneD878UviiPlusParser.ScanLists,
            DefaultsDir = _defaultsDirPathTextField.Text.ToString(),
            ExportDir = _exportDirPathTextField.Text.ToString() 
        };

        anyToneCsvCreator.CreateAllFiles();

        MessageBox.Query("Success", "The CSV files were created successfully", "OK");
    }
}