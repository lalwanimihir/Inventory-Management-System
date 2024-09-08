import { useDispatch } from "react-redux";
import { showActions } from "../../store/ModalSlice";

export default function Modal({ children, classes, isLoading }) {
  const dispatch = useDispatch();
  function closeModalHandler() {
    dispatch(showActions.closeModal());
  }
  return (
    <div
      className={`modal ${classes} d-block`}
      style={{ backdropFilter: "blur(6px)" }}
    >
      <div className="modal-dialog modal-dialog-centered">
        <div className="modal-content" style={{ backgroundColor: "#d7e0eb" }}>
          <div className="modal-header">
            <button
              type="button"
              className="btn-close"
              data-bs-dismiss="modal"
              disabled={isLoading}
              onClick={closeModalHandler}
              aria-label="Close"
            ></button>
          </div>
          {children}
        </div>
      </div>
    </div>
  );
}
