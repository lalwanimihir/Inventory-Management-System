import { useNavigate, Link, Navigate } from "react-router-dom";
import UseInput from "../../hooks/UseInput";
import Input from "../../components/shared/Input";
import {
  validateConfirmPassword,
  validateEmail,
  validatePassword,
  validatePhoneNo,
  validateUserName,
} from "../../validations/Validations";
import { useRegisterMutation } from "../../services/authentication/AuthApiSlice";
import { toast } from "react-toastify";
import Loading from "../../components/shared/Loading";
import { GetSessionData } from "../../util/AccessToken";

export default function Register() {
  const navigate = useNavigate();
  const user = GetSessionData();
  const [Register, { isLoading }] = useRegisterMutation();
  const {
    inputValue: usernameValue,
    handleOnBlur: handleUsernameBlur,
    handleOnChange: handleUsernameChange,
    setServerError: setUsernameServerError,
    didTouch: didUsernameTouch,
  } = UseInput("", validateUserName);

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
    didTouch: didPasswordTouch,
  } = UseInput("", (enteredValue) =>
    validatePassword(enteredValue, confirmPasswordValue)
  );

  const {
    inputValue: confirmPasswordValue,
    handleOnBlur: handleConfirmPasswordBlur,
    handleOnChange: handleConfirmPasswordChange,
    didTouch: didConfirmPasswordTouch,
  } = UseInput("", (confirmPassword) =>
    validateConfirmPassword(confirmPassword, passwordValue)
  );

  const {
    inputValue: phoneNoValue,
    handleOnBlur: handlePhoneNoBlur,
    handleOnChange: handlePhoneNoChange,
    didTouch: didPhoneNoTouch,
  } = UseInput("", validatePhoneNo);

  async function handleSubmit(event) {
    event.preventDefault();
    const isValid =
      validateUserName(usernameValue).isValid &&
      validateEmail(emailValue).isValid &&
      validatePassword(confirmPasswordValue, passwordValue).isValid &&
      validateConfirmPassword(passwordValue, confirmPasswordValue).isValid &&
      validatePhoneNo(phoneNoValue).isValid;
    if (isValid) {
      const formData = {
        name: usernameValue,
        email: emailValue,
        password: passwordValue,
        phoneNumber: phoneNoValue,
      };
      const response = await Register(formData);
      if (response.data?.isSucceeded === true) {
        toast.success("Registered successfully!");
        navigate("/login");
      } else {
        if (response.data?.isSucceeded === false) {
          const errors = response.data?.descriptionMessage;
          if (errors === "Username already exist")
            setUsernameServerError(errors);
          if (errors === "Email already exists!") setEmailServerError(errors);
        }
      }
    } else {
      handleUsernameBlur();
      handleEmailBlur();
      handlePasswordBlur();
      handleConfirmPasswordBlur();
      handlePhoneNoBlur();
    }
  }

  function handlePasswordAndConfirmPasswordValidation() {
    handlePasswordBlur();
    if (confirmPasswordValue !== "") {
      handleConfirmPasswordBlur();
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
            <Input
              classes={didUsernameTouch.classes}
              htmlFor="name"
              label="Name"
              id="name"
              type="text"
              name="name"
              onChange={handleUsernameChange}
              onBlur={handleUsernameBlur}
              placeholder="Enter your name"
              error={
                didUsernameTouch.touch ? didUsernameTouch.error : undefined
              }
            />
            <Input
              classes={didEmailTouch.classes}
              htmlFor="email"
              label="Email"
              id="email"
              type="email"
              name="email"
              onChange={handleEmailChange}
              onBlur={handleEmailBlur}
              placeholder="Enter your email"
              error={didEmailTouch.touch ? didEmailTouch.error : undefined}
            />
            <Input
              classes={didPasswordTouch.classes}
              htmlFor="password"
              label="Password"
              id="password"
              type="password"
              name="password"
              onChange={handlePasswordChange}
              onBlur={handlePasswordAndConfirmPasswordValidation}
              placeholder="Enter your password"
              error={
                didPasswordTouch.touch ? didPasswordTouch.error : undefined
              }
            />
            <Input
              classes={didConfirmPasswordTouch.classes}
              htmlFor="confirmPassword"
              label="Confirm Password"
              id="confirmPassword"
              type="password"
              name="confirmPassword"
              onChange={handleConfirmPasswordChange}
              onBlur={handleConfirmPasswordBlur}
              placeholder="Enter confirm password"
              error={
                didConfirmPasswordTouch.touch
                  ? didConfirmPasswordTouch.error
                  : undefined
              }
            />
            <Input
              classes={didPhoneNoTouch.classes}
              htmlFor="phoneNo"
              label="Phone Number"
              id="phoneNo"
              type="text"
              name="phoneNo"
              value={phoneNoValue}
              onChange={handlePhoneNoChange}
              onBlur={handlePhoneNoBlur}
              placeholder="Enter your phone no."
              error={didPhoneNoTouch.touch ? didPhoneNoTouch.error : undefined}
            />
            {isLoading && <Loading />}
            <button
              type="submit"
              className="btn btn-primary my-3"
              disabled={isLoading}
            >
              {isLoading ? "Submitting..." : "Register"}
            </button>
          </form>
          <p className="pt-3">
            Already have an account ?<Link to="/login">Login</Link>
          </p>
        </div>
      </div>
    </div>
  );
}
