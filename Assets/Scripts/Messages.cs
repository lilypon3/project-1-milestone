using System;
using Unity.VisualScripting;
using UnityEngine;

public interface IMessage
{
    public Component getSender();
}

public class TestMessage : IMessage
{
    Component sender;

    public TestMessage(Component _sender)
    {
        sender = _sender;
    }

    public Component getSender() { return sender; }
}

public class ArrowInputMessage : IMessage
{
    public static readonly int RIGHT_ARROW = 1; 
    public static readonly int LEFT_ARROW = 2; 
    public static readonly int UP_ARROW = 3;
    public static readonly int DOWN_ARROW = 4;

    int input;
    Component sender;
    public ArrowInputMessage(Component _sender, int _input) { input = _input; sender = _sender; }
    public Component getSender() { return sender; }
    public int getInput() { return input; }

}