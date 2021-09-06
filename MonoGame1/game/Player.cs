using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGame1
{
    public class Player : Sprite
    {
        public const int idle = 0;
        public const int move_left = 1;
        public const int move_right = 2;

        public const int SPEED = 3;

        public const float bullet_delay = 150;
        private float bulletDelayCount;

        private bool canControl;

        public Player () : base (idle)
        {
            int offsetY = 0;
            int width = 32;
            int height = 32;

            ActionInfo idleAction = new ActionInfo
            {
                frames = new FrameInfo[2],
                Loop = true,
            };
            idleAction.frames[0] = new FrameInfo
            {
                frameRectangle = new Rectangle(0, offsetY, width, height),
                colliderWidth = width,
                colliderHeight = height,
                spriteEffects = SpriteEffects.None,
            };
            idleAction.frames[1] = new FrameInfo
            {
                frameRectangle = new Rectangle(width, offsetY, width, height),
                colliderWidth = width,
                colliderHeight = height,
                spriteEffects = SpriteEffects.None
            };

            ActionInfo leftAction = new ActionInfo
            {
                frames = new FrameInfo[2],
                Loop = true
            };
            leftAction.frames[0] = new FrameInfo
            {
                frameRectangle = new Rectangle(2 * width, offsetY, width, height),
                colliderWidth = width - 4,
                colliderHeight = height,
                spriteEffects = SpriteEffects.None
            };
            leftAction.frames[1] = new FrameInfo
            {
                frameRectangle = new Rectangle(3 * width, offsetY, width, height),
                colliderWidth = width - 4,
                colliderHeight = height,
                spriteEffects = SpriteEffects.None
            };

            ActionInfo rightAction = new ActionInfo
            {
                frames = new FrameInfo[2],
                Loop = true
            };
            rightAction.frames[0] = new FrameInfo
            {
                frameRectangle = new Rectangle(2 * width, offsetY, width, height),
                colliderWidth = width - 4,
                colliderHeight = height,
                spriteEffects = SpriteEffects.FlipHorizontally
            };
            rightAction.frames[1] = new FrameInfo
            {
                frameRectangle = new Rectangle(3 * width, offsetY, width, height),
                colliderWidth = width - 4,
                colliderHeight = height,
                spriteEffects = SpriteEffects.FlipHorizontally
            };

            base.actions = new ActionInfo[3];
            base.actions[idle] = idleAction;
            base.actions[move_left] = leftAction;
            base.actions[move_right] = rightAction;
 
            base.spriteTexture = Game1.spriteTexture;
            //base.explosionTexture = Game1.explosionTexture;

            bulletDelayCount = 0;
            canControl = false;

            UseDefaultExplosion();
        }

        public bool CanControl ()
        {
            return canControl;
        }

        public void SetControl (bool canControl)
        {
            this.canControl = canControl;
        }

        public override void Update (GameTime gameTime)
        {
            if (!Destroy && !Dead)
            {
                if (bulletDelayCount > 0)
                {
                    bulletDelayCount -= (float) gameTime.ElapsedGameTime.TotalMilliseconds;
                }

                if (canControl)
                {
                    KeyboardState KeyboardState = Keyboard.GetState();

                    int moveSpeedX = 0;
                    int moveSpeedY = 0;

                    if (KeyboardState.IsKeyDown(Keys.Left))
                    {
                        velocityX = -SPEED;
                    }
                    if (KeyboardState.IsKeyDown(Keys.Right))
                    {
                        velocityX = SPEED;
                    }
                 
                    if (KeyboardState.IsKeyDown(Keys.Space) && bulletDelayCount <= 0)
                    {
                        Bullet b = new Bullet(2);
                        int bulletPositionX = positionX;
                        int bulletPositionY = positionY - (currentFrame.frameRectangle.Height / 2) - (b.GetBulletHeight() / 2);
                        b.SetPosition(bulletPositionX, bulletPositionY);
                        b.SetVelocity(0, -6);
                        Game1.data.AddPlayerBullet(b);

                       // Game1.fireFx.Play();

                        bulletDelayCount = bullet_delay;
                    }

                    SetVelocity(moveSpeedX, moveSpeedY);
                }

            }

            if (velocityX < 0) SetAction(move_left);
            else if (velocityX > 0) SetAction(move_right);
            else SetAction(idle);

            base.Update(gameTime);
        }

        public void Reset ()
        {
            Destroy = false;
            Dead = false;

            newActionId = idle;
            NewAction = true;
        }

        public override void Hit()
        {
            Destroy = true;
            NewAction = true;

            //Game1.explosionFx.Play();
        }
    }
}
