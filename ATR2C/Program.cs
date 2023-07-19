using ATCSVCreator.NitricWare.GUI;
using Terminal.Gui;
Application.Init();
MenuBar menuBar = new(
    new MenuBarItem[] {
        new MenuBarItem("File",
            new MenuItem[] {
                new MenuItem("Quit", "Quits the application", () => {Application.Shutdown();})
            }),
        new MenuBarItem("Extras",
            new MenuItem[] {
                new MenuItem("Digital Contact List...", "", () => {Application.Run<DigitalContactListWindow>();})
            })
    }
);

var mainWindow = new MainWindow();

bool errorHandler(Exception e) {
    return false;
}

Application.Top.Add(menuBar);
Application.Top.Add(mainWindow);
Application.Run(errorHandler);
Application.Shutdown();