namespace Framework.Security.Authorization
{
    using EnsureThat;
    using System;

    /// <summary>
    /// Defines the <see cref="SecurityRightIdentifierModel" />.
    /// </summary>
    public class SecurityRightIdentifierModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityRightIdentifierModel"/> class.
        /// </summary>
        public SecurityRightIdentifierModel()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityRightIdentifierModel"/> class.
        /// </summary>
        /// <param name="applicationModuleCode">The applicationModuleCode<see cref="string"/>.</param>
        /// <param name="rightCode">The rightCode<see cref="string"/>.</param>
        public SecurityRightIdentifierModel(string applicationModuleCode, string rightCode)
        {
            EnsureArg.IsNotNullOrWhiteSpace(applicationModuleCode, nameof(applicationModuleCode));
            EnsureArg.IsNotNullOrWhiteSpace(rightCode, nameof(rightCode));

            ApplicationModuleCode = applicationModuleCode;
            SecurityRightCode = rightCode;
        }

        /// <summary>
        /// Gets or sets the ApplicationModuleCode.
        /// </summary>
        public string ApplicationModuleCode { get; set; }

        /// <summary>
        /// Gets or sets the SecurityRightCode.
        /// </summary>
        public string SecurityRightCode { get; set; }

        /// <summary>
        /// The Equals.
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public override bool Equals(object obj)
        {
            var securityRightIdentifier = obj as SecurityRightIdentifierModel;
            if (obj == null)
            {
                return false;
            }

            return SecurityRightCode == securityRightIdentifier.SecurityRightCode && ApplicationModuleCode == securityRightIdentifier.ApplicationModuleCode;
        }

        /// <summary>
        /// The GetHashCode.
        /// </summary>
        /// <returns>The <see cref="int"/>.</returns>
        public override int GetHashCode()
        {
            return SecurityRightCode.GetHashCode(StringComparison.OrdinalIgnoreCase) ^ ApplicationModuleCode.GetHashCode(StringComparison.OrdinalIgnoreCase);
        }
    }
}
