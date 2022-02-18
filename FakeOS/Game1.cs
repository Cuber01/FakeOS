using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ImGuiNET;


namespace FakeOS
{
    public class Game1 : Game
    {
        
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private ImGuiRenderer GuiRenderer;
    
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        
        protected override void Initialize()
        {
            base.Initialize();

            IsMouseVisible = true;
            
            graphics.PreferredBackBufferHeight = 1000;
            graphics.PreferredBackBufferWidth= 1000;
            graphics.ApplyChanges();

            GuiRenderer = new ImGuiRenderer(this);
            GuiRenderer.RebuildFontAtlas();
            
            GraphicsDevice.SamplerStates[0] = SamplerState.PointClamp;
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }
        
        protected override void Update(GameTime gameTime)
        { 
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.Coral);

            //var oldSamplerState = GraphicsDevice.SamplerStates[0];

            GuiRenderer.BeforeLayout(gameTime);
            ImGui.LabelText("Hello World", "");

            GuiRenderer.AfterLayout();
            
            //GraphicsDevice.SamplerStates[0] = oldSamplerState;

            base.Draw(gameTime);
        }
    }
}