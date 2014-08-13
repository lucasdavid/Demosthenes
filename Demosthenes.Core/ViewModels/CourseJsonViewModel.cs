using Demosthenes.Core.Models;

namespace Demosthenes.Core.ViewModels
{
    public class CourseJsonViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public CourseJsonViewModel() { }
        public CourseJsonViewModel(Course course)
        {
            Id = course.Id;
            Title = course.Title;
        }
    }
}
