﻿using GovHistoryRepository.Identity;
using GovHistoryWeb.Controllers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

public enum RBACStatus
    {
        Success = 0,
        LockedOut = 1,
        RequiresVerification = 2,
        Failure = 3,
        EmailVerification = 4,
        PhoneVerification = 5,
        RequiresAccountActivation = 6,
        EmailUnconfirmed = 7,
        PhoneNumberUnconfirmed = 8,
        InvalidToken = 9,
    }

    public static class RBAC_ExtendedMethods
    {
        public static string c_AccountLockout = "Il tuo account è stato bloccato per {0} minuti a causa di molteplici tentativi falliti di accesso";
        //public static string c_InvalidCredentials = "Invalid credentials. You have {0} more attempt(s) before your account gets locked out";
        public static string c_InvalidCredentials = "La password inserita non è corretta. Restano ancora {0} tentativi prima che l'account sia bloccato";
        public static string c_InvalidLogin = "Accesso non riuscito";
        public static string c_InvalidUser = "L'account non esite. Inserisci credenziali differenti o creane uno nuovo.";
        public static string c_AccountEmailUnconfirmed = "E' necessario verificare l'account rispondendo all'email di attivazione prima di poter continuare...";
        public static string c_AccountPhoneNumberUnconfirmed = "E'necessario verificare l'account usando il codice di sicurezza inviato al numero di cellulare durante la procedura di registrazione, prima di poter continuare.  Un codice di sicurezza è stata spedito al seguente numero {0}.  Inserisci il codice di sicurezza nel campo che si trova sotto e spingi il tasto 'Verificia la mia identità' per completare la procedura di sicurezza...";
        public static string c_AccountUnverified = "Non è stata verificata l'identità durante la procedura di registrazione. Per favore chiedi un nuovo codice di sicurezza...";

        public static string c_EmailCode = "Email Code";
        public static string c_PhoneCode = "Phone Code";

        public static string cKey_UserLockoutEnabled = "UserLockoutEnabled";
        public static string cKey_AccountLockoutTimeSpan = "AccountLockoutTimeSpan";
        public static string cKey_MaxFailedAccessAttemptsBeforeLockout = "MaxFailedAccessAttemptsBeforeLockout";
        public static string cKey_2FAEnabled = "2FAEnabled";
        public static string cKey_2FADeviceType = "2FADeviceType";
        public static string cKey_AccountVerificationRequired = "AccountVerificationRequired";
        public static string cKey_PasswordRequiredLength = "PasswordRequiredLength";
        public static string cKey_PasswordRequireNonLetterOrDigit = "PasswordRequireNonLetterOrDigit";
        public static string cKey_PasswordRequireDigit = "PasswordRequireDigit";
        public static string cKey_PasswordRequireLowercase = "PasswordRequireLowercase";
        public static string cKey_PasswordRequireUppercase = "PasswordRequireUppercase";

        #region Registration
        private static int RegisterUser(RegisterViewModel model, ApplicationUserManager userMngr, out List<string> _errors)
        {
            int _retVal = -1;
            _errors = new List<string>();
            try
            {
                bool Is2FAEnabled = GetConfigSettingAsBool(cKey_2FAEnabled);
                var user = new ApplicationUser { UserName = model.UserName, Email = model.Email, Firstname = model.Firstname, Lastname = model.Lastname, PhoneNumber = model.Mobile, TwoFactorEnabled = Is2FAEnabled };

                if (userMngr != null)
                {
                    var result = userMngr.Create(user, model.Password);
                    if (result.Succeeded)
                    {
                        _retVal = user.Id;
                    }
                    else
                    {
                        _errors.AddRange(result.Errors);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _retVal;
        }

        public static RBACStatus Register(RegisterViewModel model, ApplicationUserManager userMngr, ApplicationSignInManager signInMngr, out List<string> _errors)
        {
            RBACStatus _retVal = RBACStatus.Failure;
            try
            {
                //Logic driven by settings defined in the application’s configuration file...
                int _userId = RBAC_ExtendedMethods.RegisterUser(model, userMngr, out _errors);
                if (_userId > -1)
                {
                    model.Id = _userId;
                    if (userMngr != null)
                    {
                        //Check if we require an Account Verification Email as part of our registration process...
                        bool IsAccountVerificationRequired = GetConfigSettingAsBool(cKey_AccountVerificationRequired);
                        bool Is2FAEnabled = GetConfigSettingAsBool(cKey_2FAEnabled);
                        string DeviceType = GetConfigSetting(cKey_2FADeviceType);

                        //if ((IsAccountVerificationRequired) || (Is2FAEnabled && DeviceType == c_EmailCode))
                        if ((IsAccountVerificationRequired && DeviceType == c_EmailCode) || (Is2FAEnabled && DeviceType == c_EmailCode))
                        {
                            //Generate Email Confirmation Token                      
                            _retVal = RBACStatus.Failure;
                            if (SendOTP2Email(userMngr, _userId, model.Email))
                                _retVal = RBACStatus.RequiresAccountActivation;

                            return _retVal;
                        }
                        //else if (Is2FAEnabled && DeviceType == c_PhoneCode)
                        else if ((IsAccountVerificationRequired && DeviceType == c_PhoneCode) || (Is2FAEnabled && DeviceType == c_PhoneCode))
                        {
                            _retVal = RBACStatus.Failure;
                            if (SendOTP2Phone(userMngr, _userId, model.Mobile))
                                _retVal = RBACStatus.PhoneVerification;

                            return _retVal;
                        }
                    }
                    _retVal = RBACStatus.Success;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _retVal;
        }
        #endregion

        #region Login
        public static RBACStatus Login(this ControllerBase controller, LoginViewModel model, ApplicationUserManager userMngr, ApplicationSignInManager signInMngr, out List<string> _errors)
        {
            RBACStatus _retVal = RBACStatus.Failure;
            _errors = new List<string>();
            try
            {
                var user = userMngr.FindByName(model.UserName);
                if (user != null)
                {
                    var validCredentials = userMngr.Find(model.UserName, model.Password);
                    if (userMngr.IsLockedOut(user.Id))
                    {
                        _errors.Add(string.Format(c_AccountLockout, GetConfigSettingAsDouble(cKey_AccountLockoutTimeSpan)));
                        return RBACStatus.LockedOut;
                    }
                    else if (userMngr.GetLockoutEnabled(user.Id) && validCredentials == null)
                    {
                        userMngr.AccessFailed(user.Id);
                        if (userMngr.IsLockedOut(user.Id))
                        {
                            _errors.Add(string.Format(c_AccountLockout, GetConfigSettingAsDouble(cKey_AccountLockoutTimeSpan)));
                            return RBACStatus.LockedOut;
                        }
                        else
                        {
                            int _attemptsLeftB4Lockout = (GetConfigSettingAsInt(cKey_MaxFailedAccessAttemptsBeforeLockout) - userMngr.GetAccessFailedCount(user.Id));
                            _errors.Add(string.Format(c_InvalidCredentials, _attemptsLeftB4Lockout));
                            return _retVal;
                        }
                    }
                    else if (validCredentials == null)
                    {
                        _errors.Add(c_InvalidLogin);
                        return _retVal;
                    }
                    else
                    {
                        //Valid credentials entered, we need to check whether email verification is required...
                        bool IsAccountVerificationRequired = GetConfigSettingAsBool(cKey_AccountVerificationRequired);
                        bool Is2FAEnabled = GetConfigSettingAsBool(cKey_2FAEnabled);
                        string DeviceType = GetConfigSetting(cKey_2FADeviceType);

                        if ((IsAccountVerificationRequired && DeviceType == c_EmailCode) || (Is2FAEnabled && DeviceType == c_EmailCode))
                        {
                            //Check if email verification has been confirmed!
                            if (!userMngr.IsEmailConfirmed(user.Id))
                            {
                                //Display error message on login page, take no further action...                         
                                _errors.Add(c_AccountEmailUnconfirmed);
                                return RBACStatus.EmailUnconfirmed;
                            }
                        }
                        //else if (Is2FAEnabled && DeviceType == c_PhoneCode)
                        else if ((IsAccountVerificationRequired && DeviceType == c_PhoneCode) || (Is2FAEnabled && DeviceType == c_PhoneCode))
                        {
                            if (!userMngr.IsPhoneNumberConfirmed(user.Id))
                            {
                                _errors.Add(c_AccountPhoneNumberUnconfirmed);
                                return RBACStatus.PhoneNumberUnconfirmed;
                            }
                        }

                        bool _userLockoutEnabled = GetConfigSettingAsBool(cKey_UserLockoutEnabled);

                        //Before we signin, check that our 2FAEnabled config setting agrees with the database setting for this user...
                        if (Is2FAEnabled != userMngr.GetTwoFactorEnabled(user.Id))
                        {
                            userMngr.SetTwoFactorEnabled(user.Id, Is2FAEnabled);
                        }

                        _retVal = (RBACStatus)signInMngr.PasswordSignIn(model.UserName, model.Password, model.RememberMe, shouldLockout: _userLockoutEnabled);
                        switch (_retVal)
                        {
                            case RBACStatus.Success:
                                {
                                    userMngr.ResetAccessFailedCount(user.Id);
                                    break;
                                }
                            default:
                                {
                                    _errors.Add(c_InvalidLogin);
                                    break;
                                }
                        }
                    }
                }
                else
                {
                    _errors.Add(c_InvalidUser);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _retVal;
        }

        #endregion

        #region Verification
        public static RBACStatus VerifyOTP4Phone(int _userId, string _phoneNumber, string _token, ApplicationUserManager userMngr, ApplicationSignInManager signInMngr, out IEnumerable<string> _errors)
        {
            RBACStatus _retVal = RBACStatus.Failure;
            try
            {
                IdentityResult result = userMngr.ChangePhoneNumber(_userId, _phoneNumber, _token);
                if (result == IdentityResult.Success)
                {
                    ApplicationUser user = userMngr.FindById(_userId);
                    if (user != null)
                    {
                        signInMngr.SignIn(user, isPersistent: false, rememberBrowser: false);
                    }
                    _retVal = RBACStatus.Success;
                }
                _errors = result.Errors;
            }
            catch (Exception)
            {
                throw;
            }
            return _retVal;
        }

        public static bool SendOTP2Phone(ApplicationUserManager _userMngr, int _userId, string _phoneNumber)
        {
            bool _retVal = false;

            if (string.IsNullOrEmpty(_phoneNumber))
                throw new Exception("No mobile number provided, unable to text notification...");

            if (_userMngr.SmsService != null)
            {
                //Generate security code for phone confirmation   
                var code = _userMngr.GenerateChangePhoneNumberToken(_userId, _phoneNumber);
                var message = new IdentityMessage
                {
                    Destination = _phoneNumber,
                    Body = "Your security code is: " + code
                };

                //Send the security code  
                _userMngr.SmsService.Send(message);
                _retVal = true;
            }
            else
            {
                throw new Exception("SMS Service has not been configured, unable to text notification...");
            }
            return _retVal;
        }

        public static bool SendOTP2Email(ApplicationUserManager _userMngr, int _userId, string _email)
        {
            bool _retVal = false;

            if (string.IsNullOrEmpty(_email))
                throw new Exception("No e-mail address provided, unable to send email notification...");

            if (_userMngr.EmailService != null)
            {
                //Generate security code for email confirmation
                string _code = _userMngr.GenerateEmailConfirmationToken(_userId);
                HttpContextWrapper httpContextWrapper = new HttpContextWrapper(System.Web.HttpContext.Current);
                var callbackUrl = new UrlHelper(new RequestContext(httpContextWrapper, RouteTable.Routes.GetRouteData(httpContextWrapper)));
                var message = new IdentityMessage { Subject = "Time-based One-Time Password (TOTP) Account Verification", Destination = _email, Body = string.Format("Please <a href='{0}'>verify</a> your account before attempting to log in to the system.", callbackUrl) };

                //Email the security code  
                _userMngr.EmailService.Send(message);
                _retVal = true;
            }
            else
            {
                throw new Exception("Smtp Service has not been configured, unable to send email notification...");
            }
            return _retVal;
        }

        public static bool VerifyOTP4Email(int _userId, string _token, ApplicationUserManager userMngr, ApplicationSignInManager signInMngr, out IEnumerable<string> _errors)
        {
            bool _retVal = false;
            _errors = new List<string>();
            try
            {
                ApplicationUser user = userMngr.FindById(_userId);
                if (userMngr.VerifyTwoFactorToken(user.Id, c_EmailCode, _token))
                {
                    signInMngr.SignIn(user, isPersistent: false, rememberBrowser: false);
                    return true;
                }
                else
                {
                    _errors = _errors = new List<string> { "Invalid code..." };
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _retVal;
        }

        public static RBACStatus RequestAccountVerification(this ControllerBase controller, int _userId, string _email, ApplicationUserManager userMngr, out IEnumerable<string> _errors)
        {
            RBACStatus _retVal = RBACStatus.Failure;
            _errors = new List<string>();

            try
            {
                if (userMngr.EmailService != null)
                {
                    string _code = userMngr.GenerateEmailConfirmationToken(_userId);

                    var callbackUrl = new UrlHelper(controller.ControllerContext.RequestContext).Action("ConfirmEmail", "Account", new { userId = _userId, code = _code }, protocol: controller.ControllerContext.RequestContext.HttpContext.Request.Url.Scheme);
                    var message = new IdentityMessage { Subject = "Account Verification", Destination = _email, Body = string.Format("Please <a href='{0}'>verify</a> your account before attempting to log in to the system.", callbackUrl) };
                    userMngr.EmailService.Send(message);

                    _retVal = RBACStatus.RequiresAccountActivation;
                }
                else
                {
                    _errors = new List<string> { "Smtp Service has not been configured!", "Unable to send e-mail Confirmation Token..." };
                }
            }
            catch (Exception)
            {

                throw;
            }


            return _retVal;
        }

        #endregion

        #region Helper Functions
        public static bool GetConfigSettingAsBool(string _name, bool _defaultValue = false)
        {
            bool _retVal = _defaultValue;
            try
            {
                _retVal = Convert.ToBoolean(ConfigurationManager.AppSettings[_name].ToString());
            }
            catch (Exception)
            {
            }
            return _retVal;
        }

        public static int GetConfigSettingAsInt(string _name, int _defaultValue = 0)
        {
            int _retVal = _defaultValue;
            try
            {
                _retVal = Convert.ToInt32(ConfigurationManager.AppSettings[_name].ToString());
            }
            catch (Exception)
            {
            }
            return _retVal;
        }

        public static double GetConfigSettingAsDouble(string _name, double _defaultValue = 0)
        {
            double _retVal = _defaultValue;
            try
            {
                _retVal = Convert.ToDouble(ConfigurationManager.AppSettings[_name].ToString());
            }
            catch (Exception)
            {
            }
            return _retVal;
        }

        public static string GetConfigSetting(string _name)
        {
            string _retVal = string.Empty;
            try
            {
                _retVal = ConfigurationManager.AppSettings[_name].ToString();
            }
            catch (Exception)
            {
            }
            return _retVal;
        }
        #endregion
    }

