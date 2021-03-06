﻿using System;

namespace Test_kitbox
{
    public class AngleBar : Piece
    {
        private int height;
        public int Height
        {
            get { return height; }
        }

        private string color;
        public string Color
        {
            get { return color; }
        }

        public AngleBar(int height, string color, int price) : base (price)
        {
            this.height = height;
            this.color = color;
        }

        override public Piece Copy()
        {
            return new AngleBar(this.height, this.color, this.price);
        }

        public override string ToString()
        {
            return "AngelBar";
        }
    }
}
