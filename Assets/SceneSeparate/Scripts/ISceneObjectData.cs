
using UnityEngine;

namespace Pumpkin.SceneSeparate
{
    public interface ISceneObjectData
    {
        public Bounds Bounds { get; }

        // 用于场景对象显示划分的节点深度
        public uint Depth { get; set; }

        public int ID { get;}

    }
}
