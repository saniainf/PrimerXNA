using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FloodControl
{
    class GamePiece
    {
        public static string[] PieceTypes =
        {
            "Left,Right",
            "Top,Bottom",
            "Left,Top",
            "Top,Right",
            "Right,Bottom",
            "Bottom,Left",
            "Empty"
        };

        public const int PieceHeight = 40;
        public const int PieceWidth = 40;

        public const int MaxPlayablePieceIndex = 5; // кол-во игровых частей
        public const int EmptyPieceIndex = 6; // индекс пустой части

        private const int textureOffsetX = 1;
        private const int textureOffsetY = 1;
        private const int texturePaddingX = 1;
        private const int texturePaddingY = 1;

        private string pieceType = ""; // тип из массива PieceType
        private string pieceSuffix = ""; // заполнена водой или нет (содержит 'W') 

        public string PieceType
        {
            get { return pieceType; }
        }

        public string Suffix
        {
            get { return pieceSuffix; }
        }

        /* конструкторы */
        public GamePiece(string type, string suffix)
        {
            pieceType = type;
            pieceSuffix = suffix;
        }

        public GamePiece(string type)
        {
            pieceType = type;
            pieceSuffix = "";
        }

        /* методы */
        public void SetPiece(string type, string suffix) // создать трубу
        {
            pieceType = type;
            pieceSuffix = suffix;
        }

        public void SetPiece(string type) // создать пустую трубу
        {
            SetPiece(type, "");
        }

        public void AddSuffix(string suffix) // залить водой
        {
            if (!pieceSuffix.Contains(suffix))
            {
                pieceSuffix += suffix;
            }
        }

        public void RemoveSuffix(string suffix) // сделать пустой
        {
            pieceSuffix = pieceSuffix.Replace(suffix, "");
        }

        public void RotatePiece(bool clockwise)
        {
            switch (pieceType)
            {
                case "Left,Right":
                    pieceType = "Top,Bottom";
                    break;
                case "Top,Bottom":
                    pieceType = "Left,Right";
                    break;
                case "Left,Top":
                    if (clockwise)
                        pieceType = "Top,Right";
                    else
                        pieceType = "Bottom,Left";
                    break;
                case "Top,Right":
                    if (clockwise)
                        pieceType = "Right,Bottom";
                    else
                        pieceType = "Left,Top";
                    break;
                case "Right,Bottom":
                    if (clockwise)
                        pieceType = "Bottom,Left";
                    else
                        pieceType = "Top,Right";
                    break;
                case "Bottom,Left":
                    if (clockwise)
                        pieceType = "Left,Top";
                    else
                        pieceType = "Right,Bottom";
                    break;
                case "Empty":
                    break;
            }
        }

        public string[] GetOtherEnds(string startingEnd) // возвращает окончание трубы (Top и Left)
        {
            List<string> opposites = new List<string>();

            foreach (string end in pieceType.Split(','))
            {
                if (end != startingEnd)
                    opposites.Add(end);
            }
            return opposites.ToArray();
        }

        public bool HasConnector(string directions)
        {
            return pieceType.Contains(directions);
        }

        public Rectangle GetSourceRect() // возвращает Rect текстуры в Tile_Sheet
        {
            int x = textureOffsetX;
            int y = textureOffsetY;

            if (pieceSuffix.Contains("W")) // если индекс есть (заполнена водой) то текстура из второго столбика
                x += PieceWidth + texturePaddingX;

            y += (Array.IndexOf(PieceTypes, pieceType) * // индекс в массиве * (размер частички + отступ)
                (PieceHeight + texturePaddingY));

            return new Rectangle(x, y, PieceWidth, PieceHeight);
        }
    }
}
