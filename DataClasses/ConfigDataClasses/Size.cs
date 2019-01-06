namespace DataClasses.ConfigDataClasses
{
    public class Size
    {
        public Size(double height, double width)
        {
            Height = height;
            Width = width;
        }
        public double Height { set; get; }
        public double Width { set; get; }
    }
}
