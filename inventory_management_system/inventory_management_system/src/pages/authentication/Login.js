import { Link, Navigate, useNavigate } from "react-router-dom";
import UseInput from "../../hooks/UseInput";
import { validateEmail, validatePassword } from "../../validations/Validations";
import Input from "../../components/shared/Input";
import { useLoginMutation } from "../../services/authentication/AuthApiSlice";
import { toast } from "react-toastify";
import { GetSessionData, SetSessionData } from "../../util/AccessToken";
import Loading from "../../components/shared/Loading";

export default function Login() {
  const [Login, { isLoading }] = useLoginMutation();
  const navigate = useNavigate();
  const user = GetSessionData();
  const {
    inputValue: emailValue,
    handleOnBlur: handleEmailBlur,
    handleOnChange: handleEmailChange,
    setServerError: setEmailServerError,
    didTouch: didEmailTouch,
  } = UseInput("", validateEmail);

  const {
    inputValue: passwordValue,
    handleOnBlur: handlePasswordBlur,
    handleOnChange: handlePasswordChange,
    setServerError: setPasswordServerError,
    didTouch: didPasswordTouch,
  } = UseInput("", validatePassword);

  async function loginFetch(userData) {
    try {
      const sessionData = await Login(userData).unwrap();
      if (sessionData.accessToken !== null) {
        console.log(sessionData);
        SetSessionData(sessionData);
      }
      if (sessionData?.isLoggedIn === false) {
        const errors = sessionData?.message;
        if (errors === "Email doesn't exists!") setEmailServerError(errors);
        if (errors === "Incorrect email or password!")
          setPasswordServerError(errors);
      }
      if (sessionData.role[0] === "User") {
        toast.success(sessionData.message);
        navigate("/user");
      } else if (sessionData.role[0] === "Admin") {
        toast.success(sessionData.message);
        navigate("/admin");
      }
    } catch (err) {
      if (err.status === 400) {
        toast.error(err.status + " username or password are incorrect!");
      } else if (err.status === 401) {
        toast.error(err.status + " Unauthrized access");
      }
    }
  }

  function handleSubmit(event) {
    event.preventDefault();
    const isValid =
      validateEmail(emailValue).isValid &&
      validatePassword(passwordValue).isValid;
    if (isValid) {
      const userData = {
        email: emailValue,
        password: passwordValue,
      };
      loginFetch(userData);
    } else {
      handleEmailBlur();
      handlePasswordBlur();
    }
  }

  const isAuth = user?.accessToken;
  if (isAuth) {
    return <Navigate to={user?.role[0] === "Admin" ? "/admin" : "/user"} />;
  }

  return (
    <div className="container-fluid">
      <div className="row m-5 py-3">
        <div
          className="col-lg-4 col-md-8 m-auto py-3 shadow border rounded-3"
          style={{ backgroundColor: "#d7e0ea" }}
        >
          <form onSubmit={handleSubmit}>
            <div className="mb-3">
              <Input
                classes={didEmailTouch.classes}
                htmlFor="email"
                label="Email"
                id="email"
                type="email"
                name="email"
                value={emailValue}
                onChange={handleEmailChange}
                onBlur={handleEmailBlur}
                placeholder="Enter your email"
                error={didEmailTouch.touch ? didEmailTouch.error : undefined}
              />
            </div>
            <div className="mb-3">
              <Input
                classes={didPasswordTouch.classes}
                htmlFor="password"
                label="Password"
                id="password"
                type="password"
                name="password"
                value={passwordValue}
                onChange={handlePasswordChange}
                onBlur={handlePasswordBlur}
                placeholder="Enter your password"
                error={
                  didPasswordTouch.touch ? didPasswordTouch.error : undefined
                }
              />
              {isLoading && <Loading />}
            </div>
            <button
              type="submit"
              className="btn btn-primary"
              disabled={isLoading}
            >
              Login
            </button>
          </form>
          <p className="pt-3">
            Don't have an account.<Link to="/register">Register</Link>
          </p>
        </div>
      </div>
    </div>
  );
}
