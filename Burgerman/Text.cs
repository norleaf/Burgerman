using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Burgerman
{
    public class Text
    {
        public double TTL { get; set; }
        public string Message { get; set; }
        private Game1 game;
        public float x;
        public float y;

        public Text(string message, double ttl)
        {
            TTL = ttl;
            Message = message;
            game = Game1.Instance;
            x = game.ScreenSize.X*0.25f;
            y = game.ScreenSize.Y*0.2f;
        }

        public void Update(GameTime gameTime)
        {
            if (gameTime.TotalGameTime.TotalMilliseconds > TTL)
            {
                Message = "";
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(game.Font, Message, new Vector2(x, y), Color.Black);
        }
    }
}
