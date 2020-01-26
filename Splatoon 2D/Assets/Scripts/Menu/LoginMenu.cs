//Script by AquaArmour

using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Splatoon2D {
    public class LoginMenu : MonoBehaviour {

        private LoginHandler _loginHandler;
            
        private string _userEmail;
        private string _userPassword;
        private string _userName;
        private string _confirmPassword;
        
        [FoldoutGroup("Panel Handlers")] [SerializeField]
        private TextMeshProUGUI loginStatus;
        
        [FoldoutGroup("Panel Handlers")] [SerializeField]
        private GameObject
            loginPanels,
            registerPanels;

        [FoldoutGroup("Login Panels")] [SerializeField]
        private TextMeshProUGUI
            loginPanelEmailInput,
            loginPanelPasswordInput;
        

        [FoldoutGroup("Register Panels")] [SerializeField]
        private TextMeshProUGUI
            registerPanelUsernameInput,
            registerPanelEmailInput,
            registerPanelPasswordInput,
            registerPanelConfirmPasswordInput;
            
        public void LoginPanel() {
            _loginHandler.ResetInfo();
            loginStatus.text = "Waiting for login";
            loginStatus.gameObject.SetActive(false);
            loginPanels.SetActive(true);
            registerPanels.SetActive(false);
                    
            registerPanelUsernameInput.text = string.Empty;
            registerPanelEmailInput.text = string.Empty;
            registerPanelPasswordInput.text = string.Empty;
            registerPanelConfirmPasswordInput.text = string.Empty;
        }
            
        public void RegisterPanel() {
            _loginHandler.ResetInfo();
            loginStatus.text = "Waiting for register";
            loginStatus.gameObject.SetActive(false);
            loginPanels.SetActive(false);
            registerPanels.SetActive(true);
                    
            loginPanelEmailInput.text = string.Empty;
            loginPanelPasswordInput.text = string.Empty;
        }
            
        public void LoginStatusText(string loginStatusString) {
            loginStatus.text = loginStatusString;
            loginStatus.gameObject.SetActive(true);
            loginPanels.SetActive(false);
            registerPanels.SetActive(false);
        }
            
        public void SetUserEmail(string emailIn) {
            _userEmail = emailIn;
        }

        public void SetUserPassword(string passwordIn) {
            _userPassword = passwordIn;
        }

        public void SetUsername(string usernameIn) {
            _userName = usernameIn;
        }

        public void SetConfirmPassword(string confirmPasswordIn) {
            _confirmPassword = confirmPasswordIn;
        }
            
        public void OnClickLogin() {
            _loginHandler.LoginWithEmail();
        }

        public void OnClickRegister() {
            _loginHandler.RegisterAccount();
        }

        public string GetUserEmail() {
            return _userEmail;
        }

        public string GetUserPassword() {
            return _userPassword;
        }

        public string GetUsername() {
            return _userName;
        }

        public string GetConfirmPassword() {
            return _confirmPassword;
        }
    }
}