using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace Void_Wanderer.Collisions
{
    /// <summary>
    /// This class was modified from the CollisionExample tutorial in CIS 580
    /// </summary>
    public struct BoundingRectangle
    {
        /// <summary>
        /// X location
        /// </summary>
        public float X;
        /// <summary>
        /// Y location
        /// </summary>
        public float Y;
        /// <summary>
        /// Width
        /// </summary>
        public float Width;

        /// <summary>
        /// Height
        /// </summary>
        public float Height;
        /// <summary>
        /// Left side
        /// </summary>
        public float Left => X;
        /// <summary>
        /// Top side
        /// </summary>
        public float Top => Y;
        /// <summary>
        /// Right side
        /// </summary>
        public float Right => (X + Width);
        /// <summary>
        /// Bottom side
        /// </summary>
        public float Bottom => (Y + Height);
        /// <summary>
        /// Constructor 1
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="w"></param>
        /// <param name="h"></param>
        public BoundingRectangle(float x, float y, float w, float h)
        {
            X = x;
            Y = y;
            Width = w;
            Height = h;

        }
        /// <summary>
        /// Constructor 2
        /// </summary>
        /// <param name="position"></param>
        /// <param name="w"></param>
        /// <param name="h"></param>
        public BoundingRectangle(Vector2 position, float w, float h)
        {
            X = position.X;
            Y = position.Y;
            Width = w;
            Height = h;

        }

        /// <summary>
        /// Determines if this BoundingPoint collides with a BoundingRectangle
        /// </summary>
        /// <param name="r">the BoundingRectangle</param>
        /// <returns>true on collision, false otherwise</returns>
        public bool CollidesWith(BoundingRectangle r)
        {
            return CollisionHelper.Collides(r, this);
        }
        /// <summary>
        /// Determines if this BoundingPoint collides with a BoundingCircle
        /// </summary>
        /// <param name="c">the BoundingCircle</param>
        /// <returns>true on collision, false otherwise</returns>
        public bool CollidesWith(BoundingCircle c)
        {
            return CollisionHelper.Collides(this, c);
        }
    }
}