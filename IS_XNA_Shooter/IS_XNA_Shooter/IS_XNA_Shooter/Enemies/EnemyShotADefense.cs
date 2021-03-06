﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace IS_XNA_Shooter
{
    class EnemyShotADefense : EnemyADefense
    {
        /// <summary>
        /// Shots' list
        /// </summary>
        protected List<Shot> shots;

        /// <summary>
        /// Time between two different shots
        /// </summary>
        protected float timeToShot;

        /// <summary>
        /// Time between two different shots (aux)
        /// </summary>
        protected float timeToShotAux;

        /// <summary>
        /// Shot velocity
        /// </summary>
        protected float shotVelocity;

        /// <summary>
        /// Shot power
        /// </summary>
        protected int shotPower;

        /// <summary>
        /// EnemyShotADefense's constructor
        /// </summary>
        /// <param name="camera">The camera of the game</param>
        /// <param name="level">The level of the game</param>
        /// <param name="position">The position of the enemy</param>
        /// <param name="rotation">The rotation of the enemy</param>
        /// <param name="frameWidth">The width of each frame of the enemy's animation</param>
        /// <param name="frameHeight">The height of each frame of the enemy's animation </param>
        /// <param name="numAnim">The number of the enemy's animations</param>
        /// <param name="frameCount">The number of the frames in each animation's fil</param>
        /// <param name="looping">Indicates if the animation has to loop</param>
        /// <param name="frametime">Indicates how long the frame lasts</param>
        /// <param name="texture">The texture of the enemy</param>
        /// <param name="timeToSpawn">The time that the enemy has to wait for appear in the game</param>
        /// <param name="velocity">The velocity of the enemy</param>
        /// <param name="life">The life of the enemy</param>
        /// <param name="value">The points you obtain if you kill it</param>
        /// <param name="ship">The player's ship</param>
        /// <param name="timeToShot">Value representing the firing rate</param>
        /// <param name="shotVelocity">value representing the velocity of the bullets</param>
        /// <param name="shotPower">value representing the bullets power</param>
        /// <param name="house">The player's house</param>
        public EnemyShotADefense(Camera camera, Level level, Vector2 position, float rotation,
            short frameWidth, short frameHeight, short numAnim, short[] frameCount, bool[] looping,
            float frametime, Texture2D texture, float timeToSpawn, float velocity, int life,
            int value, Ship ship,float timeToShot, float shotVelocity, int shotPower, Base house)
            : base (camera, level, position, rotation,
                frameWidth, frameHeight, numAnim, frameCount, looping, frametime, texture,
                timeToSpawn, velocity, life, value, ship, house)
        {
            target = random.Next(-1, 2);
            this.house = house;

            this.timeToShot = timeToShot;
            this.shotVelocity = shotVelocity;
            this.shotPower = shotPower;

            timeToShotAux = timeToShot;
            shots = new List<Shot>();
        }

        /// <summary>
        /// EnemyShotADefense's constructor
        /// </summary>
        /// <param name="camera">The camera of the game</param>
        /// <param name="level">The level of the game</param>
        /// <param name="position">The position of the enemy</param>
        /// <param name="rotation">The rotation of the enemy</param>
        /// <param name="frameWidth">The width of each frame of the enemy's animation</param>
        /// <param name="frameHeight">The height of each frame of the enemy's animation </param>
        /// <param name="numAnim">The number of the enemy's animations</param>
        /// <param name="frameCount">The number of the frames in each animation's fil</param>
        /// <param name="looping">Indicates if the animation has to loop</param>
        /// <param name="frametime">Indicates how long the frame lasts</param>
        /// <param name="texture">The texture of the enemy</param>
        /// <param name="timeToSpawn">The time that the enemy has to wait for appear in the game</param>
        /// <param name="velocity">The velocity of the enemy</param>
        /// <param name="life">The life of the enemy</param>
        /// <param name="value">The points you obtain if you kill it</param>
        /// <param name="ship">The player's ship</param>
        /// <param name="timeToShot">Value representing the firing rate</param>
        /// <param name="shotVelocity">value representing the velocity of the bullets</param>
        /// <param name="shotPower">value representing the bullets power</param>
        public EnemyShotADefense(Camera camera, Level level, Vector2 position, float rotation,
            short frameWidth, short frameHeight, short numAnim, short[] frameCount, bool[] looping,
            float frametime, Texture2D texture, float timeToSpawn, float velocity, int life,
            int value, Ship ship, float timeToShot, float shotVelocity, int shotPower)
            : base(camera, level, position, rotation,
                frameWidth, frameHeight, numAnim, frameCount, looping, frametime, texture,
                timeToSpawn, velocity, life, value, ship)
        {
            target = random.Next(-1, 2);

            this.timeToShot = timeToShot;
            this.shotVelocity = shotVelocity;
            this.shotPower = shotPower;

            timeToShotAux = timeToShot;
            shots = new List<Shot>();
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            // shots:
            for (int i = 0; i < shots.Count(); i++)
            {
                shots[i].Update(deltaTime);
                if (!shots[i].IsActive())
                    shots.RemoveAt(i);
                else  // shots-player colisions
                {
                    if (ship.collider.Collision(shots[i].position))
                    {
                        // the player is hit:
                        ship.Damage(shots[i].GetPower());

                        // the shot must be erased only if it hasn't provoked the
                        // player ship death, otherwise the shot will had be removed
                        // before from the game in: Game.PlayerDead() -> Enemy.Kill()
                        if (ship.GetLife() > 0)
                            shots.RemoveAt(i);
                    }
                    else if (house.collider.Collision(shots[i].position)) 
                    {
                        // We are in Defense mode, check the house also
                        int damage = 0;

                        damage = shots[i].GetPower();
                        shots.RemoveAt(i);

                        // the house is hit:
                        house.Damage(damage);
                    }
                }
            }
        } // Update

        /// <summary>
        /// Draws the shots 
        /// </summary>
        /// <param name="spriteBatch">The screen's canvas</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (Shot s in shots)
                s.Draw(spriteBatch);

            base.Draw(spriteBatch);
        } // Draw

        /// <summary>
        /// Kills the enemy and its shots
        /// </summary>
        public override void Kill()
        {
            base.Kill();

            shots.Clear();
        }

        /// <summary>
        /// The dead condition of enemies with shots is when its death animation has ended
        /// and the shots shoted when it was alive are no longer active
        /// </summary>
        /// <returns>dead condition</returns>
        public override bool DeadCondition()
        {
            // the dead condition of this enemy is when its death animation has ended
            // and the shots shoted when it was alive are no longer active
            return (!animActive && (shots.Count() == 0));
        }
    
    } // class EnemyShotDefense
}
