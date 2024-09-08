import { createSlice } from "@reduxjs/toolkit";

const initialModalState = { showModal: false, identifier: "" };

const showModalSlice = createSlice({
  name: "modal",
  initialState: initialModalState,
  reducers: {
    openModal(state, action) {
      state.showModal = true;
      state.identifier = action.payload;
    },
    closeModal(state, action) {
      state.showModal = false;
      state.identifier = action.payload;
    },
  },
});

export const showActions = showModalSlice.actions;
export const modalReducer = showModalSlice.reducer;
