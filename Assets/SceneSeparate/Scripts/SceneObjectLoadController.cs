using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pumpkin.SceneSeparate
{
    public class SceneObjectLoadController : MonoBehaviour
    {
        // Start is called before the first frame update

        private INode m_Tree;

        private bool m_IsInitialized;

        public void Init(Bounds bounds, TreeType treeType, uint maxDepth = 3)
        {
            if (m_IsInitialized) return;

            switch (treeType)
            {
                case TreeType.QuadTree:
                    m_Tree = new QuadTree(bounds, maxDepth);
                    break;
                case TreeType.OcTree:
                    m_Tree = new OcTree(bounds, maxDepth);
                    break;
            }

            ICommonTree tree = (ICommonTree)m_Tree;
            tree.InitTree();

            m_IsInitialized = true;
        }

        /// <summary>
        /// 构建BVH树
        /// </summary>
        public void BuildBVH(List<SceneObjectDataMono> sceneObjectDataMonos)
        {
            if (m_IsInitialized) return;
            BVH bvh = new BVH();

            List<SceneObject> sceneObjects = new List<SceneObject>(sceneObjectDataMonos.Count);
            foreach(var objData in sceneObjectDataMonos)
            {
                sceneObjects.Add(new SceneObject(objData));
            }
            bvh.BVHAccel(sceneObjects);
            m_Tree = bvh;
            m_IsInitialized = true;
        }

        /// <summary>
        /// 添加场景物体
        /// </summary>
        /// <param name="obj"></param>
        public void AddSceneObject(SceneObjectDataMono obj)
        {
            if (!m_IsInitialized)
                return;
            if (m_Tree == null)
                return;
            if (obj == null)
                return;

            SceneObject sceneObject = new SceneObject(obj);
            ITreeNode tree = (ITreeNode)m_Tree;
            tree.AddSceneObj(sceneObject);
        }

        private void OnDrawGizmosSelected()
        {
            if (m_Tree != null)
            {
                m_Tree.DrawBounds();
            }
        }
    }
}
  
