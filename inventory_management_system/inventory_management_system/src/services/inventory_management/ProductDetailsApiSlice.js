import { createApi, fetchBaseQuery } from "@reduxjs/toolkit/query/react";
import { GetSessionData } from "../../util/AccessToken";

export const productDetailsApiSlice = createApi({
  reducerPath: "inventoryApi",
  baseQuery: fetchBaseQuery({
    baseUrl: "https:localhost:7143",
    prepareHeaders: (headers) => {
      const user = GetSessionData();
      let token = user?.accessToken;
      if (token) {
        headers.set("Authorization", `Bearer ${token}`);
      }
      return headers;
    },
  }),
  tagTypes: ["Products", "InventoryRequests"],
  endpoints: (builder) => ({
    getAllProducts: builder.query({
      query: ({ searchQuery, sortBy, sortOrder, pageNo, pageSize = 5 }) => ({
        url: "/api/Product/GetAll",
        params: {
          filterQuery: searchQuery,
          sortBy,
          IsAscending: sortOrder,
          pageNo,
          pageSize,
        },
      }),
      providesTags: ["Products"],
    }),

    getAllActiveProducts: builder.query({
      query: () => ({
        url: "/api/Product/GetAllActiveProducts",
      }),
      invalidatesTags: ["Products", "InventoryRequests"],
    }),

    addProducts: builder.mutation({
      query: (formData) => ({
        url: "/api/Product/AddProducts",
        method: "POST",
        body: formData,
      }),
      invalidatesTags: ["Products", "InventoryRequests"],
    }),

    getById: builder.query({
      query: (id) => ({
        url: `/api/Product/${id}`,
        method: "GET",
      }),
    }),

    updateProducts: builder.mutation({
      query: (formData) => ({
        url: "/api/Product",
        method: "PUT",
        body: formData,
      }),
      invalidatesTags: ["Products", "InventoryRequests"],
    }),

    updateProductAvailability: builder.mutation({
      query: (id) => ({
        url: `/api/Product/UpdateProductAvailability?id=${id}`,
        method: "PUT",
      }),
      invalidatesTags: ["Products", "InventoryRequests"],
    }),

    DeleteProduct: builder.mutation({
      query: (id) => ({
        url: `/api/Product/${id}`,
        method: "DELETE",
      }),
      invalidatesTags: ["Products", "InventoryRequests"],
    }),

    getAllInventoryRequests: builder.query({
      query: ({ pageNo, pageSize = 5 }) => ({
        url: "api/InventoryRequests/GetAll",
        params: {
          pageNo,
          pageSize,
        },
      }),
      providesTags: ["InventoryRequests"],
    }),

    updateInventoryRequestStatus: builder.mutation({
      query: (formData) => ({
        url: "/api/InventoryRequests/UpdateInventoryRequestStatus",
        method: "PUT",
        body: formData,
      }),
      invalidatesTags: ["InventoryRequests", "Products"],
    }),

    getAllInventoryRequestsByUserId: builder.query({
      query: ({
        searchQuery,
        sortBy,
        sortOrder,
        pageNo,
        pageSize = 5,
        userId,
      }) => ({
        url: "api/InventoryRequests/GetAllByUserId",
        params: {
          filterQuery: searchQuery,
          sortBy,
          IsAscending: sortOrder,
          pageNo,
          pageSize,
          userId,
        },
      }),
      providesTags: ["InventoryRequests"],
    }),

    addInventoryRequest: builder.mutation({
      query: (formData) => ({
        url: "/api/InventoryRequests/AddInventoryRequest",
        method: "POST",
        body: formData,
      }),
      invalidatesTags: ["InventoryRequests", "Products"],
    }),
  }),
});

export const {
  useGetAllProductsQuery,
  useGetAllActiveProductsQuery,
  useLazyGetByIdQuery,
  useAddProductsMutation,
  useUpdateProductsMutation,
  useUpdateProductAvailabilityMutation,
  useDeleteProductMutation,
  useGetAllInventoryRequestsQuery,
  useUpdateInventoryRequestStatusMutation,
  useGetAllInventoryRequestsByUserIdQuery,
  useAddInventoryRequestMutation,
} = productDetailsApiSlice;
