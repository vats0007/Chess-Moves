using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Piece : MonoBehaviour
{
    public abstract void CalculatePossibleMoves();
    public abstract void CheckForFriendlyPieces();
}
