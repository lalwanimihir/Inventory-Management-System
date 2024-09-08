import { createApi, fetchBaseQuery } from "@reduxjs/toolkit/query/react";
//https:localhost:7143/api/Auth/Login
export const authApiSlice = createApi({
  reducerPath: "api",
  baseQuery: fetchBaseQuery({ baseUrl: "https:localhost:7143" }),
  endpoints: (builder) => ({
    register: builder.mutation({
      query: (user) => ({
        url: "/api/Auth/Register",
        method: "POST",
        body: user,
      }),
    }),
    login: builder.mutation({
      query: (data) => ({
        url: "/api/Auth/Login",
        method: "POST",
        body: data,
      }),
    }),
  }),
});

export const { useRegisterMutation, useLoginMutation } = authApiSlice;
