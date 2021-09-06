using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame1
{
    public abstract class Sprite
    {
        protected Texture2D spriteTexture;
        protected Texture2D explosionTexture;

        protected ActionInfo[] actions;
        protected ActionInfo explosionAction;

        protected bool Destroy;
        protected bool Dead;

        protected int velocityX;
        protected int velocityY;

        protected int positionX;
        protected int positionY;

        protected Rectangle destinationRectangle;
        protected Rectangle collider;
        protected Vector2 drawOrigin;

        protected int currentActionId;
        protected int newActionId;
        protected int currentFrameId;
        protected bool NewAction;

        protected ActionInfo currentAction;
        protected FrameInfo currentFrame;

        protected bool isScreenBoundary;

        protected bool isDrawCollider;

        protected float animationDelay = 70;
        protected float animationDelayCount;

        protected float invincibleCounter;
        protected bool isInvincible;
        protected float blinkCounter;
        protected bool isBlink;
        protected bool isDraw;
        protected float BLINK_DELAY = 20;
        protected float blinkDelayCounter;

        public Sprite (int firstAction)
        {
            currentActionId = firstAction;
            currentFrameId = 0;
            NewAction = true;
            Destroy = false;
            Dead = false;
            isScreenBoundary = false;

            destinationRectangle = new Rectangle(0, 0, 0, 0);
            collider = new Rectangle(0, 0, 0, 0);
            drawOrigin = new Vector2(0, 0);

            animationDelayCount = animationDelay;

            invincibleCounter = 0;
            isInvincible = false;
            blinkCounter = 0;
            isBlink = false;
            isDraw = true;
        }

        public void SetInvincible (float invincibleCounter)
        {
            this.invincibleCounter = invincibleCounter;
            if (invincibleCounter > 0) isInvincible = true;
            else isInvincible = false;
        }

        public bool IsInvincible()
        {
            return isInvincible;
        }

        public void SetBlink (float blinkCounter)
        {
            this.blinkCounter = blinkCounter;
            if (blinkCounter > 0) { 
                isBlink = true;
                blinkDelayCounter = 0;
            }
            else isBlink = false;
        }

        public void UseDefaultExplosion ()
        {
            int size = 256 / 4;
            int offsetX = 0;
            int offsetY = 0;

            explosionAction = new ActionInfo
            {
                frames = new FrameInfo[16],
                Loop = false,
            };
            for (int i = 0; i < 16; i++)
            {
                offsetX = i % 4;
                if (offsetX == 0 && i > 0) offsetY++;

                FrameInfo f = new FrameInfo
                {
                    frameRectangle = new Rectangle(offsetX * size, offsetY * size, size, size),
                    colliderWidth = 0,
                    colliderHeight = 0,
                    spriteEffects = SpriteEffects.None,
                };

                explosionAction.frames[i] = f;
            }
        }

        public void SetAction (int newActionId)
        {
            if (newActionId != currentActionId)
            {
                this.newActionId = newActionId;
                NewAction = true;
            }
        }

        public void SetVelocity (int velocityX, int velocityY)
        {
            this.velocityX = velocityX;
            this.velocityY = velocityY;
        }

        public void SetPosition (int positionX, int positionY)
        {
            this.positionX = positionX;
            this.positionY = positionY;
        }

        public Vector2 GetPosition ()
        {
            return new Vector2(positionX, positionY);
        }

        public Rectangle GetCollider ()
        {
            return collider;
        }

        public bool IsDestroy ()
        {
            return Destroy;
        }

        public bool IsDead ()
        {
            return Dead;
        }

        public void SetScreenBoundary (bool isScreenBoundary)
        {
            this.isScreenBoundary = isScreenBoundary;
        }

        public void SetDrawCollider (bool isDrawCollider)
        {
            this.isDrawCollider = isDrawCollider;
        }

        public virtual void Update (GameTime gameTime)
        {
            if (Dead) return;

            if (invincibleCounter > 0)
            {
                invincibleCounter -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (invincibleCounter <= 0)
                {
                    isInvincible = false;
                }
            }

            if (isBlink)
            {
                if (blinkDelayCounter <= 0)
                {
                    isDraw = !isDraw;
                    blinkDelayCounter = BLINK_DELAY;
                }
                else blinkDelayCounter -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;

                if (blinkCounter <= 0)
                {
                    isBlink = false;
                    isDraw = true;
                }
                else blinkCounter -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (NewAction)
            {
                if (Destroy)
                {
                    currentAction = explosionAction;
                    animationDelayCount = 0;
                }
                else
                {
                    currentActionId = newActionId;
                    currentAction = actions[currentActionId];
                    animationDelayCount = animationDelay;
                }

                currentFrameId = 0;
                NewAction = false;
            }
            else
            {
                if (animationDelayCount > 0)
                {
                    animationDelayCount -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                }
                else
                {
                    bool isAnimationEnd = false;

                    if (currentFrameId >= currentAction.frames.Length - 1)
                    {
                        if (currentAction.Loop) currentFrameId = 0;
                        else isAnimationEnd = true;
                    }
                    else currentFrameId++;

                    if (isAnimationEnd && Destroy) Dead = true;

                    if (!Destroy) animationDelayCount = animationDelay;
                }
            }

            currentFrame = currentAction.frames[currentFrameId];

            drawOrigin.X = currentFrame.frameRectangle.Width / 2;
            drawOrigin.Y = currentFrame.frameRectangle.Height / 2;

            if (!Destroy && !Dead) _MoveObject();

            _UpdateRectangles();
        }

        private void _MoveObject ()
        {
            positionX += velocityX;
            positionY += velocityY;

            if (isScreenBoundary) _AdjustPosition();
        }

        private void _AdjustPosition ()
        {
            if (positionX - (currentFrame.frameRectangle.Width / 2) < 0) positionX = currentFrame.frameRectangle.Width / 2;
            else if (positionX + (currentFrame.frameRectangle.Width / 2) > Game1.WIDTH_screen) positionX = Game1.WIDTH_screen - (currentFrame.frameRectangle.Width / 2);

            if (positionY - (currentFrame.frameRectangle.Height / 2) < 0) positionY = currentFrame.frameRectangle.Height / 2;
            else if (positionY + (currentFrame.frameRectangle.Height / 2) > Game1.HEIGHT_screen) positionY = Game1.HEIGHT_screen - (currentFrame.frameRectangle.Height / 2);
        }

        private void _UpdateRectangles ()
        {
            destinationRectangle.X = positionX;
            destinationRectangle.Y = positionY;
            destinationRectangle.Width = currentFrame.frameRectangle.Width;
            destinationRectangle.Height = currentFrame.frameRectangle.Height;

            collider.X = positionX - (currentFrame.colliderWidth / 2);
            collider.Y = positionY - (currentFrame.colliderHeight / 2);
            collider.Width = currentFrame.colliderWidth;
            collider.Height = currentFrame.colliderHeight;
        }

        public void Draw (SpriteBatch _spriteBatch)
        {
            if (Dead || !isDraw) return;

            Texture2D drawTexture;

            if (Destroy) drawTexture = explosionTexture;
            else drawTexture = spriteTexture;

            _spriteBatch.Draw(drawTexture, destinationRectangle, currentFrame.frameRectangle, Color.White, 0.0f, drawOrigin, currentFrame.spriteEffects, 0f);

            /*if (isDrawCollider)
            {
                _spriteBatch.Draw(Game1.colliderTexture, collider, Color.White);
            }*/
        }

        public abstract void Hit();
    }
}
