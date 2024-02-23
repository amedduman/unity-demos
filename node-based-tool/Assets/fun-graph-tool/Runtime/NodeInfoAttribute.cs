using System;

namespace Fun
{
    public class NodeInfoAttribute : Attribute
    {
        public string menuItem { get; private set; }
        public bool hasEnter { get; private set; }
        public bool hasExit { get; private set; }

        public NodeInfoAttribute(string menuItem, bool hasEnter = true, bool hasExit = true)
        {
            this.menuItem = menuItem;
            this.hasEnter = hasEnter;
            this.hasExit = hasExit;
        }
    }
}
