using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chess.Scripts.Core;
using System;

public class Bishop : Piece
{
    public Transform playerPositions;
    private ChessPlayerPlacementHandler _chessPlayerPlacementHandler;


    private int _row;
    private int _column;

    private int topRightBlock, topLeftBlock, bottomRightBlock, bottomLeftBlock;

    private int[,] friendlyPiecesPos;
    // Start is called before the first frame update
    void Start()
    {
        friendlyPiecesPos = new int[8, 8];
        _chessPlayerPlacementHandler = GetComponent<ChessPlayerPlacementHandler>();
        GetMyPosition();
        for (var i = 0; i < 8; i++)
        {
            for (var j = 0; j < 8; j++)
            {
                Debug.Log(i + "," + j + "=" + friendlyPiecesPos[i, j]);
            }
        }
        CheckForFriendlyPieces();
        for (var i = 0; i < 8; i++)
        {
            for (var j = 0; j < 8; j++)
            {
                Debug.Log(i + "," + j + "=" + friendlyPiecesPos[i, j]);
            }
        }
        CalculatePossibleMoves();

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
        //topRightBlock
        topRightBlock = -1;
        for (int i = _row + 1, j = _column + 1; i < 8 && j < 8; i++, j++)
        {
            if (friendlyPiecesPos[i, j] == 1)
            {
                topLeftBlock = i;
                break;
            }
            else
            {
                ChessBoardPlacementHandler.Instance.Highlight(i, j);
            }
        }
        //topLeftBlock
        topLeftBlock = -1;
        for (int i = _row + 1, j = _column - 1; i < 8 && j >= 0; i++, j--) 
        {
            if (friendlyPiecesPos[i, j] == 1)
            {
                topRightBlock = i;
                break;
            }
            else
            {
                ChessBoardPlacementHandler.Instance.Highlight(i, j);
            }
        }
        //bottomRightBlock
        bottomRightBlock = -1;
        for (int i = _row - 1, j = _column + 1; i >=0 && j < 8;i--,j++)
        {
            if (friendlyPiecesPos[i, j] == 1)
            {
                bottomRightBlock = j;
                break;
            }
            else
            {
                ChessBoardPlacementHandler.Instance.Highlight(i, j);
            }
        }
        //bottomLeftBlock
        bottomLeftBlock = -1;
        for (int i=_row-1,j = _column - 1; i >= 0 && j >= 0; i--, j--)
        {
            if (friendlyPiecesPos[i, j] == 1)
            {
                bottomLeftBlock = j;
                break;
            }
            else
            {
                ChessBoardPlacementHandler.Instance.Highlight(i, j);
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
