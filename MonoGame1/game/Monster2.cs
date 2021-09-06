using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame1
{
    public class Monster2 : Sprite
    {
        private int step;

        public Monster2() : base(0) 
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

            UseDefaultExplosion();
        }

        public override void Update (GameTime gameTime)
        {
            if (step == 0)
            {
                if (positionX < Game1.WIDTH_screen / 2)
                {
                    SetVelocity(3, 4);
                }
                else
                {
                    SetVelocity(-3, 4);
                }
                step++;
            }

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
