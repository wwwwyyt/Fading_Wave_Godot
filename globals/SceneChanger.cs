using Godot;
using System;

public partial class SceneChanger : Node
{
    public static SceneChanger Instance { get; private set; }    
    public Node CurrentScene { get; set; }

    public override void _Ready()
    {
        Instance = this;

        Viewport root = GetTree().Root;
        CurrentScene = root.GetChild(root.GetChildCount() - 1);//根节点的最后一个子节点总是已加载的场景。
    }

    //直接更改场景（不会等旧场景的代码执行完）
    public void ChangeScene(string path)
    {
        var root = GetTree().Root;
        var oldScene = GetTree().CurrentScene;//ChangeScene()获得当前场景的方法与GotoScene()不同
        var newScene = ResourceLoader.Load<PackedScene>(path).Instantiate();

        root.RemoveChild(oldScene);
        root.AddChild(newScene);   
    }

    //安全更改场景（等旧场景的代码执行完）
    public void GotoScene(string path)
    {
        CallDeferred(MethodName.DeferredGotoScene, path);
    }

    public void DeferredGotoScene(string path)
    {
        CurrentScene.Free();
        var nextScene = GD.Load<PackedScene>(path);
        CurrentScene = nextScene.Instantiate();
        GetTree().Root.AddChild(CurrentScene);
        GetTree().CurrentScene = CurrentScene;
    }
}
