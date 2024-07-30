using System.Collections;
using System.Collections.Generic;
using Chess.Scripts.Core;
using UnityEngine;

public class Pawn : Piece
{
    public Transform playerPositions;
    private ChessPlayerPlacementHandler _chessPlayerPlacementHandler;

    private int _row;
    private int _column;
    private int[,] friendlyPiecesPos;
    // Start is called before the first frame update
    void Start()
    {
        friendlyPiecesPos = new int[8, 8];
        _chessPlayerPlacementHandler = GetComponent<ChessPlayerPlacementHandler>();
        GetMyPosition();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void CalculatePossibleMoves()
    {
        GetMyPosition();
        CheckForFriendlyPieces();
        if (!isSelected) return;

        //For Pawn
        ChessBoardPlacementHandler.Instance.ClearHighlights();
        if(_row == 1)
        {
            //Highlight 2 block ahead
            for (int i = _row + 1; i <= _row + 2; i++)
            {
                if (i < 8)
                {
                    if (friendlyPiecesPos[i, _column] == 0)
                    {
                        ChessBoardPlacementHandler.Instance.Highlight(i, _column);
                    }
                }
            }
        }
        else
        {
            //Highlight 1 block ahead
            int i = _row + 1;
            if (i < 8)
            {
                if (friendlyPiecesPos[i, _column] == 0)
                {
                    ChessBoardPlacementHandler.Instance.Highlight(i, _column);
                }
            }
        }
    }

    void GetMyPosition()
    {
        _row = _chessPlayerPlacementHandler.row;
        _column = _chessPlayerPlacementHandler.column;
    }


    public override void CheckForFriendlyPieces()
    {
        SetAllIntsToZero();
        foreach (Transform friendlyPieces in playerPositions)
        {
            ChessPlayerPlacementHandler tempChessPlayerPlacementHandler = friendlyPieces.GetComponent<ChessPlayerPlacementHandler>();
            var tRow = tempChessPlayerPlacementHandler.row;
            var tCol = tempChessPlayerPlacementHandler.column;

            friendlyPiecesPos[tRow, tCol] = 1;
        }
    }

    private void SetAllIntsToZero()
    {
        for (int i = 0; i < friendlyPiecesPos.GetLength(0); i++)
        {
            for (int j = 0; j < friendlyPiecesPos.GetLength(1); j++)
            {
                friendlyPiecesPos[i, j] = 0;
            }
        }
    }


}
