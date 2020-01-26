//Script by AquaArmour

using System.Collections;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Splatoon2D {
    
    public class LoginHandler : MonoBehaviour {

        [SerializeField] private GameObject loginObject;
        
        private LoginMenu _loginMenu;
        
        private string 
            _userEmail,
            _userPassword,
            _userName,
            _confirmPassword;

        public void Start() {
            _loginMenu = GetComponent<LoginMenu>();
            SetTitleId();
        }

        private void SetTitleId() {
            if (string.IsNullOrEmpty(PlayFabSettings.TitleId)) {
                PlayFabSettings.TitleId = "3A1D6";
            }
            CheckForPreviousLogin();
        }
        
        private void CheckForPreviousLogin() {
            if (PlayerPrefs.HasKey("Email")) {
                _userEmail = PlayerPrefs.GetString("Email");
                _userPassword = PlayerPrefs.GetString("Password");
                LoginWithEmail();
            }
            else {
                _loginMenu.LoginPanel();
            }
        }

        public void LoginWithEmail() {
            _loginMenu.LoginStatusText("Logging in ...");
            var loginRequest = new LoginWithEmailAddressRequest {
                Email = _userEmail,
                Password = _userPassword
            };
            PlayFabClientAPI.LoginWithEmailAddress(loginRequest, OnLoginSuccess, OnLoginFailure);
        }

        private void OnLoginSuccess(LoginResult result) {
            CreateLoginPass(result);
            StartCoroutine(LoginSuccessCoroutine());
        }

        private void CreateLoginPass(LoginResult loginResult) {
            var loginPass = Instantiate(loginObject).GetComponent<LoginPass>();
            loginPass.name = "Login Pass";
            var userId = loginResult.PlayFabId;
            PlayFabClientAPI.GetPlayerProfile(new GetPlayerProfileRequest {
                    PlayFabId = userId,
                    ProfileConstraints = new PlayerProfileViewConstraints {
                        ShowDisplayName = true
                    }
                },
                result => loginPass.SetPlayer(result.PlayerProfile),
                error => error.GenerateErrorReport());
        }

        private IEnumerator LoginSuccessCoroutine() {
            _loginMenu.LoginStatusText("Successfully Logged In!");
            PlayerPrefs.Save();
            yield return new WaitForSeconds(1.5f);
            SceneManager.LoadScene("Lobby");
        }
        
        private void OnLoginFailure(PlayFabError error) {
            StartCoroutine(LoginFailureCoroutine(error.GenerateErrorReport()));
        }

        private IEnumerator LoginFailureCoroutine(string error) {
            _loginMenu.LoginStatusText(error);
            yield return new WaitForSeconds(2f);
            _loginMenu.LoginPanel();
        }
        
        public void RegisterAccount() {
            if (_userPassword != _confirmPassword) {
                StartCoroutine(RegistrationFailureCoroutine("Confirm password does not match, Please re-enter the password"));
                return;
            }
            _loginMenu.LoginStatusText("Registering account...");
            var registerRequest = new RegisterPlayFabUserRequest {
                Email = _userEmail,
                Password = _userPassword,
                Username = _userName,
                DisplayName = _userName
            };
            PlayFabClientAPI.RegisterPlayFabUser(registerRequest, OnRegisterSuccess, OnRegisterFailure);
        }
        
        private void OnRegisterSuccess(RegisterPlayFabUserResult result) {
            CreateRegisterPass(result);
            StartCoroutine(RegistrationSuccessCoroutine());
        }

        private void CreateRegisterPass(RegisterPlayFabUserResult registerResult) {
            var loginPass = Instantiate(loginObject).GetComponent<LoginPass>();
            loginPass.name = "Login Pass";
            var userId = registerResult.PlayFabId;
            PlayFabClientAPI.GetPlayerProfile(new GetPlayerProfileRequest {
                    PlayFabId = userId,
                    ProfileConstraints = new PlayerProfileViewConstraints {
                        ShowDisplayName = true
                    }
                },
                result => loginPass.SetPlayer(result.PlayerProfile),
                error => error.GenerateErrorReport());
        }

        private IEnumerator RegistrationSuccessCoroutine() {
            _loginMenu.LoginStatusText("Successfully Registered Account");
            PlayerPrefs.SetString("Email", _userEmail);
            PlayerPrefs.SetString("Password", _userPassword);
            PlayerPrefs.Save();
            yield return new WaitForSeconds(1.5f);
            SceneManager.LoadScene("Lobby");
        }
        
        private void OnRegisterFailure(PlayFabError error) {
            StartCoroutine(RegistrationFailureCoroutine(error.GenerateErrorReport()));
        }

        private IEnumerator RegistrationFailureCoroutine(string error) {
            _loginMenu.LoginStatusText(error);
            yield return new WaitForSeconds(2f);
            _loginMenu.RegisterPanel();
        }

        public void ResetInfo() {
            _userName = null;
            _userEmail = null;
            _userPassword = null;
        }
    }
}
