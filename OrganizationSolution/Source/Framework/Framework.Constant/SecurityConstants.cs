namespace Framework.Constant
{
    /// <summary>
    /// Defines the <see cref="SecurityConstants" />.
    /// </summary>
    public static class SecurityConstants
    {
        /// <summary>
        /// Defines the AuthenticationScheme.
        /// </summary>
        public const string AuthenticationScheme = "Bearer";

        /// <summary>
        /// Defines the TenantIdHeaderKey.
        /// </summary>
        public const string TenantIdHeaderKey = "X-Tenant-Id";

        /// <summary>
        /// Defines the GetMethod.
        /// </summary>
        public const string GetMethod = "Get";

        /// <summary>
        /// Defines the TenantIds.
        /// </summary>
        public const string TenantIds = "tenant_id_key";

        /// <summary>
        /// Defines the UserId.
        /// </summary>
        public const string UserId = "user_id";

        /// <summary>
        /// Defines the AuthorizationHeaderKey.
        /// </summary>
        public const string AuthorizationHeaderKey = "Authorization";

        /// <summary>
        /// Defines the TenantIdRequestValidationMessage.
        /// </summary>
        public const string TenantIdRequestValidationMessage = "Request does not have a Tenant id";

        /// <summary>
        /// Defines the UserIdRequestValidationMessage.
        /// </summary>
        public const string UserIdRequestValidationMessage = "Request does not have a User id";

        /// <summary>
        /// Defines the AuthTokenRequestValidationMessage.
        /// </summary>
        public const string AuthTokenRequestValidationMessage = "Request does not have valid authorization token";

        /// <summary>
        /// Defines the InvalidTenantIdValidationMessage.
        /// </summary>
        public const string InvalidTenantIdValidationMessage = "The X-Tenant-Id header had an invalid tenant id";

        /// <summary>
        /// Defines the MultipleTenantIdsValidationMessage.
        /// </summary>
        public const string MultipleTenantIdsValidationMessage = "Multiple Tenant Ids are not allowed for create, update and delete operations.";

        /// <summary>
        /// Defines the IdDoesNotExistValidationMessage.
        /// </summary>
        public const string IdDoesNotExistValidationMessage = "Id does not exist.";

        /// <summary>
        /// Defines the KeyNotSupportedValidationMessage.
        /// </summary>
        public const string KeyNotSupportedValidationMessage = "The key type is not supported:";

        /// <summary>
        /// Define the Id..
        /// </summary>
        public const string Id = "Id";
    }
}
