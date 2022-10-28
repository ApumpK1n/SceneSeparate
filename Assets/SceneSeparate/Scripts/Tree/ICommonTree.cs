using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pumpkin.SceneSeparate
{
    // 通用树的接口实现
    public interface ICommonTree
    {

        public TreeNode Root { get; set; }

        public uint ChildCount { get; set; }

        public uint MaxDepth { get; set; }

        // 初始化树
        public void InitTree();

    }

}
