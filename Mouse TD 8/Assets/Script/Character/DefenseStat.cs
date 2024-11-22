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
        SerializeProperty("_areaAttack");
        
        if (defenseStatSystem._areaAttack)
        {
            SerializeProperty("_radiusAreaAttack");
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
    public float _damage = 10f;
    public float _radiusAttack = 2f;
    public float _speedAttack = 0.25f;
    [HideInInspector] public bool _areaAttack = false;
    [HideInInspector] public float _radiusAreaAttack = 2f;
    public LayerMask _includeLayer;
}
