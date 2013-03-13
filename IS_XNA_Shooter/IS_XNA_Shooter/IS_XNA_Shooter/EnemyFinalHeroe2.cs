﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace IS_XNA_Shooter
{
    //Class for the enemy that shoots one laser
    class EnemyFinalHeroe2 : Enemy
    {
        /* ------------------- ATTRIBUTES ------------------- */
        private Matrix rotationMatrix;
        bool shooting;
        bool down;

        //For the Laser
        private Rectangle rect;
        private Vector2 p1, p2;
        private Vector2 p1Orig, p2Orig;

        private float shotVelocity = 80f;
        private float timeToShot = 3.0f;
        private float timeShoting = 0.5f;
        private int shotPower = 1;
        private Shot shot;

        /* ------------------- CONSTRUCTORS ------------------- */
        public EnemyFinalHeroe2(Camera camera, Level level, Vector2 position, float rotation,
            short frameWidth, short frameHeight, short numAnim, short[] frameCount, bool[] looping,
            float frametime, Texture2D texture, float timeToSpawn, float velocity, int life,
            int value, Ship ship, bool down)
            : base(camera, level, position, rotation, frameWidth, frameHeight, numAnim, frameCount,
                looping, frametime, texture, timeToSpawn, 50, 100, value, ship)
        {
           // setAnim(3);

          /*  Vector2[] points = new Vector2[6];
            points[0] = new Vector2(20, 20);
            points[1] = new Vector2(40, 13);
            points[2] = new Vector2(60, 20);
            points[3] = new Vector2(60, 60);
            points[4] = new Vector2(40, 65);
            points[5] = new Vector2(20, 60);
            collider = new Collider(camera, true, position, rotation, points, frameWidth, frameHeight);*/
            this.down = down;
            //For the Laser
            //Rectangle rect = new Rectangle(0, 0, 2000, 2);
       /*     p1 = new Vector2();
            p2 = new Vector2();
            p1Orig = new Vector2(0, 0);
            p2Orig = new Vector2(320, 0);*/

           
            
        }

        /* ------------------- METHODS ------------------- */
        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            if (life > 0)
            {
              timeToShot -= deltaTime;
                if (timeToShot <= 0)
                {

                    if (timeShoting > 0)
                    {
                        shooting = true;
                        LaserShot();
                        shot.Update(deltaTime);
                        timeShoting -= deltaTime;
                        
                    }
                    else {
                        shooting = false;
                        timeShoting = 0.5f;
                        timeToShot = 3.0f;
                        
                    }
                    
               
                    /*if (shooting)
                        shots.Add(shot);
                    else shots.Remove(shot);*/
                }

                if (down)
                    position.Y += deltaTime * velocity;
                else
                    position.Y -= deltaTime * velocity;
                changeDirection();

                if (position.X < SuperGame.screenWidth*3 / 4) { position.X += deltaTime * velocity*3; }
                else if (position.X > SuperGame.screenWidth*3 /4) { position.X -= deltaTime * velocity*3; }
            }

        } // Update

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (shooting)
                shot.Draw(spriteBatch);

            base.Draw(spriteBatch);
        }

        //The enemy Shoot a Laser, we calculate previously the rectangle of it
        private void LaserShot() 
        { 
            //The calculation of the rectangle
           // rotationMatrix = Matrix.CreateRotationZ(rotation);
          //  int width = level.width + 800;

            //p1 = Vector2.Transform(p1Orig, rotationMatrix);
          /*  p1 = p1Orig;
            p1 += position;

           //p2 = Vector2.Transform(p2Orig, rotationMatrix);
            p2 = p2Orig;
            p2 += position;

            rect.X = (int)position.X;
            rect.Y = (int)position.Y;*/

           // shot.position = position;

            Vector2 positionShot = new Vector2(position.X - 1040, position.Y);
            shot = new Shot(camera, level, positionShot,0, GRMng.frameWidthELBulletHeroe, GRMng.frameHeightELBulletHeroe,
                GRMng.numAnimsELBulletHeroe, GRMng.frameCountELBulletHeroe, GRMng.loopingELBulletHeroe, SuperGame.frameTime8,
                GRMng.textureELBulletHeroe, SuperGame.shootType.normal, shotVelocity, shotPower);
         //   shot.rotation = rotation;

        }

        public override void Damage(int i)
        {
            base.Damage(i);

            // if the enemy is dead, play the new animation and the death sound
            if (life <= 0)
            {
                shooting = false;
                setAnim(5, -1);
                Audio.PlayEffect("brokenBone02");
            }
        }

        private void changeDirection()
        {
            if (position.Y + this.frameHeight / 2 >= SuperGame.screenHeight)
                down = false;
            else if (position.Y - this.frameHeight / 2 <= 0)
                down = true;
        }

    } // Class EnemyMineShot
}
