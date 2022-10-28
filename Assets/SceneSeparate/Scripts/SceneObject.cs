using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Pumpkin.SceneSeparate
{

    public class SceneObject
    {
        public Bounds Bounds => m_TargetObj.Bounds;


        private ISceneObjectData m_TargetObj;

        // 场景对象所划分的节点深度
        private uint m_Depth = uint.MaxValue;

        public SceneObject(ISceneObjectData obj)
        {
            m_TargetObj = obj;
        }

        public void SetDepth(uint depth)
        {
            m_Depth = depth;
            m_TargetObj.Depth = depth;
        }

        public void OnHide()
        {
            throw new System.NotImplementedException();
        }

        public bool OnShow(Transform parent)
        {
            throw new System.NotImplementedException();
        }
    }

}
