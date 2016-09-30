using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RayTraceCore
{
    public abstract class SceneObject
    {
        public Surface Surface;
        public abstract Intersection Intersect(Ray ray);
        public abstract Vector Normal(Vector pos);
    }
}
