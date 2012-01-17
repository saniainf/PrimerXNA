using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace FloodControl
{
    class RotatingPiece : GamePiece
    {
        public bool clockwise;

        public static float rotationRate = (MathHelper.PiOver2 / 10);
        public int rotationTicksRemaining = 10;
        private float rotationAmount = 0;

        public float RotaionAmount
        {
            get
            {
                if (clockwise)
                {
                    return rotationAmount;
                } 
                else
                {
                    return (MathHelper.Pi * 2) - rotationAmount;
                }
            }
        }

        /* конструктор */
        public RotatingPiece(string pieceType, bool clockwise)
            : base(pieceType)
        {
            this.clockwise = clockwise;
        }

        /* методы */
        public void UpdatePiece()
        {
            rotationAmount += rotationRate;
            rotationTicksRemaining = (int)MathHelper.Max(0, rotationTicksRemaining - 1);
        }

    }
}
