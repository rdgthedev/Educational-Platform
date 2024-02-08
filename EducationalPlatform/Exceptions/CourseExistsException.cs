namespace EducationalPlatform.Exceptions
{
    public class CourseExistsException : Exception
    {
            public CourseExistsException() { }
            public CourseExistsException(string message) : base(message) { }
            public CourseExistsException(string message, Exception inner) : base(message, inner) { }
    }
}
