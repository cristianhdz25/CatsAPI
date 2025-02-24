namespace CatAPI.BC.Models
{
    //This class represents the response model with its properties.
    public class ResponseModel
    {
        public bool Success { get; set; }
        public string? Message { get; set; } 
        public object? Data { get; set; } 
    }
}
