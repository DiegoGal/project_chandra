﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace IS_XNA_Shooter
{
    // clase para los objetos del menu
    class MenuItem
    {
        /* ------------------- ATRIBUTOS ------------------- */
        private Rectangle rectangle; // rectángulo "contenedor" del botón
        private Vector2 position;
        private float rotation;
        private bool middlePosition, preshed;
        private Vector2 drawPoint;
        private Texture2D texture;
        private Rectangle rectIddle, rectSelected, rectPushed, rectActual;

        /* ------------------- CONSTRUCTORES ------------------- */
        public MenuItem(bool middlePosition, Vector2 position, Texture2D texture,
            Rectangle rectIddle, Rectangle rectSelected, Rectangle rectPushed)
        {
            this.middlePosition = middlePosition;
            this.position = position;
            preshed = false;
            this.texture = texture;
            this.rectIddle = rectIddle;
            this.rectSelected = rectSelected;
            this.rectPushed = rectPushed;

            if (middlePosition)
            {
                rectangle = new Rectangle((int)position.X - rectIddle.Width / 2, (int)position.Y - rectIddle.Height / 2,
                    rectIddle.Width, rectIddle.Height);
                drawPoint = new Vector2(rectIddle.Width / 2, rectIddle.Height / 2);
            }
            else
            {
                rectangle = new Rectangle((int)position.X, (int)position.Y,
                    rectIddle.Width, rectIddle.Height);
                drawPoint = Vector2.Zero;
            }

            rectActual = rectIddle;
        }

        /* ------------------- MÉTODOS ------------------- */
        public void Update(int X, int Y)
        {
            /*if (rectangle.Contains(X, Y) && !preshed)
                rectActual = rectSelected;*/
            if (rectangle.Contains(X, Y))
            {
                if (!preshed)
                    rectActual = rectSelected;
            }
            else
            {
                rectActual = rectIddle;
                preshed = false;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, rectActual, Color.White, rotation, drawPoint,
                Program.scale, SpriteEffects.None, 0);
        }

        public bool Click(int X, int Y)
        {
            if (rectangle.Contains(X, Y))
            {
                preshed = true;
                rectActual = rectPushed;
                return true;
            }
            else
                return false;
        }

        public bool Unclick(int X, int Y)
        {
            if (rectangle.Contains(X, Y) && preshed)
            {
                preshed = false;
                return true;
            }
            else
                return false;
        }

    } // class MenuItem
}
