using System;
using System.Reflection;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace VisualVerse
{
    public sealed class VV_NodeEditor : Node
    {
        readonly VV_NodeRuntime vNode;

        public VV_NodeEditor(VV_NodeRuntime n)
        {
            vNode = n;

            vNode.editorNodeGuid = viewDataKey;
            
            Type t = vNode.GetType();
            var info = t.GetCustomAttribute<NodeInfoAttribute>();

            if (info.isStartNode) vNode.isStartNode = true;
            
            if (info.hasEnter)
            {
                var p = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(ExecutionPort));
                p.portName = "enter";
                inputContainer.Add(p);

                RefreshPorts(); 
                RefreshExpandedState();
            }
            if (info.hasExit)
            {
                var p = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(ExecutionPort));
                p.portName = "exit";
                outputContainer.Add(p);

                RefreshPorts(); 
                RefreshExpandedState();
            }

            foreach (FieldInfo field in t.GetFields())
            {
                var att = field.GetCustomAttribute<ExposedFieldAttribute>();
                
                if (att == null) continue;
                
                SetFields(field, att);
                    
                RefreshPorts(); 
                RefreshExpandedState();
            }

            SetNode();
        }

        void SetNode()
        {
            SetPosition(vNode.rect);
            title = vNode.title;
        }
        
        void SetFields(FieldInfo fieldInfo, ExposedFieldAttribute att)
        {
            VisualElement fieldAndPortContainer = new VisualElement();
            fieldAndPortContainer.style.flexDirection = FlexDirection.Row;
            
            if (fieldInfo.FieldType == typeof(int))
            {
                var p = InstantiatePort(Orientation.Horizontal, att.direction , Port.Capacity.Single, fieldInfo.FieldType);
                p.portName = fieldInfo.Name;
                fieldAndPortContainer.Add(p);
                
                IntegerField integerField = new IntegerField(fieldInfo.Name);
                integerField.label = "";
                integerField.SetValueWithoutNotify((int)fieldInfo.GetValue(vNode));
                integerField.RegisterValueChangedCallback(evt => fieldInfo.SetValue(vNode, evt.newValue));
                fieldAndPortContainer.Add(integerField);
                
                if(att.direction ==Direction.Input)
                    inputContainer.Add(fieldAndPortContainer);
                if(att.direction ==Direction.Output)
                    outputContainer.Add(fieldAndPortContainer);
            }
            else if (fieldInfo.FieldType == typeof(float))
            {
                FloatField floatField = new FloatField(fieldInfo.Name);
                floatField.SetValueWithoutNotify((float)fieldInfo.GetValue(vNode));
                floatField.RegisterValueChangedCallback(evt => fieldInfo.SetValue(vNode, evt.newValue));
                mainContainer.Add(floatField);
            }
        }
    }
}
