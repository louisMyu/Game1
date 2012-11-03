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
using Squaritory.Shapes;
using System.Timers;

namespace Squaritory
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Matrix world = Matrix.CreateTranslation(0, 0, 0);
        Matrix view;
        Matrix projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45), 800f / 480f, 0.01f, 100f);
        Camera m_Camera;
        List<Tile> m_TileList;
        List<Unit> m_UnitList;
        int m_BoardHeight = 10;
        int m_BoardWidth = 10;
        TimeSpan m_UnitsSpawnedTime;
        TimeSpan m_UnitsMovedTime;
        Random m_Rand;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferMultiSampling = true;
            Content.RootDirectory = "Content";      
        }

        void GenerateUnits()
        {
            foreach(Tile tile in m_TileList)
            {
                switch (tile.UnitType)
                {
                    case Tile.TileType.Warrior:
                        m_UnitList.Add(tile.UnitSpawnPoint.SpawnWarrior());
                        break;
                    case Tile.TileType.Mage:
                        m_UnitList.Add(tile.UnitSpawnPoint.SpawnMage());
                        break;
                    case Tile.TileType.Archer:
                        m_UnitList.Add(tile.UnitSpawnPoint.SpawnArcher());
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            m_TileList = new List<Tile>();
            m_UnitList = new List<Unit>();
            m_UnitsSpawnedTime = TimeSpan.Zero;
            m_UnitsMovedTime = TimeSpan.Zero;
            m_Rand = new Random();

            m_Camera = new Camera();
            m_Camera.Move(new Vector3(10.0f, 10.0f, 5.0f));
            view = m_Camera.View;    
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
            Random rand = new Random();

            float xCoord = 0.0f;
            float yCoord = 0.0f;
            for (int y = 0; y < m_BoardHeight; y++)
            {
                for (int x = 0; x < m_BoardWidth; x++)
                {
                    Tile.TileType unitType = Tile.TileType.Empty;
                    Color color = Color.Gray;
                    switch(rand.Next(0,4))
                    {
                        case 0:
                            color = Color.Red;
                            unitType = Tile.TileType.Warrior;
                            break;
                        case 1:
                            color = Color.Blue;
                            unitType = Tile.TileType.Mage;
                            break;
                        case 2:
                            color = Color.Green;
                            unitType = Tile.TileType.Archer;
                            break;
                        default:
                            color = Color.Gray;
                            unitType = Tile.TileType.Empty;
                            break;
                    }
                    Tile tile = new Tile(color);
                    Square square = new Square(graphics.GraphicsDevice, new Vector3(0, 0, 0));
                    float xOffset = 0.0f;
                    float yOffset = 0.0f;                    

                    xCoord = (x * 2) - xOffset;
                    yCoord = y * (2 - yOffset); // + 0.05f);

                    tile.UnitType = unitType;
                    tile.XCoord = xCoord;
                    tile.YCoord = yCoord;
                    tile.TileShape = square;
                    tile.TileShape.SetStartingLocation(new Vector3(xCoord, yCoord, 0));
                    tile.UnitSpawnPoint = new SpawnPoint(new Vector3(tile.XCoord, tile.YCoord, 0.0f), GraphicsDevice);
                    
                    m_TileList.Add(tile);

                }
            }
            //Circle circle = new Circle(GraphicsDevice, new Vector3(0.0f, 0.0f, 1.0f), 0.05f);
            //Tile circleTile = new Tile(Color.CornflowerBlue);
            //circleTile.XCoord = -3.0f;
            //circleTile.YCoord = 0.0f;
            //circleTile.TileShape = circle;
            //circleTile.TileShape.SetStartingLocation(new Vector3(circleTile.XCoord, circleTile.YCoord, 0));
            //m_TileList.Add(circleTile);

            // TODO: use this.Content to load your game content here

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
            //if(gameTime.ElapsedGameTime - m_UnitsSpawnedTime > 5)
            TimeSpan timeSinceLastUnitSpawn = gameTime.TotalGameTime.Subtract(m_UnitsSpawnedTime);
            TimeSpan timeSinceLastUnitsMoved = gameTime.TotalGameTime.Subtract(m_UnitsMovedTime);
            if (timeSinceLastUnitSpawn.Seconds >= 10)
            {
                m_UnitsSpawnedTime = gameTime.TotalGameTime;
                if (m_UnitList.Count <= 1500)
                {
                    GenerateUnits();
                }
            }

            if (timeSinceLastUnitsMoved.Milliseconds >= 200)
            {
                m_UnitsMovedTime = gameTime.TotalGameTime;
                foreach (Unit unit in m_UnitList)
                {
                    unit.MoveRandom(m_Rand);
                }
            }


            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                view = m_Camera.Move(new Vector3(-0.05f, 0, 0));
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                view = m_Camera.Move(new Vector3(0.05f, 0, 0));
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                view = m_Camera.Move(new Vector3(0, 0.05f, 0));
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                view = m_Camera.Move(new Vector3(0, -0.05f, 0));
            }
            if (Keyboard.GetState().IsKeyDown(Keys.PageUp))
            {
                view = m_Camera.Move(new Vector3(0, 0, 0.05f));
            }
            if (Keyboard.GetState().IsKeyDown(Keys.PageDown))
            {
                view = m_Camera.Move(new Vector3(0, 0, -0.05f));
            }

            

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            RestoreRenderState();
            // TODO: Add your drawing code here
            foreach (Tile tile in m_TileList)
            {
                tile.TileShape.Draw(GraphicsDevice, view, projection, tile.TileColor);
            }

            foreach (Unit unit in m_UnitList)
            {
                unit.DrawUnit(GraphicsDevice, view, projection);
            }

            base.Draw(gameTime);
        }

        private void RestoreRenderState()
        {
            GraphicsDevice.BlendState = BlendState.Opaque;
            GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            GraphicsDevice.SamplerStates[0] = SamplerState.LinearWrap;
        }
    }
}
