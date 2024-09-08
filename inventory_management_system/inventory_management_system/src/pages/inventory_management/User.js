import { useDispatch, useSelector } from "react-redux";
import AddInventoryRequest from "../../components/inventory_management_system/AddInventoryRequest";
import { showActions } from "../../store/ModalSlice";
import Button from "../../components/shared/Button";
import { GetSessionData } from "../../util/AccessToken";
import { useState } from "react";
import NoData from "../../components/shared/NoData";
import { useGetAllInventoryRequestsByUserIdQuery } from "../../services/inventory_management/ProductDetailsApiSlice";
import UserInventoryRequestsInfo from "../../components/inventory_management_system/UserInventoryRequests";
import Loading from "../../components/shared/Loading";
import Pagination from "../../components/shared/Pagination";
import SearchInput from "../../components/shared/SearchInput";

export default function User() {
  const dispatch = useDispatch();
  const openModalKey = useSelector((state) => state.modal.showModal);
  const modalIdentifier = useSelector((state) => state.modal.identifier);

  const user = GetSessionData();

  const [searchQuery, setSearchQuery] = useState("");
  const [sortBy, setSortBy] = useState("");
  const [sortOrder, setSortOrder] = useState(true);
  const [currentPage, setCurrentPage] = useState(1);
  const pageSize = 5;
  const userId = user?.userId;

  const {
    data = {},
    error,
    isLoading,
  } = useGetAllInventoryRequestsByUserIdQuery({
    searchQuery,
    sortBy,
    sortOrder,
    pageNo: currentPage,
    pageSize,
    userId,
  });

  const { totalCount = 0, inventoryRequests = [] } = data;

  const totalPage = Math.ceil(totalCount / pageSize);
  const totalPageArray = new Array(totalPage);
  totalPageArray.fill("0");

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

  //__________Open modal____________

  function showModalHandler() {
    dispatch(showActions.openModal("InventoryModal"));
  }

  if (isLoading) return <Loading />;
  if (error) {
    return <NoData message="Error loading data!" />;
  }
  return (
    <div className="container-fluid">
      {openModalKey && modalIdentifier === "InventoryModal" && (
        <AddInventoryRequest />
      )}
      <div className="row ">
        <div className="py-5">
          <button
            onClick={showModalHandler}
            className="btn"
            style={{ backgroundColor: "#386eb4", color: "white" }}
          >
            Add Inventory Request
          </button>
          <SearchInput onSearchChange={handleChange} />
          <div className="table-responsive ">
            <table className="table  table-striped text-center">
              <thead>
                <tr>
                  <th>
                    <Button
                      onSort={() => handleSort("Sr No")}
                      columnName={"Sr No"}
                    />
                  </th>
                  <th>
                    <Button
                      onSort={() => handleSort("productName")}
                      columnName={"Product Name"}
                      sortOrder={
                        sortBy === "productName"
                          ? sortOrder
                            ? "asc"
                            : "desc"
                          : null
                      }
                    />
                  </th>
                  <th>
                    <Button
                      onSort={() => handleSort("quantity")}
                      columnName={"Quantity"}
                      sortOrder={
                        sortBy === "quantity"
                          ? sortOrder
                            ? "asc"
                            : "desc"
                          : null
                      }
                    />
                  </th>
                  <th>
                    <Button
                      onSort={() => handleSort("reason")}
                      columnName={"Reason"}
                      sortOrder={
                        sortBy === "reason"
                          ? sortOrder
                            ? "asc"
                            : "desc"
                          : null
                      }
                    />
                  </th>
                  <th>
                    <Button
                      onSort={() => handleSort("status")}
                      columnName={"status"}
                    />
                  </th>
                </tr>
              </thead>
              <tbody>
                {inventoryRequests && inventoryRequests.length > 0 ? (
                  inventoryRequests.map((values, index) => (
                    <UserInventoryRequestsInfo
                      key={values.id}
                      index={index}
                      {...values}
                      currentPage={currentPage}
                      pageSize={pageSize}
                    />
                  ))
                ) : (
                  <NoData message="Data is not available!" />
                )}
              </tbody>
            </table>
          </div>
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
