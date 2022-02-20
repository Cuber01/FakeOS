using System.Collections.Generic;
using FakeOS.Gui;
using FakeOS.Software;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ImGuiNET;


namespace FakeOS
{
    public class Game1 : Game
    {
        private List<GuiSoftware> windows = new List<GuiSoftware>();

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private ImGuiRenderer guiRenderer;
        private ImGuiHelper guiHelper;
    
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

            guiRenderer = new ImGuiRenderer(this);
            guiHelper = new ImGuiHelper(guiRenderer, ImGui.GetIO());

           // guiHelper.loadFont("/home/cubeq/RiderProjects/FakeOS/FakeOS/Filesystem/sys/fonts/Roboto-Regular.ttf", 18);
            //ImGui.GetIO().Fonts.AddFontDefault();
           // ImGui.GetIO().Fonts.Build();

            guiRenderer.RebuildFontAtlas();

            StyleChooser.darkTheme();
            
            windows.Add(new TextEditor());

        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }
        
        protected override void Update(GameTime gameTime)
        { 
            foreach (var window in windows)
            {
                window.update();
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.Gray);

            guiRenderer.BeforeLayout(gameTime);

            foreach (var window in windows)
            {
                window.draw();
            }
            
            ImGui.ShowDemoWindow();

            guiRenderer.AfterLayout();
            

            base.Draw(gameTime);
        }
    }
}