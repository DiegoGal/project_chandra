﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace IS_XNA_Shooter
{
    // Es la clase que gestiona los parallax del juego B.
    class BackgroundLayerB : Sprite
    {
        private float speed; // velocidad a la que se desplaza la capa

        private bool tileable;

        private int screenWidth;
        private int screenHeight;
        private float scale;
        private List<List<Rectangle>> listForCollision;
        private List<Texture2D> textureList;
        private bool collisionable;

        private int tileX, tileY;
        private int numtex1 = 0;
        private int numtex2;

        public BackgroundLayerB (bool middlePosition, Vector2 position,
            float rotation, Texture2D texture, float speed, bool tileable, float scale, bool collisionable, List<List<Rectangle>> listForCollision)
            : base(middlePosition, position, rotation, texture)
        {
            this.speed = speed;
            this.tileable = tileable;
            this.scale = scale;
            this.collisionable = collisionable;
            // añadimos los terrenos colisionables
            textureList = new List<Texture2D>();
            textureList.Add(GRMng.textureBgCol1);
            textureList.Add(GRMng.textureBgCol2);
            textureList.Add(GRMng.textureBgCol3);

            numtex2 = new Random().Next(textureList.Count);

            screenWidth = SuperGame.screenWidth;
            screenHeight = SuperGame.screenHeight;

            if (collisionable) 
            {
                this.listForCollision = listForCollision;
            }

            if (tileable)
            {
                //tileX = screenWidth / texture.Width + 2;
                tileY = screenHeight / texture.Height + 1;
                tileX = 1;
            }
            else tileX = tileY = 1;
        }

        public void Update(float deltaTime)
        {
            position.X += speed * deltaTime;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {

            //base.Draw(spriteBatch);
            if (tileable)
                if (collisionable)
                    for (int j = 0; j < tileY; j++)
                    {
                        if (position.X + texture.Width <= SuperGame.screenWidth / 2)// si la posicion del layer llega al fin
                        {
                            position.X = SuperGame.screenWidth / 2;
                            numtex1 = numtex2;
                            numtex2 = new Random().Next(textureList.Count);
                        }
                        spriteBatch.Draw(textureList[numtex2], position + new Vector2(texture.Width, texture.Height * j),
                        null, Color.White, rotation, base.drawPoint, Program.scale, SpriteEffects.None, 0);

                        spriteBatch.Draw(textureList[numtex1], position + new Vector2(0, texture.Height * j),
                        null, Color.White, rotation, base.drawPoint, Program.scale, SpriteEffects.None, 0);

                    }
                else
                {
                    for (int j = 0; j < tileY; j++)
                    {
                        if (position.X + texture.Width <= SuperGame.screenWidth / 2)// si la posicion del layer llega al fin
                            position.X = SuperGame.screenWidth / 2;

                        spriteBatch.Draw(texture, position + new Vector2(texture.Width, texture.Height * j),
                        null, Color.White, rotation, base.drawPoint, Program.scale, SpriteEffects.None, 0);

                        spriteBatch.Draw(texture, position + new Vector2(0, texture.Height * j),
                        null, Color.White, rotation, base.drawPoint, Program.scale, SpriteEffects.None, 0);
                    }
                }
            else
                spriteBatch.Draw(texture, position,
                       null, Color.White, rotation, base.drawPoint, (float)(Program.scale * scale), SpriteEffects.None, 0);
        }
    }
}
