﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace IS_XNA_Shooter
{
    class Shot : Animation
    {
        /* ------------------- ATRIBUTOS ------------------- */
        public SuperGame.shootType type;
        private float velocity;
        private int power;
        private bool active;

        public Collider collider;

        /* ------------------- CONSTRUCTORES ------------------- */
        public Shot(Camera camera, Level level, Vector2 position, float rotation,
            short frameWidth, short frameHeight, short numAnim, short[] frameCount, bool[] looping,
            float frametime, Texture2D texture, SuperGame.shootType type, float velocity, int power)
            : base(camera, level, true, position, rotation, texture, frameWidth, frameHeight, numAnim,
                frameCount, looping, frametime) // constructora Animation
        {
            this.type = type;
            this.velocity = velocity;
            this.power = power;
            active = true;

            collider = new Collider(camera, true, position, rotation, frameWidth, frameHeight);
        }

        /* ------------------- MÉTODOS ------------------- */
        public void setCollider(Vector2[] points, float radius)
        {
            collider = new Collider(camera, true, position, rotation, points, radius, frameWidth, frameHeight);
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            position.X += (float)Math.Cos(rotation) * velocity * deltaTime;
            position.Y += (float)Math.Sin(rotation) * velocity * deltaTime;

            if (position.X < -200 || position.X > level.width + 200 ||
                position.Y < -200 || position.Y > level.height + 200)
                active = false;

            collider.Update(position, rotation);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            if (SuperGame.debug)
                collider.Draw(spriteBatch);
        }

        public int GetPower()
        {
            return power;
        }

        public bool IsActive()
        {
            return active;
        }

    } // class Shoot
}
