import { useDispatch } from "react-redux";
import Modal from "../shared/Modal";
import { showActions } from "../../store/ModalSlice";
import Input from "../shared/Input";
import UseInput from "../../hooks/UseInput";
import { toast } from "react-toastify";
import { GetSessionData } from "../../util/AccessToken";
import { useEffect, useState } from "react";
import {
  validateInventoryRequestQuantity,
  validateProductName,
  validateReason,
} from "../../validations/Validations";
import { useAddInventoryRequestMutation } from "../../services/inventory_management/ProductDetailsApiSlice";
import { useGetAllActiveProductsQuery } from "../../services/inventory_management/ProductDetailsApiSlice";
import NoData from "../shared/NoData";
import Loading from "../shared/Loading";

export default function AddInventoryRequest() {
  const dispatch = useDispatch();
  const [AddInventoryRequest, { isLoading }] = useAddInventoryRequestMutation();
  const {
    data = {},
    isLoading: isFetching,
    refetch,
  } = useGetAllActiveProductsQuery();

  const userName = GetSessionData()?.user?.userName;
  useEffect(() => {
    refetch();
  }, [userName, refetch]);

  const [error, setError] = useState({
    isError: false,
    errorMessage: "",
  });

  const { activeProducts = [] } = data;
  const {
    inputValue: productNameValue,
    handleOnSelect: handleProductNameSelect,
    didSelect: didProductNameSelect,
  } = UseInput("", validateProductName);

  const {
    inputValue: quantityValue,
    handleOnBlur: handleQuantityBlur,
    handleOnChange: handleQuantityChange,
    didTouch: didQuantityTouch,
  } = UseInput("", validateInventoryRequestQuantity);

  const {
    inputValue: reasonValue,
    handleOnBlur: handleReasonBlur,
    handleOnChange: handleReasonChange,
    didTouch: didReasonTouch,
  } = UseInput("", validateReason);

  function closeModalHandler() {
    dispatch(showActions.closeModal());
  }

  async function handleSubmit(event) {
    event.preventDefault();
    const isValid =
      validateProductName(productNameValue).isValid &&
      validateInventoryRequestQuantity(quantityValue).isValid &&
      validateReason(reasonValue).isValid;
    if (isValid) {
      const selectedProduct = activeProducts.find(
        (product) => product.id === +productNameValue
      );
      const formData = {
        productId: selectedProduct.id,
        productName: selectedProduct.productName,
        quantity: quantityValue,
        reason: reasonValue,
      };
      try {
        const response = await AddInventoryRequest(formData);
        if (response.data?.isSucceeded) {
          toast.success(response.data?.descriptionMessage);
          closeModalHandler();
        } else {
          setError({
            isError: true,
            errorMessage: response.error?.data[0],
          });
        }
      } catch (err) {
        setError({
          isError: true,
          errorMessage: err.message,
        });
      }
    } else {
      handleProductNameSelect();
      handleQuantityBlur();
      handleReasonBlur();
    }
  }

  return (
    <Modal>
      {isFetching && <NoData message="Data is fetching..." />}
      {activeProducts && activeProducts.length > 0 ? (
        <div className="modal-body">
          <form className="col-md-12" onSubmit={handleSubmit}>
            <div className="row py-3 px-2">
              <div className="col-md-12 mb-3">
                <label
                  htmlFor="productName"
                  className="required form-label fw-bold"
                >
                  Product Name
                </label>
                <select
                  className={didProductNameSelect.classes}
                  id="productName"
                  name="productName"
                  value={productNameValue}
                  onChange={handleProductNameSelect}
                  onBlur={handleProductNameSelect}
                  autoFocus
                >
                  <option value="" disabled selected hidden>
                    Select Product
                  </option>
                  {activeProducts?.map((product) => (
                    <option key={product.id} value={product.id}>
                      {product.productName}
                    </option>
                  ))}
                </select>
                {didProductNameSelect.select && (
                  <small className="text-danger">
                    {didProductNameSelect.error}
                  </small>
                )}
              </div>
              <Input
                classes={didQuantityTouch.classes}
                htmlFor="quantity"
                label="Quantity"
                id="quantity"
                type="number"
                name="quantity"
                value={quantityValue}
                onChange={handleQuantityChange}
                onBlur={handleQuantityBlur}
                placeholder="Enter quantity"
                error={
                  didQuantityTouch.touch ? didQuantityTouch.error : undefined
                }
              />

              <Input
                classes={didReasonTouch.classes}
                htmlFor="reason"
                label="Reason"
                id="reason"
                type="text"
                name="reason"
                value={reasonValue}
                onChange={handleReasonChange}
                onBlur={handleReasonBlur}
                placeholder="Enter your reason"
                error={didReasonTouch.touch ? didReasonTouch.error : undefined}
              />
            </div>

            {error.isError && (
              <p className="text-danger">{error.errorMessage}</p>
            )}
            <div className="modal-footer">
              <button
                type="button"
                onClick={closeModalHandler}
                className="btn btn-secondary"
              >
                Cancel
              </button>
              <button
                type="submit"
                className="btn btn-primary"
                disabled={isLoading}
              >
                Add
              </button>
            </div>
          </form>
          {isLoading && <Loading />}
        </div>
      ) : (
        <NoData message="Data not available" />
      )}
    </Modal>
  );
}
