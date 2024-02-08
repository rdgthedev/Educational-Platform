namespace EducationalPlatform.Exceptions
{
    public class CourseNotFoundException : Exception
    {
            public CourseNotFoundException() { }
            public CourseNotFoundException(string message) : base(message) { }
            public CourseNotFoundException(string message, Exception inner) : base(message, inner) { }
    }
}
