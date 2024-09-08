import { Link, useNavigate } from "react-router-dom";
import { GetSessionData, RemoveSessionData } from "../../util/AccessToken";
import { useDispatch } from "react-redux";
import { showActions } from "../../store/ModalSlice";

export default function Header() {
  const navigate = useNavigate();
  const dispatch = useDispatch();
  const user = GetSessionData();
  const isAuth = user !== null ? user.isLoggedIn : false;
  const role = user !== null ? user.role?.length > 0 && user.role[0] : "";

  //_______________Logout session_____________

  const logoutHandler = () => {
    RemoveSessionData();
    dispatch(showActions.closeModal());
    navigate("/", { replace: true });
  };

  function openRequestModal() {
    dispatch(showActions.openModal("viewRequestModal"));
  }
  return (
    <nav className="navbar navbar-expand-lg navbar-dark">
      <div className="container-fluid">
        <Link className="navbar-brand fw-bold" to="/">
          Inventory Management System
        </Link>
        <button
          className="navbar-toggler text-white"
          type="button"
          data-bs-toggle="collapse"
          data-bs-target="#navbarSupportedContent"
          aria-controls="navbarSupportedContent"
          aria-expanded="false"
          aria-label="Toggle navigation"
        >
          <span className="navbar-toggler-icon"></span>
        </button>
        <div className="collapse navbar-collapse" id="navbarSupportedContent">
          <ul className="navbar-nav me-auto mb-2 mb-lg-0">
            {isAuth ? (
              <>
                <li className="nav-item">
                  <Link
                    className="nav-link active me-2"
                    to={role === "Admin" ? "/admin" : "/user"}
                  >
                    <svg
                      xmlns="http://www.w3.org/2000/svg"
                      width="22"
                      height="22"
                      fill="currentColor"
                      className="bi bi-person-circle text-white me-2"
                      viewBox="0 0 16 16"
                    >
                      <path d="M11 6a3 3 0 1 1-6 0 3 3 0 0 1 6 0" />
                      <path
                        fillRule="evenodd"
                        d="M0 8a8 8 0 1 1 16 0A8 8 0 0 1 0 8m8-7a7 7 0 0 0-5.468 11.37C3.242 11.226 4.805 10 8 10s4.757 1.225 5.468 2.37A7 7 0 0 0 8 1"
                      />
                    </svg>
                    {user?.userName}
                  </Link>
                </li>
                {role === "Admin" && (
                  <li className="nav-item">
                    <Link
                      className="nav-link me-2 ps-0"
                      to="#"
                      onClick={openRequestModal}
                    >
                      View Requests
                    </Link>
                  </li>
                )}
                <li className="nav-item">
                  <Link
                    className="btn btn-sm btn-outline-light rounded mt-1"
                    onClick={logoutHandler}
                  >
                    Logout
                  </Link>
                </li>
              </>
            ) : (
              <>
                <li className="nav-item">
                  <Link
                    className="btn btn-sm btn-outline-light rounded mt-1 me-2"
                    to="/login"
                  >
                    Login
                  </Link>
                </li>
                <li className="nav-item">
                  <Link
                    className="btn btn-sm btn-outline-light rounded mt-1 me-2"
                    to="/register"
                  >
                    Register
                  </Link>
                </li>
              </>
            )}
          </ul>
        </div>
      </div>
    </nav>
  );
}
