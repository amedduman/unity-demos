using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI_Animator
{
    public class NodeInfoAttribute : Attribute
    {
        public string menuItem { get; private set; }

        public NodeInfoAttribute(string menuItem)
        {
            this.menuItem = menuItem;
        }
    }
}
