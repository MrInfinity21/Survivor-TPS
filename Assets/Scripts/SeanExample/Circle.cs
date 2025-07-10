namespace SeanExample
{
    public class Circle
    {
        private float _x, _y;
        private float _radius, _diameter; //what happens if I change the radius? Diameter changes as well. Diameter and radius are coupled.
        /// <summary>
        /// Always use properties for public variables because otherwise it is damn near impossible to keep track of things that change.
        /// </summary>
        public float Radius
        {
            get => _radius;
            set
            {
                _radius = value;
                _diameter = _radius * 2;
            }
        }

        private float Diameter
        {
            get => _diameter;
            set
            {
                _diameter = value;
                _radius = _diameter / 2;
            }
        }
        
        
    }
}