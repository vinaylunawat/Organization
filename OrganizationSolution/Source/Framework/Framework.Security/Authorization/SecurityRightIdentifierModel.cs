namespace Framework.Security.Authorization
{
    using System;
    using EnsureThat;

    public class SecurityRightIdentifierModel
    {
        public SecurityRightIdentifierModel()
        {
        }

        public SecurityRightIdentifierModel(string applicationModuleCode, string rightCode)
        {
            EnsureArg.IsNotNullOrWhiteSpace(applicationModuleCode, nameof(applicationModuleCode));
            EnsureArg.IsNotNullOrWhiteSpace(rightCode, nameof(rightCode));

            ApplicationModuleCode = applicationModuleCode;
            SecurityRightCode = rightCode;
        }

        public string ApplicationModuleCode { get; set; }

        public string SecurityRightCode { get; set; }

        public override bool Equals(object obj)
        {
            var securityRightIdentifier = obj as SecurityRightIdentifierModel;
            if (obj == null)
            {
                return false;
            }

            return SecurityRightCode == securityRightIdentifier.SecurityRightCode && ApplicationModuleCode == securityRightIdentifier.ApplicationModuleCode;
        }

        public override int GetHashCode()
        {
            return SecurityRightCode.GetHashCode(StringComparison.OrdinalIgnoreCase) ^ ApplicationModuleCode.GetHashCode(StringComparison.OrdinalIgnoreCase);
        }
    }
}
