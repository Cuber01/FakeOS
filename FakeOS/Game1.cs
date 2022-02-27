using System.Collections.Generic;
using FakeOS.General;
using FakeOS.Gui;
using FakeOS.Software;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ImGuiNET;


namespace FakeOS
{
    public class Game1 : Game
    {
        public static List<GuiSoftware> windows = new List<GuiSoftware>();
        
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private ImGuiRenderer guiRenderer;
        private StyleManager styleManager;
        private FontLoader fontLoader;
        
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
            graphics.PreferredBackBufferWidth = 1000;
            graphics.ApplyChanges();

            guiRenderer = new ImGuiRenderer(this);
            styleManager = new StyleManager(Consts.themeLocation, ImGui.GetStyle());
            fontLoader = new FontLoader(Consts.fontsLocation, ImGui.GetIO());

           // fontLoader.loadIconFont(Consts.fontAwesomeLocation, Consts.defaultFontSize, (AwesomeIcons.IconMin, AwesomeIcons.IconMax));
            
            guiRenderer.RebuildFontAtlas();

            styleManager.setTheme("yetAnotherDark");

            windows.Add(new FileManager("./Filesystem/sys/fonts/"));

        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }
        
        protected override void Update(GameTime gameTime)
        {
            
            for (int i = 0; i < windows.Count; i++)
            {
                if (!windows[i].running)
                {
                    windows.Remove(windows[i]);
                    continue;
                }
                
                windows[i].update();
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