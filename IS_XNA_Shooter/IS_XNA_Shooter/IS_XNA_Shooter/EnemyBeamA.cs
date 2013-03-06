﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace IS_XNA_Shooter
{
    class EnemyBeamA : Enemy
    {
        /* ------------------- ATRIBUTOS ------------------- */
        float timeToBeam;
        float gyre;
        float dX;
        /* ------------------- CONSTRUCTORES ------------------- */
        public EnemyBeamA(Camera camera, Level level, Vector2 position, float rotation,
            short frameWidth, short frameHeight, short numAnim, short[] frameCount, bool[] looping,
            float frametime, Texture2D texture, float velocity, int life, Ship player,
            int shotPower, float shotVelocity, float timeToShot)
            : base (camera, level, position, rotation, frameWidth, frameHeight, numAnim, frameCount,
                looping, frametime, texture, velocity, life, player, shotPower, shotVelocity, timeToShot)
        {
            setAnim(1);
            timeToBeam = gyre = dX = 0;

            Vector2[] points = new Vector2[8];
            points[0] = new Vector2(21, 21);
            points[1] = new Vector2(32, 22);
            points[2] = new Vector2(49, 28);
            points[3] = new Vector2(57, 37);
            points[4] = new Vector2(57, 42);
            points[5] = new Vector2(49, 51);
            points[6] = new Vector2(32, 57);
            points[7] = new Vector2(21, 57);
            /*Vector2[] points = new Vector2[4];
            points[0] = new Vector2(20, 20);
            points[1] = new Vector2(60, 35);
            points[2] = new Vector2(60, 45);
            points[3] = new Vector2(20, 60);*/
            collider = new Collider(camera, true, position, rotation, points, frameWidth, frameHeight);
        }

        /* ------------------- MÉTODOS ------------------- */
        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            timeToBeam += deltaTime;            
          

            if(timeToBeam < 5){
                float dY = -player.position.Y + position.Y;
                dX = -player.position.X + position.X;
                gyre = (float)Math.Atan(dY / dX);
                if (dX < 0)
                {
                    rotation = gyre;
                    //position.X += (float)(velocity * Math.Cos(gyre) * deltaTime);
                    //position.Y += (float)(velocity * Math.Sin(gyre) * deltaTime);
                }
                else
                {
                    rotation = (float)Math.PI + gyre;
                    //position.X -= (float)(velocity * Math.Cos(gyre) * deltaTime);
                    //position.Y -= (float)(velocity * Math.Sin(gyre) * deltaTime);
                }
            }
            else{
                if (dX < 0)
                {
                    //rotation = gyre;
                    position.X += (float)(velocity * Math.Cos(gyre) * deltaTime);
                    position.Y += (float)(velocity * Math.Sin(gyre) * deltaTime);
                }
                else
                {
                    //rotation = (float)Math.PI + gyre;
                    position.X -= (float)(velocity * Math.Cos(gyre) * deltaTime);
                    position.Y -= (float)(velocity * Math.Sin(gyre) * deltaTime);
                }          
            }
        }
    }
}
