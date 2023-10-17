using System;

namespace DynVis.Draw.OpenGL
{
    public class OpenGLEngineException: Exception
    {
        public OpenGLEngineException()
        {

        }

        public OpenGLEngineException(string message)
            : base(message)
        {

        }
    }
}
