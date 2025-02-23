using CatAPI.BC.Models;

namespace CatAPI.DTOs
{
    public class ResponseDTO
    {
        public bool? Success { get; set; }
        public string? Message { get; set; } 
        public object? Data { get; set; }

        
        public static ResponseDTO FromResponseModel(ResponseModel responseModel)
        {
            return new ResponseDTO
            {
                Success = responseModel.Success,
                Message = responseModel.Message,
                Data = responseModel.Data
            };
        }

    }
}
