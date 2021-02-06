using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace Void_Wanderer.Collisions
{
    /// <summary>
    /// This class was modified from the CollisionExample tutorial in CIS 580
    /// </summary>
    public static class CollisionHelper
    {
        /// <summary>
        /// Detects a collision between two circles
        /// </summary>
        /// <param name="c1">the first circle</param>
        /// <param name="c2">the second circle</param>
        /// <returns>true for a collision, false otherwise</returns>
        public static bool Collides(BoundingCircle c1, BoundingCircle c2)
        {
            return Math.Pow(c1.Radius + c2.Radius, 2) >= Math.Pow(c2.Center.X - c1.Center.X, 2) + Math.Pow(c2.Center.Y - c1.Center.Y, 2);
        }
        /// <summary>
        /// Detects a collision between two rectangles
        /// </summary>
        /// <param name="r1">The first rectangle</param>
        /// <param name="r2">The second rectangle</param>
        /// <returns>true on collision, false otherwise</returns>
        public static bool Collides(BoundingRectangle r1, BoundingRectangle r2)
        {
            return !(r1.X + r1.Width < r2.X    // r1 is to the left of r2
                    || r1.X > r2.X + r2.Width     // r1 is to the right of r2
                    || r1.Y + r1.Height < r2.Y    // r1 is above r2 
                    || r1.Y > r2.Y + r2.Height); // r1 is below r2
        }
        /// <summary>
        /// Detects a collision between two points
        /// </summary>
        /// <param name="p1">the first point</param>
        /// <param name="p2">the second point</param>
        /// <returns>true when colliding, false otherwise</returns>
        public static bool Collides(BoundingPoint p1, BoundingPoint p2)
        {
            return p1.X == p2.X && p1.Y == p2.Y;
        }
        /// <summary>
        /// Detects a collision between a circle and point
        /// </summary>
        /// <param name="c">the circle</param>
        /// <param name="p">the point</param>
        /// <returns>true on collision, false otherwise</returns>
        public static bool Collides(BoundingCircle c, BoundingPoint p)
        {
            return Math.Pow(c.Radius, 2) >= Math.Pow(c.Center.X - p.X, 2) + Math.Pow(c.Center.Y - p.Y, 2);
        }
        /// <summary>
        /// Detects a collision between a rectangle and a point
        /// </summary>
        /// <param name="r">The rectangle</param>
        /// <param name="p">The point</param>
        /// <returns>true on collision, false otherwise</returns>
        public static bool Collides(BoundingRectangle r, BoundingPoint p)
        {
            return p.X >= r.X && p.X <= r.X + r.Width && p.Y >= r.Y && p.Y <= r.Y + r.Height;
        }
        /// <summary>
        /// Determines if there is a collision between a circle and rectangle
        /// </summary>
        /// <param name="r">The bounding rectangle</param>
        /// <param name="c">The bounding circle</param>
        /// <returns>true for collision, false otherwise</returns>
        public static bool Collides(BoundingRectangle r, BoundingCircle c)
        {

            float nX = MathHelper.Clamp(c.Center.X, r.Left, r.Right);
            float nY = MathHelper.Clamp(c.Center.Y, r.Top, r.Bottom);
            return Math.Pow(c.Radius, 2) >= Math.Pow(c.Center.X - nX, 2) + Math.Pow(c.Center.Y - nY, 2);
        }
        /// <summary>
        /// Alternate call
        /// </summary>
        /// <param name="c"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public static bool Collides(BoundingCircle c, BoundingRectangle r) => Collides(r, c);
    }
}