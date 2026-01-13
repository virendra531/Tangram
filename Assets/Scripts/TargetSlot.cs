using UnityEngine;

public class TargetSlot : MonoBehaviour
{
    [Header("Expected Shape")]
    public ShapeType shapeType;
    public ShapeSize shapeSize;

    [Header("Rotation")]
    public float rotationTolerance = 8f;

    public Transform snapPoint;

    [HideInInspector] public Piece currentPiece;

    public bool IsOccupied => currentPiece != null;

    public void Assign(Piece piece)
    {
        currentPiece = piece;
    }

    public void Clear()
    {
        currentPiece = null;
    }

    public bool IsCorrect()
    {
        if (currentPiece == null)
            return false;

        if (currentPiece.shapeType != shapeType)
            return false;

        if (currentPiece.shapeSize != shapeSize)
            return false;

        return IsRotationCorrect();
    }

    bool IsRotationCorrect()
    {
        float pieceY = Normalize(currentPiece.transform.eulerAngles.y);

        // 🔷 SQUARE — ABSOLUTE ROTATION RULE
        if (shapeType == ShapeType.Square)
        {
            // Reduce rotation to [0, 180)
            float mod = pieceY % 180f;

            return IsClose(mod, 45f) || IsClose(mod, 135f);
        }

        // 🔺 OTHER SHAPES — RELATIVE TO SLOT
        float targetY = Normalize(snapPoint.eulerAngles.y);

        float delta = Mathf.Abs(pieceY - targetY);
        delta = Mathf.Min(delta, 360f - delta);

        return delta <= rotationTolerance;
    }



    bool IsClose(float value, float target)
    {
        return Mathf.Abs(value - target) <= rotationTolerance;
    }

    float Normalize(float angle)
    {
        angle %= 360f;
        if (angle < 0) angle += 360f;
        return angle;
    }
}
