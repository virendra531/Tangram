using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Piece : MonoBehaviour
{
    public static Piece SelectedPiece;

    [Header("Shape Info")]
    public ShapeType shapeType;
    public ShapeSize shapeSize;

    [Header("Rotation")]
    public float rotationStep = 45f;
    public float rotationTolerance = 8f;

    [HideInInspector] public TargetSlot currentSlot;

    Camera cam;
    Plane dragPlane;
    Vector3 grabOffset;
    float fixedY;
    int targetLayerMask;

    void Start()
    {
        cam = Camera.main;
        fixedY = transform.position.y;
        dragPlane = new Plane(Vector3.up, new Vector3(0, fixedY, 0));
        targetLayerMask = LayerMask.GetMask("Target");
    }

    void OnMouseDown()
    {
        SelectedPiece = this;
        AudioManager.Instance?.PlayClick();

        if (currentSlot != null)
        {
            currentSlot.Clear();
            currentSlot = null;
        }

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (dragPlane.Raycast(ray, out float dist))
        {
            grabOffset = transform.position - ray.GetPoint(dist);
        }
    }

    void OnMouseDrag()
    {
        if (SelectedPiece != this) return;

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (dragPlane.Raycast(ray, out float dist))
        {
            Vector3 p = ray.GetPoint(dist) + grabOffset;
            transform.position = new Vector3(p.x, fixedY, p.z);
        }
    }

    void OnMouseUp()
    {
        if (SelectedPiece == this)
            TrySnap();
    }

    void Update()
    {
        if (SelectedPiece == this && Input.GetKeyDown(KeyCode.R))
        {
            transform.Rotate(Vector3.up, rotationStep);
            AudioManager.Instance?.PlayClick();
        }
    }

    void TrySnap()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (!Physics.Raycast(ray, out RaycastHit hit, 100f, targetLayerMask))
            return;

        TargetSlot slot = hit.collider.GetComponent<TargetSlot>();
        if (slot == null || slot.IsOccupied)
            return;

        SnapTo(slot);
    }

    void SnapTo(TargetSlot slot)
    {
        transform.position = slot.snapPoint.position;

        currentSlot = slot;
        slot.Assign(this);

        AudioManager.Instance?.PlayBump();
    }
}
