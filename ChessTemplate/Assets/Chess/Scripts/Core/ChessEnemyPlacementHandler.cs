using System;
using UnityEngine;

namespace Chess.Scripts.Core {
    public class ChessEnemyPlacementHandler : MonoBehaviour {
        [SerializeField] public int row, column;

        private void Start() {
            transform.position = ChessBoardPlacementHandler.Instance.GetTile(row, column).transform.position;
        }
        private void Update()
        {
            transform.position = ChessBoardPlacementHandler.Instance.GetTile(row, column).transform.position;
        }


    }
}