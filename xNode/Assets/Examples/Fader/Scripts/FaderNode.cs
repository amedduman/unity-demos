using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using XNode;

public class FaderNode : Node, IExecutableNode
{
    [Output(connectionType: ConnectionType.Override)]
    public EnterExitPin exit;
    [Input(connectionType: ConnectionType.Override)]
    public EnterExitPin enter;
    [Input] public Image image;
    public Color Color;

    public async void Execute()
    {
        image = GetInputValue<Image>(nameof(image));
        if (image == null)
        {
            Debug.Log("image is null");
        }

        await image.DOColor(Color, 1);
        
        ExecuteNextNode(nameof(exit));
    }
}