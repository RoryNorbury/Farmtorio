namespace Core;

public class Point2i
{
    public int X;
    public int Y;
    public Point2i(int x, int y) { X = x; Y = y; }
    public Point2i(Point2i pointt) { X = pointt.X; Y = pointt.Y; }
    public Point2i(int[] point) { X = point[0]; Y = point[1]; }
}
