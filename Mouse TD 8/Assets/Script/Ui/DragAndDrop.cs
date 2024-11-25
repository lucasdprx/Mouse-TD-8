using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private GameObject _defensePrefab;
    [SerializeField] private TextMeshProUGUI _textPrice;
    
    private DefenseStat _defenseStat;
    private GameObject _draggedObject;
    private Camera _camera;
    private SpriteRenderer _spriteRenderer;
    private readonly Collider[] _listColliders = new Collider[10];
    private const float _radius = 0.49f;
    private bool _canDrag;

    private void Start()
    {
        _camera = Camera.main;
        _defenseStat = _defensePrefab.GetComponent<DefenseStat>();
        _textPrice.text = _defenseStat._price.ToString();
    }

    private Vector3 GetMousePosition()
    {
        return _camera.ScreenToWorldPoint(Input.mousePosition) + Vector3.down * _camera.transform.position.y;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _canDrag = Money.instance.GetMoney() >= _defenseStat._price;
        if (_defensePrefab == null || !_canDrag) return;
        
        _draggedObject = Instantiate(_defensePrefab, GetMousePosition(), Quaternion.identity);
        _spriteRenderer = _draggedObject.GetComponentInChildren<SpriteRenderer>();
        _spriteRenderer.transform.localScale = Vector3.one * _defenseStat._radiusAttack; 
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_defensePrefab == null || !_canDrag) return;
        
        _draggedObject.transform.position = GetMousePosition();

        //Set color of sprite
        int count = Physics.OverlapSphereNonAlloc(_draggedObject.transform.position, _radius, _listColliders);
        _spriteRenderer.color = count > 1 ? Color.red - new Color{ a = 0.5f }: Color.green- new Color{ a = 0.5f };
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (_defensePrefab == null || !_canDrag) return;
        
        int count = Physics.OverlapSphereNonAlloc(_draggedObject.transform.position, _radius, _listColliders);

        if (count > 1)
            Destroy(_draggedObject);
        else
        {
            _spriteRenderer.enabled = false;
            _spriteRenderer.color = Color.gray - new Color{ a = 0.5f };
            _draggedObject.GetComponent<DefenseAttack>()._canAttack = true;
            Money.instance.RemoveMoney(_defenseStat._price);
        }
    }
}
