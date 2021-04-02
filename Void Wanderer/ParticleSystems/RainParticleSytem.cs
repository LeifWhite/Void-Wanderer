using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace Void_Wanderer.ParticleSystems
{
    public class RainParticleSystem : ParticleSystem
    {
        Rectangle _source;
        public bool IsRaining { get; set; } = true;
        private Vector2 velocity = new Vector2(0, 400);
        public RainParticleSystem(Rectangle source) : base(7000)
        {
            _source = source;
        }
        protected override void InitializeConstants()
        {
            textureFilename = "PixelCircle";
            minNumParticles = 10;
            maxNumParticles = 25;
        }
        protected override void InitializeParticle(ref Particle p, Vector2 where)
        {
            p.Initialize(where, velocity*RandomHelper.NextFloat(0.85f, 1.15f), Vector2.Zero, Color.SteelBlue, scale: RandomHelper.NextFloat(0.3f, 0.6f), lifetime: Screen.SIZE*1.1f/velocity.Y);
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (IsRaining) AddParticles(_source);
        }
       

    }
}