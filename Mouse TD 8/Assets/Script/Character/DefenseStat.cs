using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
[CustomEditor(typeof(DefenseStat))]
public class DefenseStatCustomEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DefenseStat defenseStatSystem = target as DefenseStat;
        base.OnInspectorGUI();
        
        if (defenseStatSystem._areaAttack)
        {
            SerializeProperty("_areaAttack");
            SerializeProperty("_radiusAreaAttack");
        }
        else if (defenseStatSystem._freezeAttack)
        {
            SerializeProperty("_freezeAttack");
            SerializeProperty("_freezeTime");
        }
        else if (defenseStatSystem._slowAttack)
        {
            SerializeProperty("_slowAttack");
            SerializeProperty("_slowTime");
        }
        else
        {
            SerializeProperty("_areaAttack");
            SerializeProperty("_freezeAttack");
            SerializeProperty("_slowAttack");
        }
        serializedObject.ApplyModifiedProperties();
    }
    private void SerializeProperty(string variable)
    {
        SerializedProperty serializedProperty = serializedObject.FindProperty(variable);
        EditorGUILayout.PropertyField(serializedProperty);
    }
}
#endif
public class DefenseStat : MonoBehaviour
{
    public float _radiusAttack = 2f;
    public float _speedAttack = 0.25f;
    public LayerMask _includeLayer;
    public int _price = 400;
    public GameObject _uiUpgrade;
    
    [HideInInspector] public bool _areaAttack = false;
    [HideInInspector] public float _radiusAreaAttack = 2f;
    
    [HideInInspector] public bool _freezeAttack = false;
    [HideInInspector] public float _freezeTime = 0.5f;
    
    [HideInInspector] public bool _slowAttack = false;
    [HideInInspector] public float _slowTime = 5.0f;
    
}
