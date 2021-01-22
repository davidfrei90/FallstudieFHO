using System;
using System.Web.Security;
using HsrOrderApp.BL.BusinessComponents;
using HsrOrderApp.BL.BusinessComponents.DependencyInjection;
using HsrOrderApp.BL.DomainModel;
using System.Configuration;

namespace HsrOrderApp.BL.Security
{
    public  class HsrOrderAppMembershipProvider : MembershipProvider
    {
        private SecurityBusinessComponent sb;
        private bool _requiresUniqueEmail;
        private bool _enablePasswordRetrival;
        private bool _enablePasswordReset;
        private bool _requiresQuestionAndAnswer;
        private int _maxInvalidPasswordAttempts;
        private string _passwordStrengthRegualrExpression;
        private int _minRequiredNonAlphanumericCharacters;
        private int _minrequiredPasswordLength;
        private int _passwordAttemptWindow;

        public override void Initialize(string name, System.Collections.Specialized.NameValueCollection config)
        {
            base.Initialize(name, config);
            try
            {
                _requiresUniqueEmail = Convert.ToBoolean(GetConfigValue(config["requiresUniqueEmail"], "true"));
                _enablePasswordRetrival = Convert.ToBoolean(GetConfigValue(config["enablePasswordRetrieval"], "true"));
                _enablePasswordReset = Convert.ToBoolean(GetConfigValue(config["enablePasswordReset"], "true"));
                _requiresQuestionAndAnswer = Convert.ToBoolean(GetConfigValue(config["requiresQuestionAndAnswer"], "true"));
                _maxInvalidPasswordAttempts = Convert.ToInt32(GetConfigValue(config["maxInvalidPasswordAttempts"], "10"));
                _passwordStrengthRegualrExpression = GetConfigValue(config["passwordStrengthRegularExpression"],String.Empty);
                _minRequiredNonAlphanumericCharacters = Convert.ToInt32(GetConfigValue(config["minRequiredAlphaNumericCharacters"],"1"));
                _minrequiredPasswordLength = Convert.ToInt32(GetConfigValue(config["minRequiredPasswordLength"], "2"));
                _passwordAttemptWindow = Convert.ToInt32(GetConfigValue(config["passwordAttemptWindow"], "1"));
              
            }
            catch (Exception e  )
            {

                throw new ConfigurationErrorsException("Membership Provider Configuration wrong");
            }
            
            
            sb = DependencyInjectionHelper.GetSecurityBusinessComponent();
     
        }

       

        private string GetConfigValue(string configValue, string defaultValue)
        {
            if (String.IsNullOrEmpty(configValue))
            {
                return defaultValue;
            }

            return configValue;
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            throw new NotSupportedException("The Operation is not supported");
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotSupportedException("The Operation is not supported");
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotSupportedException("The Operation is not supported");
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotSupportedException("The Operation is not supported");
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotSupportedException("The Operation is not supported");
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotSupportedException("The Operation is not supported");
        }

        public override bool ValidateUser(string username, string password)
        {
            if(username==null|| password== null)
                return false;

            try
            {
                User user = sb.GetUserByName(username);
                return (user.Password == password);

            }
            catch (Exception)
            {

                return false;
            }

            

        }

        public override bool UnlockUser(string userName)
        {
            throw new NotSupportedException("The Operation is not supported");
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotSupportedException("The Operation is not supported");
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            throw new NotSupportedException("The Operation is not supported");
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new NotSupportedException("The Operation is not supported");
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotSupportedException("The Operation is not supported");
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotSupportedException("The Operation is not supported");
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotSupportedException("The Operation is not supported");
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotSupportedException("The Operation is not supported");
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override bool EnablePasswordRetrieval
        {
            get { return _enablePasswordRetrival; }
        }

        public override bool EnablePasswordReset
        {
            get { return _enablePasswordReset; }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { return _requiresQuestionAndAnswer; }
        }

        public override string ApplicationName
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }


        public override int MaxInvalidPasswordAttempts
        {
            get { return _maxInvalidPasswordAttempts; }
        }

        public override int PasswordAttemptWindow
        {
            get { return _passwordAttemptWindow; }
        }

        public override bool RequiresUniqueEmail
        {
            get { return _requiresUniqueEmail; }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredPasswordLength
        {
            get { return _minrequiredPasswordLength; }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { return _minRequiredNonAlphanumericCharacters; }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { return _passwordStrengthRegualrExpression; }
        }
    }
}