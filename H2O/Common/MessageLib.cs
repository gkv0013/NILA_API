namespace Hydrogen.Common
{
    public static class MessageLib
    {
        // Messages related to CRUD operations
        public const string Save = "Data has been successfully saved.";
        public const string Update = "Data has been successfully updated.";
        public const string Delete = "Data has been successfully deleted.";
        public const string Retrieve = "Data has been successfully retrieved.";

        // Messages for error handling
        public const string NotFound = "The requested resource was not found.";
        public const string BadRequest = "The request is invalid.";
        public const string Unauthorized = "Authorization has been denied for this request.";
        public const string Forbidden = "Access to the requested resource is forbidden.";
        public const string InternalServerError = "An unexpected error occurred.";

        // Messages for specific scenarios
        public const string LoginSuccess = "Login successful.";
        public const string LoginFailure = "Invalid username or password.";
        public const string AccessDenied = "You do not have sufficient permissions to access this resource.";
        public const string SessionExpired = "Your session has expired. Please log in again.";
        public const string OperationCancelled = "The operation has been cancelled.";

        // Add other messages as needed
        public const string Error = "Please contact customer care";
    }

}
