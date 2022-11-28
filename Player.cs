using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace StreamGame
{
    public static class Player
    {
        public static float x = -50; //X Position
        public static float y = 570; //Y Position
        public static float width = 30; //Change this * //Sprite width and height of the Player (use in hitbox collision and sprite rendering)
        public static float height = 60; //Change this * //Sprite width and height of the Player (use in hitbox collision and sprite rendering)
        public static Texture2D sprite;
        public static float widthRatio;
        public static float heightRatio;

        public static int health = 1; //Player's Health
        public static int maxHealth = 1; //Change this * //Players Max Health
        public static int ardor = 0; //Amount of placeholder experience
        public static int balloons = 0; //Amount of placable double jumps
        public static int enemiesKilled = 0; //How many enemies the player has killed
        public static int timeTaken = 0; //How long the player has been playing
        public static float dashingSpeed = 20; //Change this * //Speed of a dash
        public static float walkingSpeed = 13; //Change this * //Walking Speed of the Player
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

        public static Vector2 topRightHitbox = new Vector2(width, 0); //Hitbox Stuff
        public static Vector2 bottomRightHitbox = new Vector2(width, -height);
        public static Vector2 topLeftHitbox = new Vector2(0, 0);
        public static Vector2 bottomLeftHitbox = new Vector2(0, -height);

        public static void changeSizes(float w, float h)
        { 
            width = w;
            height = h;
            heightRatio = height / sprite.Height;
            widthRatio = width / sprite.Width;
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
