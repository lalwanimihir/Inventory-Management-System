import { createBrowserRouter } from "react-router-dom";
import { RouterProvider } from "react-router-dom";
import ProtectedRoute from "./pages/ProtectedRoute";
import RootLayout from "./pages/Root";
import Login from "./pages/authentication/Login";
import Register from "./pages/authentication/Register";
import Admin from "./pages/inventory_management/Admin";
import User from "./pages/inventory_management/User";
import Home from "./pages/Home";
import "./App.css";
import Error from "./pages/Error";

function App() {
  const router = createBrowserRouter([
    {
      path: "/",
      element: <RootLayout />,
      children: [
        {
          path: "*",
          element: <Error />,
        },
        {
          path: "/",
          element: <Home />,
        },
        {
          path: "login",
          element: <Login />,
        },
        {
          path: "register",
          element: <Register />,
        },
        {
          path: "user",
          element: (
            <ProtectedRoute roles={["User"]}>
              <User />
            </ProtectedRoute>
          ),
        },
        {
          path: "admin",
          element: (
            <ProtectedRoute roles={["Admin"]}>
              <Admin />
            </ProtectedRoute>
          ),
        },
      ],
    },
  ]);
  return <RouterProvider router={router} />;
}

export default App;
