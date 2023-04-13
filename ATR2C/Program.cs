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

Application.Top.Add(menuBar);
Application.Top.Add(new MainWindow());
Application.Run();
Application.Shutdown();