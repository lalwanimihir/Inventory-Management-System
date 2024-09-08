import { Navigate } from "react-router-dom";
import { GetSessionData } from "../util/AccessToken";

export default function ProtectedRoute({ children, roles }) {
  const user = GetSessionData();
  const isAuth = user !== null ? user.isLoggedIn : false;

  if (!isAuth) {
    return <Navigate to="/" />;
  }

  if (roles && roles.length > 0 && !roles.includes(user.role[0])) {
    return <Navigate to="/" />;
  }

  return children;
}
