using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Pumpkin.SceneSeparate
{

    /// <summary>
    /// 八叉树的主要实现
    /// </summary>
    public class OcTree : ITreeNode, ICommonTree
    {
        public Bounds Bounds { get; set; }
        public TreeType TreeNodeType { get; set; }
        public TreeNode Root { get; set; }
        public uint ChildCount { get; set; }
        public uint MaxDepth { get; set; }

        public OcTree(Bounds bounds, uint maxDepth)
        {
            Bounds = bounds;
            MaxDepth = maxDepth;
            ChildCount = 8;

            Root = new TreeNode(Bounds, 0, ChildCount, MaxDepth, TreeType.OcTree);
        }

        public void InitTree()
        {
            Root.CreateChilds();
        }

        public void AddSceneObj(SceneObjectDataMono obj)
        {
            Root.AddSceneObj(obj);
        }

        public void CheckBoundIsInCamera(Camera camera)
        {
            Root.CheckBoundIsInCamera(camera);
        }

        public void OutsideCamera(Camera camera)
        {
            Root.OutsideCamera(camera);
        }

#if UNITY_EDITOR
        public void DrawBounds()
        {
            Root.DrawBounds();
        }
#endif
    }
}
