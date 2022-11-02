using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pumpkin.SceneSeparate
{
    public interface INode
    {
        /// <summary>
        /// 节点的包围盒
        /// </summary>
        Bounds Bounds { get; set; }

        public void CheckBoundIsInCamera(Camera camera);

        public void OutsideCamera(Camera camera);

#if UNITY_EDITOR
        void DrawBounds();
#endif
    }
}
