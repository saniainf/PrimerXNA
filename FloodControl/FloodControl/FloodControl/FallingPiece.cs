using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace FloodControl
{
    class FallingPiece : GamePiece
    {
        public int VerticalOffet;
        public static int fallRate = 5;

        /* конструктор */
        public FallingPiece(string pieceType, int verticalOffet)
            : base(pieceType)
        {
            VerticalOffet = verticalOffet;
        }

        /* методы */
        public void UpdatePiece()
        {
            VerticalOffet = (int)MathHelper.Max(0, VerticalOffet - fallRate);
        }
    }
}
