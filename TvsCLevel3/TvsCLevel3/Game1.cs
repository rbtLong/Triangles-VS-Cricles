using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TvsCLevel3
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        // Set the 3D model to draw.
        Model myModel;

        // The aspect ratio determines how to scale 3d to 2d projection.
        float aspectRatio;

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            myModel = Content.Load<Model>("Models\\p1_wedge");
            aspectRatio = (float)graphics.GraphicsDevice.Viewport.Width /
            (float)graphics.GraphicsDevice.Viewport.Height;
        }

        protected override void UnloadContent()
        {

        }
       
        protected override void Update(GameTime gameTime)
        {
            movement();
            base.Update(gameTime);
        }

            private void movement()
            {
                if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Left)
                    && modelPosition.X > -2200)
                    modelPosition.X -= 55;
                if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Right)
                    && modelPosition.X < 2200)
                    modelPosition.X += 55;

                if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Up)
                    && modelPosition.Y < 1400)
                    modelPosition.Y += 35;
                if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Down)
                    && modelPosition.Y > -1400)
                    modelPosition.Y -= 35;
            }


        // Set the position of the model in world space, and set the rotation.
        Vector3 modelPosition = Vector3.Zero;
        float _modelHorizontalRotation = 0.0f;

        // Set the position of the camera in world space, for our view matrix.
        Vector3 cameraPosition = new Vector3(0.0f, 50.0f, 5000.0f);

        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.CornflowerBlue);

            // Copy any parent transforms.
            Matrix[] transforms = new Matrix[myModel.Bones.Count];
            myModel.CopyAbsoluteBoneTransformsTo(transforms);

            // Draw the model. A model can have multiple meshes, so loop.
            foreach (ModelMesh mesh in myModel.Meshes)
            {
                // This is where the mesh orientation is set, as well as our camera and projection.
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.World = transforms[mesh.ParentBone.Index] * Matrix.CreateRotationY(_modelHorizontalRotation)
                        * Matrix.CreateTranslation(modelPosition);
                    effect.View = Matrix.CreateLookAt(cameraPosition, Vector3.Zero, Vector3.Up);
                    effect.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45.0f),
                        aspectRatio, 1.0f, 10000.0f);
                }
                // Draw the mesh, using the effects set above.
                mesh.Draw();
            }
                base.Draw(gameTime);
        }
    }
}
