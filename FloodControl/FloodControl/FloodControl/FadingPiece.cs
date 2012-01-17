using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace FloodControl
{
    class FadingPiece : GamePiece
    {
        public float alphaLevel = 1.0f;
        public static float alphaChangeRate = 0.02f;

        /* конструктор */
        public FadingPiece(string pieceType, string suffix)
            : base(pieceType, suffix)
        {

        }

        /* методы */
        public void UpdatePiece()
        {
            alphaLevel = MathHelper.Max(0, alphaLevel - alphaChangeRate);
        }
    }
}
