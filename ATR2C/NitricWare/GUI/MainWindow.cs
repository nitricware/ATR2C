using System.Net.Mime;
using System.Runtime.InteropServices;
using ATCSVCreator.NitricWare.AnyTone;
using ATCSVCreator.NitricWare.Helper;
using ATCSVCreator.NitricWare.Oevsv;
using ATCSVCreator.NitricWare.TalkGroups;
using Terminal.Gui;
using static Terminal.Gui.Application;

namespace ATCSVCreator.NitricWare.GUI;

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
    private readonly ComboBox _exportTypeComboBox;
    private readonly MenuBar _menuBar;
    
    private readonly Button btnGenerate;

    private List<View> _views = new();
    
    public MainWindow() {
        // Position the Main Window
        Height = Application.Top.Frame.Height - 1;
        Y = 1;
        
        Label hamCallsignLabel = new() {
            Text = "Callsign"
        };
        _views.Add(hamCallsignLabel);

        _hamCallsignTextField = new TextField(_hamCallsign) {
            // Position text field adjacent to the label
            X = Pos.Right (hamCallsignLabel) + 15,
            // Fill remaining horizontal space
            Width = Dim.Fill (15)
        };
        
        _views.Add(_hamCallsignTextField);

        Label hamCallsignExplanationLabel = new() {
            Text = "Must match a Radio ID in CPS.",
            X = Pos.Left(hamCallsignLabel),
            Y = Pos.Bottom(_hamCallsignTextField),
            Width = Dim.Fill()
        };

        _views.Add(hamCallsignExplanationLabel);

        Label repeaterPathLabel = new() {
            Text = "Path to repeater.csv",
            X = Pos.Left(hamCallsignLabel),
            Y = Pos.Bottom(hamCallsignExplanationLabel) + 1
        };
        
        _views.Add(repeaterPathLabel);

        _repeaterPathTextField = new TextField(_repeaterPath) {
            // Position text field adjacent to the label
            X = Pos.Right (repeaterPathLabel) + 3,
            Y = Pos.Bottom(hamCallsignExplanationLabel) +1,
            // Fill remaining horizontal space
            Width = Dim.Fill (15)
        };
        
        _views.Add(_repeaterPathTextField);
        
        Button btnSelectRepeaterPath = GuiElementHelper.GenerateFilePickerButton(_repeaterPathTextField, false);
        
        _views.Add(btnSelectRepeaterPath);
        
        Label talkgroupPathLabel = new() {
            Text = "Path to talkgroups.csv",
            X = Pos.Left (repeaterPathLabel),
            Y = Pos.Bottom (_repeaterPathTextField) +1
        };
        
        _views.Add(talkgroupPathLabel);

        _talkGroupPathTextField = new TextField(_talkGroupPath) {
            // Position text field adjacent to the label
            X = Pos.Left (_repeaterPathTextField),
            Y = Pos.Bottom (_repeaterPathTextField) + 1,
            // Fill remaining horizontal space
            Width = Dim.Fill(15)
        };
        
        _views.Add(_talkGroupPathTextField);
        
        Button btnSelectTalkgroupsPath = GuiElementHelper.GenerateFilePickerButton(_talkGroupPathTextField, false);
        
        _views.Add(btnSelectTalkgroupsPath);

        Label defaultsDirPathLabel = new() {
            Text = "Path to /defaults/",
            X = Pos.Left(talkgroupPathLabel),
            Y = Pos.Bottom(_talkGroupPathTextField) +1
        };
        
        _views.Add(defaultsDirPathLabel);
        
        _defaultsDirPathTextField = new TextField(_defaultsDirPath) {
            // Position text field adjacent to the label
            X = Pos.Left (_talkGroupPathTextField),
            Y = Pos.Top (_talkGroupPathTextField) + 2,
            // Fill remaining horizontal space
            Width = Dim.Fill(15)
        };
        
        _views.Add(_defaultsDirPathTextField);

        Label defaultsDirPathExplanationLabel = new() {
            Text = "Select the directory in which the default .csv files that are merged with the final product reside. The directory must contain a sub folder named exactly as the export type (i.e. \"CHIRP\"). Subfolder an contain none, any or all defaults files."
                .AddLineBreaks(78,""),
            X = Pos.Left(defaultsDirPathLabel),
            Y = Pos.Bottom(_defaultsDirPathTextField),
            Width = Dim.Fill(),
            AutoSize = true
        };
        
        _views.Add(defaultsDirPathExplanationLabel);
        
        Button btnSelectDefaultsDir = GuiElementHelper.GenerateFilePickerButton(_defaultsDirPathTextField, true);
        
        _views.Add(btnSelectDefaultsDir);
        
        Label exportDirPathLabel = new() {
            Text = "Path to /export/",
            X = Pos.Left(defaultsDirPathExplanationLabel),
            Y = Pos.Bottom(defaultsDirPathExplanationLabel) +1
        };
        
        _views.Add(exportDirPathLabel);
        
        _exportDirPathTextField = new TextField(_exportDirPath) {
            // Position text field adjacent to the label
            X = Pos.Left (_talkGroupPathTextField),
            Y = Pos.Bottom (defaultsDirPathExplanationLabel) + 1,
            // Fill remaining horizontal space
            Width = Dim.Fill(15)
        };
        
        _views.Add(_exportDirPathTextField);
        
        Button btnSelectExportDir = GuiElementHelper.GenerateFilePickerButton(_exportDirPathTextField, true);
        
        _views.Add(btnSelectExportDir);
        
        Label exportTypeLabel = new() {
            Text = "Export Type",
            X = Pos.Left(exportDirPathLabel),
            Y = Pos.Bottom(exportDirPathLabel) +1
        };
        
        _views.Add(exportTypeLabel);

        List<string> exportTypes = Settings.exportTypes;

        _exportTypeComboBox = new ComboBox {
            Width = Dim.Fill(15),
            X = Pos.Left (_exportDirPathTextField) ,
            Y = Pos.Bottom (_exportDirPathTextField) + 1
        };
        
        _exportTypeComboBox.SetSource(exportTypes);
        _exportTypeComboBox.SelectedItem = 0;
        _exportTypeComboBox.Height = exportTypes.Count + 1;
        
        _views.Add(_exportTypeComboBox);

        btnGenerate = new () {
            Text = "Generate CSV Files",
            Y = Pos.Top(_views.Last()) +2 ,
            // center the login button horizontally
            X = Pos.Center (),
            IsDefault = true,
            Width = Dim.Fill()
        };
        
        btnGenerate.Clicked += GenerateFiles;
        
        _views.Add(btnGenerate);

        Add(_views.ToArray());
    }

    private void GenerateFiles() {
        btnGenerate.Visible = false;
        // TODO: move to ATR2C class?
        if (
            !File.Exists(_repeaterPathTextField.Text.ToString()) || 
            !File.Exists(_talkGroupPathTextField.Text.ToString())
        ) {
            MessageBox.ErrorQuery("Error", "One of the specified files was not found.", "OK");
            btnGenerate.Visible = true;
            return;
        }

        TalkGroupFileHandler talkGroupFileHandler;
        OevsvRepeaterFileHandler oevsvRepeaterFileHandler;
        try {
            talkGroupFileHandler = new TalkGroupFileHandler(_talkGroupPathTextField.Text.ToString());
        } catch (CsvHelper.HeaderValidationException e) {
            MessageBox.ErrorQuery("Error", $"Unexpected or missing column names in talkgroups input file.", "OK");
            btnGenerate.Visible = true;
            return;
        }
        
        
        // Create export director if it does not exist yet.
        string exportPath = Path.Combine(Directory.GetCurrentDirectory(), "export");
        Directory.CreateDirectory(exportPath);

        try {
            oevsvRepeaterFileHandler = new OevsvRepeaterFileHandler(_repeaterPathTextField.Text.ToString());
        }
        catch (NullReferenceException e) {
            MessageBox.ErrorQuery("Error", $"There was an error reading the repeater input file: {e.Message}", "OK");
            btnGenerate.Visible = true;
            return;
        }
        catch (CsvHelper.HeaderValidationException e) {
            MessageBox.ErrorQuery("Error", $"Unexpected or missing column names in repeater input file.", "OK");
            btnGenerate.Visible = true;
            return;
        }

        switch (_exportTypeComboBox.Text.ToString()) {
            case "AnyTone AT-D878UVII Plus":
                var anyToneD878UviiPlusParser = new AnyToneD878UVIIPlusRepeaterParser<OevsvRepeater>(
                    oevsvRepeaterFileHandler.OevsvRepeaters,
                    talkGroupFileHandler.TalkGroupList,
                    _hamCallsignTextField.Text.ToString() ?? Settings.HamCallSign);

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
                break;
            case "CHIRP":
                
                break;
            default:
                MessageBox.ErrorQuery("Error", $"Unknown Export Type", "OK");
                break;
        }
        MessageBox.Query("Success", "The CSV files were created successfully", "OK");
        btnGenerate.Visible = true;
    }
}