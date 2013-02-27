﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Xml;

namespace IS_XNA_Shooter
{
    class Level
    {
        protected Camera camera;
        protected Player player;
        protected Vector2 playerInitPosition;
        public int width;
        public int height;
        protected List<Enemy> enemies;
        protected List<float> timeLeftEnemy;

        /* ------------------- CONSTRUCTORES ------------------- */
        public Level() 
        { 
         //Lo ponemos para que compile LevelB ya que no tiene camara y demas atributos.
        }

        public Level(Camera camera, int num, List<Enemy> enemies)
        {
            //this.camera = camera;
/*
            switch (num)
            {
                case 1:
                    width = 1200;
                    height = 800;
                    this.enemies = enemies;
                    timeLeftEnemy = new List<float>();

                    /*Enemy enemy1 = new EnemyWeak(camera, this, new Vector2(100, 100), 0,
                        GRMng.frameWidthEW, GRMng.frameHeightEW, GRMng.numAnimsEW, GRMng.frameCountEW,
                        GRMng.loopingEW, SuperGame.frameTime12, GRMng.textureEW, 80, 100, null);
                    enemies.Add(enemy1);
                    timeLeftEnemy.Add(2);*/
            /*
                    Enemy enemy;
                    for (int i = 0; i < 40; i++)
                    {
                        enemy = new EnemyWeak(camera, this, new Vector2(100, 100), 0,
                            GRMng.frameWidthEW, GRMng.frameHeightEW, GRMng.numAnimsEW, GRMng.frameCountEW,
                            GRMng.loopingEW, SuperGame.frameTime12, GRMng.textureEW, 80+i, 100, null);
                        enemies.Add(enemy);
                        timeLeftEnemy.Add(1+i);
                    }
                    
                    break;
            }
            */
           // whitePixel = GRMng.whitepixel;
           // textureBg = GRMng.textureCell;
        }

        /* ------------------- MÉTODOS ------------------- */
        public virtual void Update(float deltaTime)
        {
            for (int i = 0; i < timeLeftEnemy.Count(); i++)
            {
                timeLeftEnemy[i] -= deltaTime;
                if (!enemies[i].isActive() && timeLeftEnemy[i] <= 0 && !enemies[i].isDead())
                    enemies[i].setActive();
            }
        }
        
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            /*
            // grid del suelo
            for (int i = 0; i < width; i += textureBg.Width)
                for (int j = 0; j < height; j += textureBg.Height)
                    spriteBatch.Draw(textureBg, new Vector2(i + camera.displacement.X, j + camera.displacement.Y), Color.White);


            // linea de arriba:
            //spriteBatch.Draw(whitePixel, new Rectangle(0, 0, width, 1), null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0);
            spriteBatch.Draw(whitePixel, new Rectangle((int)camera.displacement.X, (int)camera.displacement.Y, width, 1),
                null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0);
            // linea de la derecha:
            //spriteBatch.Draw(whitePixel, new Rectangle(width, 0, 1, height), null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0);
            spriteBatch.Draw(whitePixel, new Rectangle((int)camera.displacement.X, (int)camera.displacement.Y, 1, height),
                null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0);
            // linea de abajo:
            //spriteBatch.Draw(whitePixel, new Rectangle(0, height, width, 1), null, Color.White, w0, Vector2.Zero, SpriteEffects.None, 0);
            spriteBatch.Draw(whitePixel, new Rectangle((int)camera.displacement.X + width, (int)camera.displacement.Y, 1, height),
                null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0);
            // linea de la izquierda:
            //spriteBatch.Draw(whitePixel, new Rectangle(0, 0, 1, height), null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0);
            spriteBatch.Draw(whitePixel, new Rectangle((int)camera.displacement.X, (int)camera.displacement.Y + height, width, 1),
                null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0);
        */
        }

 	protected void LeerArchivoXML(int modoDeJuego, int levelModo)
        {
            // Utilizar nombres de fichero y nodos XML idénticos a los que se guardaron

            try
            {
                //  Leer los datos del archivo
                String enemyType;
                float positionX;
                float positionY;
                //int positionX1;
                //int positionY1;
                //int positionX2;
                //int positionY2;
                float time;
                XmlDocument lvl = XMLLvlMng.lvl1;
                //var lasNoticias = lvl.getElementsByTagName("enemyList");
                //XElement enemyList = lvl.Root.Element("enemyList");
                XmlNodeList level = lvl.GetElementsByTagName("level");

                XmlNodeList lista =
                        ((XmlElement)level[0]).GetElementsByTagName("enemy");
                foreach (XmlElement nodo in lista)
                {

                    XmlAttributeCollection enemyN = nodo.Attributes;
                    //XmlAttribute a = enemyN[1];
                    enemyType = Convert.ToString(enemyN[0].Value);
                    positionX = (float)Convert.ToDouble(enemyN[1].Value);
                    positionY = (float)Convert.ToDouble(enemyN[2].Value);
                    time = (float)Convert.ToDouble(enemyN[3].Value); //aqui casca
                    timeLeftEnemy.Add(time);

                    Enemy enemy = null;

                    if (enemyType.Equals("enemyWeak"))
                        enemy = new EnemyWeak(camera, this, new Vector2(positionX, positionY), 0,
                            GRMng.frameWidthEW2, GRMng.frameHeightEW2, GRMng.numAnimsEW2, GRMng.frameCountEW2,
                            GRMng.loopingEW2, SuperGame.frameTime12, GRMng.textureEW2, 100, 100, null);
                    else
                        enemy = new EnemyBeamA(camera, this, new Vector2(positionX, positionY), 0,
                            GRMng.frameWidthEW2, GRMng.frameHeightEW2, GRMng.numAnimsEW2, GRMng.frameCountEW2,
                            GRMng.loopingEW2, SuperGame.frameTime12, GRMng.textureEW2, 1000, 100, null);
                    enemies.Add(enemy);
                    
                }

                /*XmlNodeList listaRectangles =
                        ((XmlElement)level[0]).GetElementsByTagName("rectangle");
                foreach (XmlElement nodo in listaRectangles)
                {

                    XmlAttributeCollection RectangleN = nodo.Attributes;


                    positionX1 = (int)Convert.ToDouble(RectangleN[0].Value);
                    positionY1 = (int)Convert.ToDouble(RectangleN[1].Value);
                    positionX2 = (int)Convert.ToDouble(RectangleN[2].Value);
                    positionY2 = (int)Convert.ToDouble(RectangleN[3].Value);

                    Rectangle rectangulo = new Rectangle(positionX1, positionY1, positionX2, positionY2);
                    //rectangles.Add(rectangulo);


                }*/

            }
            catch (Exception e)
            {/*
                        System.Diagnostics.Debug.WriteLine("Excepción al leer fichero XML: " + e.Message);
                        if (e.Data != null)
                        {
                            System.Diagnostics.Debug.WriteLine("    Detalles extras:");
                            foreach (DictionaryEntry entrada in e.Data)
                                Console.WriteLine("        La clave es '{0}' y el valor es: {1}", entrada.Key, entrada.Value);
                        }*/
            }
        }   //  end LeerArchivoXML()

        public virtual void setPlayer(Player player)
        {
            this.player = player;
            foreach (Enemy e in enemies)// enemigos
                e.setPlayer(player);
            player.SetPosition(playerInitPosition);
        }


        public XMLLvlMng LvlMng { get; set; }
    } // class Level
}