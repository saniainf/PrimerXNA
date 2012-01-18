using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace FloodControl
{
    class FallingPiece : GamePiece
    {
        public int VerticalOffset;
        public static int fallRate = 5;

        /* конструктор */
        public FallingPiece(string pieceType, int verticalOffet)
            : base(pieceType)
        {
            VerticalOffset = verticalOffet;
        }

        /* методы */
        public void UpdatePiece()
        {
            VerticalOffset = (int)MathHelper.Max(0, VerticalOffset - fallRate);
        }
    }
}
