using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Pumpkin.SceneSeparate
{

    /// <summary>
    /// 四叉树的主要实现
    /// </summary>
    public class QuadTree : ITreeNode, ICommonTree
    {
        public Bounds Bounds { get; set; }
        public TreeType TreeNodeType { get; set; }
        public TreeNode Root { get; set; }
        public uint ChildCount { get; set; }
        public uint MaxDepth { get; set; }

        public QuadTree(Bounds bounds, uint maxDepth)
        {
            Bounds = bounds;
            MaxDepth = maxDepth;
            ChildCount = 4;

            Root = new TreeNode(Bounds, 0, ChildCount, MaxDepth, TreeType.QuadTree);
        }

        public void InitTree()
        {
            Root.CreateChilds();
        }

        public void AddSceneObj(SceneObject obj)
        {
            Root.AddSceneObj(obj);
        }

#if UNITY_EDITOR
        public void DrawBounds()
        {
            Root.DrawBounds();
        }
#endif
    }
}
