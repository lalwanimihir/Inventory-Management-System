import { useDispatch, useSelector } from "react-redux";
import { useState, useEffect } from "react";
import ProductForm from "../../components/inventory_management_system/AddProduct";
import { showActions } from "../../store/ModalSlice";
import {
  useDeleteProductMutation,
  useGetAllProductsQuery,
  useUpdateProductAvailabilityMutation,
} from "../../services/inventory_management/ProductDetailsApiSlice";
import { useLazyGetByIdQuery } from "../../services/inventory_management/ProductDetailsApiSlice";
import NoData from "../../components/shared/NoData";
import { GetSessionData } from "../../util/AccessToken";
import Loading from "../../components/shared/Loading";
import { useRef } from "react";
import { toast } from "react-toastify";
import ViewRequests from "../../components/inventory_management_system/ViewRequests";
import Pagination from "../../components/shared/Pagination";
import SearchInput from "../../components/shared/SearchInput";
import ProductTable from "../../components/inventory_management_system/ProductTable";
import Modal from "../../components/shared/Modal";

let updateFields = {
  id: "",
  productName: "",
  category: "",
  quantity: "",
  price: "",
};

export default function Admin() {
  const openModalKey = useSelector((state) => state.modal.showModal);
  const modalIdentifier = useSelector((state) => state.modal.identifier);
  const dispatch = useDispatch();
  const [selectedId, setSelectedId] = useState(null);
  const [searchQuery, setSearchQuery] = useState("");
  const [sortBy, setSortBy] = useState("");
  const [sortOrder, setSortOrder] = useState(true);
  const [currentPage, setCurrentPage] = useState(1);
  const [isActive, setIsActive] = useState(true);

  const pageSize = 5;
  const {
    data = {},
    error,
    isLoading,
    refetch,
  } = useGetAllProductsQuery({
    searchQuery,
    sortBy,
    sortOrder,
    pageNo: currentPage,
    pageSize,
  });

  const userName = GetSessionData()?.user?.userName;

  useEffect(() => {
    refetch();
  }, [userName, refetch]);

  const [getData] = useLazyGetByIdQuery();
  const [deleteData] = useDeleteProductMutation();
  const [updateProductAvailability] = useUpdateProductAvailabilityMutation();

  const { totalCount = 0, products = [] } = data;
  const totalPage = Math.ceil(totalCount / pageSize);
  const totalPageArray = new Array(totalPage);
  totalPageArray.fill("0");

  const identifierRef = useRef();

  //_________________Search input____________

  const handleChange = (event) => {
    setCurrentPage(1);
    setSearchQuery(event.target.value);
  };

  //________________Sort data________________

  const handleSort = (column) => {
    if (sortBy === column) {
      setSortOrder(!sortOrder);
    } else {
      setSortBy(column);
      setSortOrder(true);
    }
  };
  //_________________Pagination nextpage___________

  const handleNextPage = () => {
    if (currentPage < totalPage) {
      setCurrentPage((prevPage) => {
        const newPage = prevPage + 1;
        return newPage;
      });
    }
  };
  //_________________Pagination Previouspage___________

  const handlePreviousPage = () => {
    setCurrentPage((prevPage) => Math.max(prevPage - 1, 1));
  };

  //_________________Pagination Currentpage___________

  function findCurrentPage(pageNo) {
    setCurrentPage(pageNo);
  }

  //______________Update data____________

  async function showEditForm(id) {
    const getItem = await getData(id);
    updateFields = {
      ...getItem.data,
      category: getItem.data.category + "",
    };
    identifierRef.action = "Edit";
    dispatch(showActions.openModal("EditForm"));
  }

  //_________________Delete data________________

  async function removeData(id) {
    setSelectedId(id);
    dispatch(showActions.openModal("DeleteData"));
  }
  async function handleDeleteData() {
    if (selectedId) {
      const response = await deleteData(selectedId);
      if (response.data?.isSucceeded === true) {
        toast.success("Data deleted successfully!");
        if ((totalCount % pageSize) - 1 === 0 && currentPage === totalPage) {
          setCurrentPage(currentPage - 1);
        }
        closeModalHandler();
      }
    }
  }

  //_________________Close modal_______________

  function closeModalHandler() {
    dispatch(showActions.closeModal());
  }

  //_______________Change active status______________

  async function changeActivity(id) {
    await updateProductAvailability(id);
    setIsActive(!isActive);
  }

  //__________Open modal____________

  function showModalHandler() {
    identifierRef.action = "Add";
    dispatch(showActions.openModal("EditForm"));
  }

  if (isLoading) return <Loading />;
  if (error) {
    return <NoData message="Error loading data!" />;
  }

  return (
    <div className="container-fluid">
      {openModalKey && modalIdentifier === "EditForm" && (
        <ProductForm
          identifier={identifierRef.action}
          initialValues={updateFields}
        />
      )}
      {openModalKey && modalIdentifier === "viewRequestModal" && (
        <ViewRequests />
      )}
      {openModalKey && modalIdentifier === "DeleteData" && (
        <Modal>
          <div className="modal-body text-center">
            <h3>Delete Product</h3>
            <p>Are you sure you want to delete product?</p>
          </div>
          <div className="modal-footer justify-content-center">
            <button
              type="button"
              onClick={closeModalHandler}
              className="btn btn-secondary"
            >
              Cancel
            </button>
            <button
              type="button"
              onClick={handleDeleteData}
              className="btn btn-danger"
            >
              Delete Product
            </button>
          </div>
        </Modal>
      )}
      <div className="row">
        <div className="py-5">
          <button
            onClick={showModalHandler}
            className="btn"
            style={{ backgroundColor: "#386eb4", color: "white" }}
          >
            Add Product
          </button>
          <SearchInput onSearchChange={handleChange} />
          <ProductTable
            products={products}
            onDelete={removeData}
            onUpdate={showEditForm}
            updateActivity={changeActivity}
            isActive={isActive}
            onSort={handleSort}
            sortBy={sortBy}
            sortOrder={sortOrder}
            currentPage={currentPage}
            pageSize={pageSize}
          />
          <Pagination
            currentPage={currentPage}
            totalPage={totalPage}
            onPageChange={findCurrentPage}
            onPreviousPage={handlePreviousPage}
            onNextPage={handleNextPage}
          />
        </div>
      </div>
    </div>
  );
}
