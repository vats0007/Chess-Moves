using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chess.Scripts.Core;
using System;

public class Rook : Piece
{
    public Transform playerPositions;
    //public GameObject enemyPositions;
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
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
    }

    public override void CalculatePossibleMoves()
    {
        //For Rook
        ChessBoardPlacementHandler.Instance.ClearHighlights();
        for(var i = 0; i < 8; i++)
        {
            if (i >= 0 && i < 8 && _column >= 0 && _column < 8)
            {
                if (friendlyPiecesPos[i, _column] == 0)
                {
                    ChessBoardPlacementHandler.Instance.Highlight(i, _column);
                }
            }
        }
        for (var j = 0; j < 8; j++)
        {
            if (j >= 0 && j < 8 && _row >= 0 && _row < 8)
            {
                if (friendlyPiecesPos[_row, j] == 0)
                {
                    ChessBoardPlacementHandler.Instance.Highlight(_row, j);
                }
            }
        }
        //if (i >= 0 && i < 8 && j >= 0 && j < 8)
        //{
        //    if (!(i == _row && j == _column) && friendlyPiecesPos[i, j] == 0)
        //    {
        //        ChessBoardPlacementHandler.Instance.Highlight(i, j);
        //    }
        //}
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
