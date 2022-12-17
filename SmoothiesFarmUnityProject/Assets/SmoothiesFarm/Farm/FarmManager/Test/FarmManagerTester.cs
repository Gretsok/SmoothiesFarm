using System.Collections.Generic;
using UnityEngine;

namespace SmoothiesFarm.Farm.FarmManager.Test
{
    public class FarmManagerTester : MonoBehaviour
    {
        [SerializeField]
        private FarmManager m_farmManager = null;
        [SerializeField]
        private List<SFarmCellInfos> m_cells = null;

        public void SetUpManager()
        {
            m_farmManager.SetUpFarm(m_cells);
        }
    }

#if UNITY_EDITOR
    [UnityEditor.CustomEditor(typeof(FarmManagerTester))]
    public class FarmManagerTesterEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            GUILayout.Space(30f);
            if(GUILayout.Button("Set Up"))
            {
                (target as FarmManagerTester).SetUpManager();
            }
        }
    }
#endif
}