import { configureStore } from "@reduxjs/toolkit";
import { authApiSlice } from "../services/authentication/AuthApiSlice";
import { modalReducer } from "./ModalSlice";
import { productDetailsApiSlice } from "../services/inventory_management/ProductDetailsApiSlice";

export const store = configureStore({
  reducer: {
    [authApiSlice.reducerPath]: authApiSlice.reducer,
    [productDetailsApiSlice.reducerPath]: productDetailsApiSlice.reducer,
    modal: modalReducer,
  },
  middleware: (getDefaultMiddleware) =>
    getDefaultMiddleware().concat(
      authApiSlice.middleware,
      productDetailsApiSlice.middleware
    ),
});
