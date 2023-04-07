using Terminal.Gui;

namespace ATCSVCreator.NitricWare.Helper; 

public static class GuiElementHelper {
    public static Button GenerateFilePickerButton(TextField parent, bool dirSelect) {
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
}