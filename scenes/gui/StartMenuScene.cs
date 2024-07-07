using Godot;
using System;

public partial class StartMenuScene : Control
{
    public void OnNewGameBtnPressed()
    {
        var path = "res://scenes/game/Scene_1.tscn";
        SceneChanger.Instance.GotoScene(path);
    }

    public void OnLoadGameBtnPressed()
    {
        
    }

    public void OnConfigBtnPressed()
    {
        
    }

    public void OnExitBtnPressed()
    {
        GetTree().Root.PropagateNotification((int)NotificationWMCloseRequest);
        GetTree().Quit();
    }
}
