namespace CustomNoMediatr.Posts;

public class Post
{
    public int Id { get; set; }
    public string Title { get; set; } = String.Empty;
    public string Body { get; set; } = String.Empty;
    public string CreateBy { get; set; } = String.Empty;

    public Post()
    {
        
    }
}