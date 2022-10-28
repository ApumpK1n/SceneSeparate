using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Pumpkin.SceneSeparate
{
    public class BVH : INode
    {
        private BVHBuildNode m_root;

        public Bounds Bounds { get => m_root.Bounds; set { m_root.Bounds = value; } }

        public void BVHAccel(List<SceneObject> sceneObjects)
        {
            if (sceneObjects == null || sceneObjects.Count < 1) return;

            m_root = RecursiveBuild(sceneObjects);
        }

#if UNITY_EDITOR
        public void DrawBounds()
        {
            m_root.DrawBounds();
        }
#endif

        private BVHBuildNode RecursiveBuild(List<SceneObject> sceneObjects)
        {
            BVHBuildNode node = new BVHBuildNode();

            // 计算所有物体的包围盒在BVH树中
            if (sceneObjects.Count == 1)
            {
                // 创建叶节点
                node.Bounds = sceneObjects[0].Bounds;
                node.SceneObject = sceneObjects[0];
                node.Left = null;
                node.Right = null;
                return node;
            }

            else if (sceneObjects.Count == 2)
            {
                node.Left = RecursiveBuild(new List<SceneObject>() { sceneObjects[0] });
                node.Right = RecursiveBuild(new List<SceneObject>() { sceneObjects[1] });

                node.Bounds = node.Left.Bounds.Union(node.Right.Bounds);

                return node;
            }
            else
            {
                Bounds bounds = sceneObjects[0].Bounds;
                for (int i = 1; i < sceneObjects.Count; ++i)
                {
                    bounds = bounds.Union(sceneObjects[i].Bounds);
                }

                int dim = bounds.MaxExtent();
                switch (dim)
                {
                    case 0:
                        sceneObjects.Sort(new BVHSortCompareX());
                        break;
                    case 1:
                        sceneObjects.Sort(new BVHSortCompareY());
                        break;
                    case 2:
                        sceneObjects.Sort(new BVHSortCompareZ());
                        break;
                }
                int leftIndex = 0;
                int midIndex = sceneObjects.Count / 2;
                int rightIndex = sceneObjects.Count - 1;

                List<SceneObject> leftShapes = sceneObjects.GetRange(leftIndex, midIndex - leftIndex);
                List<SceneObject> rightShapes = sceneObjects.GetRange(midIndex, rightIndex - midIndex + 1);

                Assert.AreEqual(sceneObjects.Count, leftShapes.Count + rightShapes.Count);

                node.Left = RecursiveBuild(leftShapes);
                node.Right = RecursiveBuild(rightShapes);

                node.Bounds = node.Left.Bounds.Union(node.Right.Bounds);
            }
            return node;
        }

    }

    public class BVHSortCompareX : IComparer<SceneObject>
    {

        public int Compare(SceneObject x, SceneObject y)
        {
            if (x.Bounds.center.x < y.Bounds.center.x) return -1; // x在y之前
            else return 1;
        }
    }

    public class BVHSortCompareY : IComparer<SceneObject>
    {

        public int Compare(SceneObject x, SceneObject y)
        {
            if (x.Bounds.center.y < y.Bounds.center.y) return -1; // x在y之前
            else return 1;
        }
    }

    public class BVHSortCompareZ : IComparer<SceneObject>
    {

        public int Compare(SceneObject x, SceneObject y)
        {
            if (x.Bounds.center.z < y.Bounds.center.z) return -1; // x在y之前
            else return 1;
        }
    }
}
