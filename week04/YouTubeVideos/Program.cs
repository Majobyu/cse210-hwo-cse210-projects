using System;
using System.Collections.Generic;

namespace YouTubeVideos
{
    // Class to represent a comment
    public class Comment
    {
        public string CommenterName { get; set; }
        public string Text { get; set; }

        public Comment(string commenterName, string text)
        {
            CommenterName = commenterName;
            Text = text;
        }
    }

    // Class to represent a video
    public class Video
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int DurationInSeconds { get; set; }
        private List<Comment> comments;

        public Video(string title, string author, int durationInSeconds)
        {
            Title = title;
            Author = author;
            DurationInSeconds = durationInSeconds;
            comments = new List<Comment>();
        }

        public void AddComment(Comment comment)
        {
            comments.Add(comment);
        }

        public int GetNumberOfComments()
        {
            return comments.Count;
        }

        public List<Comment> GetComments()
        {
            return comments;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Create list of videos
            List<Video> videos = new List<Video>();

            // Video 1
            Video video1 = new Video("Introduction to C#", "John Smith", 600);
            video1.AddComment(new Comment("Anna", "Great video, thanks!"));
            video1.AddComment(new Comment("Luke", "Excellent explanation."));
            video1.AddComment(new Comment("Mary", "Could you make an advanced tutorial?"));
            videos.Add(video1);

            // Video 2
            Video video2 = new Video("Python Tutorial", "Sophia Gomez", 900);
            video2.AddComment(new Comment("Charles", "Helped me a lot."));
            video2.AddComment(new Comment("Helen", "Where can I download the code?"));
            video2.AddComment(new Comment("Peter", "Very clear and simple."));
            video2.AddComment(new Comment("Lucy", "Please upload more Python videos."));
            videos.Add(video2);

            // Video 3
            Video video3 = new Video("Basic Math Concepts", "Edward Fernandez", 1200);
            video3.AddComment(new Comment("Martha", "This content is great for beginners."));
            video3.AddComment(new Comment("James", "Thanks for sharing."));
            video3.AddComment(new Comment("Sandra", "Very educational."));
            videos.Add(video3);

            // Display video info and comments
            foreach (var video in videos)
            {
                Console.WriteLine($"Title: {video.Title}");
                Console.WriteLine($"Author: {video.Author}");
                Console.WriteLine($"Duration (seconds): {video.DurationInSeconds}");
                Console.WriteLine($"Number of comments: {video.GetNumberOfComments()}");
                Console.WriteLine("Comments:");
                foreach (var comment in video.GetComments())
                {
                    Console.WriteLine($"- {comment.CommenterName}: {comment.Text}");
                }
                Console.WriteLine(new string('-', 40));
            }
        }
    }
}
