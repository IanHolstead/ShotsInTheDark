namespace UnityEngine {
    public static class GameFunctions
    {

        public static float MapRange(float value, float inRangeA, float inRangeB, float outRangeA, float outRangeB)
        {
            return (value - inRangeA) / (outRangeA - inRangeA) * (outRangeB - inRangeB) + inRangeB;
        }

        public static float MapRangeClamped(float value, float inRangeA, float inRangeB, float outRangeMin, float outRangeMax)
        {
            return Mathf.Clamp(MapRange(value, inRangeA, inRangeB, outRangeMin, outRangeMax), outRangeMin, outRangeMax);
        }
    }

    //TODO: Remove? This is a horrible way to do this but its nice and easy for testing
    public class DoOnce
    {
        bool closed;

        public DoOnce(bool startClosed = false)
        {
            closed = startClosed;
        }

        public bool Enter()
        {
            bool toReturn = !closed;
            closed = true;
            return toReturn;
        }

        public void Reset()
        {
            closed = false;
        }
    }
}
