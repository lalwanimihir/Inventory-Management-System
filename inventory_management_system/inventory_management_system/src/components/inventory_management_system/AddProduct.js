import Modal from "../shared/Modal";
import Input from "../shared/Input";
import UseInput from "../../hooks/UseInput";
import {
  validateCategory,
  validatePrice,
  validateProductName,
  validateQuantity,
} from "../../validations/Validations";
import { toast } from "react-toastify";
import { showActions } from "../../store/ModalSlice";
import {
  useAddProductsMutation,
  useUpdateProductsMutation,
} from "../../services/inventory_management/ProductDetailsApiSlice";
import { useDispatch } from "react-redux";
import Loading from "../shared/Loading";

export default function ProductForm({ initialValues, identifier }) {
  const [AddProduct, { isLoading }] = useAddProductsMutation();
  const [UpdateProduct] = useUpdateProductsMutation();
  const dispatch = useDispatch();
  const {
    inputValue: productNameValue,
    handleOnBlur: handleProductNameBlur,
    handleOnChange: handleProductNameChange,
    didTouch: didProductNameTouch,
  } = UseInput(
    identifier === "Add" ? "" : initialValues.productName,
    validateProductName
  );

  const {
    inputValue: quantityValue,
    handleOnBlur: handleQuantityBlur,
    handleOnChange: handleQuantityChange,
    didTouch: didQuantityTouch,
  } = UseInput(
    identifier === "Add" ? "" : initialValues.quantity,
    validateQuantity
  );

  const {
    inputValue: priceValue,
    handleOnBlur: handlePriceBlur,
    handleOnChange: handlePriceChange,
    didTouch: didPriceTouch,
  } = UseInput(identifier === "Add" ? "" : initialValues.price, validatePrice);

  const {
    inputValue: categoryValue,
    handleOnSelect: handleCategorySelect,
    didSelect: didCategorySelect,
  } = UseInput(
    identifier === "Add" ? "" : initialValues.category,
    validateCategory
  );

  function closeModalHandler() {
    dispatch(showActions.closeModal());
  }

  async function handleSubmit(event, identifier) {
    event.preventDefault();
    const isValid =
      validateProductName(productNameValue).isValid &&
      validateCategory(categoryValue).isValid &&
      validateQuantity(quantityValue).isValid &&
      validatePrice(priceValue).isValid;
    if (isValid) {
      initialValues.productName = productNameValue;
      initialValues.category = +categoryValue;
      initialValues.quantity = quantityValue;
      initialValues.price = priceValue;

      if (identifier === "Add") {
        const response = await AddProduct(initialValues);
        if (response.data?.isSucceeded === true) {
          toast.success(response.data?.descriptionMessage);
          closeModalHandler();
        } else {
          toast.error(response.data?.descriptionMessage);
        }
      } else {
        const response = await UpdateProduct(initialValues);
        if (response.data?.isSucceeded === true) {
          toast.success(response.data?.descriptionMessage);
          closeModalHandler();
        } else {
          toast.error(response.data?.descriptionMessage);
        }
      }
    } else {
      handleProductNameBlur();
      handleCategorySelect();
      handleQuantityBlur();
      handlePriceBlur();
    }
  }
  return (
    <Modal isLoading={isLoading}>
      <div className="modal-body">
        <form
          className="col-md-12"
          onSubmit={(e) => handleSubmit(e, identifier)}
        >
          <div className="row py-3 px-2">
            <Input
              classes={didProductNameTouch.classes}
              htmlFor="productName"
              label="Product Name"
              id="productName"
              type="text"
              name="productName"
              value={productNameValue}
              disabled={isLoading}
              onChange={handleProductNameChange}
              onBlur={handleProductNameBlur}
              placeholder="Enter product name"
              error={
                didProductNameTouch.touch
                  ? didProductNameTouch.error
                  : undefined
              }
            />
            <div className="col-md-12 mb-3">
              <label htmlFor="category" className="required form-label fw-bold">
                Category
              </label>
              <select
                className={didCategorySelect.classes}
                id="category"
                name="category"
                defaultValue={categoryValue}
                disabled={isLoading}
                onChange={handleCategorySelect}
                onBlur={handleCategorySelect}
              >
                <option value="" disabled hidden>
                  Please select category
                </option>
                <option value="1">Electronics</option>
                <option value="2">Clothing</option>
                <option value="3">Groceries</option>
                <option value="4">Other</option>
              </select>
              {didCategorySelect.select && (
                <small className="text-danger">{didCategorySelect.error}</small>
              )}
            </div>
            <Input
              classes={didQuantityTouch.classes}
              htmlFor="quantity"
              label="Quantity"
              id="quantity"
              type="text"
              name="quantity"
              value={quantityValue}
              onChange={handleQuantityChange}
              disabled={isLoading}
              onBlur={handleQuantityBlur}
              placeholder="Enter quantity"
              error={
                didQuantityTouch.touch ? didQuantityTouch.error : undefined
              }
            />
            <Input
              classes={didPriceTouch.classes}
              htmlFor="price"
              label="Price"
              id="price"
              type="text"
              name="price"
              value={priceValue}
              disabled={isLoading}
              onChange={handlePriceChange}
              onBlur={handlePriceBlur}
              placeholder="Enter price"
              error={didPriceTouch.touch ? didPriceTouch.error : undefined}
            />
          </div>
          <div className="modal-footer">
            <button
              type="button"
              disabled={isLoading}
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
              {identifier === "Add" ? "Add" : "Update"}
            </button>
          </div>
        </form>
        {isLoading && <Loading />}
      </div>
    </Modal>
  );
}
