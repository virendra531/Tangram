using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TargetSlot[] allSlots;
    bool hasWon;

    void Update()
    {
        if (hasWon) return;
        if (allSlots == null || allSlots.Length == 0) return;

        if (AreAllSlotsCorrect())
            Win();
    }

    bool AreAllSlotsCorrect()
    {
        foreach (TargetSlot slot in allSlots)
        {
            if (!slot.IsCorrect())
                return false;
        }
        return true;
    }

    void Win()
    {
        hasWon = true;
        
        AudioManager.Instance?.PlayPop();
        UIManager.Instance?.ShowWin();

        Debug.Log("🎉 PUZZLE SOLVED!");
    }
}
