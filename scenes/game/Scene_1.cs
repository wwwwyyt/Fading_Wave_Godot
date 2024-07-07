using Godot;
using System;

public partial class Scene_1 : Control
{
    //切换背景图片
    private readonly string[] BackgroundPicturePath = {
        "res://picture/subway-1.jpg",
        "res://picture/subway-2.jpg",
        "res://picture/subway-3.jpg"
    };
    private static int index = 0;
    public void OnScreenClicked()
    {   
        GetNode<AnimationPlayer>("Transition").Play("Black");
        EmitSignal(SignalName.CurrentAnimationFinished, "Black");
    }

    //进行画面过渡
    [Signal]
    public delegate void CurrentAnimationFinishedEventHandler(string animationName);
    public void OnAnimationFinished(string name)
    {
        //切换到下一张背景图片(应该另外设计一个函数处理该工作)
        if (index >= BackgroundPicturePath.Length - 1)
            index = -1;
        var picture = (Texture2D)ResourceLoader.Load(BackgroundPicturePath[++index]);
        var background = GetNode<TextureRect>("Background/Picture");
        background.Texture = picture;

        GetNode<AnimationPlayer>("Transition").PlayBackwards("Black");
    }
    
    public void OnTreeEntered()
    {
        GetNode<AnimationPlayer>("Transition").PlayBackwards("Black");
    }
}
