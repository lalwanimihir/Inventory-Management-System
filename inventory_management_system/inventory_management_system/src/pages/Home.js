import logo from "../assets/logo.png";

export default function Home() {
  return (
    <div className="text-center" style={{ backgroundColor: "#" }}>
      <h1 className="fs-4 fw-bold py-5">
        Welcome to Inventory Management System
      </h1>
      <img
        className="rounded-3"
        id="homeLogo"
        src={logo}
        alt="Inventory Management logo"
      />
    </div>
  );
}
