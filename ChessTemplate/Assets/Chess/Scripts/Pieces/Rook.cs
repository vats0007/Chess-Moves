using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chess.Scripts.Core;
using System;

public class Rook : Piece
{
    public Transform playerPositions;
    private ChessPlayerPlacementHandler _chessPlayerPlacementHandler;

    private int _row;
    private int _column;
    private int[,] friendlyPiecesPos;

    //Tracking Vars for debugging
    private int upperBlockInColumn, lowerBlockInColumn, rightBlockInRow, leftBlockInRow;
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
        ChessBoardPlacementHandler.Instance.ClearHighlights();
        //They Are Friendly
        //UpperSide
        upperBlockInColumn = -1;
        for (int i = _row + 1; i < 8; i++)
        {
            if (i >= 0 && i < 8)
            {
                if (friendlyPiecesPos[i, _column] == 1)
                {
                    upperBlockInColumn = i;
                    break;
                }
                else
                {
                    ChessBoardPlacementHandler.Instance.Highlight(i, _column);
                }
            }
        }
        //lowerSide
        lowerBlockInColumn = -1;
        for (int i = _row - 1; i >= 0; i--)
        {
            if (i >= 0 && i < 8)
            {
                if (friendlyPiecesPos[i, _column] == 1)
                {
                    lowerBlockInColumn = i;
                    break;
                }
                else
                {
                    ChessBoardPlacementHandler.Instance.Highlight(i, _column);
                }
            }
        }
        //rightSide
        rightBlockInRow = -1;
        for (int j = _column + 1; j < 8; j++)
        {
            if (friendlyPiecesPos[_row, j] == 1)
            {
                rightBlockInRow = j;
                break;
            }
            else
            {
                ChessBoardPlacementHandler.Instance.Highlight(_row, j);
            }
        }
        //leftSide
        leftBlockInRow = -1;
        for (int j = _column - 1; j >= 0; j--)
        {
            if (friendlyPiecesPos[_row, j] == 1)
            {
                leftBlockInRow = j;
                break;
            }
            else
            {
                ChessBoardPlacementHandler.Instance.Highlight(_row, j);
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
