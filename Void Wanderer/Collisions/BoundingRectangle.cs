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
        public float X;
        public float Y;
        public float Width;
        public float Height;
        public float Left => X;
        public float Top => Y;
        public float Right => (X + Width);
        public float Bottom => (Y + Height);
        public BoundingRectangle(float x, float y, float w, float h)
        {
            X = x;
            Y = y;
            Width = w;
            Height = h;

        }
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