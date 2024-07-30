using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chess.Scripts.Core;
using System;

public class Queen : Piece
{
    public Transform playerPositions;
    private ChessPlayerPlacementHandler _chessPlayerPlacementHandler;


    private int _row;
    private int _column;
    private int[,] friendlyPiecesPos;
    //Tracking Vars for debugging
    private int upperBlockInColumn, lowerBlockInColumn, rightBlockInRow, leftBlockInRow;
    private int topRightBlock, topLeftBlock, bottomRightBlock, bottomLeftBlock;
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
        //For Queen
        ChessBoardPlacementHandler.Instance.ClearHighlights();
        // Queen = King + Bishop + Rook

        //Moves of King
        KingMoves();

        //Moves of Rook
        RookMoves();

        //Moves of Bishop
        BishopMoves();
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

    void KingMoves()
    {
        for (var i = _row - 1; i <= _row + 1; i++)
        {
            for (var j = _column - 1; j <= _column + 1; j++)
            {
                if (i >= 0 && i < 8 && j >= 0 && j < 8)
                {
                    if (!(i == _row && j == _column) && friendlyPiecesPos[i, j] == 0)
                    {
                        ChessBoardPlacementHandler.Instance.Highlight(i, j);
                    }
                }
            }
        }
    }

    void RookMoves()
    {

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

    void BishopMoves()
    {
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
        for (int i = _row - 1, j = _column + 1; i >= 0 && j < 8; i--, j++)
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
        for (int i = _row - 1, j = _column - 1; i >= 0 && j >= 0; i--, j--)
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

}
