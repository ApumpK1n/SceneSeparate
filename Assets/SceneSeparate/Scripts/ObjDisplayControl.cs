using System;
using System.Collections.Generic;
using UnityEngine;
using Pumpkin.Utility;

namespace Pumpkin.SceneSeparate
{
    public class ObjDisplayControl : SingletonBehavior<ObjDisplayControl>
    {
        private Dictionary<int, SceneObjectDataMono> m_ActiveObj = new Dictionary<int, SceneObjectDataMono>();

        /// <summary>
        /// 使对象显示
        /// </summary>
        /// <param name="obj"></param>
        public void Show(SceneObjectDataMono obj)
        {
            if (m_ActiveObj.ContainsKey(obj.ID)) return;

            m_ActiveObj[obj.ID] = obj;
            obj.gameObject.SetActive(true);
        }

        /// <summary>
        /// 使对象隐藏
        /// </summary>
        /// <param name="obj"></param>
        public void Hide(SceneObjectDataMono obj)
        {
            if (!m_ActiveObj.ContainsKey(obj.ID)) return;

            m_ActiveObj.Remove(obj.ID);
            obj.gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            m_ActiveObj.Clear();
        }
    }
}
