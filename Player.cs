using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace StreamGame
{
    public static class Player
    {
        private static float _x = -50; //X Position
        private static float _y = 570; //Y Position
        public static float x
        {
            get => _x;
        }
        public static float y
        {
            get => _y;
        }
        private static float _width = 30;
        private static float _height = 60;
        private static float _heightRatio;
        private static float _widthRatio;
        public static float width
        {
            get => _width;
        }
        public static float height
        {
            get => _height;  
        }
        public static float widthRatio
        {
            get => _widthRatio;  
        }
        public static float heightRatio
        {
            get => _heightRatio;
        }
        public static Texture2D sprite;

        public static int health = 1; //Player's Health
        public static int maxHealth = 1; //Change this * //Players Max Health
        public static int ardor = 0; //Amount of placeholder experience
        public static int balloons = 0; //Amount of placable double jumps
        public static int enemiesKilled = 0; //How many enemies the player has killed
        public static int timeTaken = 0; //How long the player has been playing
        public static float dashingSpeed = 9; //Change this * //Speed of a dash
        public static float walkingSpeed = 6; //Change this * //Walking Speed of the Player
        public static float jumpingSpeed = 14f; //Change this * //Jumping Speed of the Player
        public static Boolean colorFlip = false; //Whether the colors are inverted

        public static float timeInAir = 0;
        public static double maxGravitySpeed = 28;
        public static int timeOnGround = 0; //Change this * //Whether the Player has stayed a sufficient amount of time on the ground to jump 
        public static int dashingCooldown = 0; //Whether the player has spent enough time waiting to dash again
        public static int dCooldown = 20; //Change this * //Cooldown between each dash
        public static int dTime = 20; //Change this * //Time consumed during a dash
        public static int tOGPreJump = 5; //Change this * //Time needed to wait in order to jump again
        public static float xVelocity = 0; //Players X Velocity
        public static Boolean onGround = false; //Whether Player is On Ground
        public static float yVelocity = 0; //Players Y Velocity
        public enum DashDirection
        {
            Left,
            Right,
            Down,
            none
        }
        public static DashDirection dashDirection = DashDirection.none;


        public static Vector2 topRightHitbox = new Vector2(width, 0); //Hitbox Stuff
        public static Vector2 bottomRightHitbox = new Vector2(width, -height);
        public static Vector2 topLeftHitbox = new Vector2(0, 0);
        public static Vector2 bottomLeftHitbox = new Vector2(0, -height);

        public static void changeSizes(float w, float h)
        {
            _width = w;
            _height = h;
            _heightRatio = height / sprite.Height;
            _widthRatio = width / sprite.Width;
        }

        public static void changePosition(float xP, float yP)
        {
            _x = xP;
            _y = yP;
            for (int xC = 0; xC < (Chunk.chunkLoadDistance / Chunk.chunkLength * 2); xC++)
            {
                for (int yC = 0; yC < (Chunk.chunkLoadDistance / Chunk.chunkLength * 2); yC++)
                {
                    Chunk.attemptLoadChunk(xC, yC);
                }
            }

        }
        
        public void updatePlayer(){
                 MouseState mouseInput = Mouse.GetState();
                if (Keyboard.GetState().IsKeyDown(Keys.D) && Player.dashingCooldown - Player.dTime <= 0)
                {
                    Player.movePlayer(false);
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.A) && Player.dashingCooldown - Player.dTime <= 0)
                {
                    Player.movePlayer(true);
                }

                if (Keyboard.GetState().IsKeyDown(Keys.W))
                {
                    Player.attemptDash();
                }
                Player.commenceDash();
                Player.onGround = false;
                for (int i = 0; i < tiles.Count; i++)
                {
                    Tile t = tiles[i];
                    t.groundCheck();

                }


                if (!Player.onGround)
                {
                    Player.onInAir();

                }
                else
                {
                    Player.onTouchGround();
                }



                Player.dashingCooldown -= 1;
                Player.changePosition(Player.x + Player.xVelocity, Player.y - Player.timeInAir + Player.yVelocity);
                Player.xVelocity = Player.xVelocity / 1.5f;   
        }
        
        
        
        public static Boolean attemptDash()
        {
            if (Player.dashingCooldown <= 0)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.S))
                {
                    dashDirection = DashDirection.Down;
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.A))
                {
                    dashDirection = DashDirection.Left;
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.D))
                {
                    dashDirection = DashDirection.Right;
                }
                else
                {
                    dashDirection = DashDirection.none;
                }
                if (dashDirection != DashDirection.none)
                {
                    Player.dashingCooldown = Player.dTime + Player.dCooldown;
                    return true;
                }

            }
            return false;
        }

        public static void commenceDash()
        {
            if (Player.dashingCooldown - Player.dCooldown > 0)
            {
                if (dashDirection != DashDirection.none)
                {
                    Player.yVelocity = 0;
                    Player.xVelocity = 0;
                    if (dashDirection == DashDirection.Right)
                    {
                        Player.xVelocity = Player.dashingSpeed;
                    }
                    if (dashDirection == DashDirection.Left)
                    {
                        Player.xVelocity = -Player.dashingSpeed;
                    }
                    if (dashDirection == DashDirection.Down)
                    {
                        Player.yVelocity = -Player.dashingSpeed;
                    }
                    Player.timeInAir = 0;
                }



            }
        }
        
        public static void onTouchGround(){
            Player.yVelocity = 0;
            Player.timeInAir = 0;
            Player.timeOnGround += 1;
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                Player.attemptJump();
            }
        }
        
        public static void onInAir(){
            Player.timeOnGround = 0;
            if (Player.maxGravitySpeed > Player.timeInAir)
            {
                Player.timeInAir += 0.5f;
            }
        }
        
        public static void movePlayer(bool left){
            if(Player.dashingCooldown - Player.dTime <= 0)
            {
                if(left) {
                    Player.xVelocity = Player.walkingSpeed;
                } else {
                    Player.xVelocity = -Player.walkingSpeed;
                }
                
            }
        }
        


        public static Boolean attemptJump() //Space and Jump have already been confirmed
        {
            if (Player.timeOnGround >= Player.tOGPreJump)
            {
                Player.yVelocity = Player.jumpingSpeed;
                Player.timeOnGround = 0;
                Player.onGround = false;
                return true;
            }
            return false;
        }
    }
}
