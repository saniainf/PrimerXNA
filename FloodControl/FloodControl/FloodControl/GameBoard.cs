using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace FloodControl
{
    class GameBoard
    {
        Random rand = new Random();

        public const int GameBoardWidth = 8;
        public const int GameBoardHeight = 10;

        private GamePiece[,] boardSquares = new GamePiece[GameBoardWidth, GameBoardHeight];

        private List<Vector2> WaterTracker = new List<Vector2>();

        /* конструктор */
        public GameBoard()
        {
            ClearBoard();
        } 

        public void ClearBoard()
        {
            for (int x = 0; x < GameBoardWidth; x++)
            {
                for (int y = 0; y < GameBoardHeight; y++)
                {
                    boardSquares[x, y] = new GamePiece("Empty");
                }
            }
        }

        /* методы */
        public void RotatePiece(int x, int y, bool clockwise) // повернуть трубу
        {
            boardSquares[x, y].RotatePiece(clockwise);
        }

        public Rectangle GetSourceRect(int x, int y) //
        {
            return boardSquares[x, y].GetSourceRect();
        }

        public string GetSquare(int x, int y)
        {
            return boardSquares[x, y].PieceType;
        }

        public void SetSquare(int x, int y, string pieceName)
        {
            boardSquares[x, y].SetPiece(pieceName);
        }

        public bool HasConnector(int x, int y, string direction)
        {
            return boardSquares[x, y].HasConnector(direction);
        }

        public void RandomPiece(int x, int y)
        {
            boardSquares[x, y].SetPiece(GamePiece.PieceTypes[rand.Next(0, GamePiece.MaxPlayablePieceIndex + 1)]);
        }



    }
}
