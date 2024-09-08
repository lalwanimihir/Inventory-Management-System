import Header from "../components/shared/Header";
import { Outlet } from "react-router-dom";

function RootLayout() {
  return (
    <div className="container-fluid px-0">
      <Header />
      <main className="main-content">
        <div className="content-wrapper">
          <Outlet />
        </div>
      </main>
    </div>
  );
}

export default RootLayout;
