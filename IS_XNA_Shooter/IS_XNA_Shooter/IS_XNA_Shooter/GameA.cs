﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace IS_XNA_Shooter
{
    class GameA : Game
    {
        /* ------------------------------------------------------------- */
        /*                           ATRIBUTOS                           */
        /* ------------------------------------------------------------- */
        private Sprite aimPointSprite;
        private BackgroundGameA backGround;

        /* ------------------------------------------------------------- */
        /*                          CONSTRUCTOR                          */
        /* ------------------------------------------------------------- */
        public GameA(int num, Texture2D textureAim, Texture2D textureBg,
            float playerVelocity)
            : base(playerVelocity)
        {
            hub = new IngameHubA(GRMng.hubLeft, GRMng.hubCenter, GRMng.hubRight);
            level = new LevelA(camera, num, enemies);
            backGround = new BackgroundGameA(camera, level);
            
            camera.setLevel(level);

            Vector2[] points = new Vector2[8];
            points[0] = new Vector2(15, 35);
            points[1] = new Vector2(26, 33);
            points[2] = new Vector2(34, 15);
            points[3] = new Vector2(65, 30);
            points[4] = new Vector2(65, 50);
            points[5] = new Vector2(34, 66);
            points[6] = new Vector2(26, 47);
            points[7] = new Vector2(15, 45);
            ship = new ShipA(camera, level, Vector2.Zero, 0, points, GRMng.frameWidthPA2, GRMng.frameHeightPA2,
                GRMng.numAnimsPA2, GRMng.frameCountPA2, GRMng.loopingPA2, SuperGame.frameTime24, GRMng.texturePA2,
                playerVelocity, shots);

            level.setShip(ship);

            aimPointSprite = new Sprite(true, Vector2.Zero, 0, textureAim);

            camera.setShip(ship);
        }

        /* ------------------------------------------------------------- */
        /*                            MÉTODOS                            */
        /* ------------------------------------------------------------- */
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // actualizamos posicion del puntero:
            aimPointSprite.position.X = Mouse.GetState().X;
            aimPointSprite.position.Y = Mouse.GetState().Y;

//            camera.Update(deltaTime);   // cámara
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            backGround.Draw(spriteBatch);

            base.Draw(spriteBatch); // player, enemigos, balas
            
            aimPointSprite.Draw(spriteBatch); // punto de mira

            if (SuperGame.debug)
            {
                spriteBatch.DrawString(SuperGame.fontDebug, "player rot=" + player.rotation + ".",
                    new Vector2(5, 3), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                spriteBatch.DrawString(SuperGame.fontDebug, "Ship=" + ship.position + ".",
                   new Vector2(5, 15), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                
                /* 
                spriteBatch.DrawString(SuperGame.fontDebug, "Camera=" + camera.position + ".",
                    new Vector2(5, 3), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                spriteBatch.DrawString(SuperGame.fontDebug, "Player=" + player.position + ".",
                    new Vector2(5, 15), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                */
                /*if (enemies.Count > 0)
                {
                    spriteBatch.DrawString(Game1.fontDebug, "Enemy = " + enemies[0].position.ToString() + ".",
                        new Vector2(5, 27), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);

                    spriteBatch.DrawString(Game1.fontDebug, "EnemyCollider = " + enemies[0].getRectangle().ToString() + ".",
                        new Vector2(5, 39), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);

                    spriteBatch.DrawString(Game1.fontDebug, "EnemyCollider = " + enemies[0].getPosition().ToString() + ".",
                        new Vector2(5, 51), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                }*/
            }
        }

    } // class GameA
}
