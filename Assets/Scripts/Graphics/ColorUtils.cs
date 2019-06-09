namespace TWF.Graphics
{
    using UnityEngine;

    public static class ColorUtils
    {
        public static void Superpose(ref Color result, Color front, Color back)
        {
            result.a = 1 - ((1 - front.a) * (1 - back.a));
            result.r = Mathf.Lerp(back.r, front.r, front.a);
            result.g = Mathf.Lerp(back.g, front.g, front.a);
            result.b = Mathf.Lerp(back.b, front.b, front.a);
        }
    }
}
