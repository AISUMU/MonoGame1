using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame1
{
    public class Bullet : Sprite
    {
        private int bulletWidth;
        private int bulletHeight;

        public Bullet (int type) : base (0)
        {
            switch (type)
            {
                case 0:
                    {
                        bulletWidth = 6;
                        bulletHeight = 14;
                        ActionInfo mainAction = new ActionInfo
                        {
                            frames = new FrameInfo[1],
                            Loop = false,
                        };
                        mainAction.frames[0] = new FrameInfo
                        {
                            frameRectangle = new Rectangle(7, 0, bulletWidth, bulletHeight),
                            colliderWidth = bulletWidth,
                            colliderHeight = bulletHeight,
                            spriteEffects = SpriteEffects.None,
                        };
                        base.actions = new ActionInfo[1];
                        base.actions[0] = mainAction;
                    }
                    break;

                case 1:
                    {
                        bulletWidth = 10;
                        bulletHeight = 14;
                        ActionInfo mainAction = new ActionInfo
                        {
                            frames = new FrameInfo[1],
                            Loop = false,
                        };
                        mainAction.frames[0] = new FrameInfo
                        {
                            frameRectangle = new Rectangle(19, 0, bulletWidth, bulletHeight),
                            colliderWidth = bulletWidth,
                            colliderHeight = bulletHeight,
                            spriteEffects = SpriteEffects.None,
                        };
                        base.actions = new ActionInfo[1];
                        base.actions[0] = mainAction;
                    }
                    break;

                case 2:
                    {
                        bulletWidth = 19;
                        bulletHeight = 14;
                        int offsetX = 32;
                        ActionInfo mainAction = new ActionInfo
                        {
                            frames = new FrameInfo[8],
                            Loop = true
                        };
                        for (int i = 0; i < 8; i++)
                        {
                            mainAction.frames[i] = new FrameInfo
                            {
                                frameRectangle = new Rectangle(offsetX, 0, bulletWidth, bulletHeight),
                                colliderWidth = bulletWidth,
                                colliderHeight = bulletHeight,
                                spriteEffects = SpriteEffects.None
                            };
                            offsetX += bulletWidth;
                        }

                        base.actions = new ActionInfo[1];
                        base.actions[0] = mainAction;
                    }
                    break;
            }

            base.spriteTexture = Game1.spriteTexture;
        }

        public override void Update (GameTime gameTime)
        {
            base.Update(gameTime);

            if (
                    positionX + bulletWidth / 2 < 0 ||
                    positionX - bulletWidth / 2 > Game1.WIDTH_screen ||
                    positionY + bulletHeight / 2 < 0 ||
                    positionY - bulletHeight / 2 > Game1.HEIGHT_screen
                )
                Dead = true;
        }

        public int GetBulletWidth ()
        {
            return bulletWidth;
        }

        public int GetBulletHeight ()
        {
            return bulletHeight;
        }

        public override void Hit()
        {
            Dead = true;
        }
    }
}
