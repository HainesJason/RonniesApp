using SMS.Shared.Enums;

namespace SMS.Shared.DTOs
{
    public class ExecuteCommandResponseDto
    {
        public ExecuteCommandEnum ExecutionStatus { get; set; } = ExecuteCommandEnum.Ok;
        public string ErrorMessage { get; set; }
    }
}