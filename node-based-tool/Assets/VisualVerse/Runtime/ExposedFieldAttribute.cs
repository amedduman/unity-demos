using System;
using UnityEditor.Experimental.GraphView;

namespace VisualVerse
{
    public class ExposedFieldAttribute : Attribute
    {
        public Direction direction { get; private set; }

        public ExposedFieldAttribute(bool isInput = true)
        {
            direction = isInput ? Direction.Input : Direction.Output;
        }
    }
}