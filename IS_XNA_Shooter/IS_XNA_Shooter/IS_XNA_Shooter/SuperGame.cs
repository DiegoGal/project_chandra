using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace IS_XNA_Shooter
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class SuperGame : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        // estado del juego
        public enum gameState
        {
            mainMenu,
            playing,
            pause,
            gameOver
        };

        /* ------------------------------------------------------------- */
        /*                           ATRIBUTOS                           */
        /* ------------------------------------------------------------- */
        public gameState currentState; // estado actual del juego

        public static bool debug = true;

        public static int screenWidth;  // ancho de la pantalla
        public static int screenHeight; // alto de la pantalla

        private GRMng grManager;        // gestor de recursos gr�ficos
        public ControlMng controlMng;   // gestor de controles
        private XMLLvlMng LvlMng;       // gestor de XML
        private Audio audio;            // gestor del Audio del juego

        public Vector2 pointer; // posicion del raton

        protected float totalTime; // contador del tiempo total

        private int     drawFramesCounter;
        private int     drawFramesCounterAux;
        private int     updateFramesCounter;
        private int     updateFramesCounterAux;
        private float   timeCounterSecond;
        private float   timeCounterSecondAux;

        // tiempo de duraci�n de un frame en una animaci�n:
        public static float frameTime24 =   ((float)1 / 24);
        public static float frameTime12 =   ((float)1 / 12);
        public static float frameTime8 =    ((float)1 / 8);

        public static float timeToResume = 2f; // t que tarda en volver despu�s de pause

        // objetos del juego:
        private Menu        menu;
        private MenuIngame  menuIngame;
        private MenuGameOver menuGameOver;
        private Game        game;
        public Player       player;
        private int         playerLifes = 4;

        public enum shootType
        {
            normal,
        };

        // fuentes:
        public static SpriteFont fontDebug; // courier new 12 regular

        /* ------------------------------------------------------------- */
        /*                          CONSTRUCTOR                          */
        /* ------------------------------------------------------------- */
        public SuperGame ()
        {
            graphics = new GraphicsDeviceManager(this);
            LvlMng = new XMLLvlMng();
            grManager = new GRMng(Content);
            controlMng = new ControlMng();
            audio = new Audio(Content);

            int resX = 1280, resY = 720;
            //int resX = 1366, resY = 768;
            //int resX = 1024, resY = 768;
            graphics.PreferredBackBufferWidth = resX;
            graphics.PreferredBackBufferHeight = resY;
            graphics.IsFullScreen = false;

            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            IsMouseVisible = true;

            drawFramesCounter = drawFramesCounterAux = 0;
            updateFramesCounter = updateFramesCounterAux = 0;
            timeCounterSecond = timeCounterSecondAux = 1;

            currentState = gameState.mainMenu; // ponemos el estado de juego a modo menu
            pointer = new Vector2();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Screen dimensions
            screenWidth = GraphicsDevice.Viewport.Width;
            screenHeight = GraphicsDevice.Viewport.Height;

            // TODO: use this.Content to load your game content here

            grManager.LoadContent(0); // se cargan los recursos del menu
            grManager.LoadContent(1); // se cargan los recursos del menu ingame
            audio.LoadContent(0);

            fontDebug = Content.Load<SpriteFont>("FontDebug");

            // Create the Menus
            menu = new Menu(this);
            menuIngame = new MenuIngame(this);
            menuGameOver = new MenuGameOver(this);

            // Create the player
            player = new Player(playerLifes);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            // tiempo que ha pasado desde la ultima vez que ejecutamos el metodo
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // contadores de frames:
            timeCounterSecondAux -= deltaTime;
            if (timeCounterSecondAux <= 0)
            {
                drawFramesCounter = drawFramesCounterAux;
                drawFramesCounterAux = 0;
                updateFramesCounter = updateFramesCounterAux;
                updateFramesCounterAux = 0;
                timeCounterSecondAux = timeCounterSecond;
            }
            updateFramesCounterAux++;

            if (Keyboard.GetState().IsKeyDown(Keys.F))
                debug = !debug;

            // posici�n actual del rat�n:
            pointer.X = Mouse.GetState().X;
            pointer.Y = Mouse.GetState().Y;

            // actualizamos el juego:
            switch (currentState)
            {
                case gameState.mainMenu:

                    if (debug && Keyboard.GetState().IsKeyDown(Keys.T))
                        NewGameATest();

                    menu.Update(Mouse.GetState().X, Mouse.GetState().Y);

                    if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                        menu.Click(Mouse.GetState().X, Mouse.GetState().Y);
                    else if (Mouse.GetState().LeftButton == ButtonState.Released)
                        menu.Unclick(Mouse.GetState().X, Mouse.GetState().Y);

                    break;

                case gameState.playing:

                    game.Update(gameTime);
                    totalTime += deltaTime;

                    if (Keyboard.GetState().IsKeyDown(Keys.P) ||
                        GamePad.GetState(PlayerIndex.One).Buttons.Start == ButtonState.Pressed)
                        currentState = gameState.pause;

                    break;

                case gameState.pause:

                    menuIngame.Update(deltaTime, Mouse.GetState().X, Mouse.GetState().Y);

                    if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                        menuIngame.Click(Mouse.GetState().X, Mouse.GetState().Y);
                    else if (Mouse.GetState().LeftButton == ButtonState.Released)
                        menuIngame.Unclick(Mouse.GetState().X, Mouse.GetState().Y);

                    break;

                case gameState.gameOver:

                    menuGameOver.Update(Mouse.GetState().X, Mouse.GetState().Y);
                    if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                        menuGameOver.Click(Mouse.GetState().X, Mouse.GetState().Y);
                    else if (Mouse.GetState().LeftButton == ButtonState.Released)
                        menuGameOver.Unclick(Mouse.GetState().X, Mouse.GetState().Y);

                    break;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();

            drawFramesCounterAux++;

            switch (currentState)
            {
                case gameState.mainMenu:
                    menu.Draw(spriteBatch);
                    break;

                case gameState.playing:
                    game.Draw(spriteBatch);
                    break;

                case gameState.pause:
                    game.Draw(spriteBatch);
                    menuIngame.Draw(spriteBatch);
                    break;

                case gameState.gameOver:
                    menuGameOver.Draw(spriteBatch);
                    break;
            }

            // fps:
            /*if (debug)
                 spriteBatch.DrawString(SuperGame.fontDebug,
                     "FPS=" + (float)1 / gameTime.ElapsedGameTime.Milliseconds * 1000 + ".",
                     new Vector2(screenWidth-100, 3), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);*/
            if (debug)
            {
                spriteBatch.DrawString(SuperGame.fontDebug, "Draw FPS=" + drawFramesCounter + ".",
                    new Vector2(screenWidth - 150, 3), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                spriteBatch.DrawString(SuperGame.fontDebug, "Update FPS=" + updateFramesCounter + ".",
                    new Vector2(screenWidth - 150, 15), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        /* ------------------------------------------------------------- */
        /*                            M�TODOS                            */
        /* ------------------------------------------------------------- */
        private void NewGameATest()
        {
            grManager.LoadContent(2); // cargamos los recursos del nivel 1 de GameA
            audio.LoadContent(1);
            game = new GameA(this, player, 0, GRMng.textureAim, GRMng.textureCell,
                /*ShipVelocity*/200f, /*ShipLife*/100);
            currentState = gameState.playing; // cambiamos el estado del juego a modo juego
            grManager.UnloadContent(0); // descargamos los recursos del men�
        }

        public void NewStory()
        {
            grManager.LoadContent(3); // cargamos los recursos del nivel 1 de GameB
            grManager.LoadContent(2); // cargamos los recursos del nivel 1 de GameA Pa que funcione
            audio.LoadContent(1);
            LvlMng.LoadContent(1); // cargamos los rectangulos
            LvlMng.LoadContent(0); // cargamos enemigos del levelA

            game = new GameStory(this, player,1, GRMng.textureAim,
                /*ShipVelocity*/200f, /*ShipLife*/100);
           
            currentState = gameState.playing; // cambiamos el estado del juego a modo juego

            //grManager.UnloadContent(2); 
            //grManager.UnloadContent(3); // descargamos los recursos del men�
        }

        public void newSurvival()
        {
            grManager.LoadContent(2); // cargamos los recursos del nivel 1 de GameA
            audio.LoadContent(1);
            LvlMng.LoadContent(0); // cargamos los XML


            game = new GameA(this, player, 1, GRMng.textureAim, GRMng.textureCell,
                /*ShipVelocity*/200f, /*ShipLife*/100);

            currentState = gameState.playing; // cambiamos el estado del juego a modo juego

            LvlMng.UnloadContent(0);
            grManager.UnloadContent(0); // descargamos los recursos del men�
        }

        public void newKiller()
        {
            grManager.LoadContent(2); // cargamos los recursos del nivel 1 de GameA
            audio.LoadContent(1);
            LvlMng.LoadContent(0); // cargamos los XML

            game = new GameA(this, player, 1, GRMng.textureAim, GRMng.textureCell,
                /*ShipVelocity*/200f, /*ShipLife*/100);

            currentState = gameState.playing; // cambiamos el estado del juego a modo juego

            LvlMng.UnloadContent(0);
            grManager.UnloadContent(0); // descargamos los recursos del men�

        }

        public void newDefense()
        {
            grManager.LoadContent(2); // cargamos los recursos del nivel 1 de GameA
            audio.LoadContent(1);
            LvlMng.LoadContent(0);

            game = new GameA(this, player, 1, GRMng.textureAim, GRMng.textureCell,
                /*ShipVelocity*/200f, /*ShipLife*/100);

            currentState = gameState.playing; // cambiamos el estado del juego a modo juego

            LvlMng.UnloadContent(0);
            grManager.UnloadContent(0); // descargamos los recursos del men�
        }

        public void newScroll()
        {
            grManager.LoadContent(3); // cargamos los recursos del nivel 1 de GameB
            grManager.LoadContent(2); // cargamos los recursos del nivel 1 de GameA Pa que funcione
            audio.LoadContent(1);
            LvlMng.LoadContent(1); // cargamos los rectangulos

            game = new GameB(this, player, 1, GRMng.textureAim,
                /*ShipVelocity*/200f, /*ShipLife*/100);

            currentState = gameState.playing; // cambiamos el estado del juego a modo juego

            LvlMng.UnloadContent(1);
            //grManager.UnloadContent(2);
            //grManager.UnloadContent(3); // descargamos los recursos del men�
        }

        public void Resume()
        {
            currentState = gameState.playing;
        }

        public void ExitToMenu()
        {
            grManager.LoadContent(0);
            currentState = gameState.mainMenu;
            menu.menuState = Menu.MenuState.main;
            grManager.UnloadContentGame();
            audio.UnloadContent(1);
        }

        public void GameOver()
        {
            currentState = gameState.gameOver;
        }

    } // class SuperGame

} // namespace IS_XNA_Shooter
