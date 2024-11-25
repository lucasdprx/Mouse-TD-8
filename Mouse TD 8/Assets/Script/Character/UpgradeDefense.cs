using UnityEngine;

public class UpgradeDefense : MonoBehaviour
{
    private Camera _camera;
    
    private LayerMask _defenseLayerMask;
    private void Start()
    {
        _camera = Camera.main;
        _defenseLayerMask = 1 << LayerMask.NameToLayer("Defense");
    }
    private void Update()
    {
        if (!Input.GetMouseButtonDown(0))
            return;

        if (!Physics.Raycast(GetMousePosition(), Vector3.down, out RaycastHit hit, 100f, _defenseLayerMask))
            return;
        
        DefenseStat upgradeDefense = hit.transform.GetComponentInParent<DefenseStat>();
        if (upgradeDefense == null || upgradeDefense._uiUpgrade == null) 
            return;
        
        upgradeDefense._uiUpgrade.SetActive(!upgradeDefense._uiUpgrade.activeSelf);
        SpriteRenderer spriteRenderer = upgradeDefense.GetComponentInChildren<SpriteRenderer>();
        spriteRenderer.enabled = upgradeDefense._uiUpgrade.activeSelf;
    }
    private Vector3 GetMousePosition() => _camera.ScreenToWorldPoint(Input.mousePosition);
}
