using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame1
{
    public class Monster1 : Sprite
    {
        private int step;

        private const float bulletDelay = 250;
        private float bulletDelayCounter;

        public Monster1 () : base (0)
        {
            int width = 48;
            int height = 32;

            ActionInfo mainAction = new ActionInfo
            {
                frames = new FrameInfo[1],
                Loop = false,
            };
            mainAction.frames[0] = new FrameInfo
            {
                frameRectangle = new Rectangle(0, 72, width, height),
                colliderWidth = width,
                colliderHeight = height,
                spriteEffects = SpriteEffects.FlipVertically
            };

            base.actions = new ActionInfo[1];
            base.actions[0] = mainAction;

            base.spriteTexture = Game1.spriteTexture;
            //base.explosionTexture = Game1.explosionTexture;

            step = 0;
            bulletDelayCounter = 0;

            UseDefaultExplosion();
        }

        public override void Update (GameTime gameTime)
        {
            base.Update(gameTime);

            switch (step)
            {
                case 0:
                    {
                        if (positionY - currentFrame.frameRectangle.Height / 2 >= 20)
                        {
                            SetVelocity(2, 0);
                            step++;
                        }
                        else SetVelocity(0, 4);
                    }
                    break;

                case 1:
                    {
                        if (positionX + currentFrame.frameRectangle.Width / 2 >= Game1.WIDTH_screen)
                        {
                            SetVelocity(-2, 0);
                        }
                        else if (positionX - currentFrame.frameRectangle.Width / 2 <= 0)
                        {
                            SetVelocity(2, 0);
                        }
                        if (bulletDelayCounter <= 0)
                        {
                            Bullet b = new Bullet(1);
                            int bulletPositionX = positionX;
                            int bulletPositionY = positionY + (currentFrame.frameRectangle.Height / 2) + (b.GetBulletHeight() / 2);
                            b.SetPosition(bulletPositionX, bulletPositionY);
                            b.SetVelocity(0, 6);
                            Game1.data.AddEnemyBullet(b);

                            bulletDelayCounter = bulletDelay;

                            //Game1.fireFx.Play();
                        }
                        else bulletDelayCounter -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                    }
                    break;
            }
        }

        public override void Hit()
        {
            Destroy = true;
            NewAction = true;

            //Game1.explosionFx.Play();
        }
    }
}
