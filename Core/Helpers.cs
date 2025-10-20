using SplashKitSDK;

namespace Core;
// stores a 2d floating point coordinate
public class Point
{
    public double X;
    public double Y;
    public Point(double x, double y) { X = x; Y = y; }
    public Point(int x, int y) { X = x; Y = y; }
    public Point(Point point) { X = point.X; Y = point.Y; }
    public Point(double[] point) { X = point[0]; Y = point[1]; }
    public static Point operator +(Point left, Point right) => new Point(left.X + right.X, left.Y + right.Y);
    public static Point operator -(Point left, Point right) => new Point(left.X - right.X, left.Y - right.Y);
    public static Point operator *(Point point, double scalar) => new Point(point.X * scalar, point.Y * scalar);
    public static Point operator /(Point point, double scalar) => new Point(point.X / scalar, point.Y / scalar);
    public static implicit operator Point2D(Point point) => new Point2D { X = point.X, Y = point.Y };
}

public static class Helpers
{
    public static Rectangle getMenuButtonRectangle(Point windowSize, int numButtons, int currentButton, double width, double height, double padding)
    {
        double y0 = 0.5 * (windowSize.Y - numButtons * height - (numButtons - 1) * padding);
        Point p1 = new Point(
            windowSize.X / 2.0 - width / 2.0,
            y0 + currentButton * (height + padding)
        );
        Point p2 = new Point(p1);
        p2 += new Point(width, height);
        return SplashKit.RectangleFrom(p1, p2);
    }
}