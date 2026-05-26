namespace OpenMPLauncher.Core.Models
{
    /// <summary>
    /// API Response wrapper
    /// </summary>
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }
        public int StatusCode { get; set; }
    }

    /// <summary>
    /// Login request model
    /// </summary>
    public class LoginRequest
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool RememberMe { get; set; }
    }

    /// <summary>
    /// Login response model
    /// </summary>
    public class LoginResponse
    {
        public string Token { get; set; } = string.Empty;
        public UserAccount User { get; set; } = new();
        public int ExpiresIn { get; set; }
    }

    /// <summary>
    /// Registration request model
    /// </summary>
    public class RegisterRequest
    {
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
