using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chess.Scripts.Core;
using System;

public class Knight : Piece
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
        //For Knight
        ChessBoardPlacementHandler.Instance.ClearHighlights();

        //All possible Move Adders init
        int[,] knightMoveAdder = new int[,]
        {
            { 2, 1 }, { 2, -1 }, { -2, 1 }, { -2, -1 },
            { 1, 2 }, { 1, -2 }, { -1, 2 }, { -1, -2 }
        };

        for(int i = 0; i < knightMoveAdder.GetLength(0); i++)
        {
            int newRow = _row + knightMoveAdder[i, 0];
            int newColumn = _column + knightMoveAdder[i, 1];

            if (newRow >= 0 && newRow < 8 && newColumn >= 0 && newColumn < 8)
            {
                if (friendlyPiecesPos[newRow,newColumn] == 0)
                {
                    ChessBoardPlacementHandler.Instance.Highlight(newRow, newColumn);
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
