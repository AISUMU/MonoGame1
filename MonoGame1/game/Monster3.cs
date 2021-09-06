using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame1
{
    public class Monster3 : Sprite
    {
        private int step;

        public Monster3() : base(0) 
        {
            int width = 24;
            int height = 20;

            ActionInfo mainAction = new ActionInfo
            {
                frames = new FrameInfo[1],
                Loop = false
            };
            mainAction.frames[0] = new FrameInfo
            {
                frameRectangle = new Rectangle(62, 78, width, height),
                colliderHeight = height,
                colliderWidth = width,
                spriteEffects = SpriteEffects.FlipVertically
            };

            base.actions = new ActionInfo[1];
            base.actions[0] = mainAction;

            base.spriteTexture = Game1.spriteTexture;
            //base.explosionTexture = Game1.explosionTexture;

            SetVelocity(0, 4);

            UseDefaultExplosion();
        }

        public override void Update (GameTime gameTime)
        {
            base.Update(gameTime);

            if (positionY - currentFrame.frameRectangle.Height / 2 > Game1.HEIGHT_screen) Dead = true;            
        }

        public override void Hit()
        {
            Destroy = true;
            NewAction = true;

           // Game1.explosionFx.Play();
        }
    }
}
