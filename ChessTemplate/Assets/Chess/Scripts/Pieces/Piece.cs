using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Piece : MonoBehaviour
{
    public abstract void CalculatePossibleMoves();
    public abstract void CheckForFriendlyPieces();
    public abstract void CheckForEnemyPieces();

    protected bool isSelected = false;

    public virtual void Select()
    {
        isSelected = true;
        CalculatePossibleMoves();
    }

    public virtual void Deselect()
    {
        isSelected = false;
        ChessBoardPlacementHandler.Instance.ClearHighlights();
    }
}
