using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


namespace Pumpkin.SceneSeparate
{
    /// <summary>
    /// ������
    /// </summary>
    public enum TreeType
    {
        QuadTree, // �Ĳ���
        OcTree, // �˲���
        BVH, // BVH��
    }


    /// <summary>
    /// ���������ڵ�
    /// </summary>
    public class TreeNode : ITreeNode
    {
        public Bounds Bounds { get; set;}
        public TreeType TreeNodeType { get; set; }

        /// <summary>
        /// �ӽڵ�����
        /// </summary>
        private TreeNode[] m_ChildList;

        /// <summary>
        /// �ڵ�����
        /// </summary>
        private uint m_Depth;

        /// <summary>
        /// �ӽڵ�����
        /// </summary>
        private uint m_ChildCount;

        /// <summary>
        /// ������
        /// </summary>
        private uint m_MaxDepth;

        /// <summary>
        /// �ڵ����ĳ�������
        /// </summary>
        private List<SceneObject> m_ObjectList;

        public TreeNode(Bounds bounds, uint depth, uint childCount, uint maxDepth, TreeType treeNodeType)
        {
            Bounds = bounds;
            m_Depth = depth;
            m_ChildCount = childCount;
            m_MaxDepth = maxDepth;
            TreeNodeType = treeNodeType;

            m_ChildList = new TreeNode[childCount];
            m_ObjectList = new List<SceneObject>();
        }

        /// <summary>
        /// �ݹ鴴���ӽڵ�
        /// </summary>
        public void CreateChilds() 
        {
            if (m_Depth >= m_MaxDepth) return;
            
            switch (TreeNodeType)
            {
                case TreeType.OcTree:
                    CreateOcTreeChild();
                    break;
                case TreeType.QuadTree:
                    CreateQuadTreeChild();
                    break;
            }
        }

        /// <summary>
        /// ��ӳ����������ڵ���
        /// </summary>
        /// <param name="obj"></param>
        public void AddSceneObj(SceneObject obj)
        {
            if (m_ObjectList.Contains(obj))
                return;

            // ���ñ���O(n)����ʽ�������ɸ������ݽṹ
            // ��������bounds��ֹ��һ���ӽڵ�������ڸ��ڵ���б���
            bool inChild = false;
            TreeNode findNode = null;
            if (m_ChildList != null) // ��Ҷ�ڵ�
            {
                for (int i = 0; i < m_ChildList.Length; i++)
                {
                    TreeNode node = m_ChildList[i];

                    if (node == null)
                    {
                        break;   
                    }

                    if (node.Bounds.Intersects(obj.Bounds))
                    {
                        if (findNode != null)
                        {
                            inChild = false;
                            break;
                        }
                        findNode = node;
                        inChild = true;
                    }
                }
            }
           

            if (inChild)
            {
                findNode.AddSceneObj(obj);
            }
            else
            {
                obj.SetDepth(m_Depth);
                m_ObjectList.Add(obj);
            }
        }

        /// <summary>
        /// ���������ӽڵ�
        /// </summary>
        /// <param name="boundsCenter"></param>
        /// <param name="boundsSize"></param>
        /// <param name="childIndex"></param>
        /// <returns></returns>
        private TreeNode CreateChild(Vector3 boundsCenter, Vector3 boundsSize, uint childIndex)
        {
            Bounds bounds = new Bounds(boundsCenter, boundsSize);
            TreeNode newNode = new TreeNode(bounds, m_Depth + 1, m_ChildCount, m_MaxDepth, TreeNodeType);
            m_ChildList[childIndex] = newNode;
            return newNode;
        }

        /// <summary>
        /// �Ĳ�������������
        /// </summary>
        private void CreateQuadTreeChild()
        {
            uint index = 0;
            for (uint i = 1; i < 4; i+=2)
            {
                for (uint j = 1; j < 4; j+=2)
                {
                    Vector3 centerOffset = new Vector3(Bounds.size.x / 4 * i, Bounds.extents.y, Bounds.size.z / 4 * j);
                    Vector3 size = new Vector3(Bounds.size.x / 2, Bounds.size.y, Bounds.size.z / 2);

                    TreeNode newChild = CreateChild(Bounds.min + centerOffset, size, index++);
                    newChild.CreateChilds();
                }
            }
        }

        /// <summary>
        /// �˲�������������
        /// </summary>
        private void CreateOcTreeChild()
        {
            uint index = 0;
            for (uint i = 1; i < 4; i+=2)
            {
                for (uint j = 1; j < 4; j+=2)
                {
                    for(uint k = 1; k < 4; k+=2)
                    {
                        Vector3 centerOffset = new Vector3(Bounds.size.x / 4 * i, Bounds.size.y / 4 * j, Bounds.size.z / 4 * k);
                        Vector3 size = new Vector3(Bounds.size.x / 2, Bounds.size.y / 2, Bounds.size.z / 2);

                        TreeNode newChild = CreateChild(Bounds.min + centerOffset, size, index++);
                        newChild.CreateChilds();
                    }
                }
            }
        }


#if UNITY_EDITOR
        public void DrawBounds()
        {
            Color minDepth = Color.white;
            Color maxDepth = Color.red;

            float d = (float)m_Depth / m_MaxDepth;
            Color color = Color.Lerp(minDepth, maxDepth, d);

            if (m_ChildList != null)
            {
                for (int i = 0; i < m_ChildList.Length; ++i)
                {
                    TreeNode node = m_ChildList[i];
                    if (node != null)
                    {
                        node.DrawBounds();
                    }

                }
            }

            Bounds.DrawBounds(color);
        }
#endif

    }
}

