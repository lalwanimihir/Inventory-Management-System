import { Link } from "react-router-dom";
import logo from "../assets/errorlogo1.png";

export default function Error() {
  return (
    <div className="error-page py-5 m-5 d-flex justify-content-center gap-5">
      <img
        src={logo}
        alt="ErrorLogo"
        style={{ height: "300px", width: "300px" }}
      />
      <div className="text-center pt-5">
        <h1 className="fs-1 fw-bold">Oops</h1>
        <h1>404 | Page not found!</h1>
        <p>
          We're sorry, Can't find this page, something went wrong!. Please try
          again later.
        </p>
        <Link
          href="/"
          className="btn"
          style={{ backgroundColor: "#386eb4", color: "white" }}
        >
          Go to homepage
        </Link>
      </div>
    </div>
  );
}
