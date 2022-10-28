using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pumpkin.SceneSeparate
{
    public class BVHBuildNode : INode
    {
        public Bounds Bounds { get; set; }
        public BVHBuildNode Left { get; set; }
        public BVHBuildNode Right { get; set; }

        public SceneObject SceneObject { get; set; }

#if UNITY_EDITOR
        public void DrawBounds()
        {
            if (Left != null)
            {
                Left.DrawBounds();
            }

            if (Right != null)
            {
                Right.DrawBounds();
            }

            Bounds.DrawBounds(Color.red);
   
        }
#endif
    }
}
