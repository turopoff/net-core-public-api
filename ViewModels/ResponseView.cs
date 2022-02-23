namespace PrepTeach.ViewModels
{
    public class ResponseView<TValue>
    {
        public TValue? Data { get; set; }
        public string? Message { get; set; }
        public int Status { get; set; } = 0;
    }
}
