﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace IS_XNA_Shooter
{
    // clase para manejar el menú de game over del juego
    public class MenuStart
    {
        /* ------------------- ATTRIBUTES ------------------- */
        private SuperGame mainGame;
        private int horizontalSep; // separación horizontal de las opciones
        private Vector2 mainMenuPosition; // posicion de la opcion "main menu"

        private Texture2D splash;

        private MenuItem itemMainMenu, itemQuit;

        /* ------------------- CONSTRUCTORS ------------------- */
        public MenuStart(SuperGame mainGame)
        {
            this.mainGame = mainGame;

            horizontalSep = 48;
            mainMenuPosition = new Vector2(SuperGame.screenWidth / 2, (SuperGame.screenHeight / 4) * 3);

            splash = GRMng.startSplash;

            itemMainMenu = new MenuItem(true, mainMenuPosition, GRMng.menuGameOver,
                new Rectangle(0, 0, 512, 40), new Rectangle(0, 40, 512, 40),
                new Rectangle(0, 80, 512, 40));
            itemQuit = new MenuItem(false, new Vector2(SuperGame.screenWidth - 45, 5), GRMng.menuGameOver,
                new Rectangle(0, 120, 40, 40), new Rectangle(40, 120, 40, 40),
                new Rectangle(80, 120, 40, 40));
        }

        /* ------------------- MÉTODOS ------------------- */
        public void Update(int X, int Y)
        {
            itemMainMenu.Update(X, Y);
            itemQuit.Update(X, Y);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(splash, Vector2.Zero, Color.White);
            itemMainMenu.Draw(spriteBatch);
            itemQuit.Draw(spriteBatch);
        }

        // comprueba si se ha seleccionas alguna opcion
        public void Click(int X, int Y)
        {
            itemMainMenu.Click(X, Y);
            itemQuit.Click(X, Y);
        }

        // comprueba si se ha "soltado" la selección
        public void Unclick(int X, int Y)
        {
            if (itemMainMenu.Unclick(X, Y))
            {
                Audio.PlayEffect("digitalAcent01");
                mainGame.EnterToMenu();
            }
            else if (itemQuit.Unclick(X, Y))
            {
                Audio.PlayEffect("digitalAcent01");
                mainGame.Exit();
            }
        }

    } // class Menu
}
