using UnityEngine;
using XNode;

public class SceneRefNode : Node
{
    public string id;
    [Output] public Component obj;

    public override object GetValue(NodePort port)
    {
        if (!Application.isPlaying)
        {
            return null;
        }

        return graph.BlackBoard.GetSceneObject(id);
    }
}