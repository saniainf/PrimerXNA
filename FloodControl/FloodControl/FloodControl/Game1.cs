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

namespace FloodControl
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D playingPieces;
        Texture2D backgroundScreen;
        Texture2D titleScreen;

        GameBoard gameBoard;

        Vector2 gameBoardDisplayOrigin = new Vector2(70, 89);

        int playerScore = 0;

        enum GameStates { TitleScreen, Playing };
        GameStates gameState = GameStates.TitleScreen;

        Rectangle EmptyPiece = new Rectangle(1, 247, 40, 40);

        const float MinTimeSinceLastInput = 0.25f;
        float timeSinceLastInput = 0.0f;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        #region base metods

        protected override void Initialize()
        {
            this.IsMouseVisible = true;
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
            graphics.ApplyChanges();
            gameBoard = new GameBoard();

            base.Initialize();
        }


        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            playingPieces = Content.Load<Texture2D>(@"Textures\Tile_Sheet");
            backgroundScreen = Content.Load<Texture2D>(@"Textures\Background");
            titleScreen = Content.Load<Texture2D>(@"Textures\TitleScreen");
        }


        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }


        protected override void Update(GameTime gameTime)
        {
            switch (gameState)
            {
                case GameStates.TitleScreen:
                    if (Keyboard.GetState().IsKeyDown(Keys.Space))
                    {
                        gameBoard.ClearBoard();
                        gameBoard.GenerateNewPiece(false);
                        playerScore = 0;
                        gameState = GameStates.Playing;
                    }
                    break;

                case GameStates.Playing:
                    timeSinceLastInput +=
                        (float)gameTime.ElapsedGameTime.TotalSeconds;

                    if (timeSinceLastInput >= MinTimeSinceLastInput)
                    {
                        HandleMouseInput(Mouse.GetState());
                    }

                    gameBoard.ResetWater();

                    for (int y = 0; y < GameBoard.GameBoardHeight; y++)
                    {
                        CheckScoringChain(gameBoard.GetWaterChain(y));
                    }

                    gameBoard.GenerateNewPiece(true);

                    break;
            }

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            if (gameState == GameStates.TitleScreen)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(titleScreen,
                    new Rectangle(0, 0,
                        this.Window.ClientBounds.Width,
                        this.Window.ClientBounds.Height),
                    Color.White);
                spriteBatch.End();
            }

            if (gameState == GameStates.Playing)
            {
                spriteBatch.Begin();

                spriteBatch.Draw(backgroundScreen, new Rectangle(0, 0,
                        this.Window.ClientBounds.Width,
                        this.Window.ClientBounds.Height),
                        Color.White);

                for (int x = 0; x < GameBoard.GameBoardWidth; x++)
                    for (int y = 0; y < GameBoard.GameBoardHeight; y++)
                    {
                        int pixelX = (int)gameBoardDisplayOrigin.X +
                            (x * GamePiece.PieceWidth);
                        int pixelY = (int)gameBoardDisplayOrigin.Y +
                            (y * GamePiece.PieceHeight);

                        spriteBatch.Draw(playingPieces,
                            new Rectangle(pixelX, pixelY,
                                GamePiece.PieceWidth, GamePiece.PieceHeight),
                                EmptyPiece,
                            Color.White);

                        spriteBatch.Draw(playingPieces,
                            new Rectangle(pixelX, pixelY,
                                GamePiece.PieceWidth, GamePiece.PieceHeight),
                                gameBoard.GetSourceRect(x, y),
                            Color.White);
                    }

                this.Window.Title = playerScore.ToString();

                spriteBatch.End();
            }
            base.Draw(gameTime);
        }

        #endregion

        private int DetermineScore(int SquareCount) // подсчет очков
        {
            return (int)((Math.Pow((SquareCount / 5), 2) + SquareCount) * 10);
        }

        private void CheckScoringChain(List<Vector2> WaterChain) // вычесление длинны трубы
        {
            if (WaterChain.Count > 0)
            {
                Vector2 LastPipe = WaterChain[WaterChain.Count - 1];
                if (LastPipe.X == GameBoard.GameBoardWidth - 1)
                {
                    if (gameBoard.HasConnector(
                        (int)LastPipe.X, (int)LastPipe.Y, "Right"))
                    {
                        playerScore += DetermineScore(WaterChain.Count);

                        foreach (Vector2 ScoringSquare in WaterChain)
                        {
                            gameBoard.SetSquare((int)ScoringSquare.X, (int)ScoringSquare.Y, "Empty");
                        }
                    }
                }
            }
        }

        private void HandleMouseInput(MouseState mouseState)
        {
            int x = ((mouseState.X -
                (int)gameBoardDisplayOrigin.X) / GamePiece.PieceWidth);

            int y = ((mouseState.Y -
                (int)gameBoardDisplayOrigin.Y) / GamePiece.PieceHeight);

            if ((x >= 0) && (x < GameBoard.GameBoardWidth) &&
              (y >= 0) && (y < GameBoard.GameBoardHeight))
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    gameBoard.RotatePiece(x, y, false);
                    timeSinceLastInput = 0.0f;
                }

                if (mouseState.RightButton == ButtonState.Pressed)
                {
                    gameBoard.RotatePiece(x, y, true);
                    timeSinceLastInput = 0.0f;
                }
            }
        }

    }
}
