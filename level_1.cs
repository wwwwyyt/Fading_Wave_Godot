using Godot;
using System;
using System.Runtime.Serialization;

public partial class level_1 : Control
{
    public void OnTreeEntered()
    {
        GetNode<AnimationPlayer>("Transition").Play("fade_out_black");
    }
}
