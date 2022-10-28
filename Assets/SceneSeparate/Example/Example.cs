using System.Collections.Generic;
using UnityEngine;

namespace Pumpkin.SceneSeparate
{
    public class Example : MonoBehaviour
    {
        public Terrain terrain;
        public Bounds bounds;

        public List<SceneObjectDataMono> sceneObjs;

        public TreeType treeType = TreeType.QuadTree;
        public uint m_MaxDepth;

        private SceneObjectLoadController m_Controller;

        private void Awake()
        {
            bounds = terrain.terrainData.bounds;
        }

        private void Start()
        {
            m_Controller = gameObject.GetComponent<SceneObjectLoadController>();
            if (m_Controller == null)
                m_Controller = gameObject.AddComponent<SceneObjectLoadController>();

            switch (treeType)
            {
                case TreeType.OcTree:
                case TreeType.QuadTree:
                    m_Controller.Init(bounds, treeType, m_MaxDepth);
                    for (int i = 0; i < sceneObjs.Count; i++)
                    {
                        m_Controller.AddSceneObject(sceneObjs[i]);
                    }
                    break;
                case TreeType.BVH:
                    m_Controller.BuildBVH(sceneObjs);
                    break;
            }
            
        }

        void OnDrawGizmos()
        {
            bounds.DrawBounds(Color.black);
        }
    }
}
